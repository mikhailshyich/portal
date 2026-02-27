using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.History;
using Portal.Domain.Enums;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{

    public class HardwareRepository : IHardwareDomain
    {
        private readonly PortalDbContext context;
        private static readonly string prefixMarkCode = "markCode_";
        private Dictionary<HrdwStatuses, string> hrdwStatuses = new();
        private PortalEnums status = new();

        public HardwareRepository(PortalDbContext context)
        {
            this.context = context;
            hrdwStatuses = status.ReturnHardwareStatus();
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var hardwareCount = request.Count;
            var historyList = new List<History>();
            try
            {
                string status = hrdwStatuses[HrdwStatuses.accepted].ToString(); // Получаем статус Добавлено
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
                        NameForLabel = request.Title,
                        SerialNumber = request.SerialNumber,
                        Status = status
                    };
                    context.Hardwares.Add(hardware);
                    await context.SaveChangesAsync();
                    History history = new History(
                    request.ResponsibleId,
                    hardware.Id,
                    null,
                    request.MainWarehouseId,
                    status,
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
            int sizeQR = 10;
            try
            {
                iTextSharp.text.Rectangle qrSize = new(sizeQR, sizeQR);
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
                            BarcodeQRCode qrcode = new BarcodeQRCode(text, sizeQR, sizeQR, null);
                            // Convert the QR code to an image
                            iTextSharp.text.Image img = qrcode.GetImage();
                            img.ScaleToFit(sizeQR, sizeQR);
                            img.SetAbsolutePosition((document.PageSize.Width - img.ScaledWidth) / 2, (document.PageSize.Height - img.ScaledHeight) / 2); // изображение по центру
                            //img.SetAbsolutePosition(3, (document.PageSize.Height - img.ScaledHeight) / 2); // изображение по центру
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
                iTextSharp.text.Rectangle labelSize = new(115, 90); //49,4x31,7mm 111, 84
                string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "BAHNSCHRIFT.TTF");           //
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);                                //нужно для отображения кирилицы
                var fontExt = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);    //
                var fontTitle = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
                fontTitle.Size = 10;
                fontExt.Size = 14;
                int titleLeading = 9;
                int extNumberLeading = 14;

                Document document = new Document(labelSize, 0, 0, 1, 0);

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
                                string invExt = $"{hardware.CombinedInvNumber}";
                                if (hardware.InventoryNumberExternalSystem != string.Empty)
                                {
                                    invExt = $"{hardware.InventoryNumberExternalSystem}";
                                }

                                Paragraph title = new(titleLeading, $"{hardware.NameForLabel}", fontTitle);
                                title.Alignment = Element.ALIGN_CENTER;

                                Paragraph extNumber = new(extNumberLeading, $"{invExt}", fontExt);
                                extNumber.Alignment = Element.ALIGN_CENTER;

                                // URL or text to be encoded in the QR code
                                string qrText = $"{prefixMarkCode}{hardware?.MarkCode.ToString()}";
                                // Create the QR code
                                BarcodeQRCode qrcode = new BarcodeQRCode(qrText, 1, 1, null);
                                // Convert the QR code to an image
                                iTextSharp.text.Image img = qrcode.GetImage();
                                img.Alignment = Element.ALIGN_CENTER;
                                img.ScaleToFit(45, 45);

                                document.Add(img);
                                document.Add(title);
                                document.Add(extNumber);
                                document.NewPage();
                                document.SetPageSize(labelSize);
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
            var hardware = await context.Hardwares.ToListAsync();
            return hardware;
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO)
        {
            if (moveDTO.HardwareIdList is null) return new CustomGeneralResponses(false, "Список перемещаемого оборудования равен null.");
            if (moveDTO.UserId == Guid.Empty) return new CustomGeneralResponses(false, "Пользователь на которого перемещаем оборудование равен null.");

            var userDB = await context.Users.FindAsync(moveDTO.UserId);
            if (userDB is null) return new CustomGeneralResponses(false, "Пользователь на которого перемещаем оборудование не найден в базе данных.");

            var userWarehouseDB = await context.UserWarehouses.FirstOrDefaultAsync(u => u.Id == moveDTO.UserWarehouseId & u.UserId == moveDTO.UserId);
            if (userWarehouseDB is null) return new CustomGeneralResponses(false, $"У пользователя {userDB.Username} нет такого склада.");

            List<Hardware> displacedHardware = new();
            var historyList = new List<History>();

            foreach (var hardwareID in moveDTO.HardwareIdList)
            {
                var hardwareDB = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == hardwareID & h.UserId != userDB.Id);
                if (hardwareDB != null)
                {
                    string status = hrdwStatuses[HrdwStatuses.moving].ToString(); // Получаем статус Перемещено
                    if (hardwareDB.UserId.HasValue)
                    {
                        History historySender = new History(moveDTO.ResponsibleId, moveDTO.UserId, hardwareDB.UserId, hardwareDB.Id, userWarehouseDB.Id, status, DateTime.Now);
                        historyList.Add(historySender);
                    }
                    else
                    {
                        History history = new History(moveDTO.ResponsibleId, moveDTO.UserId, null, hardwareID, userWarehouseDB.Id, status, DateTime.Now);
                        historyList.Add(history);
                    }
                    hardwareDB.UserId = userDB.Id;
                    hardwareDB.User = userDB;
                    hardwareDB.UserWarehouseId = userWarehouseDB.Id;
                    hardwareDB.UserWarehouse = userWarehouseDB;
                    hardwareDB.Status = status;

                    displacedHardware.Add(hardwareDB);
                    var userHardware = new UserHardware()
                    {
                        HardwareId = hardwareDB.Id,
                        UserId = userDB.Id
                    };
                    
                    context.UsersHardware.Add(userHardware);
                }
            }
            context.HistoryEntries.AddRange(historyList);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование в количестве {displacedHardware.Count} перемещено на {userDB.Username}", displacedHardware);
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty) return null!;

            var userDB = await context.Users.FindAsync(userId);
            if (userDB is null) return null!;

            var userHardware = await context.Hardwares.Where(h => h.UserId == userId & h.IsActive == true).ToListAsync();

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

        public async Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO)
        {
            if (returnDTO.HardwareIdList.Count == 0) return new CustomGeneralResponses(false, "Список с оборудованием для возврата равен 0.");
            if (returnDTO.ResponsibleId == Guid.Empty) return new CustomGeneralResponses(false, "ID ответственного пустой.");

            var responsible = await context.Users.AnyAsync(r => r.Id == returnDTO.ResponsibleId);
            if (responsible is false) return new CustomGeneralResponses(false, "Проверьте ID ответственного.");

            var returned = new List<Guid>();

            foreach (var hardwareID in returnDTO.HardwareIdList)
            {
                var hardwareDB = await context.Hardwares.FindAsync(hardwareID);
                if (hardwareDB != null & hardwareDB.UserId is null)
                {
                    continue;
                }
                else
                {
                    string status = hrdwStatuses[HrdwStatuses.return_main].ToString(); // Получаем статус Возврат на основной склад
                    //hardwareDB.UserId = null;
                    //hardwareDB.UserWarehouseId = null;
                    hardwareDB.Status = hrdwStatuses[HrdwStatuses.moving].ToString(); // Получаем статус Перемещено
                    returned.Add(hardwareDB.Id);
                    History history = new History(returnDTO.ResponsibleId, hardwareDB.Id, hardwareDB.MainWarehouseId, hardwareDB.UserId, hardwareDB.UserWarehouseId, status, DateTime.Now);
                    await context.HistoryEntries.AddAsync(history);
                }
            }
            await context.SaveChangesAsync();

            if (returned.Count == 0) return new CustomGeneralResponses(false, "Передаваемое оборудование уже на основном складе.");

            return new CustomGeneralResponses(true, $"Оборудование в количестве {returned.Count} возвращено на склад!");
        }

        public async Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport)
        {
            if (hardwareImport.Count == 0) return new CustomGeneralResponses(false, "Список с оборудованием для импорта пустой.");

            var hardwareList = new List<Hardware>();
            var historyList = new List<History>();
            try
            {
                string status = hrdwStatuses[HrdwStatuses.import].ToString(); // Получаем статус Импортировано
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
                            NameForLabel = hardware.Title,
                            Status = status
                        };
                        context.Hardwares.Add(newHardware);
                        await context.SaveChangesAsync();
                        History history = new History(hardware.ResponsibleId,newHardware.Id,null,newHardware.MainWarehouseId,status,DateTime.Now);
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

            string status = hrdwStatuses[HrdwStatuses.marking].ToString(); // Получаем статус Промаркировано

            hardware.MarkCode = markCode.Id;
            hardware.CombinedInvNumber = $"{categoryHrdw.ShortTitle}-{markCode.MarkCodeNumber}";
            hardware.Status = status;

            markCode.HardwareId = hardware.Id;
            markCode.Used = true;



            History history = new History(markHardwareDTO.ResponsibleId, hardware.Id, markCode.Id, null, status, DateTime.Now);
            await context.HistoryEntries.AddAsync(history);

            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование успешно промаркировано!", hardware);
        }

        public async Task<CustomGeneralResponses> MarkAllHardware(MarkAllHardwareDTO hardwareDTO)
        {
            if (hardwareDTO.HardwareIdList.Count == 0) return new CustomGeneralResponses(false, "Список с ID оборудования пустой");

            var hardwareList = new List<Hardware>();
            var markCodesList = new List<MarkCode>();
            markCodesList = await context.MarkCodes.Where(c => c.Used == false).ToListAsync();

            foreach (var id in hardwareDTO.HardwareIdList)
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

            string status = hrdwStatuses[HrdwStatuses.marking].ToString(); // Получаем статус Промаркировано

            foreach (var hardware in hardwareList)
            {
                var categoryHdwr = await context.CategoriesHardware.FirstOrDefaultAsync(c => c.Id == hardware.CategoryHardwareId);
                var markCode = await context.MarkCodes.FirstOrDefaultAsync(c => c.Used == false);
                hardware.MarkCode = markCode.Id;
                hardware.CombinedInvNumber = $"{categoryHdwr.ShortTitle}-{markCode.MarkCodeNumber}";
                hardware.Status = status;
                markCode.Used = true;
                markCode.HardwareId = hardware.Id;
                History history = new History(hardwareDTO.ResponsibleId, hardware.Id, markCode.Id, null, status, DateTime.Now);
                await context.HistoryEntries.AddAsync(history);
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

        public async Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO)
        {
            if (updateDTO is null) return null!;

            var hardware = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == updateDTO.HardwareId);
            if(hardware is null) return null!;

            Guid responsibleId = new Guid();
            Guid userId = new Guid();

            if (updateDTO.NameForLabel != string.Empty)
                hardware.NameForLabel = updateDTO.NameForLabel;
            if(updateDTO.InventoryNumberExternalSystem != string.Empty)
                hardware.InventoryNumberExternalSystem = updateDTO.InventoryNumberExternalSystem;
            if (updateDTO.UserId.HasValue)
            {
                userId = updateDTO.UserId.Value;
                hardware.UserId = userId;
            }
                
            if (updateDTO.UserWarehouseId.HasValue)
            {
                string status = hrdwStatuses[HrdwStatuses.moving].ToString(); // Получаем статус Перемещено
                hardware.UserWarehouseId = updateDTO.UserWarehouseId.Value;
                hardware.Status = status;
                if (updateDTO.ResponsibleId.HasValue)
                    responsibleId = updateDTO.ResponsibleId.Value;
                History history = new History(responsibleId, userId, null, updateDTO.HardwareId, updateDTO.UserWarehouseId.Value, status, DateTime.Now);
                await context.HistoryEntries.AddAsync(history);
            }
            await context.SaveChangesAsync();
            return hardware;
        }

        public async Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO)
        {
            if (writeOffDTO is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            if (writeOffDTO.HardwareIdList.Count == 0 ) return new CustomGeneralResponses(false, "Передаваемый список оборудования пустой.");
            if (writeOffDTO.ResponsibleId == Guid.Empty ) return new CustomGeneralResponses(false, "Передаваемый ID ответственного пустой.");

            var responsible = await context.Users.AnyAsync(r => r.Id ==  writeOffDTO.ResponsibleId);
            if(responsible == false) return new CustomGeneralResponses(false, "Ответственный сотрудник не найден.");

            var hardwareList = new List<Hardware>();

            string status = hrdwStatuses[HrdwStatuses.write_off].ToString(); // Получаем статус Списано

            foreach (var id in writeOffDTO.HardwareIdList)
            {
                var hardware = await context.Hardwares.FirstOrDefaultAsync(h =>h.Id == id);
                if( hardware is null) continue;
                hardware.IsActive = false;
                hardware.Status = status;
                hardwareList.Add(hardware);
                History history = new History(writeOffDTO.ResponsibleId, hardware.Id, null, hardware.MainWarehouseId, status, DateTime.Now);
                await context.HistoryEntries.AddAsync(history);
            }

            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, $"Оборудование в количестве {hardwareList.Count} списано!");
        }

        public async Task<byte[]> Export()
        {
            var hardwares = await context.Hardwares.Where(h => h.IsActive == true).ToListAsync();

            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Не списанное оборудование");

                worksheet.Cell(1, 1).Value = "Номер 1С";
                worksheet.Cell(1, 2).Value = "Инвентарный";
                worksheet.Cell(1, 3).Value = "Наименование";

                for(int i=0; i<hardwares.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = hardwares[i].InventoryNumberExternalSystem;
                    worksheet.Cell(i + 2, 2).Value = hardwares[i].CombinedInvNumber;
                    worksheet.Cell(i + 2, 3).Value = hardwares[i].Title;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<CustomGeneralResponses> GiveToUserAsync(HardwareMoveDTO giveDTO)
        {
            if (giveDTO.HardwareIdList is null) return new CustomGeneralResponses(false, "Список выдаваемого оборудования равен null.");
            if (giveDTO.UserId == Guid.Empty) return new CustomGeneralResponses(false, "Пользователь которому выдаётся оборудование равен null.");

            var userDB = await context.Users.FindAsync(giveDTO.UserId);
            if (userDB is null) return new CustomGeneralResponses(false, "Пользователь которому выдаём оборудование не найден в базе данных.");

            var userWarehouseDB = await context.UserWarehouses.FirstOrDefaultAsync(u => u.Id == giveDTO.UserWarehouseId & u.UserId == giveDTO.UserId);
            if (userWarehouseDB is null) return new CustomGeneralResponses(false, $"У пользователя {userDB.Username} нет такого склада.");

            List<Hardware> displacedHardware = new();
            var historyList = new List<History>();

            string status = hrdwStatuses[HrdwStatuses.give].ToString(); // Получаем статус Выдано

            foreach (var hardwareID in giveDTO.HardwareIdList)
            {
                var hardwareDB = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == hardwareID & h.UserId != userDB.Id);
                if (hardwareDB != null)
                {
                    hardwareDB.Status = status;
                    if (hardwareDB.UserId.HasValue)
                    {
                        History historySender = new History(giveDTO.ResponsibleId, giveDTO.UserId, hardwareDB.UserId, hardwareDB.Id, userWarehouseDB.Id, status, DateTime.Now);
                        historyList.Add(historySender);
                    }
                    else
                    {
                        History history = new History(giveDTO.ResponsibleId, giveDTO.UserId, null, hardwareID, userWarehouseDB.Id, status, DateTime.Now);
                        historyList.Add(history);
                    }

                    displacedHardware.Add(hardwareDB);
                }
            }
            context.HistoryEntries.AddRange(historyList);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование в количестве {displacedHardware.Count} выдано {userDB.Username}", displacedHardware);
        }

        public async Task<CustomGeneralResponses> RepairAsync(HardwareRepairDTO repairDTO)
        {
            if (repairDTO.HardwareIdList is null) return new CustomGeneralResponses(false, "Список перемещаемого оборудования равен null.");

            List<Hardware> displacedHardware = new();
            var historyList = new List<History>();

            string status = hrdwStatuses[HrdwStatuses.repair].ToString(); // Получаем статус В ремонте

            foreach (var hardwareID in repairDTO.HardwareIdList)
            {
                var hardwareDB = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == hardwareID);
                if (hardwareDB != null)
                {
                    hardwareDB.Status = status;
                    History history = new History(repairDTO.ResponsibleId, hardwareDB.Id, repairDTO.Annotation, status, DateTime.Now);
                    historyList.Add(history);

                    displacedHardware.Add(hardwareDB);
                }
            }
            context.HistoryEntries.AddRange(historyList);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование в количестве {displacedHardware.Count} передано на ремонт. Комментарий: {repairDTO.Annotation}", displacedHardware);
        }

        public async Task<CustomGeneralResponses> ReturnRepairAsync(HardwareRepairDTO repairDTO)
        {
            if (repairDTO.HardwareIdList is null) return new CustomGeneralResponses(false, "Список перемещаемого оборудования равен null.");

            List<Hardware> displacedHardware = new();
            var historyList = new List<History>();

            foreach (var hardwareID in repairDTO.HardwareIdList)
            {
                var hardwareDB = await context.Hardwares.FirstOrDefaultAsync(h => h.Id == hardwareID);
                if (hardwareDB != null)
                {
                    string status = hrdwStatuses[HrdwStatuses.repair_refund].ToString(); // Получаем статус Возврат из ремонта

                    History history = new History(repairDTO.ResponsibleId, hardwareDB.Id, repairDTO.Annotation, status, DateTime.Now);
                    historyList.Add(history);

                    hardwareDB.Status = hrdwStatuses[HrdwStatuses.moving].ToString();

                    displacedHardware.Add(hardwareDB);
                }
            }
            context.HistoryEntries.AddRange(historyList);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Оборудование в количестве {displacedHardware.Count} возвращено из ремонта. Комментарий: {repairDTO.Annotation}", displacedHardware);
        }
    }
}
