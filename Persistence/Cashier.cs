using System;

namespace Persistence
{
 public class Cashier{
     public int CashierId {set;get;}
     public string Username {set;get;}
     public string Password {set;get;}
     public string CashierName {set;get;}
     public string Phone {set;get;}
     public string Address {set;get;}
     public int Role{set;get;}

     public static int CASHIER_ROLE = 1;
 }
}
