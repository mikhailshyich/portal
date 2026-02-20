namespace Portal.Domain.Enums
{
    public enum StatusHardware
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
}
