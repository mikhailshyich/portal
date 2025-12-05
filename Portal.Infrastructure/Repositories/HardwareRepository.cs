using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class HardwareRepository : IHardwareDomain
    {
        private readonly PortalDbContext context;
        private readonly string prefixMarkCode = "markCode_";

        public HardwareRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            if(request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            
            var hardwareCount = request.Count;
            var hardwareList = new List<Hardware>();
            try
            {
                var category = await context.CategoriesHardware.FindAsync(request.CategoryHardwareId);

                for (var i = 0; i < hardwareCount; i++)
                {
                    var hardware = new Hardware()
                    {
                        MainWarehouseId = request.MainWarehouseId,
                        CategoryHardwareId = request.CategoryHardwareId,
                        DocumentExternalSystemId = request.DocumentExternalSystemId,
                        Title = request.Title,
                        Description = request.Description,
                        Count = 1,
                        InventoryNumberExternalSystem = request.InventoryNumberExternalSystem,
                        TTN = request.TTN,
                        DateTimeAdd = DateTime.Now,
                        FileNameImage = request.FileNameImage,
                        IsActive = true
                    };
                    //context.Hardwares.Add(hardware);
                    //await context.SaveChangesAsync();
                    //hardware.CombinedInvNumber = $"TMK-";
                    hardwareList.Add(hardware);
                }
                context.Hardwares.AddRange(hardwareList);
                await context.SaveChangesAsync();
                return new CustomGeneralResponses(true, "Оборудование успешно добавлено.", hardwareList);
            }
            catch
            {
                return new CustomGeneralResponses(false, "Ошибка при добавлении оборудования.\nПроверьте заполнение обязательных полей.");
            }
        }

        public async Task<string> GenerateQR(List<Guid>? idList)
        {
            try
            {
                iTextSharp.text.Rectangle qrSize = new(37, 37);
                Document document = new Document(qrSize, 0, 0, 0, 0);
                //var randomNumber = new byte[10];
                //using var rng = RandomNumberGenerator.Create();
                //rng.GetBytes(randomNumber);
                //var randomText = Convert.ToBase64String(randomNumber);
                var fileName = $"{Guid.NewGuid()}.pdf";
                var pathQR = $"labels\\{fileName}";
                if (System.IO.File.Exists(pathQR))
                {
                    System.IO.File.Delete(pathQR);
                }
                using (FileStream fs = new FileStream(pathQR, FileMode.CreateNew))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();
                    if(idList is not null)
                    {
                        foreach (var hardawreId in idList)
                        {
                            var hardware = context.Hardwares.Find(hardawreId);

                            // URL or text to be encoded in the QR code
                            string text = $"{prefixMarkCode}{hardware?.MarkCode.ToString()}";
                            // Create the QR code
                            BarcodeQRCode qrcode = new BarcodeQRCode(text, 25, 25, null);
                            // Convert the QR code to an image
                            iTextSharp.text.Image img = qrcode.GetImage();
                            // Create a PdfWriter instance
                            //PdfWriter.GetInstance(document, new FileStream(pathQR, FileMode.Create));
                            document.Add(img);
                            document.NewPage();

                        }
                    }
                    document.Close();
                }
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
        }

        public async Task<string> GenerateLabel(List<Guid>? idList)
        {
            try
            {
                iTextSharp.text.Rectangle labelSize = new(111, 84); //49,4x31,7mm 111, 84
                string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "BAHNSCHRIFT.TTF");           //
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);                                //нужно для отображения кирилицы
                var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);       //
                var fontTmk = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);    //
                var fontBold = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
                fontTmk.Size = 6;
                font.Size = 7;
                fontBold.Size = 7;

                Document document = new Document(labelSize, 0, 0, 0, 0);
                //var randomNumber = new byte[10];
                //using var rng = RandomNumberGenerator.Create();
                //rng.GetBytes(randomNumber);
                //var randomText = Convert.ToBase64String(randomNumber);
                var fileName = $"{Guid.NewGuid()}.pdf";
                var pathQR = $"labels\\{fileName}";
                if (System.IO.File.Exists(pathQR))
                {
                    System.IO.File.Delete(pathQR);
                }
                using (FileStream fs = new FileStream(pathQR, FileMode.CreateNew))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();
                    if (idList is not null)
                    {
                        foreach (var hardwareId in idList)
                        {
                            var hardware = context.Hardwares.Find(hardwareId);

                            if(hardware.MarkCode != null & hardware.NameForLabel != string.Empty)
                            {
                                Paragraph tmk = new("ОАО Туровский молочный комбинат", fontTmk);
                                tmk.Alignment = Element.ALIGN_CENTER;
                                tmk.SpacingAfter = 0;

                                string invExt = "";
                                if (hardware.InventoryNumberExternalSystem != "")
                                {
                                    invExt = $" ({hardware.InventoryNumberExternalSystem})";
                                }
                                Paragraph title = new($"{hardware.NameForLabel}", font);
                                title.Alignment = Element.ALIGN_CENTER;
                                title.SpacingAfter = 0;
                                Paragraph inventoryNumber = new($"Инв. {hardware.CombinedInvNumber}{invExt}", fontBold);
                                inventoryNumber.Alignment = Element.ALIGN_CENTER;
                                title.SpacingAfter = 0;

                                // URL or text to be encoded in the QR code
                                string qrText = $"{prefixMarkCode}{hardware?.MarkCode.ToString()}";
                                // Create the QR code
                                BarcodeQRCode qrcode = new BarcodeQRCode(qrText, 10, 10, null);
                                // Convert the QR code to an image
                                iTextSharp.text.Image img = qrcode.GetImage();
                                img.Alignment = Element.ALIGN_CENTER;
                                // Create a PdfWriter instance
                                //PdfWriter.GetInstance(document, new FileStream(pathQR, FileMode.Create));
                                document.Add(tmk);
                                document.Add(img);
                                document.Add(title);
                                document.Add(inventoryNumber);
                                document.NewPage();
                            }
                        }
                    }
                    document.Close();
                }
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            return await context.Hardwares.ToListAsync();
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(List<Guid>? hardwaresID, Guid? userID, Guid? userWarehouseID)
        {
            if (hardwaresID is null) return new CustomGeneralResponses(false, "Список перемещаемого оборудования равен null.");
            if (userID == Guid.Empty) return new CustomGeneralResponses(false, "Пользователь на которого перемещаем оборудование равен null.");

            var userDB = await context.Users.FindAsync(userID);
            if (userDB is null) return new CustomGeneralResponses(false, "Пользователь на которого перемещаем оборудование не найден в базе данных.");

            var userWarehouseDB = await context.UserWarehouses.FirstOrDefaultAsync(u => u.Id == userWarehouseID & u.UserId == userID);
            if (userWarehouseDB is null) return new CustomGeneralResponses(false, $"У пользователя {userDB.Username} нет такого склада.");

            List<Hardware> displacedHardware = new();

            foreach (var hardwareID in hardwaresID)
            {
                var hardwareDB = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == hardwareID & h.UserId != userDB.Id);
                if(hardwareDB != null)
                {
                    hardwareDB.UserId = userDB.Id;
                    hardwareDB.User = userDB;
                    hardwareDB.UserWarehouseId = userWarehouseDB.Id;
                    hardwareDB.UserWarehouse = userWarehouseDB;

                    displacedHardware.Add(hardwareDB);
                    var userHardware = new UserHardware()
                    {
                        HardwareId = hardwareDB.Id,
                        UserId = userDB.Id
                    };
                    context.UsersHardware.Add(userHardware);
                }
            }
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование в количестве {displacedHardware.Count} перемещено на {userDB.Username}", displacedHardware);
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty) return null!;

            var userDB = await context.Users.FindAsync(userId);
            if (userDB is null) return null!;

            return await context.Hardwares.Where(h => h.UserId == userId).ToListAsync();
        }

        public async Task<CustomGeneralResponses> ReturnAsync(List<Guid> hardwaresID)
        {
            if(hardwaresID.Count == 0) return new CustomGeneralResponses(false, "Список с оборудованием для возврата равен 0.");

            foreach(var hardwareID in hardwaresID)
            {
                var hardwareDB = await context.Hardwares.FindAsync(hardwareID);
                if (hardwareDB != null)
                {
                    hardwareDB.UserId = null;
                    hardwareDB.UserWarehouseId = null;
                }
            }
            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, $"Оборудование в количестве {hardwaresID.Count} возвращено на склад!");
        }

        public async Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport)
        {
            if (hardwareImport.Count == 0) return new CustomGeneralResponses(false, "Список с оборудованием для импорта пустой.");

            var hardwareList = new List<Hardware>();
            try
            {
                foreach (var hardware in hardwareImport)
                {
                    for (var i = 0; i < hardware.Count; i++)
                    {
                        var newHardware = new Hardware()
                        {
                            MainWarehouseId = hardware.MainWarehouseId,
                            CategoryHardwareId = hardware.CategoryHardwareId,
                            DocumentExternalSystemId = hardware.DocumentExternalSystemId,
                            Title = hardware.Title,
                            Count = 1,
                            InventoryNumberExternalSystem = hardware.InventoryNumberExternalSystem,
                            TTN = hardware.TTN,
                            DateTimeAdd = DateTime.Now,
                            IsActive = true
                        };
                        hardwareList.Add(newHardware);
                    }
                }
                context.AddRange(hardwareList);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new CustomGeneralResponses(false, "Ошибка при добавлении оборудования.\nПроверьте заполнение обязательных полей.");
            }
            return new CustomGeneralResponses(true, "Оборудование успешно импортировано!");
        }

        public async Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            if (markHardwareDTO is null) return new CustomGeneralResponses(false, "Проверьте введённые данные.");
            if (markHardwareDTO.HardwareId == Guid.Empty) return new CustomGeneralResponses(false, "ID оборудования пустой.");
            if (markHardwareDTO.MarkCode == Guid.Empty) return new CustomGeneralResponses(false, "ID кода маркировки пустой.");

            var hardware = await context.Hardwares.FirstOrDefaultAsync(h => h.Id ==  markHardwareDTO.HardwareId);
            if(hardware is null) return new CustomGeneralResponses(false, $"Оборудование с ID {markHardwareDTO.HardwareId} не найдено.");
            if(hardware.MarkCode != null) return new CustomGeneralResponses(false, $"Оборудование уже промаркировано.");

            var markCode = await context.MarkCodes.FirstOrDefaultAsync(m => m.Id == markHardwareDTO.MarkCode);
            if (markCode is null) return new CustomGeneralResponses(false, $"Код маркировки с ID {markHardwareDTO.MarkCode} не найден.");
            if (markCode.Used == true) return new CustomGeneralResponses(false, $"Код маркировки с ID {markHardwareDTO.MarkCode} уже используется.");

            var categoryHrdw = await context.CategoriesHardware.FirstOrDefaultAsync(c => c.Id == hardware.CategoryHardwareId);
            if (categoryHrdw is null) return new CustomGeneralResponses(false, $"Оборудование не привязано к категории.");

            hardware.MarkCode = markCode.Id;
            hardware.CombinedInvNumber = $"{categoryHrdw.ShortTitle}-{markCode.MarkCodeNumber}";

            markCode.HardwareId = hardware.Id;
            markCode.Used = true;

            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование успешно промаркировано!", hardware);
        }
    }
}
