using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Weather_Forecast
{
    internal class Program
    {
        public static string ApiKey = "ApiKey";//left blank for security purposes. You can add your own API key.
        public static string favCityFile = "favorites.txt";

        public static async Task Main(string[] args) 
        {
            LoadFavoriteCity();
            while (true)
            {

                Console.WriteLine("1- Choose your favorite cities:");
                Console.WriteLine("2- Check the weather by city:");
                Console.WriteLine("3- Check the weather in your favorite cities:");
                Console.WriteLine("4-Exit");

                string selection = Console.ReadLine();

                if (selection == "1")
                {
                    Console.WriteLine("Enter your favorite city:");
                    string favCity = Console.ReadLine();
                    SaveFavoriteCity(favCity);

                }
                else if (selection == "2")
                {
                    Console.WriteLine("Enter the city you want to check the weather for:");
                    string city = Console.ReadLine();
                    string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}";
                    await GetApi(url);
                }
                else if (selection == "3")
                {
                    await FavoriteCity();
                }
                else if (selection == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid selection");
                }
            }
        }

        static async Task GetApi(string url) 
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject weather = JObject.Parse(json);

                    double temp = (double)weather["main"]["temp"];
                    double tempC = temp - 273.15;
                    Console.WriteLine("Temperature: " + tempC.ToString("0.00") + "°C");

                    string description = (string)weather["weather"][0]["description"];
                    Console.WriteLine("Description: " + description);

                    string windSpeed = (string)weather["wind"]["speed"];
                    Console.WriteLine("Wind Speed: " + windSpeed + "m/s");

                    string humidity = (string)weather["main"]["humidity"];
                    Console.WriteLine("Humidity: " + humidity + "%");
                }
                else
                {
                    Console.WriteLine("City not found");
                }
            }
        }
        static void SaveFavoriteCity(string city)
        {
            File.AppendAllText(favCityFile, city + Environment.NewLine); 
        }

        static void LoadFavoriteCity()
        {
            if (File.Exists(favCityFile))
            {
                Console.WriteLine("Your saved favorite cities:");
                string[] cities = File.ReadAllLines(favCityFile);
                foreach (string city in cities)
                {
                    Console.WriteLine("- " + city);
                }
            }
            else
            {
                Console.WriteLine("No favorite cities found.");
            }
        }

        static async Task FavoriteCity() 
        {
            if (File.Exists(favCityFile))
            {
                string[] cities = File.ReadAllLines(favCityFile);
                foreach (string city in cities)
                {
                    string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}";
                    Console.WriteLine($"\nChecking weather for {city}...");
                    await GetApi(url);
                }
            }
            else
            {
                Console.WriteLine("No favorite cities saved.");
            }
        }
    }
}