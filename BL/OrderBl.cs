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
        public TableNumbers GetTableByStatus(int tab)
        {
            return odl.GetTableByNumber(tab);
        }
        public List<TableNumbers> GetAllTable(int tabstatus)
        {
            return odl.GetTables(new TableNumbers(){TableStatus = tabstatus});
        }
        // public Order GetOrderByTables(int tab,int Orst)
        // {
        //     return odl.GetOrderByTable(tab,Orst);
        // }
        public List<Order> GetAllOrders()
        {
            return odl.GetAllOrder();
        }
        public Order GetOrderByTab( int tab, int sta){
            return odl.GetOrderByTable(tab,sta);
        }
        public List<OrderDetail> GetOrderDetailByIds(int id)
        {
            return odl.GetOrderDetailById(id);
        }
    }
}