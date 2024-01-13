using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257176_PRG2Assignment
{
    internal class Waffle
    {
        internal class Waffle : IceCream
        {
            private string waffleFlavour;

            public string WaffleFlavour
            {
                get { return waffleFlavour; }
                set { waffleFlavour = value; }
            }

            public Waffle() { }

            public Waffle(string waffleFlavour, string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
            {
                WaffleFlavour = waffleFlavour;
            }

            public double CalculatePrice(double total)
            {
                double price = total + 3;
            }

            public override string ToString()
            {
                return base.ToString() + $"Waffle Flavour: {WaffleFlavour}";
            }
        }
    }
}
