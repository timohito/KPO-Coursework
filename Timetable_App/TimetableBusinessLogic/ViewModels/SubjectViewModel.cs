using System;
using System.ComponentModel;

namespace TimetableBusinessLogic.ViewModels
{
	public class SubjectViewModel
	{
		public int? Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Количество часов")]
		public int Hours { get; set; }
		public Dictionary<int, string> LectorSubject { get; set; }
	}
}
