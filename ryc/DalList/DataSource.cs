using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
    internal static readonly Random s_rand = new Random();
    internal static List<Product?> lProduct = new List<Product?>();
    internal static List<Order?> lOrder = new List<Order?>();  
    internal static List<OrderItem?> lOrderItem = new List<OrderItem?>();

    /// <summary>
    /// static constructor
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }

    /// <summary>
    /// calls the creating functions
    /// </summary>
    private static void s_Initialize()
    {
        createProducts();
        createOrders();
        createOrderItems();
    }

    /// <summary>
    /// creates new products
    /// </summary>
    private static void createProducts()
    {
        string[] productNames = { "burger 180g", "burger 250g", "angos burger", "coke", "franch fries", "orange juice", "onion rings", "schnitzel burger", "chocopay", "ice cream" };
        Enums.Category[] productCategories = { Enums.Category.BURGERS, Enums.Category.BURGERS, Enums.Category.BURGERS, Enums.Category.JUICES, Enums.Category.EXTRAS, Enums.Category.JUICES, Enums.Category.EXTRAS, Enums.Category.BURGERS, Enums.Category.DESSERTS, Enums.Category.DESSERTS };
        for (int i = 0; i < 10; i++)
            lProduct.Add( new Product { ID = s_rand.Next(100000, 1000000), Name = productNames[i], Price = s_rand.Next(20, 80), Category = productCategories[i], InStock = s_rand.Next(0, 50) });
    }

    /// <summary>
    /// creates new orders
    /// </summary>
    private static void createOrders()
    {
        int p;
        string[] cName = { "yael atias", "michal choen", "shira levi", "gila choen", "efrat shimoni", "yair osher", "meital ravinski", "rivka miler", "hadas rahvon", "dina levi" };
        string[] cAddress = { "aneviim 3 bnei brak", "hanatziv 67 tel aviv", "teena 45 elad", "raban gamliel 15 elad", "rabi akiva 50 bnei brak", "herzel 8 petach tikva", "nisinboim 22 bnei brak", "yaalom 7 ramat gan", "shimon hazadik 4 elad", "ben shatach 20 ramat gan" };
        string[] cEmail = { "yael@gmail.com", "mimimi@gmail.com", "shira1234@gmail.com", "choen@gmail.com", "efratii@gmail.com", "yairosher@gmail.com", "ravinski@gmail.com", "rivka2828@gmail.com", "hadasush@gmail.com", "dinale12@gmail.com" };
        for (int i = 0; i < 20; i++)
        {
            p = s_rand.Next(10);
            lOrder.Add( new Order { ID = Config.OrderID, CustomerName = cName[p], CustomerAddress = cAddress[p], CustomerEmail = cEmail[p], OrderDate = DateTime.Now, ShipDate = null, DeliveryDate = null });
        }
    }

    /// <summary>
    /// creates new orderItems
    /// </summary>
    private static void createOrderItems()
    {
        int n;
        for (int i = 0; i < 40; i++)
        {
            n = s_rand.Next(1, 10);
            lOrderItem.Add( new OrderItem { ID = Config.OrderItemID, OrderID = (int)lOrder[i % 20]?.ID!, ProductID = (int)lProduct[i % n]?.ID!, Amount = s_rand.Next(1, 10), Price = (double)lProduct[i % n]?.Price! });
        }
    }

    /// <summary>
    /// config class
    /// </summary>
    internal static class Config
    {
        private static int orderID = 1000;
        private static int orderItemID = 1000;
        public static int OrderID { get { return ++orderID; } }
        public static int OrderItemID { get { return ++orderItemID; } }
    }
}
