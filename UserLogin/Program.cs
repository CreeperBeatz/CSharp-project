using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string username;
            string password;
            Logger.CreateFileIfNotExists();
            Logger.RemoveBeforeDate(DateTime.Today.AddDays(-1));

            Console.WriteLine("Please enter an Username: ");
            username = Console.ReadLine();
            Console.WriteLine("Please enter a Password: ");
            password = Console.ReadLine();

            // User user = new User("Admin", "123456", "121219107", 1);
            LoginValidation validator = new LoginValidation(username, password, Log);

            User current_user;

            if (validator.ValidateUserInput(out current_user))
            {
                DisplayUserRole(current_user.user_role);
                switch (current_user.user_role)
                {
                    case UserRoles.ADMIN:
                        while (true) { AdminFlow(); }
                    default:
                        break;
                }

            }
        }

        public static void Log(string line)
        {
            Console.WriteLine(line);
        }

        public static void DisplayUserRole(UserRoles role)
        {
            switch (LoginValidation.currentUserRole)
            {
                case UserRoles.STUDENT:
                    Console.WriteLine("Student role");
                    break;
                case UserRoles.ADMIN:
                    Console.WriteLine("Administrator role");
                    break;
                case UserRoles.PROFESSOR:
                    Console.WriteLine("Proffesor role");
                    break;
                case UserRoles.INSPECTOR:
                    Console.WriteLine("Inspector role");
                    break;
                case UserRoles.ANONYMOUS:
                    Console.WriteLine("Anonymous role");
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }

        public static void AdminFlow()
        {
            string username;

            try
            {
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Change user role");
                Console.WriteLine("3. Change user expiration date");
                Console.WriteLine("4. Print all users");
                Console.WriteLine("5. Print log file");
                Console.WriteLine("6. Print current activities");
                int userChoise = Int32.Parse(Console.ReadLine());

                switch (userChoise)
                {
                    case 1:
                        Environment.Exit(1);
                        break;
                    case 2:
                        Console.WriteLine("username:");
                        username = Console.ReadLine();
                        try
                        {
                            UserRoles role = GetUserRole();
                            UserData.AssignUserRole(username, role);
                        } catch (FormatException)
                        {
                            Console.WriteLine("Please input only integers!");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Username: ");
                        username = Console.ReadLine();
                        try
                        {
                            DateTime date = GetUserDateTime();
                            UserData.SetUserActiveTo("username", date);
                        } catch (FormatException)
                        {
                            Console.WriteLine("Please input only integers!");
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        PrintLogFile();
                        break;
                    case 6:
                        PrintCurrentLog();
                        break;
                    default:
                        Console.WriteLine("Sorry, your input was not inderstood!");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter nubmers only!");
            }
        }

        private static void Exit(int v)
        {
            throw new NotImplementedException();
        }

        public static void PrintCurrentLog()
        {
            foreach (string line in Logger.currentSessionActivities)
            {
                Console.WriteLine(line);
            }
        }

        public static void PrintLogFile()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(Logger.log_path))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }


        ///<summary>
        ///Uses Console to get use input
        ///Throws FormatException if user inputs a non-integer
        ///</summary>
        public static DateTime GetUserDateTime()
        {
            
            int year, month, day;

            Console.WriteLine("Enter year");
            year = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enterr month");
            month = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter day");
            day = Int32.Parse(Console.ReadLine());

            return new DateTime(year, month, day);

        }

        ///<summary>
        ///Throws FormatException if user inputs a non-integer
        /// </summary>
        public static UserRoles GetUserRole()
        {
            var values = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>();
            Console.WriteLine("Choose an Enum:");

            int i = 0;
            foreach (var value in values)
            {
                Console.WriteLine($"{i}. {value}");
                i++;
            }

            int user_choise = Int32.Parse(Console.ReadLine());

            return (UserRoles)user_choise;
        }
    }
}
