using System;
using System.Collections.Generic;
using System.Text;
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;

namespace TimetableBusinessLogic.BusinessLogics
{
    public class DenearyLogic
    {
        private readonly IDenearyStorage _denearyStorage;
        public DenearyLogic(IDenearyStorage denearyStorage)
        {
            _denearyStorage = denearyStorage;
        }
        public List<DenearyViewModel> Read(DenearyBindingModel model)
        {
            if (model == null)
            {
                return _denearyStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DenearyViewModel> { _denearyStorage.GetElement(model) };
            }
            return _denearyStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DenearyBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DenearyBindingModel
            {
                Email = model.Email,
                Login = model.Login,
                Name = model.Name,
                Password = model.Password
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деканат с такими данными");
            }
            if (model.Id.HasValue)
            {
                _denearyStorage.Update(model);
            }
            else
            {
                _denearyStorage.Insert(model);
            }
        }
        public void Delete(DenearyBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DenearyBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _denearyStorage.Delete(model);
        }
    }
}
