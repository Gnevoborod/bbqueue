using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using bbqueue.Controllers.Dtos.Window;
namespace bbqueue.Mapper
{
    internal static class WindowMapper
    {
        public static Window FromEntityToModel(this WindowEntity windowEntity)
        {
            if (windowEntity == null)
                return default!;
            return new Window
            {
                Id = windowEntity.Id,
                Number = windowEntity.Number,
                Description = windowEntity.Description,
                EmployeeId = windowEntity.EmployeeId,
                WindowWorkState = windowEntity.WindowWorkState
            };
        }

        public static WindowEntity FromModelToEntity(this Window window)
        {
            if (window == null)
                return default!;
            return new WindowEntity
            {
                Id = window.Id,
                Number = window.Number,
                Description = window.Description,
                EmployeeId = window.EmployeeId,
                WindowWorkState = window.WindowWorkState
            };
        }

        public static WindowDto FromModelToDto(this Window window)
        {
            if (window == null)
                return default!;
            return new WindowDto
            {
                Id = window.Id,
                Number = window.Number,
                Description = window.Description
            };
        }

        public static Window FromChangeStateDtoToModel(this ChangeWindowWorkStateDto dto)
        {
            var enumItems = Enum.GetNames(typeof(WindowWorkState));
            var enumItemsIndexes = Enum.GetValues(typeof(WindowWorkState));
            WindowWorkState? windowWorkState=default;
            for(int i =0; i < enumItems.Length;i++)
            {
                if (enumItems[i].ToLower() == dto.WindowWorkState.ToLower())
                    windowWorkState = (WindowWorkState?)enumItemsIndexes.GetValue(i);
            }
            return new Window
            {
                Number = dto.Number,
                WindowWorkState = windowWorkState != null ? (WindowWorkState) windowWorkState:default
            };
        }

        public static Window FromDtoToModel(this WindowCreateDto windowCreateDto)
        {
            if(windowCreateDto == null)
                return default!;
            return new Window
            {
                Number = windowCreateDto.Number,
                Description = windowCreateDto.Description
            };
        }

    }
}
