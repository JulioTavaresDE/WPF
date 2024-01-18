using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

using System.Data.SqlClient;


namespace AppWPF.Repositories
{
    public static class Repository
    {
        private static IEnumerable<T> Query<T>(SqlConnection conn, SqlTransaction tran, string sql, object dto)
        {
            try
            {
                return conn.Query<T>(sql, dto, tran);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public static T QuerySingle<T>(SqlConnection conn, SqlTransaction tran, string sql, object dto)
        {
            return conn.QuerySingle<T>(sql, dto, tran);
        }

        // Insert/Update/Delete method
        public static int Execute(string sql, object dto, SqlConnection conn, SqlTransaction tran)
        {
            return conn.Execute(sql, dto, tran);
        }

        public static List<T> GetList<T>(string sql, object dto, SqlConnection conn, SqlTransaction tran)
        {
            return Query<T>(conn, tran, sql, dto).ToList<T>();
        }

        public static List<TReturn> GetList<TFirst, TSecond, TReturn>(SqlConnection conn, SqlTransaction tran, string sql, object dto, Func<TFirst, TSecond, TReturn> map, string splitOn = "EnvironmentID")
        {            
            return conn.Query<TFirst, TSecond, TReturn>(sql, map, dto, tran, splitOn: splitOn).ToList<TReturn>();
        }
        public static List<TReturn> GetList<TFirst, TSecond, TThird, TReturn>(SqlConnection conn, SqlTransaction tran, string sql, object dto, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "EnvironmentID")
        {
            return conn.Query<TFirst, TSecond, TThird, TReturn>(sql, map, dto, tran, splitOn: splitOn).ToList<TReturn>();
        }

        /// <summary>
        /// Colocar os Objetos na MESMA ORDEM DOS JOINS, caso contrário o mapeamento irá se confundir.
        /// </summary>
        /// <param name="map">Colocar os Objetos na MESMA ORDEM DOS JOINS, caso contrário o mapeamento irá se confundir.</param>
        /// <returns></returns>
    

        public static List<TReturn> GetListConnection<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, object dto, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "EnvironmentID", SqlConnection conn=null, SqlTransaction tran=null)
        {
            return conn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, dto, tran, true, splitOn).ToList<TReturn>();
        }

        public static List<TReturn> GetListConnection<TFirst, TSecond, TThird, TFourth, TFifth, TSixth,  TReturn>(string sql, object dto, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "EnvironmentID", SqlConnection conn = null, SqlTransaction tran = null)
        {
            return conn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, dto, tran, true, splitOn).ToList<TReturn>();
        }

        public static List<TReturn> GetListConnection<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, object dto, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "EnvironmentID", SqlConnection conn=null, SqlTransaction tran=null)
        {
            return conn.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, dto, tran, true, splitOn).ToList<TReturn>();
        }

        public static T Get<T>(string sql, object dto, SqlConnection conn, SqlTransaction tran)
        {
            T ret = Query<T>(conn, tran, sql, dto).FirstOrDefault<T>();
            return (ret != null) ? ret : (T)Activator.CreateInstance(typeof(T));
        }

        public static TReturn Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, object dto, SqlConnection conn, SqlTransaction tran, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "EnvironmentID")
        {
            TReturn ret = conn.Query<TFirst, TSecond,  TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, dto, tran, true, splitOn).FirstOrDefault<TReturn>();

            return (ret != null) ? ret : (TReturn)Activator.CreateInstance(typeof(TReturn));
        }

        public static TReturn Get<TFirst, TSecond, TReturn>(string sql, object dto, SqlConnection conn, SqlTransaction tran, Func<TFirst, TSecond, TReturn> map, string splitOn = "EnvironmentID")
        {
            TReturn ret = conn.Query<TFirst, TSecond, TReturn>(sql, map, dto, tran, true, splitOn).FirstOrDefault<TReturn>();

            return (ret != null) ? ret : (TReturn)Activator.CreateInstance(typeof(TReturn));
        }

        public static int Count(string sql, object dto, SqlConnection conn, SqlTransaction tran)
        {
            return QuerySingle<int>(conn, tran, sql, dto);
        }

        public static T GetNextId<T>(string sql, object dto, SqlConnection conn, SqlTransaction tran)
        {
            return QuerySingle<T>(conn, tran, sql, dto);
        }

    }
}