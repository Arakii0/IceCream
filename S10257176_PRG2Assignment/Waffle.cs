using System;

namespace S10257176_PRG2Assignment
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

        public override double CalculatePrice()
        {
            return 0.00 + 3;
        }

        public override string ToString()
        {
            return base.ToString() + $"Waffle Flavour: {WaffleFlavour}";
        }
    }
}
