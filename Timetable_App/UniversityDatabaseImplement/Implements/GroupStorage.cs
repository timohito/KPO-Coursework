using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var context = new UniversityDatabase())
            {
                return context.Groups
                  .Include(rec => rec.Deneary)
                  .Select(rec => new GroupViewModel
                  {
                      Id = rec.Id,
                      Name = rec.Name,
                      DenearyId = context.Denearies.FirstOrDefault(x => x.Id == rec.DenearyId).Id

                  }).ToList();
            }
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Groups
                  .Include(rec => rec.Deneary)
                  .Where(rec => rec.DenearyId == model.DenearyId).ToList()
                  .Select(rec => new GroupViewModel
                  {
                      Id = rec.Id,
                      Name = rec.Name,
                      DenearyId = context.Denearies.FirstOrDefault(x => x.Id == model.DenearyId).Id,               
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
            using (var context = new UniversityDatabase())
            {
                var student = context.Groups
                  .Include(rec => rec.Deneary)
                  .FirstOrDefault(rec => rec.Id == model.Id);
                return student != null ?
                  new GroupViewModel
                  {
                      Id = student.Id,
                      Name = student.Name,
                      DenearyId = context.Denearies.FirstOrDefault(x => x.Id == student.Id).Id,                   
                  } :
                  null;
            }
        }
        public void Insert(GroupBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Groups.Add(CreateModel(model, new Group(), context));
                context.SaveChanges();
            }
        }
        public void Update(GroupBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Groups.FirstOrDefault(rec => rec.Id ==
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
        public void Delete(GroupBindingModel model)
        {
            using (var context = new UniversityDatabase())
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
        private Group CreateModel(GroupBindingModel model, Group group, UniversityDatabase context)
        {
            group.Id = (int)model.Id;
            group.Name = model.Name;
            group.DenearyId = model.DenearyId;

            return group;
        }

        //public void BindingSubject(string gradebookNumber, int subjectId)
        //{
        //    using (var context = new UniversityDatabase())
        //    {
        //        context.StudentSubjects.Add(new StudentSubject
        //        {
        //            StudentGradebookNumber = gradebookNumber,
        //            SubjectId = subjectId,
        //        });
        //        context.SaveChanges();
        //    }
        //}

       
        //public List<GroupViewModel> GetBySubjectId(int subjectId)
        //{
        //    using (var context = new UniversityDatabase())
        //    {
        //        return context.Groups
        //          .Include(rec => rec.StudentSubjects)
        //          .ThenInclude(rec => rec.Subject)
        //          .Include(rec => rec.EducationPlanStudents)
        //          .ThenInclude(rec => rec.EducationPlan)
        //          .ToList()
        //          .Where(rec => rec.StudentSubjects.FirstOrDefault(ss => ss.SubjectId == subjectId) != null)
        //          .Select(rec => new GroupViewModel
        //          {
        //              GradebookNumber = rec.GradebookNumber,
        //              Name = rec.Name,
        //              Subjects = rec.StudentSubjects
        //              .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
        //              EducationPlans = rec.EducationPlanStudents
        //              .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.Name)
        //          })
        //          .ToList();
        //    }
        //}
    }
}