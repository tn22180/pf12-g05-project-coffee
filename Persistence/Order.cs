using System;
using System.Collections.Generic;
namespace Persistence{
    public  class OrderStatus{
        public const int CREATE_NEW_ORDER = 1;
        public const int ORDER_INPROGRESS = 2;
    }

    public class Order{
        public int OrderId {set;get;}
        public DateTime OrderDate {set;get;}
        public int ? OrderStatus {set;get;}
        public Cashier CashierInfo{set;get;}
        public TableNumbers Table {set;get;} 
        public List<Item> ListItem {set;get;}
        public Item this[int index]
        {
            get
            {
                if (ListItem == null || ListItem.Count == 0 || index < 0 || ListItem.Count < index) return null;
                return ListItem[index];
            }
            set
            {
                if (ListItem == null) ListItem = new List<Item>();
                ListItem.Add(value);
            }
        }
        public Order()
        {
            ListItem = new List<Item>();
        }
        public override bool Equals(object obj){
            if(obj is Order){
                return ((Order)obj).OrderId.Equals(OrderId);
            }
            return false;
        }

        public override int GetHashCode(){
            return OrderId.GetHashCode();
        }
    }
}