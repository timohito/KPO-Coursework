using System;
using System.Collections.Generic;
using System.Text;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.HelperModels
{
    public class WorkerPdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportEducationPlansViewModel> EducationPlansStudentsSubjects { get; set; }
    }
}
