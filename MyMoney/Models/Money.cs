using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyMoney
{
    public class Money
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Currency))]
        public int CurrID { get; set; }
        public double Value { get; set; }
    }
}
