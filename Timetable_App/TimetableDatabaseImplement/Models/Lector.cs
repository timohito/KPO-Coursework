using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDatabaseImplement.Models
{
	public class Lector
	{
        [Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[ForeignKey("LectorId")]
		public virtual List<LectorSubject> LectorSubjects { get; set; }
	}
}
