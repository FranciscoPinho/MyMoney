using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyMoney
{
    public class Money
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string Cur { get; set; }
        public double Value { get; set; }
    }
}
