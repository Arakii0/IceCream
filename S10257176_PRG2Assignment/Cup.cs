using System;

namespace S10257176_PRG2Assignment
{
    class Cup : IceCream
    {

        public Cup() { }

        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {

        }

        public override double CalculatePrice()
        {
            return 0.00;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
