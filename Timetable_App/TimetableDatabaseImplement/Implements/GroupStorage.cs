using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;
using TimetableDatabaseImplement.Models;

namespace TimetableDatabaseImplement.Implements
{
    public class GroupStorage : IGroupStorage
    {
        public List<GroupViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Groups
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DenearyId = rec.DenearyId
                }).ToList();
            }
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Groups
                .Include(rec => rec.Deneary)
                .Where(rec => rec.DenearyId == model.DenearyId)
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DenearyId = rec.DenearyId
                })
                .ToList();
            }
        }
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                var Group = context.Groups
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
                return Group != null ?
                new GroupViewModel
                {
                    Id = Group.Id,
                    Name = Group.Name,
                    DenearyId = Group.DenearyId                   
                } :
                null;
            }
        }
        public void Insert(GroupBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                context.Groups.Add(CreateModel(model, new Group()));
                context.SaveChanges();
            }
        }
        public void Update(GroupBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                var element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(GroupBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                Group element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Groups.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Group CreateModel(GroupBindingModel model, Group Group)
        {
            Group.Name = model.Name;
            Group.DenearyId = model.DenearyId;
            return Group;
        }
    }
}
