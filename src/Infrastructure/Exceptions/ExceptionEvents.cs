﻿namespace bbqueue.Infrastructure.Exceptions
{
    public static class ExceptionEvents
    {

        public static readonly EventId WindowNotExists = new(1, "Не удалось найти указанное окно");
        public static readonly EventId WindowRelatedToAnotherEmployee = new(2, "Невозможно изменить состояние окна, так как к данному окну привязан другой пользователь");
        public static readonly EventId WindowOccupied = new(3, "Для данного окна уже назначен сотрудник");
        public static readonly EventId WindowNumberExists = new(4, "Окно с таким номером уже существует");
        public static readonly EventId WindowTargetExists = new(5, "Цель уже назначена этому окну");

        public static readonly EventId EmployeeNotFound = new(11, "Сотрудник не найден");
        public static readonly EventId WrongRoleInRequest = new(12, "Некорректно указана роль");
        public static readonly EventId UserIdUndefined = new(13, "Значение идентификатора пользователя не установлено");

        public static readonly EventId TicketNotFound = new(21, "Не найдено талона для обновления");
        public static readonly EventId TicketUnableToTakeToWork = new(22, "Невозможно взять в работу следующий талон. Пользователь не прикреплён к окну");
        public static readonly EventId TicketNotRelatedToThisEmployee = new(23, "Талон не связан с данным сотрудником.");
        public static readonly EventId TicketCleaningFailed = new(27, "Очистка данных в БД закончилась неудачей");
        public static readonly EventId TicketUnableToCreate = new(28, "Невозможно создать новый талон. Ошибка при сохранении данных в БД");
        public static readonly EventId TicketTakeToWorkTransactionFailed = new(29, "Невозможно взять в работу следующий талон. Ошибка при сохранении данных в БД");

        public static readonly EventId TargetPrefixUndefined = new(31, "Для указанной цели отсутствует префикс");
        public static readonly EventId TargetPrefixExists = new(32, "Цель с таким префиксом уже существует");
        public static readonly EventId TargetNameExists = new(33, "Цель с таким названием уже существует");
        public static readonly EventId TargetNotExists = new(34, "Не удалось найти указанную цель");

        public static readonly EventId GroupParentIdNotExists = new(35, "Невозможно назначить предка для подраздела: предок-раздел с таким идентификатором отсутствует");
        public static readonly EventId GroupNameExists = new(36, "Раздел с таким названием уже существует");

        public static readonly EventId ValidatorEmptyValue = new(300, "Пустое значение");
        public static readonly EventId EnumValidatorTypeNotEnum = new(351,"Используемый в ограничении тип - не enum");
        public static readonly EventId EnumValidatorValueNotInScope = new(352, "Значение не найдено среди допустимых:");


        public static readonly EventId HostedServiceErrorWhileExecuted = new(701, "Ошибка при выполнении автозадания по очистке данных о талонах.");

        public static readonly EventId Default = new(0, "Unkonown error");

    }
}
