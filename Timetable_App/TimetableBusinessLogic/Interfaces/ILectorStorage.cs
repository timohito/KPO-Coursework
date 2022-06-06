using System.Collections.Generic;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.Interfaces
{
	public interface ILectorStorage
	{
		List<LectorViewModel> GetFullList();
		List<LectorViewModel> GetFilteredList(LectorBindingModel model);
		LectorViewModel GetElement(LectorBindingModel model);
		void Insert(LectorBindingModel model);
		void Update(LectorBindingModel model);
		void Delete(LectorBindingModel model);
		void BindingSubject(int LId, int SId);
	}
}
