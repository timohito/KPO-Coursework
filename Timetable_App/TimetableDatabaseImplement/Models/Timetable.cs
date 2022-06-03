using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDatabaseImplement.Models
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }
        public int LectorSubjectId { get; set; }
        public virtual LectorSubject LectorSubject { get; set; }
    }
}
