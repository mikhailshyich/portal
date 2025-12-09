using Portal.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.History
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? HardwareId { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public Guid? WarehouseId { get; set; }
        public string OperationType { get; set; }
        public DateTime DateTimeChanges { get; set; }

        public User? User { get; set; }

        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        /// <param name="responsibleId">ID ответственного пользователя</param>
        /// <param name="hardwareId">ID оборудования</param>
        /// <param name="senderId">ID отправителя</param>
        /// <param name="recipientId">ID получателя</param>
        /// <param name="warehouseId">ID склада</param>
        /// <param name="operationType">Вид операции</param>
        /// <param name="dateTimeChanges">Дата и время изменений</param>
        public History(Guid? responsibleId, Guid? hardwareId, Guid? senderId, Guid? warehouseId, string operationType, Guid? recipientId, DateTime dateTimeChanges)
        {
            this.ResponsibleId = responsibleId;
            this.HardwareId = hardwareId;
            this.SenderId = senderId;
            this.WarehouseId = warehouseId;
            this.OperationType = operationType;
            this.RecipientId = recipientId;
            this.DateTimeChanges = dateTimeChanges;
        }

        /// <summary>
        /// Добавление оборудования на склад
        /// </summary>
        /// <param name="responsibleId">ID ответственного пользователя</param>
        /// <param name="hardwareId">ID оборудования</param>
        /// <param name="warehouseId">ID склада</param>
        /// <param name="operationType">Вид операции</param>
        /// <param name="dateTimeChanges">Дата и время изменений</param>
        public History(Guid? responsibleId, Guid hardwareId, Guid warehouseId, string operationType, DateTime dateTimeChanges)
        {
            this.ResponsibleId = responsibleId;
            this.HardwareId = hardwareId;
            this.WarehouseId = warehouseId;
            this.OperationType = operationType;
            this.DateTimeChanges = dateTimeChanges;
        }
    }
}
