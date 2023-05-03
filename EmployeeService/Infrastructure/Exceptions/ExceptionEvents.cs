namespace EmployeeService.Infrastructure.Exceptions
{
    public static class ExceptionEvents
    {

        public static readonly EventId EmployeeNotFound = new(11, "Сотрудник не найден");
        public static readonly EventId WrongRoleInRequest = new(12, "Некорректно указана роль");
        public static readonly EventId UserIdUndefined = new(13, "Значение идентификатора пользователя не установлено");

        public static readonly EventId ValidatorEmptyValue = new(300, "Пустое значение");
        public static readonly EventId EnumValidatorTypeNotEnum = new(351,"Используемый в ограничении тип - не enum");
        public static readonly EventId EnumValidatorValueNotInScope = new(352, "Значение не найдено среди допустимых:");

        public static readonly EventId SecretKeyGeneratorErrorWhileExecuted = new(702, "Ошибка при работе автоматического задания по созданию нового секретного ключа");
        public static readonly EventId Default = new(0, "Unkonown error");

    }
}
