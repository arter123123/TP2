using System;
using SecuritySystemListImplement.Models;
using System.Collections.Generic;

namespace SecuritySystemListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Equipmentt> Equipmentts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Kit> Kits { get; set; }
        private DataListSingleton()
        {
            Equipmentts = new List<Equipmentt>();
            Orders = new List<Order>();
            Kits = new List<Kit>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}

