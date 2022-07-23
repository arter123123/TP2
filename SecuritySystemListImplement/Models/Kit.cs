using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemListImplement.Models
{
    /// <summary>
    /// Набор, изготавливаемый в магазине
    /// </summary>
    public class Kit
    {
        public int Id { get; set; }
        public string KitName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> KitEquipmentts { get; set; }
    }
}
