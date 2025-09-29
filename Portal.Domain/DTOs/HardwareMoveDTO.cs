
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class HardwareMoveDTO
    {
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid UserWarehouseId { get; set; }
    }
}
