using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class OrderBl
    {
        private OrderDal odl = new OrderDal();
        public bool CreateOrder(Order order)
        {
            bool result = odl.CreateOrder(order);
            return result;
        }
        public TableNumbers GetTableByNumberr(int tab)
        {
            return odl.GetTableByNumber(tab);
        }
        public List<TableNumbers> GetAllTableByStatus(int tabstatus)
        {
            return odl.GetTablesByStatus(new TableNumbers(){TableStatus = tabstatus});
        }
        public List<TableNumbers> GetAllTable()
        {   
            return odl.GetAllTable();
        }
        public Order GetOrderByTableAndStatus( int tab, int sta){
            return odl.GetOrderByTabAndStt(tab,sta);
        }
        public List<OrderDetail> GetOrderDetailByOrderId(int id)
        {
            return odl.GetOrderDetailById(id);
        }
        // public Order GetOrderByTables(int tab,int Orst)
        // {
        //     return odl.GetOrderByTable(tab,Orst);
        // }
        // public List<Order> GetAllOrders()
        // {
        //     return odl.GetAllOrder();
        // }
        public void Update(int Orid, int Tab)
        {
            odl.Update(Orid,Tab);
        }
        public bool UpdateOrder(Order order)
        {
            bool result = odl.UpdateOrder(order);
            return result;
        }
        public bool CheckItemInList(Order order, int itemId)
        {
            return odl.CheckItemInList(order,itemId);
        }
        public void UpdateQuantity(int quantity, int orderId, int itemId)
        {
            odl.UpdateQuantity(quantity,orderId,itemId);
        }
    }
}