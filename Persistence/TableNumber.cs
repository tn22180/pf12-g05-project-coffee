using System;
namespace Persistence{
    public static class TableStatus{
        public const int EMPTY = 1;
        public const int NO_EMPTY = 2;
    }
    public class TableNumber{
        public int table_number{set;get;}
        public int ? table_status{set;get;}
    }
}