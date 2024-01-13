using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257176_PRG2Assignment
{
    internal class Cone : IceCream
    {
        private bool dipped;

        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }

        public Cone() { }

        public Cone(bool dipped, string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
        }

        public override double CalculatePrice(double total)
        {
            return total + 2;
        }

        public override string ToString()
        {
            return base.ToString() + $"\t Dipped: {Dipped}";
        }

    }
}

