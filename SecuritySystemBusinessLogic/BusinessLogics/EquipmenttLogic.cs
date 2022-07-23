using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.BusinessLogicsContracts;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemBusinessLogic.BusinessLogics
{
    public class EquipmenttLogic : IEquipmenttLogic
    {
        private readonly IEquipmenttStorage _equipmenttStorage;
        public EquipmenttLogic(IEquipmenttStorage equipmenttStorage)
        {
            _equipmenttStorage = equipmenttStorage;
        }
        public List<EquipmenttViewModel> Read(EquipmenttBindingModel model)
        {
            if (model == null)
            {
                return _equipmenttStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<EquipmenttViewModel> { _equipmenttStorage.GetElement(model)
};
            }
            return _equipmenttStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(EquipmenttBindingModel model)
        {
            var element = _equipmenttStorage.GetElement(new EquipmenttBindingModel
            {
                EquipmenttName = model.EquipmenttName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _equipmenttStorage.Update(model);
            }
            else
            {
                _equipmenttStorage.Insert(model);
            }
        }
        public void Delete(EquipmenttBindingModel model)
        {
            var element = _equipmenttStorage.GetElement(new EquipmenttBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _equipmenttStorage.Delete(model);
        }
    }
}

