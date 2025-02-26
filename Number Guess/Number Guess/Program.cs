using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_Guess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            int number = 0;
            int guess = 0;

            Console.WriteLine("Guess a number between 1 and 100");
            number = Convert.ToInt32(Console.ReadLine());

            while (number != randomNumber)
            {
                guess++;
                if (number < randomNumber)
                {
                    Console.WriteLine("enter a higher number.");
                    number = Convert.ToInt32(Console.ReadLine());
                }
                else if (number > randomNumber)
                {
                    Console.WriteLine("enter a lower number.");
                    number = Convert.ToInt32(Console.ReadLine());
                }
                
            }
            Console.WriteLine("Congratulations! You guessed the number in " + ++guess + " tries.");

        }
    }
}
