namespace Portal.Domain.Entities.Users
{
    public class UserView
    {
        public Guid Id { get; set; }
        public Guid? UserDepartmentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public UserDepartment? UserDepartment { get; set; }

        public UserView()
        {
            
        }

        /// <summary>
        /// Класс для вывода основной информации о сотруднике
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="userDepartmentId">ID отдела сотрудника</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastname">Фамилия</param>
        /// <param name="specialization">Должность</param>
        /// <param name="email">Почта</param>
        /// <param name="isActive">Статус</param>
        /// <param name="department">Отдел сотрудника</param>
        public UserView(
                        Guid id, 
                        Guid? userDepartmentId,
                        string? firstName,
                        string? lastname,
                        string? specialization,
                        string? email, 
                        bool isActive,
                        UserDepartment department
                        )
        {
            this.Id = id;
            this.UserDepartmentId = userDepartmentId;
            this.FirstName = firstName;
            this.LastName = lastname;
            this.Specialization = specialization;
            this.Email = email;
            this.IsActive = isActive;
            this.UserDepartment = department;
        }
    }
}
