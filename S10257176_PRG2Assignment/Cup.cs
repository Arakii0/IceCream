//==========================================================
// Student Number : Benjamin Hwang
// Student Name : S10262171E
// Partner Name : Araki Yeo
//==========================================================

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
            double cost = 0.0;

            switch (Scoops)
            {
                case 1:
                    cost = 4.00;
                    break;
                case 2:
                    cost = 5.50;
                    break;
                case 3:
                    cost = 6.50;
                    break;
                default:
                    Console.WriteLine("Invalid number of scoops");
                    return 0;
            }

            foreach (Flavour flavour in Flavours)
            {
                for (int i = 0; i < flavour.Quantity; i++)
                {
                    if (flavour.Premium)
                    {
                        cost += 2;
                    }
                }
            }

            cost += Toppings.Count;

            return cost;
        }
    

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
