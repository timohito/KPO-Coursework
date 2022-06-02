using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimetableBusinessLogic.ViewModels
{
    public class TimetableViewModel
    {
        public int Id { get; set; }

        [DisplayName("Аудитория")]
        public int Classroom { get; set; }
        public int LectorId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }

    }
}
