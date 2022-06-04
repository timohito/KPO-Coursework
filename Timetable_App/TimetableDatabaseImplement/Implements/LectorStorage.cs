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
    public class LectorStorage : ILectorStorage
    {
        public List<LectorViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Lectors
                .Select(rec => new LectorViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,                    
                }).ToList();
            }
        }
        public List<LectorViewModel> GetFilteredList(LectorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Lectors
                //.Where(rec => rec.DepartmentLogin == model.DepartmentLogin)
                .Select(rec => new LectorViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                })
                .ToList();
            }
        }
        public LectorViewModel GetElement(LectorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                var Lector = context.Lectors
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return Lector != null ?
                new LectorViewModel
                {
                    Id = Lector.Id,
                    Name = Lector.Name,                   
                } :
                null;
            }
        }
        public void Insert(LectorBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                context.Lectors.Add(CreateModel(model, new Lector()));
                context.SaveChanges();
            }
        }
        public void Update(LectorBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                var element = context.Lectors.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(LectorBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                Lector element = context.Lectors.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Lectors.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Lector CreateModel(LectorBindingModel model, Lector Lector)
        {
            Lector.Name = model.Name;
            return Lector;
        }
    }
}
