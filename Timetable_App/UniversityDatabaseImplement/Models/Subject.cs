using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableDatabaseImplement.Models
{
	public class Subject
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Hours { get; set; }

		public int DenearyId { get; set; }

		public virtual Deneary Deneary { get; set; }
		[ForeignKey("SubjectId")]
		public List<Timetable> Timetables { get; set; }
	}
}
