using System;
using System.Collections.Generic;
namespace Persistence{
    public class OrderDetail{
        public int _OrderId {get;set;}
        public string _ItemName{get;set;}
        public double _ItemPrice{get;set;}
        public int _ItemQuantity{get;set;}
    }
}