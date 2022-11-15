using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
    public static readonly Random s_rand = new Random();
    internal static Product[] productsArray = new Product[50];
    internal static Order[] ordersArray = new Order[100];
    internal static OrderItem[] orderItemsArray = new OrderItem[200];
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
            productsArray[i] = new Product { ID = s_rand.Next(100000, 1000000), Name = productNames[i], Price = s_rand.Next(20, 80), Category = productCategories[i], InStock = s_rand.Next(0, 50) };
    }
    /// <summary>
    /// creates new orders
    /// </summary>
    private static void createOrders()
    {
        TimeSpan t;
        int p;
        string[] cName = { "yael atias", "michal choen", "shira levi", "gila choen", "efrat shimoni", "yair osher", "meital ravinski", "rivka miler", "hadas rahvon", "dina levi" };
        string[] cAddress = { "aneviim 3 bnei brak", "hanatziv 67 tel aviv", "teena 45 elad", "raban gamliel 15 elad", "rabi akiva 50 bnei brak", "herzel 8 petach tikva", "nisinboim 22 bnei brak", "yaalom 7 ramat gan", "shimon hazadik 4 elad", "ben shatach 20 ramat gan" };
        string[] cEmail = { "yael@gmail.com", "mimimi@gmail.com", "shira1234@gmail.com", "choen@gmail.com", "efratii@gmail.com", "yairosher@gmail.com", "ravinski@gmail.com", "rivka2828@gmail.com", "hadasush@gmail.com", "dinale12@gmail.com" };
        for (int i = 0; i < 20; i++)
        {
            t = new TimeSpan(0, s_rand.Next(60), s_rand.Next(60));
            p = s_rand.Next(10);
            ordersArray[i] = new Order { ID = Config.OrderID, CustomerName = cName[p], CustomerAddress = cAddress[p], CustomerEmail = cEmail[p], OrderDate = DateTime.MinValue, ShipDate = DateTime.MinValue + t, DeliveryDate = DateTime.MinValue + t.Add(new TimeSpan(0, s_rand.Next(60), s_rand.Next(60))) };
        }
    }
    /// <summary>
    /// creates new orderItems
    /// </summary>
    private static void createOrderItems()
    {
        for (int i = 0; i < 40; i++)
            orderItemsArray[i] = new OrderItem { ID = Config.OrderItemID, OrderID = ordersArray[i % 20].ID, ProductID = productsArray[i % 10].ID, Amount = s_rand.Next(1, 10), Price = productsArray[i % 10].Price };
    }
    /// <summary>
    /// config class
    /// </summary>
    internal static class Config
    {
        internal static int lastProduct = 10;
        internal static int lastOrder = 20;
        internal static int lastOrderItem = 40;
        private static int orderID = 1000;
        private static int orderItemID = 1000;
        public static int OrderID { get { return ++orderID; } }
        public static int OrderItemID { get { return ++orderItemID; } }
    }
}
