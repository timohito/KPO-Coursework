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
                    Day = rec.Day,
                    Class = rec.Class,
                    LectorSubject_LectorId = rec.LectorSubject.LectorId,
                    LectorSubject_SubjectId = rec.LectorSubject.SubjectId,
                    LectorName = rec.LectorSubject.Lector.Name, //?
                    SubjectName = rec.LectorSubject.Subject.Name,
                    ClassroomNumber = rec.Classroom.Number,
                    GroupName = rec.Group.Name,
                    //LectorSubjects = rec.LectorSubjects.ToDictionary(recRC => recRC.CosmeticId, recRC => (recRC.Cosmetic?.CosmeticName, recRC.Count)),
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
                .ThenInclude(rec => rec.Lector).Where(rec => rec.GroupId == model.GroupId)
                .ToList()
                .Select(rec => new TimetableViewModel //?
                {
                    Id = rec.Id,
                    LectorSubjectId = rec.LectorSubjectId,
                    ClassroomId = rec.ClassroomId,
                    GroupId = rec.GroupId,
                    Day = rec.Day,
                    Class = rec.Class,
                    LectorSubject_LectorId = rec.LectorSubject.LectorId,
                    LectorSubject_SubjectId = rec.LectorSubject.SubjectId,
                    LectorName = rec.LectorSubject.Lector.Name, //?
                    SubjectName = rec.LectorSubject.Subject.Name,
                    ClassroomNumber = rec.Classroom.Number,
                    GroupName = rec.Group.Name,
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
                    Day = Timetable.Day,
                    Class = Timetable.Class,
                    LectorSubject_LectorId = Timetable.LectorSubject.LectorId,
                    LectorSubject_SubjectId = Timetable.LectorSubject.SubjectId,
                    LectorName = Timetable.LectorSubject.Lector.Name, //?
                    SubjectName = Timetable.LectorSubject.Subject.Name,
                    ClassroomNumber = Timetable.Classroom.Number,
                    GroupName = Timetable.Group.Name,
                } : null;
            }
        }

        public void Insert(TimetableBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                //using (var transaction = context.Database.BeginTransaction())
                //{
                //    try
                //    {
                //        context.Timetables.Add(CreateModel(model, new Timetable(), context));
                //        transaction.Commit();
                //    }
                //    catch
                //    {
                //        transaction.Rollback();
                //        throw;
                //    }
                //}
                context.Timetables.Add(CreateModel(model, new Timetable(), context));
                context.SaveChanges();
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
            var foundRecsByClassroomClassDay = context.Timetables.Where(rec => rec.ClassroomId == model.ClassroomId &&
            rec.Class == model.Class && rec.Day == model.Day).ToList();
            var foundRecsByLectorClassDay = context.Timetables.Where(rec => rec.LectorSubject_LectorId == model.LectorSubject_LectorId &&
            rec.Class == model.Class && rec.Day == model.Day).ToList();
            var foundRecsByGroupClassDay = context.Timetables.Where(rec => rec.GroupId == model.GroupId &&
            rec.Class == model.Class && rec.Day == model.Day).ToList();

            if (foundRecsByClassroomClassDay.Count > 0)
            {
                throw new Exception("В аудитории уже проводится пара в данное время");
            }

            if (foundRecsByLectorClassDay.Count > 0)
            {
                throw new Exception("Преподаватель уже ведет пару у какой-то из групп в данное время");
            }

            if (foundRecsByGroupClassDay.Count > 0)
            {
                throw new Exception("У группы уже есть пара в данное время");
            }

            Timetable.GroupId = (int)model.GroupId;
            Timetable.ClassroomId = (int)model.ClassroomId;
            Timetable.LectorSubjectId = (int)model.LectorSubjectId;
            Timetable.LectorSubject_LectorId = (int)model.LectorSubject_LectorId;
            Timetable.LectorSubject_SubjectId = (int)model.LectorSubject_SubjectId;
            Timetable.Day = model.Day;
            Timetable.Class = (int)model.Class;


            return Timetable;
        }

        public int FindLectorSubjectIdByForeignKeys(int LId, int SId)
        {
            using (var context = new TimetableDatabase())
            {
                var result = context.LectorSubjects.FirstOrDefault(rec => rec.LectorId == LId && rec.SubjectId == SId);
                return result != null ? result.Id : throw new Exception("Преподаватель не ведет данную дисциплину");
            }
        }
    }
}
