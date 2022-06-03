//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TimetableBusinessLogic.BindingModels;
//using TimetableBusinessLogic.HelperModels;
//using TimetableBusinessLogic.Interfaces;
//using TimetableBusinessLogic.ViewModels;

//namespace TimetableBusinessLogic.BusinessLogics
//{
//    public class ReportWorkerLogic
//    {
//        private readonly ISubjectStorage _subjectStorage;
//        private readonly ITimetableStorage _educationPlanStorage;
//        private readonly IGroupStorage _studentStorage;
//        private readonly IReportStorage _reportStorage;

//        public ReportWorkerLogic(ISubjectStorage subjectStorage, ITimetableStorage educationPlanStorage, IGroupStorage studentStorage, IReportStorage reportStorage)
//        {
//            _subjectStorage = subjectStorage;
//            _educationPlanStorage = educationPlanStorage;
//            _studentStorage = studentStorage;
//            _reportStorage = reportStorage;
//        }

//        public List<ReportEducationPlanSubjectsViewModel> GetEducationPlanSubjects(List<TimetableViewModel> selectedEPs)
//        {
//            var list = new List<ReportEducationPlanSubjectsViewModel>();

//            foreach (var ep in selectedEPs)
//            {
//                list.Add(_reportStorage.GetEducationPlanSubjects(new TimetableBindingModel
//                {
//                    Id = ep.Id,
//                    Name = ep.Name
//                }));
//            }
//            return list;
//        }

//        public List<ReportEducationPlansViewModel> GetEducationPlanStudentsSubjects(ReportEducationPlanBindingModel model)
//        {
//            var list = _reportStorage.GetFullListEducationPlans(model);
//            return list;
//        }

//        public void SaveEducationPlanSubjectsToWord(ReportEducationPlanBindingModel model)
//        {
//            WorkerSaveToWord.CreateDoc(new WorkerWordExcelInfo
//            {
//                FileName = model.FileName,
//                Title = "Список дисциплин по выбранным планам",
//                EducationPlanSubjects = GetEducationPlanSubjects(model.EducationPlans)
//            });
//        }

//        public void SaveEducationPlanSubjectsToExcel(ReportEducationPlanBindingModel model)
//        {
//            WorkerSaveToExcel.CreateDoc(new WorkerWordExcelInfo
//            {
//                FileName = model.FileName,
//                Title = "Список дисциплин по выбранным планам",
//                EducationPlanSubjects = GetEducationPlanSubjects(model.EducationPlans)
//            });
//        }

//        public void SaveEPStudentsSubjectsToPdf(ReportEducationPlanBindingModel model)
//        {
//            WorkerSaveToPdf.CreateDoc(new WorkerPdfInfo
//            {
//                FileName = model.FileName,
//                Title = "Список студентов и дисциплин по выбранным планам",
//                DateFrom = model.DateFrom.Value,
//                DateTo = model.DateTo.Value,
//                EducationPlansStudentsSubjects = GetEducationPlanStudentsSubjects(model)
//            });
//        }

//    }
//}
