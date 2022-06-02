using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableDatabaseImplement.Models
{
	public class Lector
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int SubjectId { get; set; }
		public int DenearyId { get; set; }
		public virtual Deneary Deneary { get; set; }
		public virtual Subject Subject { get; set; }

		[ForeignKey("LectorId")]
		public List<Timetable> Timetables { get; set; }

	}
}
