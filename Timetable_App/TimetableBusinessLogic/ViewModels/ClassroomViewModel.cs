using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableBusinessLogic.ViewModels
{
    public class ClassroomViewModel
    {
        public int Id { get; set; }
        [DisplayName("Номер")]
        public int Number { get; set; }
    }
}
