using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBusinessLogic.BindingModels
{
    public class TimetableBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int SubjectId { get; set; }

        public int LectorId { get; set; }

        public int GroupId { get; set; }

        public int Classroom { get; set; }
    }
}
