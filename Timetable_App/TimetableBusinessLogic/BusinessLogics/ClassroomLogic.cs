using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.BusinessLogics
{
    public class ClassroomLogic
    {
        private readonly IClassroomStorage _ClassroomStorage;
        public ClassroomLogic(IClassroomStorage ClassroomStorage)
        {
            _ClassroomStorage = ClassroomStorage;
        }
        public List<ClassroomViewModel> Read(ClassroomBindingModel model)
        {
            if (model == null)
            {
                return _ClassroomStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ClassroomViewModel> { _ClassroomStorage.GetElement(model) };
            }
            return _ClassroomStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ClassroomBindingModel model)
        {
            var element = _ClassroomStorage.GetElement(new ClassroomBindingModel
            {
                Number = model.Number,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деканат с такими данными");
            }
            if (model.Id.HasValue)
            {
                _ClassroomStorage.Update(model);
            }
            else
            {
                _ClassroomStorage.Insert(model);
            }
        }
        public void Delete(ClassroomBindingModel model)
        {
            var element = _ClassroomStorage.GetElement(new ClassroomBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _ClassroomStorage.Delete(model);
        }
    }
}
