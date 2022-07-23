using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.BusinessLogicsContracts
{
    public interface IKitLogic
    {
        List<KitViewModel> Read(KitBindingModel model);
        void CreateOrUpdate(KitBindingModel model);
        void Delete(KitBindingModel model);
    }
}
