using System;
using System.Collections.Generic;
using System.Text;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.BusinessLogics
{
    public class TimetableLogic
    {
		private readonly ITimetableStorage _educationPlanStorage;
		public TimetableLogic(ITimetableStorage educationPlanStorage)
		{
			_educationPlanStorage = educationPlanStorage;
		}
		public List<TimetableViewModel> Read(TimetableBindingModel model)
		{
			if (model == null)
			{
				return _educationPlanStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<TimetableViewModel> { _educationPlanStorage.GetElement(model) };
			}
			return _educationPlanStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(TimetableBindingModel model)
		{						
			var element = _educationPlanStorage.GetElement(new TimetableBindingModel
			{
				Name = model.Name,
				Classroom = model.Classroom
			});
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть план с такими данными");
			}
			if (model.Id.HasValue)
			{
				_educationPlanStorage.Update(model);
			}
			else
			{
				_educationPlanStorage.Insert(model);
			}
		}
		public void Delete(TimetableBindingModel model)
		{
			var element = _educationPlanStorage.GetElement(new TimetableBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_educationPlanStorage.Delete(model);
		}
    }
}
