using System;
using System.Collections.Generic;
using System.Text;
using GStoreApp;
using System.Linq;
using DB.Repo;
using GStoreApp.Library;
using System.Text.RegularExpressions;
using NLog;


namespace GStoreApp.ConsoleApp
{
    public class Menu
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Customer menu include searching current customer and adding new customer
        /// After that will go to placing order menu.
        /// </summary>
        public void CustomerMenu()
        {
            int poMenu = 0;
            Console.WriteLine("If you are a new customer, press 1");
            Console.WriteLine("to add new customer.");
            Console.WriteLine("Or press 2 to search your name."); ;
            Console.WriteLine("--------");
            Console.WriteLine("press 0 to back to Mainmenu.");
            Console.WriteLine("----------------------");
            Console.WriteLine("Please Enter: ");
            try
            {
                poMenu = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                logger.Error("(Customer Menu)Invalid input format  " + ex.Message);
                poMenu = InputCheckInt( -1, 2 );
            }

            switch (poMenu)
            {
                ///<summary>
                ///Function to add new customer, then pass it to data access project
                ///include firtname, last name, phone number and favorite store.
                ///</summary>
                case 1:
                    Console.WriteLine("Please Enter your first name:");
                    string fNameInput = Console.ReadLine();
                    string fName = NameFormat(fNameInput);
                    Console.WriteLine("Please Enter your last name: ");
                    string lNameInput = Console.ReadLine();
                    string lName = NameFormat(lNameInput);
                    Console.WriteLine("Please enter your 10 digit phone number ");
                    Console.WriteLine("without () or - :   ");
                    string phone = Console.ReadLine();
                    phone = PhoneCheck(phone);
                    Console.WriteLine("Please enter your default store: ");
                    int favStore = 0;
                    try
                    {
                        favStore = Int32.Parse(Console.ReadLine());
                    }
                    catch ( FormatException ex )
                    {
                        logger.Error("(New Customer)Invalid input format: " + ex.Message);
                        favStore = InputCheckInt(favStore, 999999);
                    }
                    Repo newGuys = new Repo();
                    Store storeFound = newGuys.CheckIfStoreExists( favStore );
                    if (storeFound != null)
                    {
                        Console.WriteLine($"Your first name is     {fName}");
                        Console.WriteLine($"Your last name is      {lName}");
                        Console.WriteLine($"Your phone number is   {phone}");
                        Console.WriteLine($"Your favorite Store is {storeFound.StoreName}");
                        Console.WriteLine($"Is That correct?(y/n):   ");
                        
                        string confirm = "";
                        bool confirmCheck;

                        do
                        {
                            confirm = Console.ReadLine();
                            confirmCheck = confirm != "n" && confirm != "y";
                            if (confirmCheck)
                            {
                                Console.WriteLine("The input must be y or n");
                                Console.WriteLine("Plese type again(y/n):  ");
                                logger.Warn($"(New Customer)Invalid Input:  {confirmCheck}");
                            }
                        } while (confirmCheck);

                        if (confirm == "y")
                        {
                            Customer customerNew = new Customer(fName, lName, phone, favStore);
                            newGuys.AddCustomer(customerNew);
                            logger.Info("New Customer is added into database.");
                            Console.WriteLine("");
                            //PlaceOrder(customerNew, newGuys);
                        }

                    } else
                    {
                        Console.WriteLine("Sorry! The Store ID doesn't not exist.");
                        Console.WriteLine("Please press Enter to back to main menu.");
                        string back = Console.ReadLine();
                    }
                    break;

                ///<summary>
                ///Function to search a existed customer
                ///Via his/her first name and last name
                ///then pass them to data access project to get the result
                ///</summary>
                ///<remark>
                ///if the result is more then 1, add new input(phone number or id to get the exact one.
                ///</remark>
                case 2:
                    Console.WriteLine("Please Enter your first name:");
                    fNameInput = Console.ReadLine();
                    fName = NameFormat(fNameInput);
                    Console.WriteLine("");
                    Console.WriteLine("Please Enter your last name: ");
                    lNameInput = Console.ReadLine();
                    lName = NameFormat(lNameInput);
                    Console.WriteLine("");

                    Customer customerOld = new Customer(fName, lName);

                    Repo oldGuys = new Repo();
                    List<Customer> customerFound = oldGuys.SearchCustomer(customerOld).ToList();
                    if (customerFound.Count > 0 )
                    {
                        for (int i = 0; i < customerFound.Count; i++)
                        {
                            Console.WriteLine($"Your First Name is:     {customerFound[i].FirstName}");
                            Console.WriteLine($"Your Last Name is:      {customerFound[i].LastName}");
                            Console.WriteLine($"Your Phone Number is:   {customerFound[i].PhoneNumber}");
                            Console.WriteLine($"Your Favorite Store is: {customerFound[i].FavoriteStore}");
                            Console.WriteLine("----------");
                            Console.WriteLine($"Is that correct?(y/n)");

                            string confirm = "";
                            bool confirmCheck;

                            do
                            {
                                confirm = Console.ReadLine();
                                confirmCheck = confirm != "n" && confirm != "y";
                                if (confirmCheck)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("The input must be y or n");
                                    Console.WriteLine("Plese type again(y/n):  ");
                                    logger.Warn($"(Existed Customer)Invalid Input:  {confirmCheck}");
                                }
                            } while (confirmCheck);

                            if (confirm == "y")
                            {
                                PlaceOrder(customerFound[i], oldGuys);
                                break;
                            }
                        }
                    } else
                    {
                        Console.WriteLine("Sorry! We don't have your record.");
                        Console.WriteLine("Press enter to back to main menu");
                        string back = Console.ReadLine();
                    }
                    break;

                default:
                    break;
            }

