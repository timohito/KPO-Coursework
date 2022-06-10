using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBusinessLogic.BindingModels
{
    public class TimetableBindingModel
    {
        public int? Id { get; set; }

        public int LectorSubjectId { get; set; }

        public int LectorSubject_LectorId { get; set; }

        public int LectorSubject_SubjectId { get; set; }

        public int Day { get; set; }

        public int Class { get; set; }

        public int GroupId { get; set; }

        public int ClassroomId { get; set; }
    }
}
