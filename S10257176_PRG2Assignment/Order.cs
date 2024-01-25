//==========================================================
// Student Number : S10257176H
// Student Name : Araki Yeo
// Partner Name : Benjamin Hwang
//==========================================================

using System;
using System.Globalization;

namespace S10257176_PRG2Assignment
{
    class Order
    {
        private int id;
        private DateTime timeRecieved;
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamList;

        public int Id
        { get; set; }
        public DateTime TimeRecieved
        { get; set; }
        public DateTime? TimeFulfilled
        { get; set; }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();


        public Order() { }
        public Order(int id, DateTime tr)
        {
            Id = id;
            TimeRecieved = tr;
        }

        public void ModifyIceCream()
        {
            IceCream iceCream = null;
            bool loop = true;

            Console.WriteLine("Modification Table");
            Console.WriteLine("==================");

            int icecreamOption;

                while (true)
                {
                    for (int i = 0; i < IceCreamList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {IceCreamList[i].Option}");
                    }
                    try
                    {
                        Console.Write("Enter Icecream to modify : ");
                        icecreamOption = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Choosen option {icecreamOption} ({IceCreamList[icecreamOption - 1].Option})");
                        iceCream = IceCreamList[icecreamOption - 1];
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Please enter a number, please try again");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Invalid Option! Please enter a valid option!");
                    }
                }

                while(loop)
                {
                    Console.WriteLine(@"1. Option");
                    Console.WriteLine("2. Scoops");
                    Console.WriteLine("3. Toppings");
                    Console.WriteLine("4. Flavours");
                    Console.WriteLine("0. Exit");
                    Console.Write("Enter Option : ");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Exiting...");
                            loop = false;
                            break;

                        case 1:
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine(@"1. Cup");
                                Console.WriteLine("2. Cone");
                                Console.WriteLine("3. Waffle");
                                Console.WriteLine("0. Exit");
                                Console.Write("Enter Option : ");

                                int opt1 = Convert.ToInt32(Console.ReadLine());

                                if (opt1 == 1)
                                { 
                                    IceCreamList[icecreamOption - 1] = new Cup("Cup", iceCream.Scoops, iceCream.Flavours, iceCream.Toppings);
                                    iceCream = IceCreamList[icecreamOption - 1];
                                    break; 
                                }
                                if (opt1 == 2)
                                {
                                    Console.Write("Chocolate-dipped cone (+$2) (y/n) : ");
                                    string choco = Console.ReadLine();
                                    bool Dipped = false;
                                    if (choco.ToLower() == "y")
                                    {
                                        Dipped = true;
                                    }
                                    IceCreamList[icecreamOption - 1] = new Cone("Cone", iceCream.Scoops, iceCream.Flavours, iceCream.Toppings, Dipped);
                                    iceCream = IceCreamList[icecreamOption - 1];
                                    break;
                                }
                                if (opt1 == 3)
                                {
                                    Console.Write("Red Velvet, Charcoal, Pandan Waffle or n (+$3) : ");
                                    string waffleChoice = Console.ReadLine();
                                    string waffleFlavour = "Original";
                                    if (waffleChoice.ToLower() != "n")
                                    {
                                        waffleFlavour = CapitalizeWords(waffleChoice);
                                    }
                                    IceCreamList[icecreamOption - 1] = new Waffle("Waffle", iceCream.Scoops, iceCream.Flavours, iceCream.Toppings, waffleFlavour);
                                    iceCream = IceCreamList[icecreamOption - 1];
                                    break;
                                }
                                if (opt1 == 0)
                                    break;
                                else
                                    { Console.WriteLine("Invalid Option"); }
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine("Enter an Integer");
                            }
                            }
                            break;

                        case 2:
                            Console.WriteLine(@"1. Single");
                            Console.WriteLine("2. Double");
                            Console.WriteLine("3. Triple");
                            Console.WriteLine("0. Exit");
                            Console.Write("Enter Option : ");

                            int opt2 = Convert.ToInt32(Console.ReadLine());
                            if (opt2 == 0)
                                continue;
                            else
                                iceCream.Scoops = opt2;
                            break;

                        case 3:
                            Console.WriteLine(@"1. Sprinkles");
                            Console.WriteLine("2. Mochi");
                            Console.WriteLine("3. Sago");
                            Console.WriteLine("4. Oreos");
                            Console.WriteLine("0. Exit");
                            Console.Write("Enter Option : ");

                        int opt3 = Convert.ToInt32(Console.ReadLine());
                            if (opt3 == 0)
                                continue;
                            if (opt3 == 1)
                                iceCream.Toppings.Add(new Topping("SprinKles"));
                            if (opt3 == 2)
                                iceCream.Toppings.Add(new Topping("Mochi"));
                            if (opt3 == 3)
                                iceCream.Toppings.Add(new Topping("Sago"));
                            if (opt3 == 4)
                                iceCream.Toppings.Add(new Topping("Oreos"));
                            break;

                        case 4:
                            Console.WriteLine(@"1. Vanilla");
                            Console.WriteLine("2. Chocolate");
                            Console.WriteLine("3. Strawberry");
                            Console.WriteLine("4. Durian (Premium)");
                            Console.WriteLine("5. Ube (Premium)");
                            Console.WriteLine("6. Sea Salt (Premium)");
                            Console.WriteLine("0. Exit");
                            Console.Write("Enter Option : ");

                        int opt4 = Convert.ToInt32(Console.ReadLine());
                            if (opt4 == 0)
                                continue;

                            if (opt4 == 1)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Vanilla"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Vanilla");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Vanilla", false, 1));

                            if (opt4 == 2)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Chocolate"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Chocolate");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Chocolate", false, 1));

                            if (opt4 == 3)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Strawberry"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Strawberry");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Strawberry", false, 1));

                            if (opt4 == 4)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Durian"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Durian");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Durain", true, 1));

                            if (opt4 == 5)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Ube"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Ube");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Ube", true, 1));

                            if (opt4 == 6)
                                if (iceCream.Flavours.Any(flavour => flavour.Type == "Sea Salt"))
                                {
                                    int index = iceCream.Flavours.FindIndex(flavour => flavour.Type == "Sea Salt");
                                    iceCream.Flavours[index].Quantity += 1;
                                }
                                else
                                    iceCream.Flavours.Add(new Flavour("Sea Salt", true, 1));
                            break;

                        case 5:
                            Console.WriteLine(iceCream.Option);
                            foreach (Flavour f in iceCream.Flavours)
                            {
                                Console.WriteLine(f.Type);
                                Console.WriteLine(f.Quantity);
                            }
                            Console.WriteLine(iceCream.Scoops);
                            foreach(Topping t in iceCream.Toppings)
                                Console.WriteLine(t.Type);
                            break;
                        default:
                            Console.WriteLine("Invalid Option, Please try again!");
                            break;
                    }
                }
                static string CapitalizeWords(string input)
                {
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;

                    return textInfo.ToTitleCase(input);
                }
        }








        public void AddIceCream(IceCream icecream)
        {
            IceCreamList.Add(icecream);
        }


        public void DeleteIceCream(int id)
        {
            IceCreamList.RemoveAt(id-1);
        }

        public double CalculateTotal()
        {
            double price= 0.00;
            foreach (IceCream c in IceCreamList)
            {
                price += c.CalculatePrice();
            }
            return price;
        }


        public override string ToString()
        {
            string order = "";
            foreach(IceCream c in IceCreamList)
            {
                order = $"{Id,-4}\t{TimeRecieved,-15}\t{c.Option}";
            }

            return order;
        }
    }
}
