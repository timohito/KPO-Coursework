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
    public class ClassroomStorage : IClassroomStorage
    {
        public List<ClassroomViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Classrooms
                .Select(rec => new ClassroomViewModel
                {
                    Id = rec.Id,
                    Number = rec.Number
                }).ToList();
            }
        }
        public List<ClassroomViewModel> GetFilteredList(ClassroomBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Classrooms
                .Where(rec => rec.Id == model.Id)
                .Select(rec => new ClassroomViewModel
                {
                    Id = rec.Id,
                    Number = rec.Number
                })
                .ToList();
            }
        }
        public ClassroomViewModel GetElement(ClassroomBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                var Classroom = context.Classrooms
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Number == model.Number);
                return Classroom != null ?
                new ClassroomViewModel
                {
                    Id = Classroom.Id,
                    Number = Classroom.Number
                } :
                null;
            }
        }
        public void Insert(ClassroomBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                context.Classrooms.Add(CreateModel(model, new Classroom()));
                context.SaveChanges();
            }
        }
        public void Update(ClassroomBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                var element = context.Classrooms.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(ClassroomBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                Classroom element = context.Classrooms.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Classrooms.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Classroom CreateModel(ClassroomBindingModel model, Classroom Classroom)
        {
            Classroom.Number = model.Number;

            return Classroom;
        }
    }
}

