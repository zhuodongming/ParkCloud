using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Park.Rep
{
    public class NPocoDatabase : Database
    {
        public NPocoDatabase(string connectionString, DatabaseType databaseType, DbProviderFactory provider) : base(connectionString, databaseType, provider)
        {
        }

        protected override bool OnInserting(InsertContext insertContext)
        {
            return base.OnInserting(insertContext);
        }

        protected override bool OnUpdating(UpdateContext updateContext)
        {
            return base.OnUpdating(updateContext);
        }

        protected override void OnExecutedCommand(DbCommand cmd)
        {
            base.OnExecutedCommand(cmd);
        }

        protected override void OnExecutingCommand(DbCommand cmd)
        {
            base.OnExecutingCommand(cmd);
        }

        protected override bool OnDeleting(DeleteContext deleteContext)
        {
            return base.OnDeleting(deleteContext);
        }

        protected override void OnException(Exception exception)
        {
            base.OnException(exception);
        }
    }
}
