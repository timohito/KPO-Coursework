using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableDatabaseImplement.Models
{
	public class Group
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int DenearyId { get; set; }
		public virtual Deneary Deneary { get; set; }
		[ForeignKey("GroupId")]
		public List<Timetable> Timetables { get; set; }
	}
}
