using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleAdatbazis
{
    class Program
    {
        static MySqlConnection connection = null;
        static MySqlCommand sql = null;
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "orszagok";
            sb.UserID = "root";
            sb.Password = "";
            connection = new MySqlConnection(sb.ToString());
            try
            {
                connection.Open();//Kapcsolat létsítése az adatbázishoz Connection objektum segítségével
                sql = connection.CreateCommand();//command objektum létrehozása
            }
            catch (MySqlException myex)
            {

                Console.WriteLine(myex.Message);
                Console.ReadKey();
                Environment.Exit(0);

            }
            sql.CommandText = "SELECT `orszag`,`terulet`,`allamforma` FROM `orszagok` WHERE 1";
            //Datareader objektum létrehozása
            using (MySqlDataReader dr=sql.ExecuteReader())
            {
                int i = 0;
                //rekordok kiolvasása
                while (dr.Read())
                {
                    string orszag = dr.GetString("orszag");
                    double terulet = dr.GetDouble("terulet");
                    string allamforma = dr.GetString("allamforma");
                    Console.WriteLine($"{i++}ország:{orszag},\tterulet:{terulet:N2}\tallamforma:{allamforma}");
                }
            }
            Console.WriteLine("\nProgram vége:");
            Console.ReadKey();
        }
    }
}
