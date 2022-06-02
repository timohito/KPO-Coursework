using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;
using TimetableDatabaseImplement.Models;

namespace TimetableDatabaseImplement.Implements
{
    public class TimetableStorage : ITimetableStorage
    {
        public List<TimetableViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Timetables
                          .Include(rec => rec.Group)
                          .ThenInclude(rec => rec.Deneary)
                          .Include(rec => rec.Subject)
                          .Include(rec => rec.Lector).ToList()
                .Select(rec => new TimetableViewModel
                {
                    Id = rec.Id,
                    Classroom = rec.Classroom,
                    GroupId = rec.GroupId,
                    LectorId = rec.LectorId,
                    SubjectId = rec.SubjectId
                }).ToList();
            }
        }
        public List<TimetableViewModel> GetFilteredList(TimetableBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Timetables
                          .Include(rec => rec.Group)
                          .ThenInclude(rec => rec.Deneary)
                          .Include(rec => rec.Subject)
                          .Include(rec => rec.Lector).ToList()
                .Select(rec => new TimetableViewModel
                {
                    Id = rec.Id,
                    Classroom = rec.Classroom,
                    GroupId = rec.GroupId,
                    SubjectId = rec.SubjectId,
                    LectorId = rec.LectorId,                 
                })
                .ToList();
            }
        }
        public TimetableViewModel GetElement(TimetableBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var t = context.Timetables
                          .Include(rec => rec.Group)
                          .ThenInclude(rec => rec.Deneary)
                          .Include(rec => rec.Subject)
                          .Include(rec => rec.Lector).ToList()
                .FirstOrDefault(rec => rec.Id == model.Id);
                return t != null ?
                new TimetableViewModel
                {
                    Id = t.Id,
                    Classroom = t.Classroom,
                    GroupId = t.GroupId,
                    SubjectId = t.SubjectId,
                    LectorId = t.LectorId,
                } :
                null;
            }
        }
        public void Insert(TimetableBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Timetable(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(TimetableBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Timetables.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(TimetableBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Timetable element = context.Timetables.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Timetables.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Timetable CreateModel(TimetableBindingModel model, Timetable t, UniversityDatabase context)
        {
            t.Classroom = model.Classroom;
            t.GroupId = model.GroupId;
            t.SubjectId = model.SubjectId;
            t.LectorId = model.LectorId;

            if (t.Id == 0)
            {
                context.Timetables.Add(t);
                context.SaveChanges();
            }

           
            return t;
        }
    }
}
