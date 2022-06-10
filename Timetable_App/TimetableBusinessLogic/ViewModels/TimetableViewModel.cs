using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimetableBusinessLogic.ViewModels
{
    public class TimetableViewModel
    {
        public int? Id { get; set; }
        public int Day { get; set; }
        public int Class { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ClassroomId { get; set; }
        public int ClassroomNumber { get; set; }
        public int LectorSubjectId { get; set; }
        public int LectorSubject_LectorId { get; set; }
        public string LectorName { get; set; }
        public int LectorSubject_SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Dictionary<int, (string, int)> LectorSubjects { get; set; }
    }
}
