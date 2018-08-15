using Infrastructure.DI;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NPoco;
using Park.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Rep
{
    public abstract class ParkRepository<T> : BaseRepository<T> where T : class, new()
    {
        public ParkRepository()
        {
            var options = IocManager.GetRequiredService<IOptions<ConnectionStrings>>();
            db = new Database(options.Value.Park, DatabaseType.MySQL, MySqlClientFactory.Instance);
        }
    }
}
