//==========================================================
// Student Number : 
// Student Name : 
// Partner Name : 
//==========================================================

using System;

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
                Console.WriteLine("==================================");
                Console.WriteLine($"{"Main Menu", 22}");
                Console.WriteLine("==================================");
                Console.WriteLine("1) List all customers");
                Console.WriteLine("2) List all current orders");
                Console.WriteLine("3) Register a new customer");
                Console.WriteLine("4) Create a customer's order");
                Console.WriteLine("5) Display order details of a customer");
                Console.WriteLine("6) Modify order details");
                Console.WriteLine("8) Display monthly charged amounts breakdown & total charged amounts for the year");
                Console.WriteLine("0) Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllCustomers(customers);
                        break;
                    case "2":
                        ListAllCurrentOrders(regularQueue, goldQueue);
                        break;
                    case "3":
                        RegisterNewCustomer(customers);
                        break;
                    case "4":
                        CreateCustomerOrder(customers, orders, goldQueue, regularQueue);
                        break;
                    case "5":
                        while (true)
                        {
                            Console.WriteLine("============================================");
                            Console.WriteLine($"{"Customer",-10} {"Member ID"}");
                            foreach (Customer customer in customers)
                            {
                                Console.WriteLine($"{customer.Name,-10} {customer.MemberId}");
                            }
                            try
                            {
                                Console.Write("Enter Option : ");
                                int memberoption5 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("=====================================");
                                bool memberFound = false;
                                foreach (Customer customer in customers)
                                {
                                    if (customer.MemberId == memberoption5)
                                    {
                                        Customer Targetcus = customer;
                                        Console.WriteLine("Current Order : ");
                                        Console.WriteLine(Targetcus.CurrentOrder);
                                        Console.WriteLine("Order History : ");
                                        Console.WriteLine($"{"ID",-7} {"Time Recieved",-25} {"Time Fulfuilled"}");
                                        foreach (Order order in Targetcus.OrderHistory)
                                        {
                                            Console.WriteLine(order + $"{order.TimeFulfilled,26}");
                                        }
                                        memberFound = true;
                                    }
                                }
                                if (!memberFound)
                                    Console.WriteLine("Member Not Found!");
                                else
                                    break;
                            }
                            catch(FormatException e)
                            {
                                Console.WriteLine("Invalid Option! Integer only!");
                            }
                        }
                        break;
                    case "6":
                        foreach (Customer customer in customers)
                        {
                            Console.WriteLine($"{customer.Name,-10} {customer.MemberId}");
                        }
                        Console.Write("Enter Option : ");
                        int memberoption6 = Convert.ToInt32(Console.ReadLine());
                        bool memberFound6 = false;
                        foreach (Customer customer in customers)
                        {
                            if (customer.MemberId == memberoption6)
                            {
                                memberFound6 = true;
                                Customer customerTarget = customer;
                                Console.WriteLine("1) Modify Existing IceCream");
                                Console.WriteLine("2) Create New IceCream");
                                Console.WriteLine("3) Delete Existing IceCream");
                                Console.Write("Enter Option : ");
                                int option6 = Convert.ToInt32(Console.ReadLine());

                                if (option6 == 1)
                                    if (customerTarget.CurrentOrder != null)
                                    { customerTarget.CurrentOrder.ModifyIceCream(); break; }
                                    else
                                    { Console.WriteLine("No Current Order!"); break; }
                                else if (option6 == 2)
                                {
                                    if (customerTarget.CurrentOrder != null)
                                    { AddIceCream(customerTarget); break; }
                                    else
                                    { Console.WriteLine("Please Create An Order First!"); break; }
                                }
                                //else if (option6 == 3)
                                //customerTarget.CurrentOrder.DeleteIceCream();
                                else
                                    { Console.WriteLine("Invalid Option"); break; }
                            }
                        }
                        if (!memberFound6)
                            Console.WriteLine("Member Not Found!");
                        break;




                    case "8":
                        break;

                    case "0":
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
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                        neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                                neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                        neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                                neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                        neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
                                                Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.Parse(data[2]));
                                                neworder.TimeFulfilled = DateTime.Parse(data[3]);
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
            Console.WriteLine("==========================");
            Console.WriteLine("List of all current orders");
            Console.WriteLine("==========================");
            Console.WriteLine("Gold");
            int i = 1;
            if (gold.Count == 0)
                Console.WriteLine("No Queue!");
            else
            {
                Console.WriteLine($"{"ID",5} {"Time Recieved",15}");
                foreach (Order order in gold)
                {
                    if (order != null)
                    {
                        int count = 0;
                        Console.WriteLine($"{i}. {order,5}");
                        foreach (IceCream ice in order.IceCreamList)
                        {
                            count++;
                            Console.WriteLine($"        {i}.{count}: {ice}");
                        }
                        i++;
                    }
                }
            }
            Console.WriteLine("=========================================");
            Console.WriteLine();
            Console.WriteLine("Regular");
            int x = 1;
            if (regular.Count == 0)
                Console.WriteLine("No Queue!");
            else
            {
                Console.WriteLine($"{"ID",5} {"Time Recieved",15}");
                foreach (Order order in regular)
                {
                    if (order != null)
                    {
                        int count = 0;
                        Console.WriteLine($"{x}. {order,5}");
                        foreach (IceCream ice in order.IceCreamList)
                        {
                            count++;
                            Console.WriteLine($"        {x}.{count}: {ice}");
                        }
                        x++;
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
                newOrder.Id = 1; // PLACEHOLDER FOR NOW, NEED GET ID FROM CSV FILE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! PLEASE HELP ME DO IT, THANK YOU
                newOrder.TimeRecieved = DateTime.Now;

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

        static void AddIceCream(Customer customer)
        {
            bool loop = true;
            bool scooped = false;
            IceCream icecream = null;
            while (true)
            {
                try
                {
                    Console.WriteLine("Choose your IceCream Option");
                    Console.WriteLine("1. Cup");
                    Console.WriteLine("2. Cone");
                    Console.WriteLine("3. Waffle");
                    Console.Write("Enter Option : ");

                    int opt1 = Convert.ToInt32(Console.ReadLine());

                    if (opt1 == 1)
                    {
                        icecream = new Cup();
                        icecream.Option = "Cup";
                        break;
                    }
                    if (opt1 == 2)
                    {
                        icecream = new Cone();
                        icecream.Option = "Cone";
                        break;
                    }
                    if (opt1 == 3)
                    {
                        icecream = new Waffle();
                        icecream.Option = "Waffle";
                        break;
                    }
                    else
                    { Console.WriteLine("Invalid Option"); }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Enter an Integer");
                }
            }

            while (loop)
            {
                Console.WriteLine("1. Scoops");
                Console.WriteLine("2. Toppings");
                Console.WriteLine("3. Flavours");
                Console.WriteLine("4. Complete IceCream");
                Console.WriteLine("0. Exit");
                Console.Write("Enter Option : ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;

                    case 1:
                        Console.WriteLine("1. Single");
                        Console.WriteLine("2. Double");
                        Console.WriteLine("3. Triple");
                        Console.WriteLine("0. Exit");
                        Console.Write("Enter Option : ");

                        int opt1 = Convert.ToInt32(Console.ReadLine());
                        if (opt1 == 0)
                            continue;
                        else
                            icecream.Scoops = opt1;
                        scooped = true;
                        break;

                    case 2:
                        Console.WriteLine("1. Sprinkles");
                        Console.WriteLine("2. Mochi");
                        Console.WriteLine("3. Sago");
                        Console.WriteLine("4. Oreos");
                        Console.WriteLine("0. Exit");
                        Console.Write("Enter Option : ");

                        int opt2 = Convert.ToInt32(Console.ReadLine());
                        if (opt2 == 0)
                            continue;
                        if (opt2 == 1)
                            icecream.Toppings.Add(new Topping("SprinKles"));
                        if (opt2 == 2)
                            icecream.Toppings.Add(new Topping("Mochi"));
                        if (opt2 == 3)
                            icecream.Toppings.Add(new Topping("Sago"));
                        if (opt2 == 4)
                            icecream.Toppings.Add(new Topping("Oreos"));
                        break;

                    case 3:
                        Console.WriteLine("1. Vanilla");
                        Console.WriteLine("2. Chocolate");
                        Console.WriteLine("3. Strawberry");
                        Console.WriteLine("4. Durian (Premium)");
                        Console.WriteLine("5. Ube (Premium)");
                        Console.WriteLine("6. Sea Salt (Premium)");
                        Console.WriteLine("0. Exit");
                        Console.Write("Enter Option : ");

                        int opt3 = Convert.ToInt32(Console.ReadLine());
                        if (opt3 == 0)
                            continue;

                        if (opt3 == 1)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Vanilla"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Vanilla");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Vanilla", false, 1));

                        if (opt3 == 2)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Chocolate"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Chocolate");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Chocolate", false, 1));

                        if (opt3 == 3)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Strawberry"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Strawberry");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Strawberry", false, 1));

                        if (opt3 == 4)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Durian"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Durian");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Durain", true, 1));

                        if (opt3 == 5)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Ube"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Ube");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Ube", true, 1));

                        if (opt3 == 6)
                            if (icecream.Flavours.Any(flavour => flavour.Type == "Sea Salt"))
                            {
                                int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Sea Salt");
                                icecream.Flavours[index].Quantity += 1;
                            }
                            else
                                icecream.Flavours.Add(new Flavour("Sea Salt", true, 1));
                        break;

                    case 4:
                        if (scooped)
                        {
                            Console.WriteLine($"Created {icecream.Option}");
                            customer.CurrentOrder.AddIceCream(icecream);
                            Console.WriteLine("asdasddddddddddddddddddddd");
                            loop = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Scoop!");
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid Option, Please try again!");
                        break;
                }
            }
        }
    
        static void ReadFileToppings(Dictionary<string, double> toppings)
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

    }
}

