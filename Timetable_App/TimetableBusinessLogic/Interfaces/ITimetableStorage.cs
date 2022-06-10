using System;
using System.Collections.Generic;
using System.Text;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.Interfaces
{
    public interface ITimetableStorage
    {
		List<TimetableViewModel> GetFullList();
		List<TimetableViewModel> GetFilteredList(TimetableBindingModel model);
		TimetableViewModel GetElement(TimetableBindingModel model);
		void Insert(TimetableBindingModel model);
		void Update(TimetableBindingModel model);
		void Delete(TimetableBindingModel model);
		int FindLectorSubjectIdByForeignKeys(int LId, int SId);

	}
}
