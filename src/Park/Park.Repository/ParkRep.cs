using MySql.Data.MySqlClient;
using NPoco;
using Park.Entity;

namespace Park.Repository
{
    public class ParkRep<T> : BaseRepository<T> where T : class, new()
    {
        public ParkRep()
        {
            db = new NPocoDatabase(ConnectionStrings.Park, DatabaseType.MySQL, MySqlClientFactory.Instance);
        }
    }
}
