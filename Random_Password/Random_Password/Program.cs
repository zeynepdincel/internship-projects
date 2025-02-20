using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Password
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password;
            int pass_length;
            bool useSpecialChar;

            Console.WriteLine("Enter the password length:");
            pass_length = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Should special characters be used (Y/N)");
            useSpecialChar = Console.ReadLine().ToUpper() == "Y";

            password = GeneratePassword(pass_length, useSpecialChar);
            Console.WriteLine($"Oluşturulan Şifre: {password}");


        }
        static string GeneratePassword(int length, bool useSpecialChar)
        {

            string baseString = Guid.NewGuid().ToString("N");

            while (baseString.Length < length)
            {
                baseString += Guid.NewGuid().ToString();
            }
            if (useSpecialChar)
            {
                string specialChars = "!@#$%^&*()_+-=[]{}|;:'\",.<>?";
                Random random = new Random();
                for (int i = 0; i < length / 4; i++)
                {
                    int insertIndex = random.Next(baseString.Length);
                    char specialChar = specialChars[random.Next(specialChars.Length)];
                    baseString = baseString.Insert(insertIndex, specialChar.ToString());
                }

            }
            return baseString.Substring(0, length);
        }
    }
}
