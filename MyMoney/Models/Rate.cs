using SQLite;

namespace MyMoney
{
    public class Rate
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FromCur { get; set; }
        public string TargetCur { get; set; }
        public double Value { get; set; }
    }
}
