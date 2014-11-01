using System.Data.Entity;
using System.Data.Entity.SqlServerCompact;

namespace TimeTrackR.Core.Data
{
    public class SqlServerCeConfiguration : DbConfiguration
    {
        public SqlServerCeConfiguration()
        {
            SetProviderServices(SqlCeProviderServices.ProviderInvariantName, SqlCeProviderServices.Instance);
        }
    }
}