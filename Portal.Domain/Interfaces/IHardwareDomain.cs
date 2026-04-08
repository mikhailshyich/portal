using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IHardwareDomain
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO);
        Task<CustomGeneralResponses> GiveToUserAsync(HardwareMoveDTO giveDTO);
        Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO);
        Task<CustomGeneralResponses> RepairAsync(HardwareRepairDTO repairDTO);
        Task<CustomGeneralResponses> ReturnRepairAsync(HardwareRepairDTO repairDTO);
        Task<string> GenerateQR(List<Guid>? idList);
        Task<string> GenerateLabel(List<Guid>? idList);
        Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport);
        Task<Byte[]> Export();
        Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO);
        Task<CustomGeneralResponses> MarkAllHardware(MarkAllHardwareDTO markAllHardwareDTO);
        Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO);
        Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO);

        #region Методы для получения оборудования

        /// <summary>
        /// Получить всё оборудование
        /// </summary>
        /// <returns>Список с оборудованием</returns>
        Task<List<Hardware>> GetAllAsync();

        /// <summary>
        /// Получить оборудование для конкретного пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Список оборудования</returns>
        Task<List<Hardware>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Получить оборудование по Id
        /// </summary>
        /// <param name="id">Id оборудования</param>
        /// <returns>Объект класс Hardware</returns>
        Task<Hardware> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить оборудование по коду маркировки
        /// </summary>
        /// <param name="markCode">Код маркировки</param>
        /// <returns>Объект класса Hardware</returns>
        Task<Hardware> GetByMarkCodeAsync(Guid markCode);

        #endregion
    }
}
