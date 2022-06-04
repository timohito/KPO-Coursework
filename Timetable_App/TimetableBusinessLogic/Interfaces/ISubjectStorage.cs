using System.Collections.Generic;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.Interfaces
{
	public interface ISubjectStorage
	{
		List<SubjectViewModel> GetFullList();
		List<SubjectViewModel> GetFilteredList(SubjectBindingModel model);
		SubjectViewModel GetElement(SubjectBindingModel model);
		void Insert(SubjectBindingModel model);
		void Update(SubjectBindingModel model);
		void Delete(SubjectBindingModel model);
		void BindingLector(int LId, int SId);
	}
}
