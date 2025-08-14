using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;
using System.Security.Cryptography;

namespace Portal.Infrastructure.Repositories
{
    public class HardwareRepository : IHardwareDomain
    {
        private readonly PortalDbContext context;

        public HardwareRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            if(request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            
            var hardwareCount = request.Count;
            var hardwareList = new List<Hardware>();

            var category = await context.CategoriesHardware.FindAsync(request.CategoryHardwareId);

            for(var i=0; i < hardwareCount; i++)
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
                    FileNameImage = request.FileNameImage
                };
                context.Hardwares.Add(hardware);
                await context.SaveChangesAsync();
                hardware.CombinedInvNumber = $"{category?.ShortTitle}-{hardware.InventoryNumber}";
            }

            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, "Оборудование успешно добавлено.", hardwareList);
        }

        public async Task<string> GenerateQR(Guid? id, List<Guid>? idList)
        {
            try
            {
                iTextSharp.text.Rectangle qrSize = new(30, 30); //49,4x31,7mm 111, 84
                Document document = new Document(qrSize, 0, 0, 0, 0);
                var randomNumber = new byte[10];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                var randomText = Convert.ToBase64String(randomNumber);
                var fileName = $"{randomText}-qr.pdf";
                var pathQR = $"Labels\\{fileName}";
                if (System.IO.File.Exists(pathQR))
                {
                    System.IO.File.Delete(pathQR);
                }
                using (FileStream fs = new FileStream(pathQR, FileMode.CreateNew))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.Open();
                    foreach (var hardawreId in idList)
                    {
                        var hardware = context.Hardwares.Find(hardawreId);

                        // URL or text to be encoded in the QR code
                        string text = hardware?.CombinedInvNumber;
                        // Create the QR code
                        BarcodeQRCode qrcode = new BarcodeQRCode(text, 25, 25, null);
                        // Convert the QR code to an image
                        iTextSharp.text.Image img = qrcode.GetImage();
                        // Create a PdfWriter instance
                        //PdfWriter.GetInstance(document, new FileStream(pathQR, FileMode.Create));
                        document.Add(img);
                        document.NewPage();

                    }
                    document.Close();
                }
                return fileName;
                //if (System.IO.File.Exists(pathBarcode))
                //{
                //    System.IO.File.Delete(pathBarcode);
                //}

                // Create a Document object
                //iTextSharp.text.Rectangle labelSize = new(111, 84); //49,4x31,7mm 111, 84



                // Open the document for writing

                // Add the QR code image to the document
                // Close the document
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
    }
}
