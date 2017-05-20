using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace MyMoney
{
   public class MyMoneyDatabase
    {
        readonly SQLiteAsyncConnection database;

        public MyMoneyDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.DropTableAsync<Currency>().Wait();
            database.CreateTableAsync<Currency>().Wait();
            database.CreateTableAsync<Money>().Wait();
            database.CreateTableAsync<Rate>().Wait();
            fillCurrencyDB();

        }
        public Task<List<Money>> GetMoneyAsync()
        {
            return database.Table<Money>().ToListAsync();
        }

        public Task<List<Currency>> GetCurrenciesAsync()
        {
            return database.Table<Currency>().ToListAsync();
        }
        public Task<Currency> GetCurrencyAsync(int id)
        {
            return database.Table<Currency>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Currency GetCurrencyAsyncName(string code)
        {
            return database.QueryAsync<Currency>("SELECT [Symbol] FROM [Currency] WHERE [Code]=?", code).Result[0];
        }
        public Rate GetRateAsync(string from,string to)
        {
            List<Rate> results;
            results = database.QueryAsync<Rate>("SELECT * FROM [Rate] WHERE [FromCur]=? AND [TargetCur]=?",from,to).Result;
            if (results.Count == 0)
                return null;
            else
            {
                return results[0];
            }
        }
        public Task<int> SaveRateAsync(Rate item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> SaveMoneyAsync(Money item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Money item)
        {
            return database.DeleteAsync(item);
        }

        private void fillCurrencyDB()
        {
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Leke', 'ALL', 'Lek')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'USD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Afghanis', 'AFN', '؋')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'ARS', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Guilders', 'AWG', 'ƒ')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'AUD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('New Manats', 'AZN', 'ман')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'BSD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'BBD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rubles', 'BYR', 'p.')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Euro', 'EUR', '€')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'BZD', 'BZ$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'BMD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Bolivianos', 'BOB', '$b')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Convertible Marka', 'BAM', 'KM')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pula', 'BWP', 'P')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Leva', 'BGN', 'лв')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Reais', 'BRL', 'R$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'GBP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'BND', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Riels', 'KHR', '៛')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'CAD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'KYD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'CLP', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Yuan Renminbi', 'CNY', '¥')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'COP', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Colón', 'CRC', '₡')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Kuna', 'HRK', 'kn')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'CUP', '₱')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Koruny', 'CZK', 'Kč')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Kroner', 'DKK', 'kr')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'DOP ', 'RD$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'XCD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'EGP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Colones', 'SVC', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'FKP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'FJD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Cedis', 'GHC', '¢')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'GIP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Quetzales', 'GTQ', 'Q')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'GGP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'GYD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Lempiras', 'HNL', 'L')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'HKD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Forint', 'HUF', 'Ft')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Kronur', 'ISK', 'kr')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'INR', 'Rp')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupiahs', 'IDR', 'Rp')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rials', 'IRR', '﷼')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'IMP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('New Shekels', 'ILS', '₪')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'JMD', 'J$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Yen', 'JPY', '¥')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'JEP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Tenge', 'KZT', 'лв')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Won', 'KPW', '₩')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Won', 'KRW', '₩')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Soms', 'KGS', 'лв')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Kips', 'LAK', '₭')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Lati', 'LVL', 'Ls')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'LBP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'LRD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Switzerland Francs', 'CHF', 'CHF')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Litai', 'LTL', 'Lt')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Denars', 'MKD', 'ден')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Ringgits', 'MYR', 'RM')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'MUR', '₨')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'MXN', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Tugriks', 'MNT', '₮')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Meticais', 'MZN', 'MT')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'NAD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'NPR', '₨')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Guilders', 'ANG', 'ƒ')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'NZD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Cordobas', 'NIO', 'C$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Nairas', 'NGN', '₦')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Krone', 'NOK', 'kr')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rials', 'OMR', '﷼')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'PKR', '₨')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Balboa', 'PAB', 'B/.')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Guarani', 'PYG', 'Gs')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Nuevos Soles', 'PEN', 'S/.')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'PHP', 'Php')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Zlotych', 'PLN', 'zł')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rials', 'QAR', '﷼')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('New Lei', 'RON', 'lei')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rubles', 'RUB', 'руб')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'SHP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Riyals', 'SAR', '﷼')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dinars', 'RSD', 'Дин.')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'SCR', '₨')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'SGD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'SBD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Shillings', 'SOS', 'S')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rand', 'ZAR', 'R')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rupees', 'LKR', '₨')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Kronor', 'SEK', 'kr')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'SRD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pounds', 'SYP', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('New Dollars', 'TWD', 'NT$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Baht', 'THB', '฿')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'TTD', 'TT$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Lira', 'TRY', 'TL')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Liras', 'TRL', '£')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dollars', 'TVD', '$')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Hryvnia', 'UAH', '₴')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Pesos', 'UYU', '$U')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Sums', 'UZS', 'лв')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Bolivares Fuertes', 'VEF', 'Bs')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Dong', 'VND', '₫')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Rials', 'YER', '﷼')");
            database.QueryAsync<Money>("INSERT INTO [Currency] ([Name], [Code], [Symbol]) VALUES ('Zimbabwe Dollars', 'ZWD', 'Z$')");
        }
    }
}
