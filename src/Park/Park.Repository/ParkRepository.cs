using MySql.Data.MySqlClient;
using NPoco;
using Park.Entity;

namespace Park.Repository
{
    public abstract class ParkRepository<T> : BaseRepository<T> where T : class, new()
    {
        public ParkRepository()
        {
            db = new NPocoDatabase(ConnectionStrings.Park, DatabaseType.MySQL, MySqlClientFactory.Instance);
        }
    }
}
