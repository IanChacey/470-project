using System;
using System.Data;
using System.Configuration;
using System.Collections;
using MySql.Data.MySqlClient;
//using "DatabaseConnection"

namespace MySQL_test
{
    //This class is for gathering a database connection from the app.config file
    class DatabaseConnection
    {
        static MySqlConnection databaseConnection = null;
        public MySqlConnection getDBConnection(string connectionName)
        {
            if (databaseConnection == null)
            {
                string connString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                databaseConnection = new MySqlConnection(connString);
            }
            return databaseConnection;
        }
    }

    //This class runs the majority of the application, all functionality between MySql and visual studio
    class Program
    {
        static MySqlConnection conn = connect();

        static MySqlConnection connect()
        {
            DatabaseConnection connection = new DatabaseConnection();
            return connection.getDBConnection("testAccount");
        }

        //Employee submenu
        static void employeeMenu(MySqlConnection conn)
        {
            string choice = "0";

            
            
            while (choice != "4")
            {
                Console.WriteLine("Employee Menu:");
                Console.WriteLine("1. Return all customer information");
                Console.WriteLine("2. Return all stores and their information");
                Console.WriteLine("3. View all products sold");
                Console.WriteLine("4. Go Back");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        string query = "SELECT CustomerID, firstname, lastname, email, rewardlevel FROM customer;";
                        printData(query, conn, "customer");
                        break;
                    case "2":
                        string query2 = "SELECT * FROM store";
                        printData(query2, conn, "store");
                        break;
                    case "3":
                        string query3 = "SELECT * FROM product";
                        printData(query3, conn, "product");
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Invalid input please try again");
                        break;
                        
                }
            }
        }

        static int getCountData(string countQuery, string table)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(countQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, table);
            DataTable dt = ds.Tables[table];

            int count = 0;

