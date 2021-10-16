using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class CashierDalTest
    {
        private Cashier cashier = new Cashier();
        private CashierDal dal = new CashierDal();
        [Fact]
        public void LoginTest1()
        {
            cashier.Username = "tuanpf12";
            cashier.Password = "tuan2001";
            int expected = 1;
            int result = dal.Login(cashier);
            Assert.True(result == expected);
        }

        [Theory]
        [InlineData("tuanpf12",  "tuan2001", 1)]
        [InlineData("hapf12",  "tuan2001", 1)]
        [InlineData("tuanpf122",  "tuan2001", 1)]
        [InlineData("tuanpf12",  "tuan2001dsfg", 1)]
        [InlineData("tuanpf12",  "tuan2001", 0)]
        [InlineData("hapf122",  "tuan2001", 1)]
        [InlineData("hapf12",  "tuan2001dsfg", 1)]
        [InlineData("hapf12",  "tuan2001", 0)]
        
        public void LoginTest2(string userName, string pass, int expected)
        {
            cashier.Username = userName;
            cashier.Password = pass;
            cashier.Role = expected;
            int result = dal.Login(cashier);
            Assert.True(result == 1);
        }
    }
}
