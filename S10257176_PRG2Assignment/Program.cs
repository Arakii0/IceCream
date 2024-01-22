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

            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.ToString());
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

            foreach (var kvp in flavours)
            {
                Console.WriteLine($"Flavour: {kvp.Key}\nCost: {kvp.Value}");
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
                string option = Convert.ToString(data[4]);
                int scoops = Convert.ToInt32(data[5]);
                bool dipped = Convert.ToBoolean(data[6]);
                string waffleFlavour = Convert.ToString(data[7]);
                string flavour1 = Convert.ToString(data[8]);
                string flavour2 = Convert.ToString(data[9]);
                string flavour3 = Convert.ToString(data[10]);
                string topping1 = Convert.ToString(data[11]);
                string topping2 = Convert.ToString(data[12]);
                string topping3 = Convert.ToString(data[13]);
                string topping4 = Convert.ToString(data[14]);

                Order ord = new Order(); 
            }

            foreach (Order order in orders)
            {
                Console.WriteLine(order.ToString());
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


        static void ReadFileOptions(List<IceCream> options)
        {
            string[] lines = File.ReadAllLines("options.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string option = Convert.ToString(data[0]);
                int scoops = Convert.ToInt32(data[1]);
                bool dipped = Convert.ToBoolean(data[2]);
                string waffleFlavour = Convert.ToString(data[3]);

                if (option == "Cup")
                    options.Add(new Cup(option, scoops, new List<Flavour>(), new List<Topping>()));
                if (option == "Cone")
                    options.Add(new Cone());
                if (option == "Waffle")
                    options.Add(new Waffle());



                options.Add(new IceCream { Option = option, Scoops = scoops, Dipped = dipped, WaffleFlavour = waffleFlavour});

                foreach (IceCream option in options)
                {
                    Console.WriteLine(option.ToString());
                }
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
