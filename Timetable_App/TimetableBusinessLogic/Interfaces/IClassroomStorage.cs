using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.Interfaces
{
    public interface IClassroomStorage
    {
		public interface IClassroomStorage
		{
			List<ClassroomViewModel> GetFullList();
			List<ClassroomViewModel> GetFilteredList(ClassroomBindingModel model);
			ClassroomViewModel GetElement(ClassroomBindingModel model);
			void Insert(ClassroomBindingModel model);
			void Update(ClassroomBindingModel model);
			void Delete(ClassroomBindingModel model);
		}
	}
}
