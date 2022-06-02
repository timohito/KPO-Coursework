using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimetableDatabaseImplement.Models
{
    public class Timetable
    {
		public int Id { get; set; }
		[Required]
		public int Classroom { get; set; }
		public int LectorId { get; set; }
		public virtual Lector Lector { get; set; }
		public int SubjectId { get; set; }
		public virtual Subject Subject { get; set; }
		public int GroupId { get; set; }
		public virtual Group Group { get; set; }
	}
}
