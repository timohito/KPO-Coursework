﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var context = new UniversityDatabase())
            {
                return context.Lectors
                .Select(rec => new LectorViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    SubjectName = context.Subjects.FirstOrDefault(recSubject => rec.SubjectId == recSubject.Id).Name,
                    SubjectId = rec.SubjectId,
                    DenearyId = rec.DenearyId
                }).ToList();
            }
        }
        public List<LectorViewModel> GetFilteredList(LectorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Lectors
                .Include(rec => rec.Deneary)
                .Include(rec => rec.Subject)
                .Where(rec => rec.SubjectId == model.SubjectId)
                .Select(rec => new LectorViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    SubjectName = context.Subjects.FirstOrDefault(recSubject => rec.SubjectId == recSubject.Id).Name,
                    SubjectId = rec.SubjectId,
                    DenearyId = rec.DenearyId
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
            using (var context = new UniversityDatabase())
            {
                var lector = context.Lectors
                .Include(rec => rec.Deneary)
                .Include(rec => rec.Subject)
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return lector != null ?
                new LectorViewModel
                {
                    Id = lector.Id,
                    Name = lector.Name,
                    SubjectName = context.Subjects.FirstOrDefault(recSubject => lector.SubjectId == recSubject.Id).Name,
                    SubjectId = lector.SubjectId,
                    DenearyId = lector.DenearyId
                } :
                null;
            }
        }
        public void Insert(LectorBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Lectors.Add(CreateModel(model, new Lector()));
                context.SaveChanges();
            }
        }
        public void Update(LectorBindingModel model)
        {
            using (var context = new UniversityDatabase())
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
            using (var context = new UniversityDatabase())
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
        private Lector CreateModel(LectorBindingModel model, Lector lector)
        {
            lector.Name = model.Name;
            lector.SubjectId = model.SubjectId;
            lector.DenearyId = model.DenearyId;
            return lector;
        }
    }
}
