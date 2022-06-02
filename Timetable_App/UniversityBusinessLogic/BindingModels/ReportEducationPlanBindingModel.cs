using System;
using System.Collections.Generic;
using System.Text;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.BindingModels
{
    public class ReportEducationPlanBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<TimetableViewModel> EducationPlans { get; set; }
    }
}
