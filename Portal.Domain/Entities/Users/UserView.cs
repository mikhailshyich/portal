namespace Portal.Domain.Entities.Users
{
    public class UserView
    {
        public Guid Id { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Patronymic { get; set; } = string.Empty; //отчество
        public string? Specialization { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public UserView() { }

        /// <summary>
        /// Конструктор для вывода основной информации о пользователе
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="userRoleId">ID роли пользователя</param>
        /// <param name="userDepartmentId">ID отдела пользователя</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastname">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="specialization">Должность</param>
        /// <param name="email">Почта</param>
        /// <param name="isActive">Статус</param>
        public UserView(Guid id, Guid userRoleId, Guid userDepartmentId, string? firstName, string? lastname, string? patronymic,
                        string? specialization, string? email, bool isActive)
        {
            this.Id = id;
            this.UserRoleId = userRoleId;
            this.UserDepartmentId = userDepartmentId;
            this.FirstName = firstName;
            this.LastName = lastname;
            this.Patronymic = patronymic;
            this.Specialization = specialization;
            this.Email = email;
            this.IsActive = isActive;
        }
    }
}
