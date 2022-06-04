using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimetableBusinessLogic.ViewModels
{
    public class TimetableViewModel
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }
        public int ClassroomId { get; set; }
        public int LectorSubjectId { get; set; }
        public int LectorSubject_LectorId { get; set; }
        public int LectorSubject_SubjectId { get; set; }
        public Dictionary<int, (string, int)> LectorSubjects { get; set; }
    }
}
