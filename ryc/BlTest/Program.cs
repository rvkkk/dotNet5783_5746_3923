using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlTest
{
    internal class Program
    {
        private static IBL bl = new Bl();

        /// <summary>
        /// calls the products functions
        /// </summary>
        static void Products(Cart c)
        {
            int choose, ID;
            Console.WriteLine("enter 0 to add product");
            Console.WriteLine("enter 1 to show product");
            Console.WriteLine("enter 2 to show product in cart");
            Console.WriteLine("enter 3 to view all products");
            Console.WriteLine("enter 4 to update product");
            Console.WriteLine("enter 5 to delete product");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                case 0:
                    {
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");
                        bool flag = int.TryParse(Console.ReadLine(), out ID);
                        if (!flag)
                            break;
                        p.ID = ID;
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine() ?? throw new Exception("price was empty"));
                        Console.WriteLine("enter product category: press 0 to BURGERS, press 1 to EXTRAS, press 2 to DESSERTS, press 3 to JUICES, press 4 to FOODֹ_COMBOS");
                        p.Category = (BO.Enums.Category)int.Parse(Console.ReadLine() ?? throw new Exception("category was empty"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new Exception("number in stock was empty"));
                        bl.Product.Add(p);
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Product.Get(ID));
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Product.Get(ID, c));
                    }
                    break;
                case 3:
                    {
                        foreach (ProductForList? item in bl.Product.GetAll())
                            Console.WriteLine(item);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Product.Get(ID));
                        Product p = new Product();
                        p.ID = ID;
                        Console.WriteLine("enter product new name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product new price:");
                        p.Price = int.Parse(Console.ReadLine() ?? throw new Exception("price was empty"));
                        Console.WriteLine("enter product new category: press 0 to BURGERS, press 1 to EXTRAS, press 2 to DESSERTS, press 3 to JUICES, press 4 to FOODֹ_COMBOS");
                        p.Category = (BO.Enums.Category)int.Parse(Console.ReadLine() ?? throw new Exception("category was empty"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new Exception("number in stock was empty"));
                        bl.Product.Update(p);
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        bl.Product.Delete(ID);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        break;
                    }
            }
        }
        /// <summary>
        /// calls the orders functions
        /// </summary>
        static void Orders()
        {
            int choose, ID;
            Console.WriteLine("enter 0 to show order");
            Console.WriteLine("enter 1 to view all orders");
            Console.WriteLine("enter 2 to update ship date of order");
            Console.WriteLine("enter 3 to update supply date of order");
            Console.WriteLine("enter 4 to update amount of item in order");
            Console.WriteLine("enter 5 to track order");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                case 0:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Order.Get(ID));
                        break;
                    }
                case 1:
                    {
                        foreach (OrderForList? item in bl.Order.GetAll())
                            Console.WriteLine(item);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Order.ShipUpdate(ID));
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Order.DeliveryUpdate(ID));
                        break;
                    }
                case 4:
                    {
                        int pID, amount;
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out pID);
                        Console.WriteLine("enter product amonut:");
                        int.TryParse(Console.ReadLine(), out amount);
                        Console.WriteLine(bl.Order.ManagerUpdate(ID, pID, amount));
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Order.OrderTracking(ID));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        break;
                    }
            }
        }
        /// <summary>
        /// calls the cart functions
        /// </summary>
        static void Cart(Cart c)
        {
            int choose, ID;
            Console.WriteLine("enter 0 to add product to cart");
            Console.WriteLine("enter 1 to update product in cart");
            Console.WriteLine("enter 2 to confirm order");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                case 0:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(bl.Cart.AddToCart(c, ID));
                        break;
                    }
                case 1:
                    {
                        int amount;
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine("enter product amount:");
                        int.TryParse(Console.ReadLine(), out amount);
                        bl.Cart.UpdateCart(c, ID, amount);
                        break;
                    }
                case 2:
                    {
                        bl.Cart.MakeAnOrder(c);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        break;
                    }
            }
        }
        static void Main(string[] args)
        {
            int show;
            Cart c = new Cart() { CustomerName = "rivki kreisman", CustomerAddress = "raban gamliel 15 elad", CustomerEmail = "rvkk2828@gmail.com", Items = { }, TotalPrice = 0 };
            c.Items = new List<OrderItem?>();
            do
            {
                Console.WriteLine("enter 0 to exit");
                Console.WriteLine("enter 1 to products");
                Console.WriteLine("enter 2 to orders");
                Console.WriteLine("enter 3 to cart");
                int.TryParse(Console.ReadLine(), out show);
                try
                {
                    switch (show)
                    {
                        case 1:
                            Products(c);
                            break;
                        case 2:
                            Orders();
                            break;
                        case 3:
                            Cart(c);
                            break;
                        default:
                            {
                                Console.WriteLine("enter again");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    string str = "";
                    str += ex.GetType() + ": ";
                    str += ex.Message;
                    if (ex.InnerException != null)
                    {
                        str += "\n" + ex.InnerException.GetType() + ": ";
                        str += ex.InnerException.Message;
                    }
                    Console.WriteLine(str);
                }
            } while (show != 0);
        }
    }
}