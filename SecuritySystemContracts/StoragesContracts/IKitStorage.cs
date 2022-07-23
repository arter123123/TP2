using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.StoragesContracts
{
    public interface IKitStorage
    {
        List<KitViewModel> GetFullList();
        List<KitViewModel> GetFilteredList(KitBindingModel model);
        KitViewModel GetElement(KitBindingModel model);
        void Insert(KitBindingModel model);
        void Update(KitBindingModel model);
        void Delete(KitBindingModel model);
    }
}