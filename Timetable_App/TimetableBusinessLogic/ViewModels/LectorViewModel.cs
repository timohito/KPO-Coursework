using System.ComponentModel;

namespace TimetableBusinessLogic.ViewModels
{
	public class LectorViewModel
	{
		public int Id { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }
		[DisplayName("Предмет")]
		public string SubjectName { get; set; }
		public int? SubjectId { get; set; }
		public int? DenearyId { get; set; }
	}
}
