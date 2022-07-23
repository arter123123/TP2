using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace SecuritySystemContracts.ViewModels
{
    /// <summary>
    /// Набор, изготавливаемый в магазине
    /// </summary>
    public class KitViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название набора")]
        public string KitName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> KitEquipmentts { get; set; }
    }
}
