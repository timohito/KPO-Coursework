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
    public class SubjectStorage :ISubjectStorage
    {
        public List<SubjectViewModel> GetFullList()
        {
            using (var context = new TimetableDatabase())
            {
                return context.Subjects
                  .Include(rec => rec.LectorSubjects)
                  .ThenInclude(rec => rec.Lector).ToList()
                  .Select(rec => new SubjectViewModel
                  {
                      Id = rec.Id,
                      Hours = rec.Hours,
                      Name = rec.Name
                  }).ToList();
            }
        }
        public List<SubjectViewModel> GetFilteredList(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                return context.Subjects
                  .Include(rec => rec.LectorSubjects)
                  .ThenInclude(rec => rec.Lector).ToList()
                  //.Where(rec => rec.DenearyLogin == model.DenearyLogin).ToList()
                  .Select(rec => new SubjectViewModel
                  {
                      Id = rec.Id,
                      Name = rec.Name,
                      Hours = rec.Hours,
                      LectorSubject = rec.LectorSubjects.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name),
                  })
                  .ToList();
            }
        }
        public SubjectViewModel GetElement(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TimetableDatabase())
            {
                var Subject = context.Subjects
                  .Include(rec => rec.LectorSubjects)
                  .ThenInclude(rec => rec.Lector)
                  .FirstOrDefault(rec => rec.Id == model.Id);
                return Subject != null ?
                  new SubjectViewModel
                  {
                      Id = Subject.Id,
                      Name = Subject.Name,
                      Hours = Subject.Hours,
                      LectorSubject = Subject.LectorSubjects.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name),
                  } :
                  null;
            }
        }
        public void Insert(SubjectBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                context.Subjects.Add(CreateModel(model, new Subject(), context));
                context.SaveChanges();
            }
        }
        public void Update(SubjectBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Subjects.FirstOrDefault(rec => rec.Id ==
                       model.Id);
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
        public void Delete(SubjectBindingModel model)
        {
            using (var context = new TimetableDatabase())
            {
                Subject element = context.Subjects.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Subjects.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Subject CreateModel(SubjectBindingModel model, Subject Subject, TimetableDatabase context)
        {
            Subject.Name = model.Name;
            Subject.Hours = model.Hours;

            return Subject;
        }
        public void BindingLector(int LId, int SId)
        {
            using (var context = new TimetableDatabase())
            {
                context.LectorSubjects.Add(new LectorSubject
                {
                    LectorId = LId,
                    SubjectId = SId,
                });
                context.SaveChanges();
            }
        }
    }
}
