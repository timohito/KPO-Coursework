using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBusinessLogic.BindingModels
{
    public class TimetableBindingModel
    {
        public int? Id { get; set; }

        public int LectorSubjectId { get; set; }

        public int GroupId { get; set; }

        public int ClassroomId { get; set; }
    }
}
