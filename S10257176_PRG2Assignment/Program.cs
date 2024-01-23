using System;
using System.Collections.Generic;
using System.IO;


namespace S10257176_PRG2Assignment
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>();

            Dictionary<string, double> flavours = new Dictionary<string, double>();

            Dictionary<int, List<Order>> orders = new Dictionary<int, List<Order>>();

            Dictionary<string, double> topping = new Dictionary<string, double>();
         

            List<IceCream> options = new List<IceCream>();


            ReadfileCustomer(customers);

            while (true)
            {
                Console.WriteLine("1) List all customers");
                Console.WriteLine("2) List all current orders");
                Console.WriteLine("3) Register a new customer");
                Console.WriteLine("4) Create a customer's order");
                Console.WriteLine("5) Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllCustomers(customers);
                        break;
                    case "2":
                        ListAllCurrentOrders(customers);
                        break;
                    case "3":
                        RegisterNewCustomer(customers);
                        break;
                    case "4":
                        
                        break;
                    case "5":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ReadfileCustomer(List<Customer> customers)
        {
            string[] lines = File.ReadAllLines("customers.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = Convert.ToString(data[0]);
                int memberId = Convert.ToInt32(data[1]);
                DateTime dob = Convert.ToDateTime(data[2]);

                customers.Add(new Customer(name, memberId, dob));
            }
        }

        static void ReadFileFlavors(Dictionary<string, double> flavours)
        {
            string[] lines = File.ReadAllLines("flavors.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = Convert.ToString(data[0]);
                double cost = Convert.ToDouble(data[1]);

                flavours.Add(name, cost);
            }
        }

        static void ReadFileOrders(Dictionary<int, List<Order>> orders)
        {
            string[] lines = File.ReadAllLines("orders.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                int id = Convert.ToInt32(data[0]);
                int memberId = Convert.ToInt32(data[1]);
                DateTime timeReceived = Convert.ToDateTime(data[2]);
                DateTime timeFulfilled = Convert.ToDateTime(data[3]);
                string option = data[4];
                int scoops = Convert.ToInt32(data[5]);
                bool dipped = Convert.ToBoolean(data[6]);
                string waffleFlavour = data[7];
                string flavour1 = data[8];
                string flavour2 = data[9];
                string flavour3 = data[10];
                string topping1 = data[11];
                string topping2 = data[12];
                string topping3 = data[13];
                string topping4 = data[14];

                List<Flavour> f = new List<Flavour>();
                for(int x = 8; x < 11; x++)
                {
                    if (data[x] != "")
                        if (data[x] == "Durian" || data[x] == "Ube" || data[x] == "Sea salt")
                        {
                            if (f.Any(flavour => flavour.Type == data[x]))
                            {
                                int index = f.FindIndex(flavour => flavour.Type == data[x]);
                                f[index].Quantity += 1;
                            }
                            else
                                f.Add(new Flavour(data[x], true, 1));
                        }
                        else
                        {
                            if (f.Any(flavour => flavour.Type == data[x]))
                            {
                                int index = f.FindIndex(flavour => flavour.Type == data[x]);
                                f[index].Quantity += 1;
                            }
                            else
                                f.Add(new Flavour(data[x], false, 1));
                        }
                }

                List<Topping> t = new List<Topping>();
                for (int x = 11; x < 15; x++)
                {
                    if (data[x] != "")
                        t.Add(new Topping(data[x]));    
                }

                Order newOrder = new Order(id, timeReceived);


                if (!orders.ContainsKey(id))
                {
                    orders[id] = new List<Order>();
                }

                orders[id].Add(newOrder);
            }
        }

        static void ReadFileToppings(Dictionary<string,double> toppings)
        {
            string[] lines = File.ReadAllLines("toppings.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = Convert.ToString(data[0]);
                double cost = Convert.ToDouble(data[1]);

                toppings.Add(name, cost);
            }

            foreach (var kvp in toppings)
            {
                Console.WriteLine($"Topping : {kvp.Key}\nCost : {kvp.Value}");
            }
        }

        static void ReadFileOptions(Dictionary<IceCream, double> options)
        {
            string[] lines = File.ReadAllLines("options.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string option = Convert.ToString(data[0]);
                int scoops = Convert.ToInt32(data[1]);
                bool dipped = Convert.ToBoolean(data[2]);
                string waffleFlavour = Convert.ToString(data[3]);
                double cost = Convert.ToDouble(data[4]);

                if (option == "Cup")
                    options.Add(new Cup(option, scoops, new List<Flavour>(), new List<Topping>()), cost);
                if (option == "Cone")
                    options.Add(new Cone(option, scoops, new List<Flavour>(), new List<Topping>(), dipped), cost);
                if (option == "Waffle")
                    options.Add(new Waffle(option, scoops, new List<Flavour>(), new List<Topping>(), waffleFlavour), cost);
            }
        }

        static void ListAllCustomers(List<Customer> customers)
        {
            Console.WriteLine("List of all customers:");
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }

        static void ListAllCurrentOrders(List<Customer> customers)
        {
            Console.WriteLine("List of all current orders:");
            foreach (Customer customer in customers)
            {
                if (customer.CurrentOrder != null)
                {
                    Console.WriteLine($"Customer: {customer.Name}, Current Order: {customer.CurrentOrder.ToString()}");
                }
            }
        }

        static void RegisterNewCustomer(List<Customer> customers)
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer ID number: ");
            int memberId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter customer date of birth (MM/DD/YYYY): ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());

            Customer newCustomer = new Customer(name, memberId, dob);
            newCustomer.Rewards = new PointCard();

            customers.Add(newCustomer);

            Console.WriteLine("Customer registered successfully!");

        }
    }
}
