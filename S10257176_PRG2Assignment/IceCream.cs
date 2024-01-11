using System;

namespace S10257176_PRG2Assignment
{
    abstract class IceCream
    {
        private string option;
        private int scoops;

        public string Option
        {
            get { return option; }
            set { option = value; }
        }

        public int Scoops
        {
            get { return scoops; }
            set { scoops = value; }
        }

        public List<Flavour> flavours { get { return flavours; } } = new List<Flavour> ();

        public List<Topping> toppings { get { return toppings; } } = new List<Topping> ();


        public IceCream()
        {
            Option = "";
            Scoops = 0;
        }

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }



        public abstract double CalculatePrice();

        public override string ToString()
        {
            return ($"Option: {Option} Scoops: {scoops}");
        }
    }
}
