using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace SecuritySystemContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class EquipmenttViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название экипировки")]
        public string EquipmenttName { get; set; }
    }
}
