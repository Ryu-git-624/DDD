using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using NDDD.Infrastructure.SqlServer;
using System;

using System.Collections.Generic;

using System.Data.SqlClient;

namespace DDD.Infrastructure.SQLServer
{
    public sealed class AreasSQLserver : IAreasRepository
    {
        public IReadOnlyList<AreaEntity> GetData()
        {
            string sql = @"
select AreaId,AreaName
from Areas
";
            var result = new List<AreaEntity>();
            SqlServerHelper.Query(sql, null, reader =>
            {
                result.Add(createEntity(reader));
            }
            );

            return result;

        }

        private AreaEntity createEntity(SqlDataReader reader)
        {
            return new AreaEntity(Convert.ToInt32(reader["AreaId"]), reader["AreaName"].ToString());
        }
    }
}


