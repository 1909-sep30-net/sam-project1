using System;
using NLog;

namespace GStoreApp.ConsoleApp
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            int mainMenu = 0;
            Menu m = new Menu();

            /// <summary>
            /// Main Menu of the UI.
            /// </summary>
            do
            {
                Console.WriteLine("Welcome to GCStore!");
                Console.WriteLine("How can I help you today?");
                Console.WriteLine("1. Place Order");
                Console.WriteLine("2. Display Details of a Previous Order by Order Id");
                Console.WriteLine("3. Display All History Order by Store");
                Console.WriteLine("4. Display All History Order by Customer");
                Console.WriteLine("0. Exit");
                Console.WriteLine("---------------------");

                try
                {
                    mainMenu = Int32.Parse(Console.ReadLine());
                    if ( mainMenu < 0 || mainMenu > 4)
                    {
                        logger.Warn("(Main menu)Invalid input number.");
                    }
                }
                catch( FormatException ex )
                {
                    logger.Error("(Main menu)Invalid input format:  " + ex.Message);
                    mainMenu = m.InputCheckInt(- 1, 1);
                }

                switch (mainMenu)
                {
                    case 1:
                        m.CustomerMenu();
                        break;
                    case 2:
                        m.SearchOrder();
                        break;
                    case 3:
                        m.SearchByStore();
                        break;
                    case 4:
                        m.SearchByCustomer();
                        break;
                    default:
                        break;
                }
            } while (mainMenu != 0);
        }
    }
}
