//==========================================================
// Student Number : Benjamin Hwang
// Student Name : S10262171E
// Partner Name : Araki Yeo
//==========================================================

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

        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            {
                double cost = 0.0;

                switch (Scoops)
                {
                    case 1:
                        cost = 7.00;
                        break;
                    case 2:
                        cost = 8.50;
                        break;
                    case 3:
                        cost = 9.50;
                        break;
                    default:
                        Console.WriteLine("Invalid number of scoops");
                        return 0;
                }

                // if there is no input of waffle flavour then it would not add $3
                if (!string.IsNullOrEmpty(WaffleFlavour))
                {
                    cost += 3; 
                }

                foreach (Flavour flavour in Flavours)
                {
                    if (flavour.IsPremium)
                    {
                        cost += 2;
                    }
                }

                cost += Toppings.Count;

                return cost;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"Waffle Flavour: {WaffleFlavour}";
        }
    }
}