            //conn.Open();
            foreach(DataRow row in dt.Rows)
            {
                foreach(DataColumn col in dt.Columns)
                {
                    count = int.Parse(row[col].ToString());
                }
            }

                
            return count;
        }

        static void insertSale(string productName, string location, int storeid)
        {
            try
            {
                //MySqlConnection myconn2 = conn;

                int newID = getCountData("SELECT COUNT(ProductSaleID) FROM product", "product");

                int productSaleID = newID + 1;

                

                string newProductName = "\"" + productName + "\"";
                string newLocation = "\"" + location + "\"";
                string query = "INSERT INTO product(ProductSaleID, productname, location, storetype, amount, storeid) values (" + productSaleID + ", " + newProductName + ", " + newLocation + ", " + "\"Online\"" + ", " + 23 + ", " + storeid + ");";
                MySqlCommand myCommand = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void insertCustomer(string email, string firstname, string lastname, string password)
        {
            try
            {
                int customerID = getCountData("SELECT COUNT(customerID) FROM customer", "customer") + 1;
                string newEmail = "\"" + email + "\"";
                string newFirst = "\"" + firstname + "\"";
                string newLast = "\"" + lastname + "\"";
                string newPass = "\"" + password + "\"";
                //string expiration = "''2022-04-09'";
                string query = "INSERT INTO customer(customerID, email, firstname, lastname, rewardlevel, rewardpoints, accpassword) values (" + customerID + ", " + newEmail + ", " + newFirst + ", " + newLast + ", " + 0 + ", " + 0 + ", " + newPass + ");";
                MySqlCommand myCommand = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void itemMenu(MySqlConnection conn, string location, int storeid)
        {
            Console.WriteLine("Our Menu:\n 1. Hot Coffee \n 2. Hot Tea \n 3. Hot Chocolate \n 4. Iced Coffee");

            string menuChoice = Console.ReadLine();
            Random rd = new Random();

            switch (menuChoice)
            {

                case "1":
                    Console.WriteLine("Hot Coffee: \n 1. Espresso \n 2. Cappuccino \n 3. Dark Roast \n 4. Blonde Roast");
                    string hotChoice = Console.ReadLine();

                    switch (hotChoice)
                    {
                        case "1":
                            insertSale("Espresso", location, storeid);
                            break;

                        case "2":
                            insertSale("Cappuccino", location, storeid);
                            break;

                        case "3":
                            insertSale("Dark Roast", location, storeid);
                            break;
                        case "4":
                            insertSale("Blonde Roast", location, storeid);
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again");
                            break;
                    }
                    break;

                case "2":
                    Console.WriteLine("Hot Tea: \n 1. Chai Tea, \n 2. Chai Latte");
                    string teaChoice = Console.ReadLine();
                    switch (teaChoice)
                    {
                        case "1":
                            insertSale("Chai Tea", location, storeid);
                            break;
                        case "2":
                            insertSale("Chai Latte", location, storeid);
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again");
                            break;
                    }
                    break;

                case "3":
                    Console.WriteLine("Hot Chocolates: \n 1. Hot Chocolate \n 2. White Hot Chocolate");
                    string chocChoice = Console.ReadLine();
                    switch (chocChoice)
                    {
                        case "1":
                            insertSale("Hot Chocolate", location, storeid);
                            break;
                        case "2":
                            insertSale("White Hot Chocolate", location, storeid);
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again");
                            break;
                    }
                    break;

                case "4":
                    Console.WriteLine("Iced Teas: \n 1. Iced Chai Latte \n 2. Iced Black Tea");
                    string iceChoice = Console.ReadLine();
                    switch (iceChoice)
                    {
                        case "1":
                            insertSale("Iced Chai Latte", location, storeid);
                            break;
                        case "2":
                            insertSale("Iced Black Tea", location, storeid);
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again");
                            break;
                    }
                    break;
            }
        }

        static void customerMenu(MySqlConnection conn, string email)
        {


            string choice = "0";

            while (choice != "4")
            {
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. View our Menu");
                Console.WriteLine("2. Return all stores and their information");
                Console.WriteLine("3. View Reward Information");
                Console.WriteLine("4. Go Back");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        itemMenu(conn, "Online", 100);
                        break;
                    case "2":
                        string query2 = "SELECT * FROM store";
                        printData(query2, conn, "store");
                        break;
                    case "3":
                        string query = "SELECT firstname, lastname, rewardlevel, rewardpoints FROM customer WHERE email = \"" + email + "\"";
                        printData(query, conn, "customer");
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again");
                        break;
                }
            }
        }

        static void employeeLogin(MySqlConnection conn)
        {
            string eid;
            Console.WriteLine("Type in your employee id");


            eid = Console.ReadLine();

            string password;
            Console.WriteLine("Enter your password:");
            password = Console.ReadLine();

            string query = "SELECT * FROM employee WHERE EmployeeID = " + "\"" + eid + "\"";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            DataTable dt = ds.Tables["employee"];

            string query2 = "SELECT * FROM employee WHERE accpassword = " + "\"" + password + "\"";
            MySqlDataAdapter da2 = new MySqlDataAdapter(query, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "employee");
            DataTable dt2 = ds2.Tables["employee"];

            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                employeeMenu(conn);
            else
                Console.WriteLine("invalid employee id or password");
            
        }

        static void customerLogin(MySqlConnection conn)
        {
            string email;
            Console.WriteLine("Type in your email");


            email = Console.ReadLine();

            string password;
            Console.WriteLine("Enter your password:");
            password = Console.ReadLine();

            //foreach ()
            string query = "SELECT * FROM customer WHERE email = " + "\"" + email + "\"";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "customer");
            DataTable dt = ds.Tables["customer"];

            string query2 = "SELECT * FROM customer WHERE accpassword = " + "\"" + password + "\"";
            MySqlDataAdapter da2 = new MySqlDataAdapter(query, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "customer");
            DataTable dt2 = ds2.Tables["customer"];

            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                customerMenu(conn, email);
            else
                Console.WriteLine("invalid email or password");
        }

        static void printData(string query, MySqlConnection conn, string table)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, table);
            DataTable dt = ds.Tables[table];

            Console.Write("\n");

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(col.ColumnName + ": " + row[col].ToString().PadRight(5) + "\t");
                }

                Console.Write("\n");
            }
            Console.Write("\n");
        }
        static void Main(string[] args)
        {
            string input = "0";

            try
            {
                conn.Open();
                while (input != "5")
                {
                    

                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1. Make an account");
                    Console.WriteLine("2. List Locations");
                    Console.WriteLine("3. Customer Login");
                    Console.WriteLine("4. Employee Login");
                    Console.WriteLine("5. Exit");


                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("What is your email?");
                            string email = Console.ReadLine();
                            Console.WriteLine("What is your first name?");
                            string first = Console.ReadLine();
                            Console.WriteLine("What is your last name?");
                            string last = Console.ReadLine();
                            Console.WriteLine("What should be your password?");
                            string password = Console.ReadLine();
                            insertCustomer(email, first, last, password);
                            break;
                        case "2":
                            string query2 = "SELECT * FROM store";
                            printData(query2, conn, "store");
                            break;
                        case "3":
                            customerLogin(conn);
                            break;
                        case "4":
                            employeeLogin(conn);
                            break;
                        case "5":
                            break;
                        default:
                            Console.WriteLine("Invalid input, please try again");
                            break;
                    }
                }    
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}