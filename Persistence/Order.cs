using System;
using System.Collections.Generic;
namespace Persistence{
    public  class OrderStatus{
        public const int CREATE_NEW_ORDER = 1;
        public const int ORDER_INPROGRESS = 2;
    }

    public class Order{
        public int order_id {set;get;}
        public DateTime order_date {set;get;}
        public int ? order_status {set;get;}
        public Cashier cashierInfo{set;get;}
        public TableNumber table {set;get;} 
        public List<Item> listItem {set;get;}
        public Item this[int index]
        {
            get
            {
                if (listItem == null || listItem.Count == 0 || index < 0 || listItem.Count < index) return null;
                return listItem[index];
            }
            set
            {
                if (listItem == null) listItem = new List<Item>();
                listItem.Add(value);
            }
        }
        public Order()
        {
            listItem = new List<Item>();
        }
    }
}