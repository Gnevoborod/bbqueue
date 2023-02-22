using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using bbqueue.Controllers.Dtos.Group;
using System.Linq;

namespace bbqueue.Mapper
{
    internal static class GroupMapper
    {
        public static Group? FromEntityToModel(this GroupEntity? groupEntity)
        {
            if (groupEntity == null)
                return null;
            return new Group
            {
                Id = groupEntity.Id,
                Name = groupEntity.Name,
                Description = groupEntity.Description,
                GroupLinkId = groupEntity.GroupLinkId,//вероятно не нужно
                GroupLink = FromEntityToModel(groupEntity.GroupLink) /* Мы так можем делать? ParentGroup*/
            };
        }

        public static GroupEntity? FromModelToEntity(this Group? group)
        {
            if (group == null)
                return null;
            return new GroupEntity
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                GroupLinkId = group.GroupLinkId,
                GroupLink = FromModelToEntity(group.GroupLink) /* Мы так можем делать? */
            };
        }

        public static GroupDto? FromModelToDto(this Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                GroupLinkId = group.GroupLinkId
            };
        }

        //Метод неиспользуется, см.пояснения к сл.методу
        public static GroupHierarchyDto _FromModelToHierarchyDto(this List<Group> group, List<Target> target, GroupDto? groupsDto, long parentId=-1)
        {
            GroupHierarchyDto groupHierarchyDto = new GroupHierarchyDto();

            groupHierarchyDto.Name = (groupsDto?.Name)?? "";
            groupHierarchyDto.Description = (groupsDto?.Description) ?? "";
            groupHierarchyDto.Id = (groupsDto?.Id)??-1;
            groupHierarchyDto.GroupsInHierarchy = new List<GroupHierarchyDto>();
            var groupsToProcess = group.Where(g => g.GroupLinkId == (parentId > -1 ? parentId : groupsDto?.GroupLinkId));
            var groupsDtoToProcess = groupsToProcess.Select(g => g.FromModelToDto()).ToList();
            var groupToNextProcess = group.Except(groupsToProcess).ToList();
            for(int i =0; i< groupsDtoToProcess.Count;i++)
            {
                groupHierarchyDto?.GroupsInHierarchy?.Add(groupToNextProcess?._FromModelToHierarchyDto(target!, groupsDtoToProcess[i]!, groupsDtoToProcess[i]!.Id)!) ;
            }
            groupHierarchyDto!.Targets = target.Where(t => t.GroupLinkId == (parentId > -1 ? parentId : groupsDto?.GroupLinkId)).Select(t => t.FromModelToDto()!).ToList();

            return groupHierarchyDto;
        }


        /*В среднем нерекурсивный метод, использующий LINQ по-минимуму быстрее лишь на 0,118644444 миллисекунд (результат 18 вызовов).
        Максимальная вложенность подразделов = 6
        При максимальной вложенности = 12 за 20 вызовов нерекурсивный метод быстрее в среднем на 0,09116 миллисекунд (но тут рекурсивный метод в самом первом случае отработал быстрее аж на 1 миллисекунду
        и если брать только 19 вызовов, то нерекурсивный метод отработал быстрее в среднем на 0,1546 миллисекунд
        */
        public static GroupHierarchyDto FromModelToHierarchyDto(this List<Group> group, List<Target> target)
        {
            GroupHierarchyDto groupHierarchyDto = new GroupHierarchyDto();
            GroupHierarchyDto father = groupHierarchyDto;
            GroupHierarchyDto child=default!;

            groupHierarchyDto.Name = "";
            groupHierarchyDto.Description = "";
            groupHierarchyDto.Id = -1;

            groupHierarchyDto.GroupsInHierarchy = new List<GroupHierarchyDto>();


            Stack<Group> groupStack = new Stack<Group>();
            List<long> visitedGroup = new List<long>();   

            //текущая группа, с которой работаем
            Group currentGroup;
            for(int i=0;group[i].GroupLinkId==null ;i++)
            {
                groupStack.Push(group[i]);
            }
            while(groupStack.Count>0)
            {
                if ((groupStack.Peek().GroupLinkId != father.Id || father.Id==-1) && father.Father!=null)
                {
                    father = father?.Father!;
                    continue;
                }
                currentGroup =groupStack.Pop();

                if (visitedGroup.Contains(currentGroup.Id))
                    continue;
                
                visitedGroup.Add(currentGroup.Id);
                child = new();
                if(father.GroupsInHierarchy==null)
                    father.GroupsInHierarchy = new List<GroupHierarchyDto>();
                father.GroupsInHierarchy!.Add(child);


                //сразу заполним информацию по текущей группе(оно же раздел)
                child.Name = currentGroup.Name;
                child.Description = currentGroup.Description;
                child.Id = currentGroup.Id;
                child.Father = father;
                child.Targets = target.Where(t => t.GroupLinkId == child.Id).Select(t => t.FromModelToDto()!).ToList();
                
                //отбираем кучку групп которые являются дочерними к рабочей группе
                var groupsAtThisLevel=group.Where(g=>g.GroupLinkId == child.Id).ToList();
                if (groupsAtThisLevel.Count == 0)
                {
                    father = child.Father;
                    continue;
                }
                //загоним в стэк группы для дальнейшей работы с ними
                for(int i=0;i< groupsAtThisLevel?.Count;i++)
                {
                    groupStack.Push(groupsAtThisLevel[i]);
                }

                father = child;
                
            }

            return groupHierarchyDto;
        }
    }
}
