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
            string strPKCConn = "Database=park;Data Source=120.25.215.63;User Id=root;Password=abcd*1234;charset=utf8;SslMode=none;allowPublicKeyRetrieval=true;";
            MySqlConnection pkcConn = new MySqlConnection(strPKCConn);
            PocoGenClient client = new PocoGenClient(options =>
             {
                 options.IsGenNPoco = true;
                 options.Namespace = "Park.Repository";
                 options.ClassSuffix = "PO";
             });
            var pkcResult = client.GenerateAllTables(pkcConn);
            File.WriteAllText("../../../../Park.Repository/ParkPOs.cs", pkcResult);
        }
    }
}
