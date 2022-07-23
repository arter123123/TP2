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
    public class KitLogic : IKitLogic
    {
        private readonly IKitStorage _kitStorage;
        public KitLogic(IKitStorage kitStorage)
        {
            _kitStorage = kitStorage;
        }
        public List<KitViewModel> Read(KitBindingModel model)
        {
            if (model == null)
            {
                return _kitStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<KitViewModel> { _kitStorage.GetElement(model)
};
            }
            return _kitStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(KitBindingModel model)
        {
            var element = _kitStorage.GetElement(new KitBindingModel
            {
                KitName = model.KitName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _kitStorage.Update(model);
            }
            else
            {
                _kitStorage.Insert(model);
            }
        }
        public void Delete(KitBindingModel model)
        {
            var element = _kitStorage.GetElement(new KitBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _kitStorage.Delete(model);
        }
    }
}

