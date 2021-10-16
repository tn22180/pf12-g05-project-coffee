using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class OrderDalTest
    { 
          Order order = new Order();
          OrderDal odl = new OrderDal();
         
        public void OrderTest1()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 2;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = 2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == true);
        }
    }
}