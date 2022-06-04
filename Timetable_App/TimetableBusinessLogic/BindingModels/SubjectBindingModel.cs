using System;
using System.Collections.Generic;

namespace TimetableBusinessLogic.BindingModels
{
	public class SubjectBindingModel
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public int Hours{ get; set; }
		public Dictionary<int, string> LectorSubject { get; set; }
	}
}
