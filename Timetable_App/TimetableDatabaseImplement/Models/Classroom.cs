using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDatabaseImplement.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [ForeignKey("ClassroomId")]
        public virtual List<Timetable> Timetables { get; set; }
    }
}
