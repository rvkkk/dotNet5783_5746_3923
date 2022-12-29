using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;

namespace DalTest
{
    internal class MainRun
    {
        private static DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// calls the products functions
        /// </summary>
        static void Products()
        {

            int choose, ID;
            Console.WriteLine("enter 0 to add product");
            Console.WriteLine("enter 1 to show product");
            Console.WriteLine("enter 2 to view all products");
            Console.WriteLine("enter 3 to update product");
            Console.WriteLine("enter 4 to delete product");
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
                        p.Category = (Category)int.Parse(Console.ReadLine() ?? throw new Exception("category was empty"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new Exception("number in stock was empty"));
                        ID = dal!.Product.Add(p);
                        Console.WriteLine($"the id of the product is: {ID}");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(dal!.Product.Get(ID));
                        break;
                    }

                case 2:
                    {
                        foreach (Product? item in dal!.Product.GetAll())
                            Console.WriteLine(item);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(dal!.Product.Get(ID));
                        Product p = new Product();
                        Console.WriteLine("enter product new ID:");
                        p.ID = int.Parse(Console.ReadLine() ?? throw new Exception("ID was empty"));
                        Console.WriteLine("enter product new name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product new price:");
                        p.Price = int.Parse(Console.ReadLine() ?? throw new Exception("price was empty"));
                        Console.WriteLine("enter product new category:");
                        p.Category = (Category)int.Parse(Console.ReadLine() ?? throw new Exception("category was empty"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new Exception("number in stock was empty")  );
                        dal.Product.Update(p);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        dal!.Product.Delete(ID);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        int.TryParse(Console.ReadLine(), out choose);
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
            Console.WriteLine("enter 0 to add order");
            Console.WriteLine("enter 1 to show order");
            Console.WriteLine("enter 2 to view all orders");
            Console.WriteLine("enter 3 to update order");
            Console.WriteLine("enter 4 to delete order");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                case 0:
                    {
                        Order o = new Order();
                        Console.WriteLine("enter customer name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer email:");
                        o.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer address:");
                        o.CustomerAddress = Console.ReadLine();//check
                        Console.WriteLine("enter order date:");
                        o.OrderDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("order date was empty"));
                        Console.WriteLine("enter ship date:");
                        o.ShipDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("ship date was empty"));
                        Console.WriteLine("enter delivery date:");
                        o.DeliveryDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("delivery date was empty"));
                        ID = dal!.Order.Add(o);
                        Console.WriteLine($"the id of the order is: {ID}");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(dal!.Order.Get(ID));
                        break;
                    }
                case 2:
                    {
                        foreach (Order? item in dal!.Order.GetAll())
                            Console.WriteLine(item);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("enter product ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Order o = dal!.Order.Get(ID);
                        Console.WriteLine(o);
                        Console.WriteLine("enter customer new name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer new email:");
                        o.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer new address:");
                        o.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("enter new order date:");
                        o.OrderDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("order date was empty"));
                        Console.WriteLine("enter new ship date:");
                        o.ShipDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("ship date was empty"));
                        Console.WriteLine("enter new delivery date:");
                        o.DeliveryDate = DateTime.Parse(Console.ReadLine() ?? throw new Exception("delivery date was empty"));
                        dal.Order.Update(o);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        dal!.Order.Delete(ID);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        int.TryParse(Console.ReadLine(), out choose);
                        break;
                    }
            }
        }
        /// <summary>
        /// calls the orderItems functions
        /// </summary>
        static void OrderItems()
        {
            int choose, ID;
            Console.WriteLine("enter 0 to add order item");
            Console.WriteLine("enter 1 to show order item");
            Console.WriteLine("enter 2 to view all order items");
            Console.WriteLine("enter 3 to update order item");
            Console.WriteLine("enter 4 to delete order item");
            Console.WriteLine("enter 5 to view all items in order");
            Console.WriteLine("enter 6 to get order item by product and order");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                case 0:
                    {
                        OrderItem oI = new OrderItem();
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine() ?? throw new Exception("product ID was empty"));
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine() ?? throw new Exception("order ID was empty"));
                        Console.WriteLine("enter item price:");
                        oI.Price = int.Parse(Console.ReadLine() ?? throw new Exception("item price was empty"));
                        Console.WriteLine("enter item amount:");
                        oI.Amount = int.Parse(Console.ReadLine() ?? throw new Exception("item amount was empty"));
                        ID = dal!.OrderItem.Add(oI);
                        Console.WriteLine($"the id of the order item is: {ID}");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("enter order item ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        Console.WriteLine(dal!.OrderItem.Get(ID));
                        break;
                    }
                case 2:
                    {
                        foreach (OrderItem? item in dal!.OrderItem.GetAll())
                            Console.WriteLine(item);
                        break;
                    }
                case 3:
                    {

                        Console.WriteLine("enter order item ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        OrderItem oI = dal!.OrderItem.Get(ID);
                        Console.WriteLine(oI);
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine() ?? throw new Exception("product ID was empty"));
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine() ?? throw new Exception("order ID was empty"));
                        Console.WriteLine("enter item price:");
                        oI.Price = int.Parse(Console.ReadLine() ?? throw new Exception("item price was empty"));
                        Console.WriteLine("enter item amount:");
                        oI.Amount = int.Parse(Console.ReadLine() ?? throw new Exception("item amount was empty"));
                        dal.OrderItem.Update(oI);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("enter order item ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        dal!.OrderItem.Delete(ID);
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("enter order ID:");
                        int.TryParse(Console.ReadLine(), out ID);
                        foreach (OrderItem? item in dal!.OrderItem.GetAll((OrderItem? orderItem) => orderItem?.OrderID == ID))
                            Console.WriteLine(item);
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("enter order ID and product ID:");
                        int IDO, IDP;
                        int.TryParse(Console.ReadLine(), out IDO);
                        int.TryParse(Console.ReadLine(), out IDP);
                        Console.WriteLine(dal!.OrderItem.GetByF((OrderItem? o) => { return o?.OrderID == IDO && o?.ProductID == IDP; }));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("enter again");
                        int.TryParse(Console.ReadLine(), out choose);
                        break;
                    }
            }
        }
        /// <summary>
        /// main run
        /// </summary>
        static void Main()
        {
            int show;
            do
            {
                Console.WriteLine("enter 0 to exit");
                Console.WriteLine("enter 1 to products");
                Console.WriteLine("enter 2 to orders");
                Console.WriteLine("enter 3 to order items");
                int.TryParse(Console.ReadLine(), out show);
                try
                {
                    switch (show)
                    {
                        case 1:
                            Products();
                            break;
                        case 2:
                            Orders();
                            break;
                        case 3:
                            OrderItems();
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
                    Console.WriteLine(str);
                }
            } while (show != 0);
        }
    }
}