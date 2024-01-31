//==========================================================
// Student Number : S10257176H
// Student Name : Araki Yeo
// Partner Name : Benjamin Hwang
//==========================================================

using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace S10257176_PRG2Assignment
{
    class Program
    {
        static int orderId = 0;
        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>();

            Dictionary<string, double> flavours = new Dictionary<string, double>();

            Dictionary<int, Order> orders = new Dictionary<int, Order>();

            Dictionary<string, double> toppings = new Dictionary<string, double>();

            List<IceCream> options = new List<IceCream>();

            Queue<Order> goldQueue = new Queue<Order>();
            Queue<Order> regularQueue = new Queue<Order>();

            ReadfileCustomer(customers);
            ReadFileOrders(customers);
            ReadFileFlavours(flavours);
            ReadFileToppings(toppings);
            string[] lines = File.ReadAllLines("orders.csv");
            List<string> seen = new List<string>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                if (!seen.Contains(data[0]))
                { orderId += 1; seen.Add(data[0]); }
            }


            while (true)
            {
                Console.WriteLine("==================================");
                Console.WriteLine($"{"Main Menu",22}");
                Console.WriteLine("==================================");
                Console.WriteLine("1) List all customers");
                Console.WriteLine("2) List all current orders");
                Console.WriteLine("3) Register a new customer");
                Console.WriteLine("4) Create a customer's order");
                Console.WriteLine("5) Display order details of a customer");
                Console.WriteLine("6) Modify order details");
                Console.WriteLine("7) Process an order and checkout");
                Console.WriteLine("8) Display monthly charged amounts breakdown & total charged amounts for the year");
                Console.WriteLine("0) Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //==========================================================
                        // Student Number : S10262171E
                        // Student Name : Benjamin Hwang
                        //==========================================================
                        ListAllCustomers(customers);
                        break;

                    case "2":
                        //==========================================================
                        // Student Number : S10257176H
                        // Student Name : Araki Yeo
                        //==========================================================
                        ListAllCurrentOrders(regularQueue, goldQueue);
                        break;

                    case "3":
                        //==========================================================
                        // Student Number : S10262171E
                        // Student Name : Benjamin Hwang
                        //==========================================================
                        RegisterNewCustomer(customers);
                        break;

                    case "4":
                        //==========================================================
                        // Student Number : S10262171E
                        // Student Name : Benjamin Hwang
                        //==========================================================
                        CreateCustomerOrder(customers, orders, goldQueue, regularQueue, flavours, toppings);
                        break;

                    case "5":
                        //==========================================================
                        // Student Number : S10257176H
                        // Student Name : Araki Yeo
                        //==========================================================
                        while (true)
                        {
                            Console.WriteLine("============================================");
                            Console.WriteLine($"{"Customer",-10} {"Member ID"}");
                            // Get the customer name and member id
                            foreach (Customer customer in customers)
                            {
                                Console.WriteLine($"{customer.Name,-10} {customer.MemberId}");
                            }
                            try
                            {
                                // Ask for the member id
                                Console.Write("Enter Option : ");
                                int memberoption5 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("=====================================");
                                bool memberFound = false;
                                foreach (Customer customer in customers)
                                {
                                    // Check if the member id is the same as the one keyed in
                                    if (customer.MemberId == memberoption5)
                                    {
                                        Customer Targetcus = customer;
                                        Console.WriteLine("Current Order : ");
                                        if (Targetcus.CurrentOrder != null)
                                        {
                                            // Display the current order
                                            Console.WriteLine($"{"ID",-7} {"Time Recieved"}");
                                            Console.WriteLine(Targetcus.CurrentOrder);
                                            Console.WriteLine("\tIceCreams : ");
                                            PrintIceCreams(Targetcus.CurrentOrder.IceCreamList);
                                        }
                                        else
                                            Console.WriteLine("No Current Order!");
                                        Console.WriteLine("Order History : ");
                                        if (Targetcus.OrderHistory.Count != 0)
                                        {
                                            // Display the order history
                                            Console.WriteLine($"{"ID",-7} {"Time Recieved",-25} {"Time Fulfuilled"}");
                                            foreach (Order order in Targetcus.OrderHistory)
                                            {
                                                Console.WriteLine(order + $"{order.TimeFulfilled,26}");
                                                Console.WriteLine("\tIceCreams : ");
                                                PrintIceCreams(order.IceCreamList);
                                            }
                                        }
                                        else
                                            Console.WriteLine("No Order History!");
                                        memberFound = true;
                                    }
                                }
                                if (!memberFound)
                                    Console.WriteLine("Member Not Found!");
                                else
                                    break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalid Option! Integer only!");
                            }
                        }
                        break;

                    case "6":
                        //==========================================================
                        // Student Number : S10257176H
                        // Student Name : Araki Yeo
                        //==========================================================
                        while (true)
                        {
                            try
                            {
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
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine("1) Modify Existing IceCream");
                                                Console.WriteLine("2) Create New IceCream");
                                                Console.WriteLine("3) Delete Existing IceCream");
                                                Console.Write("Enter Option : ");
                                                int option6 = Convert.ToInt32(Console.ReadLine());

                                                if (option6 == 1)
                                                    // Modify the ice cream
                                                    if (customerTarget.CurrentOrder != null)
                                                    { customerTarget.CurrentOrder.ModifyIceCream(); break; }
                                                    else
                                                    { Console.WriteLine("No Current Order!"); break; }
                                                else if (option6 == 2)
                                                {
                                                    // Create a new ice cream
                                                    if (customerTarget.CurrentOrder != null)
                                                    { AddIceCream(customerTarget); break; }
                                                    else
                                                    { Console.WriteLine("Please Create An Order First!"); break; }
                                                }
                                                else if (option6 == 3)
                                                    // Delete an ice cream
                                                    DeleteAnIceCream(customerTarget);
                                                else
                                                { Console.WriteLine("Invalid Option"); }
                                            }
                                            catch (FormatException e)
                                            {
                                                Console.WriteLine("Invalid Option! Integer only!");
                                            }
                                        }
                                    }
                                }
                                if (!memberFound6)
                                    Console.WriteLine("Member Not Found!");
                                else
                                    break;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Invalid Option! Integer only!");
                            }
                        }
                        
                        break;

                    case "7":
                        //==========================================================
                        // Student Number : S10262171E
                        // Student Name : Benjamin Hwang
                        //==========================================================
                        ProcessOrderAndCheckout(goldQueue, regularQueue, customers);
                        break;

                    case "8":
                        //==========================================================
                        // Student Number : S10257176H
                        // Student Name : Araki Yeo
                        //==========================================================
                        DisplayMonthlyCharges(customers);
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
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================
            string[] lines = File.ReadAllLines("customers.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = Convert.ToString(data[0]);
                int memberId = Convert.ToInt32(data[1]);
                DateTime dob = DateTime.ParseExact(data[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Customer addcustomer = new Customer(name, memberId, dob);
                addcustomer.Rewards = new PointCard(Convert.ToInt32(data[4]), Convert.ToInt32(data[5]));
                addcustomer.Rewards.Tier = data[3];
                customers.Add(addcustomer);
            }
        }

        static void ReadFileOrders(List<Customer> customers)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
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
                        // Create a list of flavours
                        List<Flavour> flavs = new List<Flavour>();
                        // From the data, add the flavoure
                        List<string> Flavoursdata = new List<string>();
                        Flavoursdata.Add(data[8]);
                        Flavoursdata.Add(data[9]);
                        Flavoursdata.Add(data[10]);
                        Dictionary<string, int> flacounter = new Dictionary<string, int>();
                        // Count the number of flavours and add it to the dictionary
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
                            // Check if the flavour is premium
                            bool prem = false;
                            if (k.Key == "Sea Salt" || k.Key == "Ube" || k.Key == "Durian")
                            {
                                prem = true;
                            }
                            if (k.Key != "")
                                flavs.Add(new Flavour(k.Key, prem, k.Value));
                        }

                        // Create a list of toppings
                        List<Topping> tops = new List<Topping>();
                        // From the data, add the toppings
                        List<string> Toppingdata = new List<string>();
                        Toppingdata.Add(data[11]);
                        Toppingdata.Add(data[12]);
                        Toppingdata.Add(data[13]);
                        Toppingdata.Add(data[14]);
                        List<string> newToppingdata = new List<string>();
                        //Add the toppings to the list
                        foreach (string topping in Toppingdata)
                        {
                            if (topping != "")
                            {
                                tops.Add(new Topping(topping));
                            }
                        }
                        if (data[4].ToLower() == "cup")
                        {
                            // Create a cup ice cream
                            Cup icecream = new Cup(data[4], Convert.ToInt32(data[5]), flavs, tops);
                            foreach (Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    // Check if the customer has an order history
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                        neworder.ParseDateString(data[3]);
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        bool seen = false;
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            // Check if the order id is the same as the one in the order history
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                                seen = true;
                                            }
                                        }
                                        if (!seen)
                                        {
                                            // Create a new order
                                            Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                            neworder.ParseDateString(data[3]);
                                            neworder.AddIceCream(icecream);
                                            customer.OrderHistory.Add(neworder);
                                        }
                                    }
                                }
                            }

                        }
                        else if (data[4].ToLower() == "cone")
                        {
                            // Create a cone ice cream
                            Cone icecream = new Cone(data[4], Convert.ToInt32(data[5]), flavs, tops, Convert.ToBoolean(data[6]));
                            foreach (Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    // Check if the customer has an order history
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                        neworder.ParseDateString(data[3]);
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        bool seen = false;
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            // Check if the order id is the same as the one in the order history
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                                seen = true;
                                            }
                                        }
                                        if (!seen)
                                        {
                                            // Create a new order
                                            Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                            neworder.ParseDateString(data[3]);
                                            neworder.AddIceCream(icecream);
                                            customer.OrderHistory.Add(neworder);
                                        }
                                    }
                                }
                            }

                        }
                        else if (data[4].ToLower() == "waffle")
                        {
                            // Create a waffle ice cream
                            Waffle icecream = new Waffle(data[4], Convert.ToInt32(data[5]), flavs, tops, data[7]);
                            foreach (Customer customer in customers)
                            {
                                if (customer.MemberId == Convert.ToInt32(data[1]))
                                {
                                    // Check if the customer has an order history
                                    if (customer.OrderHistory.Count == 0)
                                    {
                                        Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                        neworder.ParseDateString(data[3]);
                                        neworder.AddIceCream(icecream);
                                        customer.OrderHistory.Add(neworder);
                                    }
                                    else
                                    {
                                        // Check if the order id is the same as the one in the order history
                                        bool seen = false;
                                        for (int i = 0; i < customer.OrderHistory.Count; i++)
                                        {
                                            if (customer.OrderHistory[i].Id == Convert.ToInt32(data[0]))
                                            {
                                                customer.OrderHistory[i].AddIceCream(icecream);
                                                seen = true;
                                            }
                                        }
                                        if (!seen)
                                        {
                                            // Create a new order
                                            Order neworder = new Order(Convert.ToInt32(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture));
                                            neworder.ParseDateString(data[3]);
                                            neworder.AddIceCream(icecream);
                                            customer.OrderHistory.Add(neworder);
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
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            Console.WriteLine("List of all customers:");
            Console.WriteLine("======================");

            // column header
            Console.WriteLine($"{"Name",-10} {"Member ID",-15} {"Date of Birth",-28} {"Points",-10} {"PunchCard",-12} {"Tier"}");

            // find customer
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }

        static void ListAllCurrentOrders(Queue<Order> regular, Queue<Order> gold)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
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
                        Console.WriteLine("\tIceCreams : ");
                        PrintIceCreams(order.IceCreamList);
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
                        Console.WriteLine("\tIceCreams : ");
                        PrintIceCreams(order.IceCreamList);
                        x++;
                    }
                }
            }
        }

        static void RegisterNewCustomer(List<Customer> customers)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            // ask user for customer name and validate it
            Console.Write("Enter customer name: ");
            string name;

            while (true)
            {
                try
                {
                    name = Console.ReadLine();

                    // checks that the name contains only letters and nothing else
                    bool isValidName = true;

                    foreach (char c in name)
                    {
                        if (!Char.IsLetter(c))
                        {
                            isValidName = false;
                            Console.Write("Enter a valid name: ");
                            break;
                        }
                    }

                    if (isValidName)
                    {
                        break;
                    }
                    
                }
                catch (FormatException)
                {
                    Console.Write("Enter a valid name ");
                }
            }

            // ask user for customer ID and validate it
            Console.Write("Enter customer ID number: ");
            int memberId;

            while (true)
            {
                try
                {
                    memberId = Convert.ToInt32(Console.ReadLine());

                    // checks if there is  existing memberId that is the same as the one keyed in
                    if (!customers.Any(customer => customer.MemberId == memberId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Member ID already exists. Enter a different customer ID number.");
                        Console.Write("Enter a valid customer ID number: ");
                    }
                }
                catch (FormatException)
                {
                    Console.Write("Enter a valid customer ID number: ");
                }
            }

            DateTime dob;

            // ask user for customer date of birth and validate it
            while (true)
            {
                Console.Write("Enter customer date of birth (DD/MM//YYYY): ");

                try
                {
                    dob = Convert.ToDateTime(Console.ReadLine());
                    break;
                }

                catch (FormatException e)
                {
                    Console.WriteLine("Enter date of birth in format DD/MM/YYYY");
                }
            }

            // create a new customer 
            Customer newCustomer = new Customer(name, memberId, dob);
            newCustomer.Rewards = new PointCard();

            // add the  customer to the list
            customers.Add(newCustomer);

            // save customer information to customer.csv
            string fileName = "customers.csv";
            string customerCsvLine = $"{newCustomer.Name},{newCustomer.MemberId},{newCustomer.Dob:dd/MM/yyyy},{newCustomer.Rewards.Tier},{newCustomer.Rewards.Points},{newCustomer.Rewards.PunchCards}";

            try
            {
                File.AppendAllText(fileName, $"{customerCsvLine}\n");
                Console.WriteLine("Customer registered successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Customer not registered.");
            }



        }

        static void CreateCustomerOrder(List<Customer> customers, Dictionary<int, Order> orders, Queue<Order> goldQueue, Queue<Order> regularQueue, Dictionary<string, double> flavours, Dictionary<string, double> toppings)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            // display all customers
            ListAllCustomers(customers);

            // place holder for selectedCustomer
            Customer selectedCustomer = null;

            // loop until a valid customer is selected
            while (selectedCustomer == null)
            {
                Console.Write("Enter the Member ID of the customer to create an order: ");
                string input = Console.ReadLine().ToLower();

                try
                {
                    int memberId = Convert.ToInt32(input);

                    if (memberId > 0)
                    {
                        // find customer with matching member id
                        selectedCustomer = customers.Find(customer => customer.MemberId == memberId);

                        if (selectedCustomer == null)
                        {
                            Console.WriteLine("Customer not found! Please try again.");
                        }
                        else if (selectedCustomer.CurrentOrder != null)
                        {
                            Console.WriteLine("You already have an existing order.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Member ID. Please enter a positive number.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid Member ID.");
                }
            }
            //  new order object for the selected customer
            Order newOrder = new Order();

            // loop to add ice cream to the order
            do
            {
                // dictionaries to store selected flavors and toppings
                Dictionary<string, int> flavourselect = new Dictionary<string, int>();
                Dictionary<string, int> toppingselect = new Dictionary<string, int>();

                // ask for option and validate it
                string option;

                while (true)
                {
                    Console.Write("Enter ice cream option (Cup, Cone, Waffle): ");
                    option = Console.ReadLine().ToLower();

                    if (option == "cup" || option == "cone" || option == "waffle")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid ice cream option. Please enter Cup, Cone, or Waffle.");
                    }
                }

                // ask for number of scoops and validate it
                int scoops;

                while (true)
                {
                    Console.Write("Enter number of scoops (1, 2, 3): ");

                    try
                    {
                        scoops = Convert.ToInt32(Console.ReadLine());

                        if (scoops >= 1 && scoops <= 3)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }

                Console.WriteLine();

                //display available flavors
                Console.Write("Regular Flavours: ");
                Console.WriteLine(string.Join(", ", flavours.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key)));

                Console.WriteLine();
                Console.Write("Premium Flavours (+$2 per scoop):");
                Console.WriteLine(string.Join(", ", flavours.Where(kvp => kvp.Value == 2).Select(kvp => kvp.Key)));

                // loop to select flavours for each scoop and validate it 
                for (int scoop = 1; scoop <= scoops; scoop++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Enter ice cream flavours for scoop {scoop}: ");
                            string flavourInput = Console.ReadLine().ToLower();

                            if (!(flavourInput == "vanilla" || flavourInput == "chocolate" || flavourInput == "strawberry" || flavourInput == "durian" || flavourInput == "ube" || flavourInput == "sea salt"))
                            {
                                Console.WriteLine("Invalid flavour. Choose from options above.");
                                continue;
                            }

                            // update the selected flavour
                            if (flavourselect.ContainsKey(flavourInput))
                            {
                                flavourselect[flavourInput]++;
                            }
                            else
                            {
                                flavourselect.Add(flavourInput, 1);
                            }

                            break;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Invalid input. Choose from options above.");
                        }
                    }
                }


                // display available toppings
                Console.Write("Toppings: ");
                Console.WriteLine(string.Join(", ", toppings.Where(kvp => kvp.Value == 1).Select(kvp => kvp.Key)));

                // loop to get toppings and validate it 
                while (true)
                {
                    Console.Write("Topping (or press Enter to finish): ");
                    string toppingInput = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(toppingInput))
                    {
                        break;
                    }

                    if (!(toppingInput == "sprinkles" || toppingInput == "mochi" || toppingInput == "sago" || toppingInput == "oreos"))
                    {
                        Console.WriteLine("Invalid topping. Choose from options above.");
                        continue;
                    }

                    // ppdate the topping selection
                    if (!toppingselect.ContainsKey(toppingInput))
                    {
                        toppingselect.Add(toppingInput, 1);
                    }
                }

                // create ice cream based on use input
                IceCream iceCream;


                switch (option.ToLower())
                {
                    case "cup":
                        // SelectMany is  used to flatten  sequence into a single list
                        // Enumerable.Repeat generates a sequence that contains a repeated value
                        List<Flavour> cupFlavours = flavourselect.SelectMany(kv => Enumerable.Repeat(new Flavour(kv.Key, false, 1), kv.Value)).ToList();
                        List<Topping> cupToppings = toppingselect.SelectMany(kv => Enumerable.Repeat(new Topping(kv.Key), kv.Value)).ToList();
                        iceCream = new Cup(option, scoops, cupFlavours, cupToppings);
                        break;

                    case "cone":
                        bool isValidDippedInput = false;
                        bool dipped = false;

                        while (!isValidDippedInput)
                        {
                            Console.Write("Is it a chocolate-dipped cone? (true/false): ");
                            string input = Console.ReadLine();

                            try
                            {
                                dipped = Convert.ToBoolean(input);
                                isValidDippedInput = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
                            }
                        }

                        List<Flavour> coneFlavours = flavourselect.SelectMany(kv => Enumerable.Repeat(new Flavour(kv.Key, false, 1), kv.Value)).ToList();
                        List<Topping> coneToppings = toppingselect.SelectMany(kv => Enumerable.Repeat(new Topping(kv.Key), kv.Value)).ToList();
                        iceCream = new Cone(option, scoops, coneFlavours, coneToppings, dipped);
                        break;

                    case "waffle":
                        Console.WriteLine("Waffle Flavour: Red Velvet, Charcoal, or Pandan");
                        Console.Write("Enter waffle flavour (or 'n' for no additional cost): ");

                        string waffleFlavour;

                        while (true)
                        {
                            waffleFlavour = Console.ReadLine().ToLower();

                            if (waffleFlavour == "red velvet" || waffleFlavour == "charcoal" || waffleFlavour == "pandan" || waffleFlavour == "n")
                            {
                                break;
                            }

                            Console.Write("Invalid waffle flavour. Choose from options above: .");
                            
                        }

                        
                        List<Flavour> waffleFlavours = flavourselect.SelectMany(kv => Enumerable.Repeat(new Flavour(kv.Key, false, 1), kv.Value)).ToList();
                        List<Topping> waffleToppings = toppingselect.SelectMany(kv => Enumerable.Repeat(new Topping(kv.Key), kv.Value)).ToList();
                        iceCream = new Waffle(option, scoops, waffleFlavours, waffleToppings, waffleFlavour);
                        break;

                    default:
                        Console.WriteLine("Invalid ice cream option. Please try again.");
                        return;
                }

                // add ice cream to order
                newOrder.AddIceCream(iceCream);

                // increment order id and set timestamp
                orderId++;
                newOrder.Id = orderId;

                newOrder.TimeRecieved = DateTime.Now;

                Console.Write("Do you want to add another ice cream to the order? (Y/N): ");
            }

            // loops whole code if customer want to add new ice cream
            while (Console.ReadLine().ToLower() == "y");

            // set the current order for the selected customer
            selectedCustomer.CurrentOrder = newOrder;

            // enqueue order based on tier
            if (selectedCustomer.Rewards.Tier == "Gold")
                goldQueue.Enqueue(selectedCustomer.CurrentOrder);
            else
                regularQueue.Enqueue(selectedCustomer.CurrentOrder);

            Console.WriteLine();
            Console.WriteLine("Order has been made successfully!");
        }

        static void AddIceCream(Customer customer)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
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
                //Choose the options
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
                        loop = false;
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
                        for (int i = 1; i <= icecream.Scoops; i++)
                        {
                            Console.WriteLine($"Enter ice cream flavours for scoop {i}: ");
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
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Vanilla"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Vanilla");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Vanilla", false, 1));

                            if (opt3 == 2)
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Chocolate"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Chocolate");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Chocolate", false, 1));

                            if (opt3 == 3)
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Strawberry"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Strawberry");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Strawberry", false, 1));

                            if (opt3 == 4)
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Durian"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Durian");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Durain", true, 1));

                            if (opt3 == 5)
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Ube"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Ube");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Ube", true, 1));

                            if (opt3 == 6)
                                // Check if the flavour is already in the list
                                if (icecream.Flavours.Any(flavour => flavour.Type == "Sea Salt"))
                                {
                                    // Get the index of the flavour
                                    int index = icecream.Flavours.FindIndex(flavour => flavour.Type == "Sea Salt");
                                    icecream.Flavours[index].Quantity += 1;
                                }
                                else
                                    // Add the flavour to the list
                                    icecream.Flavours.Add(new Flavour("Sea Salt", true, 1));
                        }
                        break;

                    case 4:
                        if (scooped)
                        {
                            Console.WriteLine($"Created {icecream.Option}");
                            customer.CurrentOrder.AddIceCream(icecream);
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

        static void DeleteAnIceCream(Customer customer)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
            while (true)
            {
                List<IceCream> currentorder = customer.CurrentOrder.IceCreamList;
                for (int i = 0; i < currentorder.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentorder[i].Option}");
                }
                Console.WriteLine("0. Exit");
                try
                {
                    Console.Write("Enter Icecream to delete (0 to exit): ");
                    int icecreamOption = Convert.ToInt32(Console.ReadLine());

                    if (icecreamOption == 0)
                        break;

                    Console.Write($"You are deleting {currentorder[icecreamOption - 1].Option} (y/n): ");
                    string confirm = Console.ReadLine();
                    if (confirm.ToLower() == "n")
                        continue;
                    else
                    {
                        customer.CurrentOrder.DeleteIceCream(icecreamOption);
                        Console.WriteLine($"Successfully Deleted({currentorder[icecreamOption - 1].Option})");
                        break;
                    }
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
        }

        static void ProcessOrderAndCheckout(Queue<Order> goldQueue, Queue<Order> regularQueue, List<Customer> customers)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            //dequeue the first order to come in both queues
            Order currentOrder;

            if (goldQueue.Count > 0)
            {
                currentOrder = goldQueue.Dequeue();
            }
            else if (regularQueue.Count > 0)
            {
                currentOrder = regularQueue.Dequeue();
            }
            else
            {
                Console.WriteLine("No orders in the queue.");
                return;
            }

            // display ice creams in the order
            Console.WriteLine("Ice Creams in the Order:");
            foreach (IceCream iceCream in currentOrder.IceCreamList)
            {
                Console.WriteLine(iceCream.ToString());
            }

            // calculate and display the total bill amount
            double totalBill = currentOrder.CalculateTotal();
            Console.WriteLine($"Total Bill Amount: ${totalBill}");


            // display customer's membership status and points
            Customer customer = customers.Find(c => c.CurrentOrder == currentOrder);

            if (customer != null)
            {
                Console.WriteLine($"Membership Status: {customer.Rewards.Tier}");
                Console.WriteLine($"Points: {customer.Rewards.Points}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }

            // check if it's customer's birthday
            if (customer.IsBirthday())
            {
                // calculate the final bill with the most expensive ice cream costing $0
                totalBill = CalculateBillForBirthday(currentOrder, true);
            }

            // check if the customer has enough punch cards for discount
            if (customer.Rewards.PunchCards >= 10)
            {
                // calculate final bill with the first ice cream costing $0
                totalBill -= currentOrder.IceCreamList.First().CalculatePrice();

                customer.Rewards.PunchCards = 0;
            }

            // check if customer has any points to redeem
            if (customer.Rewards.Points > 0)
            {
                if (customer.Rewards.Tier == "Silver" || customer.Rewards.Tier == "Gold")
                {
                    int pointsToRedeem;

                    while (true)
                    {
                        Console.Write($"How many points do you want to use to offset the bill? (1 point = $0.02, available points: {customer.Rewards.Points}): ");
                        string input = Console.ReadLine();

                        try
                        {
                            pointsToRedeem = Convert.ToInt32(input);

                            if (pointsToRedeem >= 0 && pointsToRedeem <= customer.Rewards.Points)
                            {
                                // redeem points
                                int actualPointsToRedeem = pointsToRedeem;
                                totalBill -= actualPointsToRedeem * 0.02;
                                customer.Rewards.RedeemPoints(actualPointsToRedeem);
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Please enter a non-negative integer equal to or less than the available points ({customer.Rewards.Points}).");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a valid integer for the points.");
                        }
                    }
                }
            }

            // display the final total bill
            Console.WriteLine($"Final Total Bill: ${totalBill}");

            foreach (IceCream iceCream in currentOrder.IceCreamList)
            {
                if (customer.Rewards.PunchCards < 10)
                {
                    customer.Rewards.Punch();
                }
            }

            // earn points and upgrade membership status 
            double pointsEarned = Math.Floor(totalBill * 0.72);
            customer.Rewards.AddPoints((int)pointsEarned);

            // set the fulfillment time for order
            currentOrder.TimeFulfilled = DateTime.Now;

            // add order to the customer's order history and reset current order
            customer.OrderHistory.Add(currentOrder);
            customer.CurrentOrder = null;

            // update customer info in the customers.csv
            UpdateCustomerCSV(customers);

        }

        static void DisplayMonthlyCharges(List<Customer> customers)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
            Console.WriteLine("Displaying monthly charges breakdown and total charged amounts for the year");
            Console.WriteLine("===========================================================================");

            Dictionary<int, double> monthlyCharges = new Dictionary<int, double>();
            for (int i = 1; i <= 12; i++)
            {
                monthlyCharges.Add(i, 0);
            }
            try
            {
                Console.Write("Enter the year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                foreach (Customer customer in customers)
                {
                    foreach (Order order in customer.OrderHistory)
                    {
                        // Check if the order is fulfilled in the year
                        int? isityear = order.TimeFulfilled?.Year;
                        if (isityear.HasValue)
                        {
                            if (isityear == year)
                            {
                                // Check if the order is fulfilled in the month
                                int? isitmonth = order.TimeFulfilled?.Month;
                                if (isitmonth.HasValue)
                                {
                                    // Add the total price of the order to the monthly charges
                                    monthlyCharges[(int)isitmonth] += order.CalculateTotal();
                                }
                            }
                        }
                    }
                }
                for (int i = 1; i <= 12; i++)
                {
                    // Display the monthly charges
                    Console.WriteLine($"{new DateTime(1, i, 1).ToString("MMM")} {year} : \t${monthlyCharges[i].ToString("F2")}");
                }
                Console.WriteLine($"\nTotal : \t${monthlyCharges.Values.Sum().ToString("F2")}");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid Year! Integer only!");
            }
        }

        static double CalculateBillForBirthday(Order currentOrder, bool isBirthday)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            // Check if it's the customer's birthday or if there are no ice creams in order
            if (!isBirthday || currentOrder.IceCreamList.Count == 0)
            {
                return currentOrder.CalculateTotal();
            }

            // make the most expensive ice cream as first ice cream in order
            IceCream mostExpensiveIceCream = currentOrder.IceCreamList[0];

            // find the most expensive ice cream in the order
            foreach (IceCream iceCream in currentOrder.IceCreamList)
            {
                if (iceCream.CalculatePrice() > mostExpensiveIceCream.CalculatePrice())
                {
                    mostExpensiveIceCream = iceCream;
                }
            }


            double discount = mostExpensiveIceCream.CalculatePrice();
            double finalBill = currentOrder.CalculateTotal() - discount;

            Console.WriteLine($"Birthday discount applied: ${discount} (Most expensive item: {mostExpensiveIceCream})");

            return finalBill;
        }

        static void PrintIceCreams(List<IceCream> icl)
        {
            //==========================================================
            // Student Number : S10257176H
            // Student Name : Araki Yeo
            //==========================================================
            int x = 1;
            foreach (IceCream ice in icl)
            {
                Console.WriteLine($"\t\t{x} Options: " + ice.Option);
                Console.WriteLine("\t\tScoops: " + ice.Scoops);
                Console.WriteLine("\t\tFlavours: ");
                foreach (Flavour flav in ice.Flavours)
                {
                    Console.Write("\t\t\t" + flav.Type);
                    Console.WriteLine(" " + flav.Quantity);
                }
                Console.WriteLine("\t\tToppings: ");
                foreach (Topping top in ice.Toppings)
                {
                    Console.WriteLine("\t\t\t" + top.Type);
                }
                x++;

                if (ice.Option.ToLower() == "cone")
                {
                    Cone cone = (Cone)ice;
                    Console.WriteLine($"\t\tDipped: {cone.Dipped}");
                }
                if (ice.Option.ToLower() == "waffle")
                {
                    Waffle waffle = (Waffle)ice;
                    Console.WriteLine($"\t\tWaffle Flavour: {waffle.WaffleFlavour}");
                }
            }
        }

        static void ReadFileToppings(Dictionary<string, double> toppings)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================
            string[] lines = File.ReadAllLines("toppings.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                string name = Convert.ToString(data[0]);
                double cost = Convert.ToDouble(data[1]);

                toppings.Add(name, cost);

            }


        }

        static void ReadFileFlavours(Dictionary<string, double> flavours)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================
            string[] lines = File.ReadAllLines("flavours.csv");

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
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

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

        static void UpdateCustomerCSV(List<Customer> customers)
        {
            //==========================================================
            // Student Number : S10262171E
            // Student Name : Benjamin Hwang
            //==========================================================

            string[] lines = File.ReadAllLines("customers.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                int id = Convert.ToInt32(data[1]);

                //find corresponding customer in the list based on memberId.
                Customer customer = customers.Find(c => c.MemberId == id);

                // if customer is found, update points and punch cards
                if (customer != null)
                {
                    data[4] = customer.Rewards.Points.ToString();
                    data[5] = customer.Rewards.PunchCards.ToString();

                    // update the line in data 
                    lines[i] = string.Join(",", data);
                }
            }

            // write the updated lines to the customers.csv
            File.WriteAllLines("customers.csv", lines);
        }

    }
}