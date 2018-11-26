using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using API_BoilerPlate.DAL.Interfaces;
using Dapper;

namespace API_BoilerPlate.DAL.DapperRepositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected string _connectionString;

        public virtual async Task<IEnumerable<T>> QueryMultipleAsync<T>(string sql, object parameters = null)
        {

            List<T> list1 = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(sql);
            }

        }


        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsyncSP<T>(string spName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(spName, param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public virtual async Task<string> QueryAsyncConcat(string spName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return string.Concat(await connection.QueryAsync<string>(
                    spName,
                    param: parameters,
                    commandType: CommandType.StoredProcedure));
            }
        }



        public virtual async Task<int> ExecuteAsyncSP(string spName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(
                spName,
                param: parameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public virtual async Task<int> ExecuteAsync(string sql, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sql, commandType);
            }
        }

        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, CommandType commandType = CommandType.Text, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, commandType: commandType, param: parameters);
            }
        }

        public virtual async Task<T> QueryFirstAsync<T>(string sql)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<T>(sql);
            }
        }

    }
}

