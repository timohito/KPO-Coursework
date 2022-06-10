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
		private readonly ITimetableStorage _timetableStorage;
		public TimetableLogic(ITimetableStorage timetableStorage)
		{
			_timetableStorage = timetableStorage;
		}
		public List<TimetableViewModel> Read(TimetableBindingModel model)
		{
			if (model == null)
			{
				return _timetableStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<TimetableViewModel> { _timetableStorage.GetElement(model) };
			}
			return _timetableStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(TimetableBindingModel model)
		{						
			var element = _timetableStorage.GetElement(new TimetableBindingModel
			{
				GroupId = model.GroupId,
				ClassroomId = model.ClassroomId,
				LectorSubjectId = model.LectorSubjectId,
				LectorSubject_LectorId = model.LectorSubject_LectorId,
				LectorSubject_SubjectId = model.LectorSubject_LectorId,
				Day = model.Day,
				Class = model.Class,
			});
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть запись с такими данными");
			}
			if (model.Id.HasValue)
			{
				_timetableStorage.Update(model);
			}
			else
			{
				_timetableStorage.Insert(model);
			}
		}
		public void Delete(TimetableBindingModel model)
		{
			var element = _timetableStorage.GetElement(new TimetableBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_timetableStorage.Delete(model);
		}

        public int FindLectorSubjectIdByForeignKeys(int LId, int SId)
        {
			return _timetableStorage.FindLectorSubjectIdByForeignKeys(LId, SId);
		}

	}
}
