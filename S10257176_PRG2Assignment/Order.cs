using System;

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

        public void ModifyIceCream(int id)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Modification Table");
                Console.WriteLine("==================");

                Console.WriteLine(@"1. Option
2. Scoops
3. Toppings
4. Flavours
5. Additional Upgrades
6. Finish Ice Cream
0. Exit
Enter Option : ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        loop = false;
                        break;

                    case 1:
                        break;

                    case 2:
                        Console.WriteLine(@"1. Single
2. Double
3. Triple
Enter Option : ");
                        int opt = Convert.ToInt32(Console.ReadLine());



                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Invalid Option, Please try again!");
                        break;
                }
            }






        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int id)
        {
            
        }

        public double CalculateTotal()
        {
            return 0.00;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