            Console.Clear();
        }

        /// <summary>
        /// Function to place an order from user input
        /// via N number's string, we can place an order for multiple products
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="repo"></param>
        public void PlaceOrder( Customer customer, Repo repo )
        {

            Product nsP = repo.SearchProduct(1);
            Product xbP = repo.SearchProduct(2);
            Product psP = repo.SearchProduct(3);
            Console.WriteLine("");
            Console.WriteLine("Here is our menu today: ");
            Console.WriteLine($"{nsP.ProductName,-0:G}: ${nsP.UnitPrice, -50}");
            Console.WriteLine($"{xbP.ProductName,-0:G}: ${xbP.UnitPrice, -50}");
            Console.WriteLine($"{psP.ProductName,-0:G}: ${psP.UnitPrice, -50}");
            Console.WriteLine("If you want NS*1, Xbox*1, PS4*1,");
            Console.WriteLine("Please Enter 111");
            Console.WriteLine("If you want Xbox*1, PS*1");
            Console.WriteLine("Please Enter 011");
            Console.WriteLine("Enter 000 to cancel your order and back to main menu.");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Please Enter you order: ");

            string order;
            double a = -1;
            double b = -1;
            double c = -1;
            bool amountCheck = a == -1 || b == -1 || c == -1;
            do
            {
                order = Console.ReadLine();
                if (order.Length == 3)
                {
                    a = Char.GetNumericValue(order[0]);
                    b = Char.GetNumericValue(order[1]);
                    c = Char.GetNumericValue(order[2]);
                    amountCheck = a == -1 || b == -1 || c == -1;
                    // Console.WriteLine($"\n{a} {b} {c}\n");
                    if (order == "000")
                    {
                        break;
                    }
                    else if (amountCheck)
                    {
                        Console.WriteLine("The Order must be 3 numbers.");
                        Console.WriteLine("Please type again");
                        Console.WriteLine("Or 000 to back to main menu:  ");
                        logger.Warn("(placing order)Invalid input format");
                    }
                } else
                {
                    Console.WriteLine("The Order must be 3 numbers.");
                    Console.WriteLine("Please type again");
                    Console.WriteLine("Or 000 to back to main menu:  ");
                    logger.Warn("(placing order)Invalid length Input");
                }
            } while ( amountCheck );



            if (order != "000")
            {
                int ns = Int32.Parse(order[0].ToString());
                int xb = Int32.Parse(order[1].ToString());
                int ps = Int32.Parse(order[2].ToString());
                decimal totalPrice = (ns * nsP.UnitPrice + xb * xbP.UnitPrice + ps * psP.UnitPrice);

                Console.WriteLine("\nYour Order: ");
                Console.WriteLine($"    Customer Name:    {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"  Nintendo Switch:    {ns}");
                Console.WriteLine($"         XBox One:    {xb}");
                Console.WriteLine($"Playstation 4 Pro:    {ps}");
                Console.WriteLine($"Total price is:      ${totalPrice}");
                Console.WriteLine($"Is that correct?(y/n)");

                string confirm = "";
                bool confirmCheck;
                int storeId = (int)customer.FavoriteStore;
                do
                {
                    confirm = Console.ReadLine();
                    confirmCheck = confirm != "n" && confirm != "y";
                    if (confirmCheck)
                    {
                        Console.WriteLine("The input must be y or n");
                        Console.WriteLine("Plese type again(y/n):  ");
                        logger.Warn($"(placing order)Invalid Input:  {confirmCheck}");
                    }
                } while (confirmCheck);

                if (confirm == "y")
                {
                    Order newOrder = new Order(customer, ns, xb, ps, DateTime.Today, totalPrice, storeId);
                    string success = repo.OrderPlaced(newOrder);
                    Console.WriteLine(success);
                    Console.WriteLine("Press Enter to continue");
                    string back = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Search past order detail by input order id
        /// then pass it to data access project to get the result
        /// </summary>
        public void SearchOrder()
        {
            int orderId = 0;
            Console.WriteLine("Please Enter your order number: ");
            try
            {
                orderId = Int32.Parse(Console.ReadLine());
            }
            catch ( FormatException ex )
            {
                logger.Error("(search past order)Invalid input format: " + ex.Message);
                orderId = InputCheckInt(orderId, 999999);
            }
            Repo search = new Repo();
            if (search.SearchPastOrder(orderId) != null)
            {
                int StoreId = search.SearchPastOrder(orderId).StoreId;
                int customerId = search.SearchPastOrder(orderId).CustomerId;
                DateTime orderDate = search.SearchPastOrder(orderId).OrderDate;
                decimal totalPrice = search.SearchPastOrder(orderId).TotalPrice;

                Console.Clear();
                Console.WriteLine("Your Order Detail");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Order ID:     {orderId}");
                Console.WriteLine($"Store ID:     {StoreId}");
                Console.WriteLine($"Costomer ID:  {customerId}");
                Console.WriteLine($"Order Date:   {orderDate}");
                Console.WriteLine($"Total Price:  {totalPrice}");

                List<OrderItem> items = search.SearchPastOrderItem(orderId).ToList();
                for (int i = 0; i < items.Count(); i++)
                {
                    Console.WriteLine($"{items[i].ProductName}:  {items[i].Amount}");
                }
            } else
            {
                Console.WriteLine("Sorry! We cannot find your record.");
                logger.Info($"There no record for Order ID: {orderId}");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Search and display all order overview for specific by store id
        /// then pass it to data access project and get the result
        /// </summary>
        public void SearchByStore()
        {
            int storeId =0;
            Console.WriteLine("Please Enter StoreId");
            try
            {
                storeId = Int32.Parse(Console.ReadLine());
            }
            catch ( FormatException ex )
            {
                logger.Error("(order history by storeID)Invalid input format: " + ex.Message);
                storeId = InputCheckInt(storeId, 999999);
            }
             
            Repo search = new Repo();
            List<OrderOverView> history = search.DisplayOrderByStore(storeId).ToList();
            if ( history.Count() != 0)
            {
                int oId;
                int cId;
                DateTime date;
                decimal p;
                Console.Clear();
                Console.WriteLine($"Here is Order History at store with Store ID: {storeId}");
                Console.WriteLine("OrderId, CustomerId, OrderDaTe,          TotalPrice");
                for( int i = 0; i < history.Count(); i++)
                {
                    oId = history[i].OrderId;
                    cId = history[i].CustomerId;
                    date = history[i].OrderDate;
                    p = history[i].TotalPrice;
                    Console.WriteLine($"{oId}        {cId}           {date}   {p}");
                }
            } else
            {
                Console.WriteLine("Sorry! There's no record of this store ID.");
                logger.Info($"There no record for Store ID: {storeId}");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("Press enter to continue.");
            string stop = Console.ReadLine();
        }

        /// <summary>
        /// search and display order overview for specific customer by customer id
        /// then pass it into data access porject and get the result
        /// </summary>
        public void SearchByCustomer()
        {
            Console.WriteLine("Please Enter the Customer ID:");
            int customerId = 0;
            try
            {
                customerId = Int32.Parse(Console.ReadLine());
            }
            catch ( FormatException ex )
            {
                logger.Error("(order history by customer id)Invalid input format: " + ex.Message);
                customerId = InputCheckInt(customerId, 999999);
            }
            Repo search = new Repo();
            List<OrderOverView> history = search.DisplayOrderByCustomer(customerId).ToList();

            if (history.Count() != 0)
            {
                int oId;
                int cId;
                DateTime date;
                decimal p;
                Console.Clear();
                Console.WriteLine($"Here is Order History at store with Customer ID: {customerId}");
                Console.WriteLine("OrderId, StoreId, OrderDaTe,          TotalPrice");
                for (int i = 0; i < history.Count(); i++)
                {
                    oId = history[i].OrderId;
                    cId = history[i].StoreId;
                    date = history[i].OrderDate;
                    p = history[i].TotalPrice;
                    Console.WriteLine($"{oId}        {cId}        {date}   {p}");
                }
            }
            else
            {
                Console.WriteLine("Sorry! There's no record of this customer ID.");
                logger.Info($"There no record for Customer ID: {customerId}");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("Press enter to continue.");
            string stop = Console.ReadLine();
        }

        /// <summary>
        /// If UI input should be a number, check the input format and range here
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns>will return correct input</returns>
        public int InputCheckInt ( int input , int menuType )
        {
            int finalInput = input;
            int menuMaxOption = 0;
            
            if ( menuType == 1 )
            {
                menuMaxOption = 4;
            } else if ( menuType == 2 ){
                menuMaxOption = 2;
            } else
            {
                menuMaxOption = 999999;
            }

            while (finalInput < 0 || finalInput > menuMaxOption)
            {
                Console.WriteLine($"Input must be between 0 to {menuMaxOption}");
                Console.WriteLine($"Please Enter Your Answer(0-{menuMaxOption}):  ");

                try
                {
                    finalInput = Int32.Parse(Console.ReadLine());
                    if (finalInput < 0 || finalInput > menuMaxOption)
                    {
                        logger.Warn("(input check)Input value is Invalid.");
                    }
                }
                catch (FormatException ex)
                {
                    
                    logger.Error($"(input check)Input format is invalid.  {ex.Message}");
                }

            };

            return finalInput;
        }

        /// <summary>
        /// Function to check phone number
        /// via take out all non-numeric value, then check the length
        /// finally, add () and - into phone number to adjust to format
        /// </summary>
        /// <returns></returns>
        public string PhoneCheck( string phone )
        {
            string phoneOp = Regex.Replace(phone, @"[^0-9]+", "");
            while (phoneOp.Length != 10)
            {
                Console.WriteLine("The input must be 10 digit number");
                logger.Warn("The input of phone number is wrong.");
                Console.WriteLine("Please type again:  ");
                phoneOp = Console.ReadLine();
                phoneOp = Regex.Replace(phoneOp, @"[^0-9]+", "");

            };

            phoneOp = "(" + phoneOp.Substring(0, 3) + ")" + phoneOp.Substring(3, 3)
                    + "-" + phoneOp.Substring(6, 4);
            return phoneOp;
        }
        /// <summary>
        /// Function to handle users' input on their name
        /// and fix its format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string NameFormat( string name )
        {
            name = Regex.Replace(name, @"[^A-z]+", "");
            string outputName = name.Substring(0, 1).ToUpper()
                              + name.Substring(1, name.Length - 1 ).ToLower();

            return outputName;
        }
    }
}
