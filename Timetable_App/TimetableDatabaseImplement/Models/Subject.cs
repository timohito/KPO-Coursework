using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDatabaseImplement.Models
{
	public class Subject
	{
        [Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Hours { get; set; }

		[ForeignKey("SubjectId")]
		public virtual List<LectorSubject> LectorSubjects { get; set; }

	}
}
