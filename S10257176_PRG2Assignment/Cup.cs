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
        public Cup(string n, int s, List<Flavour> f, List<Topping> t) : base(n,s,f,t) { }

        public override double CalculatePrice()
        {
                
        }

        public override string ToString()
        {
            return "";
        }
    }
}
