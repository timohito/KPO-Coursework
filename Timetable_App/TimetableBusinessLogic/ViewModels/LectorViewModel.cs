using System.Collections.Generic;
using System.ComponentModel;

namespace TimetableBusinessLogic.ViewModels
{
	public class LectorViewModel
	{
		public int Id { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }

		public Dictionary<int, string> Subjects { get; set; }
	}
}
