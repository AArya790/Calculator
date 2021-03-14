using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Cal_Data_Core.Model;

namespace Cal_Data
{
    class Calculator
    {
        public static void LandingPage()
        {
            Console.WriteLine("Press 1 to Sign-up");
            Console.WriteLine("Press 2 to Sign-in");
            try
            {
                int loginInput = Convert.ToInt32(Console.ReadLine());

                if (loginInput == 1)
                {
                    Console.Clear();
                    Calculator.SignUp();
                }
                else if (loginInput == 2)
                {
                    Console.Clear();
                    Calculator.SignIn();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                    Calculator.LandingPage();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SignUp()
        {            
            try
            {
                string userListFilePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\UserList\UserList.txt";
                string newUser = "";

                string verifiedUserListFilePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\UserList\VerifiedUsers.txt";

                UserModel usr = new UserModel();

                Console.WriteLine("Enter Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Email");
                string email = Console.ReadLine();

                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();

                // Generate Verification Code
                Random verifyCode = new Random();
                int displayVC = verifyCode.Next(1000, 9999);

                Console.WriteLine(displayVC);

                usr.Name = name;
                usr.Email = email;
                usr.Password = password;

                newUser = $"{name}--{email}--{password}--{displayVC}\n";
                File.AppendAllText(userListFilePath, newUser);

                List<UserModel> userList = new List<UserModel>();

                // Read verfication code from file
                using (StreamReader sr = new StreamReader(userListFilePath))
                {
                    string getVC = sr.ReadToEnd();
                    string[] usrStringRows = getVC.Trim().Split('\n');

                    foreach (string usrStringRow in usrStringRows)
                    {
                        UserModel user = new UserModel();

                        if (!string.IsNullOrEmpty(usrStringRow))
                        {

                            string[] usrStringColumns = usrStringRow.Split("--");

                            user.Name = usrStringColumns[0];
                            user.Email = usrStringColumns[1];
                            user.Password = usrStringColumns[2];
                            user.VerificationCode = usrStringColumns[3];

                            userList.Add(user);
                        }
                    }
                }


                Console.WriteLine("Enter verification code");
                int iVerifyCode = Convert.ToInt32(Console.ReadLine());

                var foundUser = userList.FirstOrDefault(user => user.Email == email && user.VerificationCode == iVerifyCode.ToString());

                if (foundUser != null)

                {
                    Console.Clear();
                    File.AppendAllText(verifiedUserListFilePath, $"{foundUser.Email}\n");
                    Calculator.MainMenu();

                }
                else
                {
                    Console.WriteLine("Invalid Verification Code");

                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool SignIn()
        {            
            bool canSignIn = false;

            try
            {
                string userListFilePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\UserList\UserList.txt";
                string verifiedUserListFilePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\UserList\VerifiedUsers.txt";

                Console.WriteLine("Enter Email");
                string email = Console.ReadLine();

                using (StreamReader srV = new StreamReader(verifiedUserListFilePath))
                {
                    string getEmailV = srV.ReadToEnd();
                    string[] usrStringVRows = getEmailV.Split('\n');

                    foreach (string usrStringVRow in usrStringVRows)
                    {                    
                       if (email == usrStringVRow)
                        {
                            Console.WriteLine("Enter Password");
                            string password = Console.ReadLine();

                            using (StreamReader sr = new StreamReader(userListFilePath))
                            {
                                string getEmail = sr.ReadToEnd();
                                string[] usrStringRows = getEmail.Trim().Split('\n');

                                foreach (string usrStringRow in usrStringRows)
                                {
                                    UserModel user = new UserModel();

                                    string[] usrStringColumns = usrStringRow.Split("--");

                                    user.Name = usrStringColumns[0];
                                    user.Email = usrStringColumns[1];
                                    user.Password = usrStringColumns[2];
                                    user.VerificationCode = usrStringColumns[3];

                                    if (user.Email == email && user.Password == password)
                                    {
                                        canSignIn = true;
                                    }
                                }
                            }
                        }
                    }
                }               

                
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }

            if (canSignIn == true)
            {
                Console.Clear();
                Calculator.MainMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect Login Details");
                Console.ReadLine();
                Console.Clear();
                Calculator.LandingPage();
            }
            return canSignIn;
        }

        public static void MainMenu()
        {
            try
            {
                // Main Menu
                Console.WriteLine("Press 1 to Calculate");
                Console.WriteLine("Press 2 to View History");
                Console.WriteLine("Press 3 to Exit");

                int mainMenuInput = Convert.ToInt32(Console.ReadLine());

                switch (mainMenuInput)
                {
                    case 1:
                        {
                            Console.Clear();
                            Calculator.Cal();
                            break;
                        }

                    case 2:
                        {
                            Console.Clear();
                            Calculator.History();
                            break;
                        }

                    case 3:
                        {
                            Console.Clear();
                            Environment.Exit(3);
                            return;                            
                        }
                    default:
                        {
                            break;
                        }
                }
            } 

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Calculator.LandingPage();
            }
        }

        public static void Cal()
        {
            string filePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\Data\";
            string dateAppendToPath = DateTime.Today.ToString("dd_MM_yyyy");
            string finalFilePath = filePath + dateAppendToPath + ".txt";
            string data = "";

            int i = 1;
            do
            {
                //Menu
                Console.WriteLine("Press 1 for Addition");
                Console.WriteLine("Press 2 for Subtraction");
                Console.WriteLine("Press 3 for Multiplication");
                Console.WriteLine("Press 4 for Division");
                Console.WriteLine("Press 5 for Main Menu");

                try
                {
                    //Function input taken from user
                    int function = Convert.ToInt32(Console.ReadLine());

                    if (function == 5)
                    {
                        Console.Clear();
                        Calculator.MainMenu();
                    }
                    else if (function > 4 || function < 1)
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadLine();
                        Console.Clear();
                        Calculator.Cal();
                    }



                    //Numbers input taken from user to perform the function
                    Console.Write("Enter a number: ");
                    double num1 = Convert.ToDouble(Console.ReadLine());


                    Console.Write("Enter another number: ");
                    double num2 = Convert.ToDouble(Console.ReadLine());

                    double result = 0;
                    string op = "";

                    switch (function)
                    {
                        case 1:
                            {
                                result = num1 + num2;
                                op = "+";
                                Console.WriteLine($"Result: {result}");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            break;

                        case 2:
                            {
                                result = num1 - num2;
                                op = "-";
                                Console.WriteLine($"Result: {result}");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            break;

                        case 3:
                            {
                                result = num1 * num2;
                                op = "*";
                                Console.WriteLine($"Result: {result}");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            break;

                        case 4:
                            {
                                result = num1 / num2;
                                op = "/";
                                Console.WriteLine($"Result: {result}");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            break;

                    }


                    data = $"{DateTime.Now.ToString("hh:mm:ss")}    {num1} {op} {num2} = {result} \n";
                    File.AppendAllText(finalFilePath, data);

                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Invalid Selection");
                    Console.ReadLine();
                    Console.Clear();
                }


            } while (i == 1);
            
                                   

        }

        public static void History()
        {            
            int i1 = 1;

            while (i1 == 1)
            {
                try
                {
                    Console.WriteLine("Press 1 to View calulations history by date");
                    Console.WriteLine("Press 2 to Main Menu");
                    int actionRequest = Convert.ToInt32(Console.ReadLine());


                    if (actionRequest == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter DAY of the date");
                        int recDayRequest = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter MONTH of the date");
                        int recMonthRequest = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter YEAR of the date");
                        int recYearRequest = Convert.ToInt32(Console.ReadLine());

                        DateTime inputDate = new DateTime(recYearRequest, recMonthRequest, recDayRequest);

                        //string date = $"{recDayRequest}_{recMonthRequest}_{recYearRequest}";

                        string date = inputDate.ToString("dd_MM_yyyy");

                        string reqFilePath = @"C:\Users\anush\source\repos\Cal_Data\Cal_Data\Data\" + date + ".txt";
                        try
                        {
                            using (var sr = new StreamReader(reqFilePath))
                            {
                                Console.WriteLine(sr.ReadToEnd());
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("No calculations to show from this date");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    else if (actionRequest == 2)
                    {
                        Console.Clear();
                        Calculator.MainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }
                
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Console.Clear();
                }
               

            }

        }

    }
    
}
