using DO;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
    public static readonly Random s_rand = new();
    internal static Product[] productsArray = new Product[50];
    internal static Order[] ordersArray = new Order[100];
    internal static OrderItem[] orderItemsArray = new OrderItem[200];
    
	static DataSource()
	{
		s_Initialize();
	}

	private static void s_Initialize()
	{
		createProducts();
		createOrders();
		createOrderItems();
	}

    private static void createProducts()
	{
		TimeSpan t = new TimeSpan(0);
		string[] productNames = {"burger 180g","burger 250g", "angos burger", "coke", "franch fries", ""};
		for(int i=0; i<10; i++)
		{
			productsArray[i] = new Product { ID = s_rand.Next(100000+1000000), Name = productNames[i], Price=s_rand.Next(200), Category=s_rand.Next(5), InStock};
		}
	}
	
	private static void createOrders()
	{
		string[] cName = { "yael atias", "michal choen", "shira levi", "gila choen", "efrat shimoni", "yair osher", "meital ravinski", "rivka miler", "hadas rahvon"};
        string[] cAddress = { "aneviim 3 bnei brak", "hanatziv 67 tel aviv" };
        string[] cEmail = { "yael@gmail.com", "mimimi@gmail.com", "shira1234@gmail.com", "choen@gmail.com", "efratii@gmail.com", "yairosher@gmail.com", "ravinski@gmail.com", "rivka2828@gmail.com", "hadasush@gmail.com" };
        for (int i=0; i<20; i++)
		{
			ordersArray[i] = new Order { ID = Config.OrderID, CustomerName=cName[i], DeliveryDate = DateTime.MinValue,  };
		}
	}

	private static void createOrderItems()
	{
        for (int i = 0; i < 40; i++)
        {
			orderItemsArray[i] = new OrderItem { ID = Config.OrderItemID };

        }
    }

	internal static class Config
	{
		internal static int lastProduct = 0;
        internal static int lastOrder = 0;
        internal static int lastOrderItem = 0;
        private static int orderID = 1000;
		private static int orderItemID = 1000;

		public static int OrderID { get { return ++orderID; } }
        public static int OrderItemID { get { return ++orderItemID; } }

    }
}
