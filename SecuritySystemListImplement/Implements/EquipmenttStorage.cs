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
    public class EquipmenttStorage : IEquipmenttStorage
    {
        private readonly DataListSingleton source;
        public EquipmenttStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<EquipmenttViewModel> GetFullList()
        {
            var result = new List<EquipmenttViewModel>();
            foreach (var equipmentt in source.Equipmentts)
            {
                result.Add(CreateModel(equipmentt));
            }
            return result;
        }
        public List<EquipmenttViewModel> GetFilteredList(EquipmenttBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<EquipmenttViewModel>();
            foreach (var equipmentt in source.Equipmentts)
            {
                if (equipmentt.EquipmenttName.Contains(model.EquipmenttName))
                {
                    result.Add(CreateModel(equipmentt));
                }
            }
            return result;
        }
        public EquipmenttViewModel GetElement(EquipmenttBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var equipmentt in source.Equipmentts)
            {
                if (equipmentt.Id == model.Id || equipmentt.EquipmenttName ==
               model.EquipmenttName)
                {
                    return CreateModel(equipmentt);
                }
            }
            return null;
        }
        public void Insert(EquipmenttBindingModel model)
        {
            var tempEquipmentt = new Equipmentt { Id = 1 };
            foreach (var equipmentt in source.Equipmentts)
            {
                if (equipmentt.Id >= tempEquipmentt.Id)
                {
                    tempEquipmentt.Id = equipmentt.Id + 1;
                }
            }
            source.Equipmentts.Add(CreateModel(model, tempEquipmentt));
        }
        public void Update(EquipmenttBindingModel model)
        {
            Equipmentt tempEquipmentt = null;
            foreach (var equipmentt in source.Equipmentts)
            {
                if (equipmentt.Id == model.Id)
                {
                    tempEquipmentt = equipmentt;
                }
            }
            if (tempEquipmentt == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempEquipmentt);
        }
        public void Delete(EquipmenttBindingModel model)
        {
            for (int i = 0; i < source.Equipmentts.Count; ++i)
            {
                if (source.Equipmentts[i].Id == model.Id.Value)
                {
                    source.Equipmentts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Equipmentt CreateModel(EquipmenttBindingModel model, Equipmentt
       equipmentt)
        {
            equipmentt.EquipmenttName = model.EquipmenttName;
            return equipmentt;
        }
        private static EquipmenttViewModel CreateModel(Equipmentt equipmentt)
        {
            return new EquipmenttViewModel
            {
                Id = equipmentt.Id,
                EquipmenttName = equipmentt.EquipmenttName
            };
        }
    }
}
