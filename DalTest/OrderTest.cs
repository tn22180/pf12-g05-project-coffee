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
        [Fact]
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
            Assert.True(result != true);
        }
        [Theory]
        [InlineData(1,1,1,1,1,1)]
        [InlineData(1,2,1,1,1,1)]
        [InlineData(1,1,2,1,1,1)]
        [InlineData(1,1,1,2,1,1)]
        [InlineData(1,1,1,1,2,1)]
        [InlineData(1,1,1,1,1,2)]
        [InlineData(2,1,1,1,1,1)]
        [InlineData(2,2,1,1,1,1)]
        [InlineData(2,1,2,1,1,1)]
        [InlineData(2,1,1,2,1,1)]
        [InlineData(2,1,1,1,2,1)]
        [InlineData(2,1,1,1,1,2)]

        public void OrderTest2(int tab, int casId, int orderStatus, int OrID, int itemId, int quantity)
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = tab;
            order.CashierInfo.CashierId = casId;
            order.OrderStatus = orderStatus;
            order.OrderId = OrID;
            foreach(var i in order.ListItem)
            {
                i.ItemId = itemId;
                i.ItemQuantity = quantity;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result != true);
        }


    }
}