using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_management_system
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Management management = new Management();
            int idCounter = 1;
            while(true)
            {
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Update Password");
                Console.WriteLine("3. Delete User");
                Console.WriteLine("4. Get All Users");
                Console.WriteLine("5. Get User By Id");
                Console.WriteLine("6. Enter System");
                Console.WriteLine("7. Exit");
                string option = Console.ReadLine(); 

                switch(option)
                {
                    case "1":
                        
                        user.Id = idCounter;
                        Console.WriteLine("Name: ");
                        user.Name = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        user.password = Console.ReadLine();
                        management.AddUser(user);
                        idCounter++;
                        break;
                    case "2":
                        Console.WriteLine("Id: ");
                        user.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Password: ");
                        user.password = Console.ReadLine();
                        management.UpdatePassword(user);
                        break;
                    case "3":
                        Console.WriteLine("Id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        management.DeleteUser(id);
                        break;
                    case "4":
                        List<User> users = management.GetAllUsers();
                        foreach (User u in users)
                        {
                            Console.WriteLine("User Id: " + u.Id);
                            Console.WriteLine("User Name: " + u.Name);
                            Console.WriteLine("User Password: " + u.password);
                        }
                        break;
                    case "5":
                        Console.WriteLine("Id: ");
                        int id1 = Convert.ToInt32(Console.ReadLine());
                        management.GetUserById(id1);
                        break;
                    case "6":
                        management.EnterSystem();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
    class Management
    {
        public List<User> Users= new List<User>();
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public void UpdatePassword(User user)
        {
            User existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.password = user.password; 
            }
        }
        public void DeleteUser(int id)
        {
            User existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                Users.Remove(existingUser);
            }
        }
        public List<User> GetAllUsers()
        {
            return Users;
        }
        public void GetUserById(int id)
        {
            User existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                Console.WriteLine("User Id: " + existingUser.Id);
                Console.WriteLine("User Name: " + existingUser.Name);
                Console.WriteLine("User Password: " + existingUser.password);
            }
        }
        public void EnterSystem()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("password: ");
            string password = Console.ReadLine();
            User existingUser = Users.FirstOrDefault(u => u.Name == name && u.password == password);
            if (existingUser != null)
            {
                Console.WriteLine("Welcome " + existingUser.Name);
            }
            else
            {
                Console.WriteLine("Invalid Id or password");
            }
        }
    }
}
