using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Services
{

    public static class DataAcccessHelper
    {
        static string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];



        public static int Execute(string sql, object param = null)
        {
            var connection = new SqlConnection(connectionString);

            return connection.Execute(sql, param);
        }

        public static IEnumerable<T> Query<T>(string sql, object param = null, bool limit = true)
        {
            var connection = new SqlConnection(connectionString);

            //if (limit)
            //    sql = sql + " limit @StartIndex,@PageSize ";

            if (param == null)
                param = new object();

            return connection.Query<T>(sql, param);
        }

        public static T QueryFirst<T>(string sql, object param = null)
        {
            var connection = new SqlConnection(connectionString);

            return connection.QueryFirst<T>(sql, param);
        }
        public static T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            var connection = new SqlConnection(connectionString);

            return connection.QueryFirstOrDefault<T>(sql, param);
        }
        
    }
}
