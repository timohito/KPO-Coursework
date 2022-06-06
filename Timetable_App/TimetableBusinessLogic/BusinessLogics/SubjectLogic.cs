﻿using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
namespace TimetableBusinessLogic.BusinessLogics
{
    public class SubjectLogic
    {
        private readonly ISubjectStorage _subjectStorage;
        public SubjectLogic(ISubjectStorage subjectStorage)
        {
            _subjectStorage = subjectStorage;
        }
        public List<SubjectViewModel> Read(SubjectBindingModel model)
        {
            if (model == null)
            {
                return _subjectStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SubjectViewModel> { _subjectStorage.GetElement(model) };
            }
            return _subjectStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SubjectBindingModel model)
        {
            var element = _subjectStorage.GetElement(new SubjectBindingModel
            {
                Name = model.Name,
                Hours = model.Hours,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такой предмет");
            }
            if (model.Id.HasValue)
            {
                _subjectStorage.Update(model);
            }
            else
            {
                _subjectStorage.Insert(model);
            }
        }
        public void Delete(SubjectBindingModel model)
        {
            var element = _subjectStorage.GetElement(new SubjectBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Предмет не найден");
            }
            _subjectStorage.Delete(model);
        }
        public void BindingLector(int LId, int SId)
        {
            _subjectStorage.BindingLector(LId, SId);
        }
    }
}
