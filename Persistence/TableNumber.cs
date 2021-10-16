using System;
namespace Persistence{
    public static class TableStatus{
        public const int EMPTY = 1;
        public const int NO_EMPTY = 2;
    }
    public class TableNumbers{
        public int TableNumber{set;get;}
        public int ? TableStatus{set;get;}
    }
}