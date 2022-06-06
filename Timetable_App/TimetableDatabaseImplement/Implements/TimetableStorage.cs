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
    public class TimetableStorage : ITimetableStorage
    {
        public List<TimetableViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Timetables
                .Include(rec => rec.Classroom)
                .Include(rec => rec.Group)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Subject)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Lector)
                .ToList()
                .Select(rec => new TimetableViewModel //?
                {
                    Id = rec.Id,
                    LectorSubjectId = rec.LectorSubjectId,
                    ClassroomId = rec.ClassroomId,
                    GroupId = rec.GroupId,
                    LectorSubject_LectorId = rec.LectorSubject.LectorId,
                    LectorSubject_SubjectId = rec.LectorSubject.SubjectId,
                    LectorName = rec.LectorSubject.Lector.Name //?
                })
                .ToList();
            }
        }

        public List<TimetableViewModel> GetFilteredList(TimetableBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Timetables
                .Include(rec => rec.Classroom)
                .Include(rec => rec.Group)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Subject)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Lector)
                .ToList()
                .Select(rec => new TimetableViewModel //?
                {
                    Id = rec.Id,
                    LectorSubjectId = rec.LectorSubjectId,
                    ClassroomId = rec.ClassroomId,
                    GroupId = rec.GroupId,
                    LectorSubject_LectorId = rec.LectorSubject.LectorId,
                    LectorSubject_SubjectId = rec.LectorSubject.SubjectId,
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

            using (var context = new TimetableDatabase())
            {
                Timetable Timetable = context.Timetables
                .Include(rec => rec.Classroom)
                .Include(rec => rec.Group)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Subject)
                .Include(rec => rec.LectorSubject)
                .ThenInclude(rec => rec.Lector)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return Timetable != null ? new TimetableViewModel
                {
                    Id = Timetable.Id,
                    LectorSubjectId = Timetable.LectorSubjectId,
                    ClassroomId = Timetable.ClassroomId,
                    GroupId = Timetable.GroupId,
                    LectorSubject_LectorId = Timetable.LectorSubject.LectorId,
                    LectorSubject_SubjectId = Timetable.LectorSubject.SubjectId,
                } : null;
            }
        }

        public void Insert(TimetableBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Timetable(), context);
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
            using (var context = new TimetableDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Timetable element = context.Timetables.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element, context);
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
            using (var context = new TimetableDatabase())
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

        private Timetable CreateModel(TimetableBindingModel model, Timetable Timetable, TimetableDatabase context)
        {
            Timetable.GroupId= (int)model.GroupId;
            Timetable.ClassroomId = (int)model.ClassroomId;
            Timetable.LectorSubjectId = (int)model.LectorSubjectId;


            return Timetable;
        }
    }
}
