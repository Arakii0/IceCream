using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257176_PRG2Assignment
{
    class Cup : IceCream
    {

        public Cup() { }

        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {

        }

        public double CalculatePrice(double total)
        {
            return total;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
