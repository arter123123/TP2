using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.StoragesContracts
{
    public interface IEquipmenttStorage
    {
        List<EquipmenttViewModel> GetFullList();
        List<EquipmenttViewModel> GetFilteredList(EquipmenttBindingModel model);
        EquipmenttViewModel GetElement(EquipmenttBindingModel model);
        void Insert(EquipmenttBindingModel model);
        void Update(EquipmenttBindingModel model);
        void Delete(EquipmenttBindingModel model);
    }
}
