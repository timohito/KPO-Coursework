using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.Interfaces;
using TimetableBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace TimetableBusinessLogic.BusinessLogics
{
    public class GroupLogic
    {
        private readonly IGroupStorage _groupStorage;
        private readonly ILectorStorage _lectorStorage;

        public GroupLogic(IGroupStorage studentStorage, ILectorStorage lectorStorage)
        {
            _groupStorage = studentStorage;
            _lectorStorage = lectorStorage;
        }

        public List<GroupViewModel> Read(GroupBindingModel model)
        {
            if (model == null)
            {
                return _groupStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GroupViewModel> { _groupStorage.GetElement(model) };
            }
            return _groupStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel { 
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такая группа");
            }
            if (model.Id.HasValue)
            {
                _groupStorage.Update(model);
            }
            else
            {
                _groupStorage.Insert(model);
            }
        }

        public void Delete(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _groupStorage.Delete(model);
        }

        //public List<GroupViewModel> SelectByLector(LectorBindingModel model)
        //{
        //    var lector = _lectorStorage.GetElement(model);
        //    if (lector == null)
        //    {
        //        throw new Exception("Преподаватель не найден");
        //    }
        //    return _groupStorage.GetBySubjectId(lector.SubjectId);
        //}
    }
}
