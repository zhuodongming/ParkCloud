using MySql.Data.MySqlClient;
using PocoGen;
using System;
using System.IO;

namespace ParkPocoGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //Park
            string strPKCConn = "Database=park;Data Source=127.0.0.1;User Id=root;Password=root;charset=utf8;SslMode=none;allowPublicKeyRetrieval=true;";
            MySqlConnection pkcConn = new MySqlConnection(strPKCConn);
            PocoGenClient client = new PocoGenClient(options =>
             {
                 options.IsGenPetaPoco = true;
                 options.Namespace = "Park.Repository";
                 options.ClassSuffix = "PO";
             });
            var pkcResult = client.GenerateAllTables(pkcConn);
            File.WriteAllText("../../../../Park.Repository/ParkPOs.cs", pkcResult);
        }
    }
}
