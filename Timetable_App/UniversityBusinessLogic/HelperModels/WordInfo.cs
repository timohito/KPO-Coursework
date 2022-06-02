using System.Collections.Generic;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string LectorName { get; set; }
        public List<GroupViewModel> Students { get; set; }
    }
}
