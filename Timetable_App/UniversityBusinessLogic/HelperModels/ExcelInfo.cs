using TimetableBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace TimetableBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string LectorName { get; set; }
        public List<GroupViewModel> Students { get; set; }
    }
}
