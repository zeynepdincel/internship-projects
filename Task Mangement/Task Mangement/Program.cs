using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Task_Mangement
{
    class Task
    {
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        public string Email { get; set; }
        public bool IsComplete { get; set; } = false;

        public Task(string title, string taskDescription, DateTime dueDate, string email)
        {
            Title = title;
            TaskDescription = taskDescription;
            DueDate = dueDate;
            Email = email;
        }
        internal class Program
        {
            static Management management = new Management();
            static List<Task> tasks = new List<Task>();
            static System.Timers.Timer timer;
            static void TimerElapsed(object sender, ElapsedEventArgs e)
            {
                DateTime now = DateTime.Now;
                var dueTasks = tasks.Where(t => t.DueDate <= now && !t.IsComplete).ToList();

                foreach (var task in dueTasks)
                {
                    SendEmail(task.Email,task);
                    task.IsComplete = true;
                }
            }
            static string senderEmail;
            static string password;
            static void Main(string[] args)
            {
                timer = new System.Timers.Timer(TimeSpan.FromDays(1).TotalMilliseconds);
                timer.Elapsed += TimerElapsed;
                timer.Start();
                
                while (true)
                {
                    Console.WriteLine("1- Add Task");
                    Console.WriteLine("2- List Task");
                    Console.WriteLine("3- Update Task");
                    Console.WriteLine("4- SMTP");
                    Console.WriteLine("5- Exit");
                    Console.Write("Seçiminiz: ");
                    string selection = Console.ReadLine();
                    switch (selection)
                    {
                        case "1":
                            management.AddTask();
                            break;
                        case "2":
                            management.ListTasks();
                            break;
                        case "3":
                            management.UpdateTask();
                            break;
                        case "4":
                            Console.Write("Gönderici E-Posta: ");
                            senderEmail = Console.ReadLine();
                            Console.Write("Şifre: ");
                            password = Console.ReadLine();
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid selection!");
                            break;
                    }
                }
            }
            static void SendEmail(string recipientEmail, Task task)
            {
                try
                {
                    Console.WriteLine($"Email sent to {task.Email} - {task.Title}");
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    {
                        client.Port = 587;
                        client.Credentials = new System.Net.NetworkCredential(senderEmail, password);
                        client.EnableSsl = true;

                    };
                    MailMessage mail = new MailMessage("mail@gmail.com", task.Email)
                    {
                        Subject = "Task Reminder",
                        Body = $"Hello,\n\n'{task.Title}'it's time for your mission .\nDate: {task.DueDate}\n\nHave a good day!"
                    };

                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Email could not be sent to {task.Email}");
                    Console.WriteLine(ex.Message);
                }

            }
            class Management
            {
                public List<Task> tasks = new List<Task>();
                public void AddTask()
                {
                    Console.Write("Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Task Description: ");
                    string taskDescription = Console.ReadLine();
                    Console.Write("Due Date: ");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Task task = new Task(title, taskDescription, dueDate, email);
                    tasks.Add(task);
                }
                public void ListTasks()
                {
                    foreach (var task in tasks)
                    {
                        Console.WriteLine($" Title: {task.Title}, Description: {task.TaskDescription}, Due Date:{task.DueDate}, Email:{task.Email}");

                    }
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("There is no task!");
                    }
                }
                public void UpdateTask()
                {
                    Console.Write("Enter the title of the task you want to update: ");
                    string title = Console.ReadLine();
                    Task task = tasks.FirstOrDefault(x => x.Title == title);
                    if (task == null)
                    {
                        Console.WriteLine("Task not found!");
                        return;
                    }
                    Console.WriteLine("1- Update Title");
                    Console.WriteLine("2- Update Task Description");
                    Console.WriteLine("3- Update Due Date");
                    Console.WriteLine("4- Update Email");
                    Console.WriteLine("5- Update IsComplete");
                    string selection = Console.ReadLine();
                    switch (selection)
                    {
                        case "1":
                            Console.Write("New Title: ");
                            task.Title = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("New Task Description: ");
                            task.TaskDescription = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("New Due Date: ");
                            task.DueDate = DateTime.Parse(Console.ReadLine());
                            break;
                        case "4":
                            Console.Write("New Email: ");
                            task.Email = Console.ReadLine();
                            break;
                        case "5":
                            Console.Write("IsComplete: ");
                            task.IsComplete = bool.Parse(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Geçersiz seçim!");
                            break;
                    }
                }

            }
        }
    }
}
