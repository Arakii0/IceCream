//==========================================================
// Student Number : Benjamin Hwang
// Student Name : S10262171E
// Partner Name : Araki Yeo
//==========================================================

using System;

namespace S10257176_PRG2Assignment
{
    abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

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

        public List<Flavour> Flavours { get; set; } = new List<Flavour> ();

        public List<Topping> Toppings { get; set; } = new List<Topping> ();


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
