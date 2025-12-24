using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.History
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? HardwareId { get; set; }
        public Guid? HardwareMarkCode { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? UserWarehouseId { get; set; }
        public string OperationType { get; set; }
        public DateTime DateTimeChanges { get; set; }
        public string? Responsible { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? MainWarehouse { get; set; }
        public string? UserWarehouse { get; set; }
        public string? Annotation { get; set; }


        public History() { }

        /// <summary>
        /// Возврат/Добавление/Импорт/Списание/Маркировка оборудования
        /// </summary>
        /// <param name="responsibleId">ID ответственного пользователя</param>
        /// <param name="hardwareId">ID оборудования</param>
        /// <param name="hardwareMarkCode">Код маркировки (не обязательный)</param>
        /// <param name="warehouseId">ID основного склада</param>
        /// <param name="operationType">Вид операции</param>
        /// <param name="dateTimeChanges">Дата и время изменений</param>
        public History(Guid responsibleId, Guid hardwareId, Guid? hardwareMarkCode, Guid? warehouseId, string operationType, DateTime dateTimeChanges)
        {
            this.ResponsibleId = responsibleId;
            this.HardwareId = hardwareId;
            this.HardwareMarkCode = hardwareMarkCode;
            this.WarehouseId = warehouseId;
            this.OperationType = operationType;
            this.DateTimeChanges = dateTimeChanges;
        }


        /// <summary>
        /// Переместить оборудование на пользователя
        /// </summary>
        /// <param name="responsibleId">ID ответственного</param>
        /// <param name="recipientId">ID получателя</param>
        /// <param name="senderId">ID отправителя</param>
        /// <param name="hardwareId">ID оборудования</param>
        /// <param name="userWarehouseId">ID склада пользователя</param>
        /// <param name="operationType">Вид операции</param>
        /// <param name="dateTimeChanges">Дата и время изменений</param>
        public History(Guid responsibleId, Guid recipientId, Guid? senderId, Guid hardwareId, Guid userWarehouseId, string operationType, DateTime dateTimeChanges)
        {
            this.ResponsibleId = responsibleId;
            this.RecipientId = recipientId;
            this.SenderId = senderId;
            this.HardwareId = hardwareId;
            this.UserWarehouseId = userWarehouseId;
            this.OperationType = operationType;
            this.DateTimeChanges = dateTimeChanges;
        }
    }
}
