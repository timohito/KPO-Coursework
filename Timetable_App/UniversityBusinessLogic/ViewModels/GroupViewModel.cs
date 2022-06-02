using System.Collections.Generic;
using System.ComponentModel;

namespace TimetableBusinessLogic.ViewModels
{
	public class GroupViewModel
	{
		public int? Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		public int DenearyId { get; set; }
	}
}
