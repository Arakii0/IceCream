//==========================================================
// Student Number : Benjamin Hwang
// Student Name : S10262171E
// Partner Name : Araki Yeo
//==========================================================

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

        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
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

            if (Dipped)
            {
                cost += 2;
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
            return base.ToString() + $"\t Dipped: {Dipped}";
        }

    }
}

