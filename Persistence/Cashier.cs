using System;

namespace Persistence
{
 public class Cashier{
     public int cashier_id {set;get;}
     public string username {set;get;}
     public string password {set;get;}
     public string cashier_name {set;get;}
     public string phone {set;get;}
     public string address {set;get;}
     public int Role{set;get;}

     public static int CASHIER_ROLE = 1;
 }
}
