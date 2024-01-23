using System;
using System.Collections.Generic;
using System.IO;
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
                        // ListAllCurrentOrders(customers);
                        ReadFileOrders(orders);
                        Customer_to_Order(orders, customers);

                        foreach(Customer customer in customers)
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
                        break;
                    case "5":
                        foreach (Customer customer in customers)
                            Console.WriteLine($"{customer.MemberId}: {customer.Name}");
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
                addcustomer.Rewards = new PointCard();
                customers.Add(addcustomer);
                
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

        static void ReadFileOrders(Dictionary<int, Order> orders)
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
                        Dictionary<string, int> flavourcounter = new Dictionary<string, int>();
                        foreach (string flav in Flavoursdata)
                        {
                            if (flavourcounter.ContainsKey(flav))
                            {
                                flavourcounter[flav] = flavourcounter[flav] + 1;
                            }
                            else
                            {
                                flavourcounter[flav] = 1;
                            }

                        }
                        foreach (KeyValuePair<string, int> k in flavourcounter)
                        {
                            bool prem = false;
                            if (k.Key == "Durian" || k.Key == "Ube" || k.Key == "Sea Salt")
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
                        if (data[4].ToLower() == "waffle")
                        {
                            Waffle tempicecream = new Waffle(data[4], Convert.ToInt32(data[5]), flavs, tops, data[7]);
                            if (orders.ContainsKey(Convert.ToInt32(data[1])))
                            {
                                orders[Convert.ToInt32(data[1])].AddIceCream(tempicecream);
                            }
                            else
                            {
                                Order neworder = new Order(Convert.ToInt32(data[1]), DateTime.Parse(data[3]));
                                neworder.AddIceCream(tempicecream);
                                orders[Convert.ToInt32(data[1])] = neworder;
                            }

                        }
                        else if (data[4].ToLower() == "cone")
                        {
                            Cone tempicecream = new Cone(data[4], Convert.ToInt32(data[5]), flavs, tops, Convert.ToBoolean(data[6]));
                            if (orders.ContainsKey(Convert.ToInt32(data[1])))
                            {
                                orders[Convert.ToInt32(data[1])].AddIceCream(tempicecream);
                            }
                            else
                            {
                                Order neworder = new Order(Convert.ToInt32(data[1]), DateTime.Parse(data[3]));
                                neworder.AddIceCream(tempicecream);
                                orders[Convert.ToInt32(data[1])] = neworder;
                            }
                        }
                        else if (data[4].ToLower() == "cup")
                        {
                            Cup tempicecream = new Cup(data[4], Convert.ToInt32(data[5]), flavs, tops);
                            if (orders.ContainsKey(Convert.ToInt32(data[1])))
                            {
                                orders[Convert.ToInt32(data[1])].AddIceCream(tempicecream);
                            }
                            else
                            {
                                Order neworder = new Order(Convert.ToInt32(data[1]), DateTime.Parse(data[3]));
                                neworder.AddIceCream(tempicecream);
                                orders[Convert.ToInt32(data[1])] = neworder;
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
                    Console.WriteLine($"{order.IceCreamList}");
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
        
        static void Customer_to_Order(Dictionary<int, Order> orderlist, List<Customer> customers)
        {
            for(int i = 0; i < customers.Count; i++)
            {
                foreach(int member in orderlist.Keys)
                {
                    if (customers[i].MemberId == member)
                    {
                        customers[i].CurrentOrder = (orderlist[member]);
                    }
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
