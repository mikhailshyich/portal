namespace Portal.Domain.Enums
{
    /// <summary>
    /// Статусы для оборудования
    /// </summary>
    public enum HrdwStatuses
    {
        accepted,       // Добавлено
        marking,        // Промаркировано
        moving,         // Перемещено
        give,           // Выдано
        return_main,    // Возврат на основной склад
        repair,         // В ремонте
        repair_refund,  // Возврат из ремонта
        write_off,      // Списано
        import          // Импортировано
    }

    public class PortalEnums
    {
        private Dictionary<HrdwStatuses, string> HardwareStatuses { get; } = new();

        /// <summary>
        /// Получить статусы оборудования
        /// </summary>
        /// <returns>Dictionary<HardwareStatuses, string></returns>
        public Dictionary<HrdwStatuses, string> ReturnHardwareStatus()
        {
            HardwareStatuses.Add(HrdwStatuses.accepted, "Добавлено");
            HardwareStatuses.Add(HrdwStatuses.marking, "Промаркировано");
            HardwareStatuses.Add(HrdwStatuses.moving, "Перемещено");
            HardwareStatuses.Add(HrdwStatuses.give, "Выдано");
            HardwareStatuses.Add(HrdwStatuses.return_main, "Возврат на основной склад");
            HardwareStatuses.Add(HrdwStatuses.repair, "В ремонте");
            HardwareStatuses.Add(HrdwStatuses.repair_refund, "Возврат из ремонта");
            HardwareStatuses.Add(HrdwStatuses.write_off, "Списано");
            HardwareStatuses.Add(HrdwStatuses.import, "Импортировано");

            return HardwareStatuses;
        }

    }
}
