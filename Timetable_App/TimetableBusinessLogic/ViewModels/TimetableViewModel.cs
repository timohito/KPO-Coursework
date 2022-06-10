using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimetableBusinessLogic.ViewModels
{
    public class TimetableViewModel
    {
        public int? Id { get; set; }
        [DisplayName("День")]
        public string Day { get; set; }
        [DisplayName("Пара")]
        public int Class { get; set; }
        [DisplayName("Группа")]
        public int GroupId { get; set; }
        [DisplayName("Группа")]
        public string GroupName { get; set; }
        [DisplayName("Аудитория")]
        public int ClassroomId { get; set; }
        [DisplayName("Аудитория")]
        public int ClassroomNumber { get; set; }
        public int LectorSubjectId { get; set; }
        [DisplayName("Преподаватель")]
        public int LectorSubject_LectorId { get; set; }
        [DisplayName("Преподаватель")]
        public string LectorName { get; set; }
        [DisplayName("Предмет")]
        public int LectorSubject_SubjectId { get; set; }
        [DisplayName("Предмет")]
        public string SubjectName { get; set; }
        public Dictionary<int, (string, int)> LectorSubjects { get; set; }
    }
}
