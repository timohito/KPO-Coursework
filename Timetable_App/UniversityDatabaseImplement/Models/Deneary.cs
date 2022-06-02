using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimetableDatabaseImplement.Models
{
    public class Deneary
    {
		[Key]
		public int Id { get; set; }
		[Required]
		public string Login { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
        public string Email { get; internal set; }

        [ForeignKey("DenearyId")]
        public List<Group> Groups { get; set; }

        [ForeignKey("DenearyId")]
        public List<Lector> Lectors { get; set; }

        [ForeignKey("DenearyId")]
        public List<Subject> Subjects { get; set; }
    }
}
