using MySql.Data.MySqlClient;
using Park.Entity;
using PetaPoco;

namespace Park.Repository
{
    public class ParkRep<T> : BaseRepository<T> where T : class, new()
    {
        public ParkRep()
        {
            db = new Database(ConnectionStrings.Park, MySqlClientFactory.Instance);
        }
    }
}
