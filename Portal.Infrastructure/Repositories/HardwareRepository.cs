using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.History;
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
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var hardwareCount = request.Count;
            var historyList = new List<History>();
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
                        IsActive = true,
                        NameForLabel = request.Title
                    };
                    context.Hardwares.Add(hardware);
                    await context.SaveChangesAsync();
                    //hardware.CombinedInvNumber = $"TMK-";
                    History history = new History(
                    request.ResponsibleId,
                    hardware.Id,
                    request.MainWarehouseId,
                    "Добавление",
                    DateTime.Now
                    );
                    historyList.Add(history);
                }
                context.HistoryEntries.AddRange(historyList);
                await context.SaveChangesAsync();
                return new CustomGeneralResponses(true, $"Оборудование в количестве {hardwareCount} успешно добавлено.");
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
                    if (idList is not null)
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
                var fontExt = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);    //
                var fontInventory = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
                var fontTitle = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
                fontInventory.Size = 7;
                fontExt.Size = 17;
                fontTitle.Size = 5;

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

                            if (hardware.MarkCode != null & hardware.NameForLabel != string.Empty)
                            {
                                //Paragraph tmk = new("ОАО Туровский молочный комбинат", fontTmk);
                                //tmk.Alignment = Element.ALIGN_CENTER;
                                //tmk.SpacingAfter = 0;

                                string invExt = "";
                                if (hardware.InventoryNumberExternalSystem != "")
                                {
                                    invExt = $"{hardware.InventoryNumberExternalSystem}";
                                }
                                Paragraph title = new($"{hardware.NameForLabel}", fontTitle);
                                title.Alignment = Element.ALIGN_CENTER;
                                title.SpacingAfter = 0;
                                title.Leading = 5;

                                Paragraph inventoryNumber = new($"ИНВ. {hardware.CombinedInvNumber}", fontInventory);
                                inventoryNumber.Alignment = Element.ALIGN_CENTER;
                                inventoryNumber.SpacingAfter = 0;
                                inventoryNumber.SpacingBefore = 0;
                                inventoryNumber.Leading = 8;

                                Paragraph extNumber = new($"{invExt}", fontExt);
                                extNumber.Alignment = Element.ALIGN_CENTER;
                                extNumber.SpacingAfter = 0;
                                extNumber.SpacingBefore = 0;
                                extNumber.PaddingTop = 0;
                                extNumber.Leading = 15;

                                // URL or text to be encoded in the QR code
                                string qrText = $"{prefixMarkCode}{hardware?.MarkCode.ToString()}";
                                // Create the QR code
                                BarcodeQRCode qrcode = new BarcodeQRCode(qrText, 1, 1, null);
                                // Convert the QR code to an image
                                iTextSharp.text.Image img = qrcode.GetImage();
                                img.Alignment = Element.ALIGN_CENTER;
                                img.SpacingAfter = 0;
                                img.SpacingBefore = 0;
                                img.ScaleToFit(40, 40);
                                // Create a PdfWriter instance
                                //PdfWriter.GetInstance(document, new FileStream(pathQR, FileMode.Create));
                                //document.Add(tmk);
                                document.Add(img);
                                document.Add(title);
                                document.Add(inventoryNumber);
                                document.Add(extNumber);
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
                if (hardwareDB != null)
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

            var userHardware = await context.Hardwares.Where(h => h.UserId == userId).ToListAsync();

            foreach(var hardware in userHardware)
            {
                var userWarehouse = await context.UserWarehouses.FirstOrDefaultAsync(h => h.Id == hardware.UserWarehouseId);
                if(userWarehouse != null)
                {
                    hardware.UserWarehouse = userWarehouse;
                }
            }

            return userHardware;
        }

        public async Task<CustomGeneralResponses> ReturnAsync(List<Guid> hardwaresID)
        {
            if (hardwaresID.Count == 0) return new CustomGeneralResponses(false, "Список с оборудованием для возврата равен 0.");

            foreach (var hardwareID in hardwaresID)
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
            var historyList = new List<History>();
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
                            IsActive = true,
                            NameForLabel = hardware.Title
                        };
                        context.Hardwares.Add(newHardware);
                        await context.SaveChangesAsync();
                        History history = new History(
                        hardware.ResponsibleId,
                        newHardware.Id,
                        newHardware.MainWarehouseId,
                        "Импорт",
                        DateTime.Now
                        );
                        historyList.Add(history);
                    }
                }
                await context.HistoryEntries.AddRangeAsync(historyList);
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

            var hardware = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == markHardwareDTO.HardwareId);
            if (hardware is null) return new CustomGeneralResponses(false, $"Оборудование с ID {markHardwareDTO.HardwareId} не найдено.");
            if (hardware.MarkCode != null) return new CustomGeneralResponses(false, $"Оборудование уже промаркировано.");

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

        public async Task<CustomGeneralResponses> MarkAllHardware(List<Guid> hardwareId)
        {
            if (hardwareId.Count == 0) return new CustomGeneralResponses(false, "Список с ID оборудования пустой");

            var hardwareList = new List<Hardware>();
            var markCodesList = new List<MarkCode>();
            markCodesList = await context.MarkCodes.Where(c => c.Used == false).ToListAsync();

            foreach (var id in hardwareId)
            {
                var hardware = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == id & h.MarkCode == null);
                if (hardware is null) continue;
                hardwareList.Add(hardware);
            }

            if (markCodesList.Count < hardwareList.Count)
            {
                var numberCodes = hardwareList.Count - markCodesList.Count;
                var markCodes = new List<MarkCode>();
                for(int i = 0; i < numberCodes; i++)
                {
                    var markCode = new MarkCode();
                    context.MarkCodes.Add(markCode);
                }
                await context.SaveChangesAsync();
            }
            markCodesList.Clear();
            
            foreach(var hardware in hardwareList)
            {
                var categoryHdwr = await context.CategoriesHardware.FirstOrDefaultAsync(c => c.Id == hardware.CategoryHardwareId);
                var markCode = await context.MarkCodes.FirstOrDefaultAsync(c => c.Used == false);
                hardware.MarkCode = markCode.Id;
                hardware.CombinedInvNumber = $"{categoryHdwr.ShortTitle}-{markCode.MarkCodeNumber}";
                markCode.Used = true;
                markCode.HardwareId = hardware.Id;
                await context.SaveChangesAsync();
            }

            return new CustomGeneralResponses(true, $"Оборудование в количестве {hardwareList.Count} промаркировано.");
        }

        public async Task<Hardware> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null!;

            var hardware = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == id);

            if(hardware == null) return null!;

            var warehouse = await context.MainWarehouses.FirstOrDefaultAsync(w => w.Id == hardware.MainWarehouseId);
            var category = await context.CategoriesHardware.FirstOrDefaultAsync(c => c.Id == hardware.CategoryHardwareId);
            var document = await context.DocumentsExternalSystem.FirstOrDefaultAsync(d => d.Id == hardware.DocumentExternalSystemId);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == hardware.UserId);
            var userWarehouse = await context.UserWarehouses.FirstOrDefaultAsync(uw => uw.Id == hardware.UserWarehouseId);

            hardware.MainWarehouse = warehouse;
            hardware.CategoryHardware = category;
            hardware.DocumentExternalSystem = document;
            if(user != null)
                hardware.User = user;
            if(userWarehouse != null)
                hardware.UserWarehouse = userWarehouse;

            return hardware;
        }
    }
}
