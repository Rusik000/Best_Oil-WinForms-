using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil_Task
{
    public class MiniCafe
    {
        public readonly List<Meal> Meals = new List<Meal>();
        public int AllCount { get; set; }
        public double AllPrice { get; set; }
    }
}
