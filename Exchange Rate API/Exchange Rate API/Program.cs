using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string[]currencies={"EUR: Euro" ,
            "USD: American dollar" ,
            "TRY: Turkish Lira" ,
            "AUD: Australian dollar",
            "CAD: Canadian dollar" ,
            "SGD: Singapore dollar" ,
            "CHF: Swiss franc" ,
            "MYR: Malaysian ringgit" ,
            "JPY: Japanese yen" ,
            "CNY: Chinese yuan" ,
            "NZD: New Zealand dollar" ,
            "BGN: Bulgarian lev" ,
            "CZK: Czech koruna" ,
            "DKK: Danish krone" ,
            "GBP: British pound" ,
            "HUF: Hungarian forint" ,
            "PLN: Polish zloty" ,
            "RON: Romanian leu" ,
            "SEK: Swedish krona" ,
            "IDR: Indonesian rupiah" ,
            "INR: Indian rupee" ,
            "BRL: Brazilian real" ,
            "RUB: Russian ruble" ,
            "HRK: Croatian kuna" ,
            "THB: Thai baht" ,
            "KRW: South Korean won" ,
            "NOK: Norweigan krone",
            "XPF: CFP franc" ,
            "AZN: Azerbaijani manat" ,
            "CLP: Chilean peso" ,
            "PHP: Philippine peso" ,
            "IDR: Indonesian rupiah",
            "INR: Indian rupee",
            "BRL: Brazilian real",
            "RUB: Russian ruble",
            "HRK: Croatian kuna",
            "THB: Thai baht",
            "KRW: South Korean won",
            "NOK: Norweigan krone",
            "XPF: CFP franc",
            "AZN: Azerbaijani manat",
            "CLP: Chilean peso",
            "PHP: Philippine peso"


         };
        Console.WriteLine(string.Join("\n", currencies));

        Console.Write("Currency to be converted: ");
        string fromCurrency = Console.ReadLine().ToUpper();

        Console.Write("Target Currency: ");
        string toCurrency = Console.ReadLine().ToUpper();

        Console.Write("amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        double exchangeRate = await GetExchangeRate(fromCurrency, toCurrency);

        if (exchangeRate == 0)
        {
            Console.WriteLine("try again");
            return;
        }

        double convertedAmount = amount * exchangeRate;
        Console.WriteLine($"{amount} {fromCurrency} = {convertedAmount} {toCurrency} (exchange rate: {exchangeRate})");
    }

    static async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
    {
        string apiKey = "API key";//left blank for security purposes. You can add your own API key. 
        string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{fromCurrency}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                if (json["conversion_rates"]?[toCurrency] != null)
                {
                    return json["conversion_rates"][toCurrency].ToObject<double>();
                }
            }
        }

        return 0;
    }
}
