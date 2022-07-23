using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemListImplement.Models;

namespace SecuritySystemListImplement.Implements
{
    public class KitStorage : IKitStorage
    {
        private readonly DataListSingleton source;
        public KitStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<KitViewModel> GetFullList()
        {
            List<KitViewModel> result = new List<KitViewModel>();
            foreach (var equipmentt in source.Kits)
            {
                result.Add(CreateModel(equipmentt));
            }
            return result;
        }
        public List<KitViewModel> GetFilteredList(KitBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<KitViewModel>();
            foreach (var kit in source.Kits)
            {
                if (kit.KitName.Contains(model.KitName))
                {
                    result.Add(CreateModel(kit));
                }
            }
            return result;

        }
        public KitViewModel GetElement(KitBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var kit in source.Kits)
            {
                if (kit.Id == model.Id || kit.KitName ==
                model.KitName)
                {
                    return CreateModel(kit);
                }
            }
            return null;
        }
        public void Insert(KitBindingModel model)
        {
            var tempKit = new Kit
            {
                Id = 1,
                KitEquipmentts = new Dictionary<int, int>()
            };
            foreach (var kit in source.Kits)
            {
                if (kit.Id >= tempKit.Id)
                {
                    tempKit.Id = kit.Id + 1;
                }
            }
            source.Kits.Add(CreateModel(model, tempKit));
        }
        public void Update(KitBindingModel model)
        {
            Kit tempKit = null;
            foreach (var kit in source.Kits)
            {
                if (kit.Id == model.Id)
                {
                    tempKit = kit;
                }
            }
            if (tempKit == null)
            {
                throw new Exception("Мебель не найдена");
            }
            CreateModel(model, tempKit);
        }
        public void Delete(KitBindingModel sample)
        {
            for (int i = 0; i < source.Kits.Count; ++i)
            {
                if (source.Kits[i].Id == sample.Id)
                {
                    source.Kits.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Мебель не найдена");
        }
        private Kit CreateModel(KitBindingModel model, Kit kit)
        {
            kit.KitName = model.KitName;
            kit.Price = model.Price;
            // удаляем убранные
            foreach (var key in kit.KitEquipmentts.Keys.ToList())
            {
                if (!model.KitEquipmentts.ContainsKey(key))
                {
                    kit.KitEquipmentts.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var equipmentt in model.KitEquipmentts)
            {
                if (kit.KitEquipmentts.ContainsKey(equipmentt.Key))
                {
                    kit.KitEquipmentts[equipmentt.Key] = model.KitEquipmentts[equipmentt.Key].Item2;
                }
                else
                {
                    kit.KitEquipmentts.Add(equipmentt.Key, model.KitEquipmentts[equipmentt.Key].Item2);
                }
            }
            return kit;
        }
        private KitViewModel CreateModel(Kit kit)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            var kitEquipmentts = new Dictionary<int, (string, int)>();
            foreach (var pc in kit.KitEquipmentts)
            {
                string equipmenttName = string.Empty;
                foreach (var equipmentt in source.Equipmentts)
                {
                    if (pc.Key == equipmentt.Id)
                    {
                        equipmenttName = equipmentt.EquipmenttName;
                        break;
                    }
                }
                kitEquipmentts.Add(pc.Key, (equipmenttName, pc.Value));
            }
            return new KitViewModel
            {
                Id = kit.Id,
                KitName = kit.KitName,
                Price = kit.Price,
                KitEquipmentts = kitEquipmentts
            };
        }
    }
}


