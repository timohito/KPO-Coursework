using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDatabaseImplement.Models
{
    public class LectorSubject
    {
        [Key]
        public int Id { get; set; }

        public int LectorId { get; set; }

        public int SubjectId { get; set; }

        public virtual Lector Lector { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey("LectorSubjectId")]
        public virtual List<Timetable> Timetables { get; set; }
    }
}
