using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.BusinessLogicsContracts
{
    public interface IEquipmenttLogic
    {
        List<EquipmenttViewModel> Read(EquipmenttBindingModel model);
        void CreateOrUpdate(EquipmenttBindingModel model);
        void Delete(EquipmenttBindingModel model);
    }
}
