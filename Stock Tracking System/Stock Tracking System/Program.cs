using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stock_Tracking_System
{
    class Product
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        public Product( string productName, int stock, double price)
        {
            ProductName = productName;
            Stock = stock;
            Price = price;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"Product Name: {ProductName}, Price: {Price}, Stock: {Stock}");
        }
    }
    internal class Program
    {
        
        static void Main(string[] args)
        {
            StockManager stockManager = new StockManager();

            string choice;
            while(true){
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. List Product");
                Console.WriteLine("3. Update Stocks");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Exit");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.Write("Product name: ");
                        string name = Console.ReadLine();

                        Console.Write("Stock: ");
                        int stock = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Price: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        stockManager.AddProduct(new Product(name, stock , price));
                        break;
                    case "2":
                        stockManager.ListProducts();
                        break;
                    case "3":
                        Console.WriteLine("product name:");
                        string productName = Console.ReadLine();

                        Console.WriteLine("stock:");
                        int newStock = Convert.ToInt32(Console.ReadLine());

                        stockManager.UpdateStock(productName, newStock);
                        break;
                    case "4":
                        Console.WriteLine("product name:");
                        string deleteProductName = Console.ReadLine();
                        stockManager.DeleteProduct(deleteProductName);
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            
        }
    }
    
    class StockManager
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        public void ListProducts()
        {
           if(products.Count == 0)
            {
                Console.WriteLine("No product found");
            }
            else
            {
                foreach (var product in products)
                {
                    product.DisplayProduct();
                }
            }
        }
        public void UpdateStock(string productName, int stock)
        {
            var product = products.Find(p => p.ProductName == productName);
            if (product != null)
            {
                product.Stock = stock;
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        public void DeleteProduct(string productName)
        {
            var product = products.Find(p => p.ProductName == productName);
            if (product != null)
            {
                products.Remove(product);
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
    }
}
