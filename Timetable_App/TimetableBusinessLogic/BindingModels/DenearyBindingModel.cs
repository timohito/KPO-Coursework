using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableBusinessLogic.BindingModels
{
    public class DenearyBindingModel
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
