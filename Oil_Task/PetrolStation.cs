using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil_Task
{
    public class PetrolStation
    {
        public readonly List<Gasoline> Gasolines = new List<Gasoline>();
        public int BoughtLitr { get; set; }

        public int BoughtPrice { get; set; }

        public double AllPrice { get; set; }

    }
}
