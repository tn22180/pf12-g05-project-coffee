using System;
using Xunit;
using Persistence;
using DAL;
using System.Collections.Generic;

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
        [Fact]
        public void OrderTest3()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = -1;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = 2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
        }
         [Fact]
        public void OrderTest5()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 1;
            order.CashierInfo.CashierId = -1;
            order.OrderStatus = 2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
        }
        [Fact]
        public void OrderTest6()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 1;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = -2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
        }
        [Fact]
        public void OrderTest7()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 1;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = 2;
            order.OrderId = -50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
        }
        [Fact]
        public void OrderTest8()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 1;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = 2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = -1;
                i.ItemQuantity = 1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
        }
        [Fact]
        public void OrderTest9()
        {
            order.Table = new TableNumbers();
            order.CashierInfo = new Cashier();
            order.Table.TableNumber = 1;
            order.CashierInfo.CashierId = 1;
            order.OrderStatus = 2;
            order.OrderId = 50;
            foreach(var i in order.ListItem)
            {
                i.ItemId = 1;
                i.ItemQuantity = -1;
            }
            bool result = odl.CreateOrder(order);
            Assert.True(result == false);
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
        [Theory]
        [InlineData(-1,1,1,1,1,1)]
        [InlineData(-1,2,1,1,1,1)]
        [InlineData(-1,1,2,1,1,1)]
        [InlineData(-1,1,1,2,1,1)]
        [InlineData(-1,1,1,1,2,1)]
        [InlineData(-1,1,1,1,1,2)]
        [InlineData(-2,1,1,1,1,1)]
        [InlineData(-2,2,1,1,1,1)]
        [InlineData(-2,1,2,1,1,1)]
        [InlineData(-2,1,1,2,1,1)]
        [InlineData(-2,1,1,1,2,1)]
        [InlineData(-2,1,1,1,1,2)]
        public void OrderTest4(int tab, int casId, int orderStatus, int OrID, int itemId, int quantity)
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
            Assert.True(result == false);
        }
        [Theory]
        [InlineData(-1,1,1,1,1,1)]
        [InlineData(-1,-2,1,1,1,1)]
        [InlineData(-1,1,-2,1,1,1)]
        [InlineData(-1,1,1,-2,1,1)]
        [InlineData(-1,1,1,1,-2,1)]
        [InlineData(-1,1,1,1,1,-2)]
        public void OrderTest10(int tab, int casId, int orderStatus, int OrID, int itemId, int quantity)
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
            Assert.False(result == true);
        }


    }
}