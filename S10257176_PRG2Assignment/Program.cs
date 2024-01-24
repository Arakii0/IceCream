//==========================================================
// Student Number : 
// Student Name : 
// Partner Name : 
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization;
using System.Security.AccessControl;


namespace S10257176_PRG2Assignment
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>();

            Dictionary<string, double> flavours = new Dictionary<string, double>();

            Dictionary<int, Order> orders = new Dictionary<int, Order>();

            Dictionary<string, double> topping = new Dictionary<string, double>();
         

            List<IceCream> options = new List<IceCream>();

            Queue<Order> goldQueue = new Queue<Order>();
            Queue<Order> regularQueue = new Queue<Order>();

            ReadfileCustomer(customers);
            ReadFileOrders(customers);
            while (true)
            {
                Console.WriteLine("1) List all customers");
                Console.WriteLine("2) List all current orders");
                Console.WriteLine("3) Register a new customer");
                Console.WriteLine("4) Create a customer's order");
                Console.WriteLine("5) Display order details of a customer");
                Console.WriteLine("6) Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllCustomers(customers);
                        break;
                    case "2":
                        foreach (Customer customer in customers)
                        {
                            ReplaceCurentOrder(customer);
                        }
                        foreach (Customer customer in customers)
                        {
                            if (customer.Rewards.Tier == "Gold")
                                goldQueue.Enqueue(customer.CurrentOrder);
                            else
                                regularQueue.Enqueue(customer.CurrentOrder);
                        }
                        ListAllCurrentOrders(regularQueue, goldQueue);
                        break;
                    case "3":
                        RegisterNewCustomer(customers);
                        break;
                    case "4":
                        CreateCustomerOrder(customers, orders, goldQueue, regularQueue);
                        break;
                    case "5":
                        int i = 0;
                        foreach (Customer customer in customers)
                        {
                            Console.WriteLine($"{i+1}: {customer.Name}");
                            i++;
                        }
                        Console.Write("Enter Option : ");
                        int memberoption = Convert.ToInt32(Console.Read()) -1;
                        foreach(Order order in customers[memberoption].OrderHistory)
                        {
                            Console.WriteLine(order);

                        }
                        Console.WriteLine(customers[memberoption].CurrentOrder);

                        return;
                    case "6":
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

                Customer addcustomer = new Customer(name, memberId, dob);
                addcustomer.Rewards = new PointCard(Convert.ToInt32(data[4]), Convert.ToInt32(data[5]));
                addcustomer.Rewards.Tier = data[3];
                customers.Add(addcustomer);
            }
        }

        static void ReadFileOrders(List<Customer> customers)
        {
            using (StreamReader sr = new StreamReader("Orders.csv"))
            {
                string line = sr.ReadLine();
                List<string[]> datas = new List<string[]>();
                while ((line = sr.ReadLine()) != null)
                {
                    datas.Add(line.Split(","));
                }
                foreach (string[] data in datas)
                {
                    try
                    {

                        List<Flavour> flavs = new List<Flavour>();
                        List<string> Flavoursdata = new List<string>();
                        Flavoursdata.Add(data[8]);
                        Flavoursdata.Add(data[9]);
                        Flavoursdata.Add(data[10]);
                        Dictionary<string, int> flacounter = new Dictionary<string, int>();
                        foreach (string flav in Flavoursdata)
                        {
                            if (flacounter.ContainsKey(flav))
                            {
                                flacounter[flav] = flacounter[flav] + 1;
                            }
                            else
                            {
                                flacounter[flav] = 1;
                            }

                        }
                        foreach (KeyValuePair<string, int> k in flacounter)
                        {
                            bool prem = false;
                            if (k.Key == "Sea Salt" || k.Key == "Ube" || k.Key == "Durian")
                            {
                                prem = true;
                            }
                            flavs.Add(new Flavour(k.Key, prem, k.Value));
                        }

                        List<Topping> tops = new List<Topping>();
                        List<string> Toppingdata = new List<string>();
                        Toppingdata.Add(data[11]);
                        Toppingdata.Add(data[12]);
                        Toppingdata.Add(data[13]);
                        Toppingdata.Add(data[14]);
                        List<string> newToppingdata = new List<string>();
                        foreach (string topping in Toppingdata)
                        {
                            if (topping != "")
                            {
                                tops.Add(new Topping(topping));
                            }
                        }
                        if (data[4].ToLower() == "cup")
                        {
                            Cup icecream = new Cup(data[4], Convert.ToInt32(data[5]), flavs, tops);
                            foreach(Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                            }
                                            else
                                            {
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                                neworder.AddIceCream(icecream);
                                                customer.OrderHistory.Add(neworder);
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                        else if (data[4].ToLower() == "cone")
                        {
                            Cone icecream = new Cone(data[4], Convert.ToInt32(data[5]), flavs, tops, Convert.ToBoolean(data[6]));
                            foreach (Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                            }
                                            else
                                            {
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                                neworder.AddIceCream(icecream);
                                                customer.OrderHistory.Add(neworder);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else if(data[4].ToLower() == "waffle")
                        {
                            Waffle icecream = new Waffle(data[4], Convert.ToInt32(data[5]), flavs, tops, data[7]);
                            foreach (Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                            }
                                            else
                                            {
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[3]));
                                                neworder.AddIceCream(icecream);
                                                customer.OrderHistory.Add(neworder);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                    catch (FormatException)
                    {
                    }
                }
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

        static void ListAllCurrentOrders(Queue<Order> regular, Queue<Order>gold)
        {
            Console.WriteLine("List of all current orders:");
            Console.WriteLine("Gold");
            int i = 1;
            if (gold.Count == 0)
                Console.WriteLine("No Queue!");
            foreach(Order order in gold)
            {
                if (order != null)
                {
                    Console.WriteLine($"{order}");
                    i++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Regular");
            int x = 1;
            if (regular.Count == 0)
                Console.WriteLine("No Queue!");
            foreach (Order order in regular)
            {
                if (order != null)
                {
                    Console.WriteLine($"{order}");
                    x++;
                }
            }
        }   

        static void ReplaceCurentOrder(Customer customer)
        {
            if (customer.OrderHistory.Count != 0)
            {
                customer.CurrentOrder = customer.OrderHistory[customer.OrderCount];
                customer.OrderCount++;
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

        static void CreateCustomerOrder(List<Customer> customers, Dictionary<int, Order> orders, Queue<Order> goldQueue, Queue<Order> regularQueue)
        {
            Console.WriteLine("List of all customers:");
            ListAllCustomers(customers);

            Console.Write("Enter the Member ID of the customer to create an order: ");
            int memberId = Convert.ToInt32(Console.ReadLine());

            Customer selectedCustomer = customers.Find(customer => customer.MemberId == memberId);

            if (selectedCustomer == null)
            {
                Console.WriteLine("Customer not found!");
                return;
            }

            Order newOrder = new Order();
            do
            {
                Console.Write("Enter ice cream option (Cup, Cone, Waffle): ");
                string option = Console.ReadLine();

                Console.Write("Enter number of scoops (1, 2, 3): ");
                int scoops = Convert.ToInt32(Console.ReadLine());

                List<Flavour> flavours = new List<Flavour>();
                Console.WriteLine("Enter ice cream flavours:");
                string flavourInput = Console.ReadLine();
                   

                flavours.Add(new Flavour(flavourInput, false, 1)); 
                

                List<Topping> toppings = new List<Topping>();
                Console.WriteLine("Enter ice cream toppings (press Enter when finished):");
                while (true)
                {
                    Console.Write("Topping (or press Enter to finish): ");
                    string toppingInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(toppingInput))
                    {
                        break;
                    }

                    toppings.Add(new Topping(toppingInput));
                }


                IceCream iceCream;


                switch (option.ToLower())
                {
                    case "cup":
                        iceCream = new Cup(option, scoops, flavours, toppings);
                        break;
                    case "cone":
                        Console.Write("Is it a chocolate-dipped cone? (true/false): ");
                        bool dipped = Convert.ToBoolean(Console.ReadLine());
                        iceCream = new Cone(option, scoops, flavours, toppings, dipped);
                        break;
                    case "waffle":
                        Console.Write("Enter waffle flavour (or 'n' for no additional cost): ");
                        string waffleFlavour = Console.ReadLine();
                        iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                        break;
                    default:
                        Console.WriteLine("Invalid ice cream option. Please try again.");
                        return;
                }

                newOrder.AddIceCream(iceCream);

                Console.Write("Do you want to add another ice cream to the order? (Y/N): ");
            } 
            
            while (Console.ReadLine().ToLower() == "y");

            selectedCustomer.CurrentOrder = newOrder;


            if (selectedCustomer.Rewards.Tier == "Gold")
                goldQueue.Enqueue(selectedCustomer.CurrentOrder);
            else
                regularQueue.Enqueue(selectedCustomer.CurrentOrder);

            Console.WriteLine("Order has been made successfully!");
        }

    }
}

