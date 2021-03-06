﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OliveFramework.tool
{
    public class MysqlHelper:Database
    {
        private string _connectionString = "";
        private MySqlConnection _conn = null;

        private static uint newtime = 0;
        private static uint distime = 0;

        public MysqlHelper()
        {
            newtime++;
            System.Diagnostics.Debug.WriteLine("A mysql instance was created.");
        }

        ~MysqlHelper()
        {
            _conn.Dispose();
        }

        public override void Connect(string server, string uid, string pwd, string db, uint timeout)
        {
            _connectionString = GetConnectionString(server,uid,pwd,db,timeout);
            System.Diagnostics.Debug.WriteLine("Connectiong string:" + _connectionString);
            _conn = new MySqlConnection(_connectionString);
            //_conn.Open();
        }

        public override void Close()
        {
            _conn.Close();
        }

        public override void Dispose()
        {
            distime++;
            System.Diagnostics.Debug.WriteLine("A mysql instance was disposed with dispose-times/create-times : {0}/{1}", distime,newtime);
            _conn.Close();
            _conn.Dispose();
        }

        private string GetConnectionString(string server, string uid, string pwd, string db, uint timeout)
        {

            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder();

            connectionString.Server = server;
            connectionString.UserID = uid;
            connectionString.Password = pwd;
            connectionString.Database = db;
            connectionString.ConnectionTimeout = timeout;
            //不使用连接池
            connectionString.Pooling = false;
            return connectionString.GetConnectionString(true);
        }


        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        private int _ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();
            int val = 0;

            try
            {
                PrepareCommand(cmd, _conn, null, cmdType, cmdText, commandParameters);
                val = cmd.ExecuteNonQuery();
                
            }catch(MySqlException ex)
            {
                throw new Database.Exception(ex.Number, ex.Message);
            }finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            return val;
        }


        public MySqlDataReader _ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;


            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, _conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
            }
            catch (MySqlException ex)
            {
                throw new Database.Exception(ex.Number, ex.Message);
            }finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

            return reader;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:
        ///  MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }

        public override DataSet GetDataSet(string sql)
        {
            return this._GetDataSet(CommandType.Text, sql, null);
        }

        public override List<V> SelectData<V>(string table)
        {
            return SelectData<V>(table, null, null, false, null);
        }

        public override List<V> SelectData<V>(string table,string where)
        {
            return SelectData<V>(table, null, where,false,null);
        }

        public override List<V> SelectData<V>(string table,string join,string where)
        {
            return SelectData<V>(table, join, where, false,null);
        }

        public override List<V> SelectData<V>(string table, string join, string where, bool distinct, string orderby)
        {
            List<V> RecordList = null;
            StringBuilder sql = new StringBuilder();
            StringBuilder col = new StringBuilder();

            V view =Activator.CreateInstance<V>();

            foreach (PropertyInfo p in view.GetType().GetProperties())
            {
                col.AppendFormat("`{0}`.`{1}`,",table, p.Name);
            }

            col.Remove(col.Length - 1, 1);

            if(distinct==true)
            {
                sql.AppendFormat("SELECT DISTINCT {0} FROM {1}", col.ToString(), table);
            }
            else
            {
                sql.AppendFormat("SELECT {0} FROM {1}", col.ToString(), table);
            }
            

            if(!string.IsNullOrWhiteSpace(join))
            {
                sql.AppendFormat(" {0}", join);
            }

            if(!string.IsNullOrWhiteSpace(where))
            {
                sql.AppendFormat(" WHERE {0}", where);
            }

            if(!string.IsNullOrWhiteSpace(orderby))
            {
                sql.AppendFormat(" ORDER BY {0}", orderby);
            }

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            DataSet ds =this._GetDataSet(CommandType.Text, sql.ToString(), null);

            if(ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
            {
                RecordList = FillData<V>(ds.Tables[0]);
            }else
            {

            }
            
            return RecordList;
        }

        public override int InsertSingleLine<T>(string tableName,T dataModel)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder col = new StringBuilder();
            StringBuilder val = new StringBuilder();

            //INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)

            foreach (PropertyInfo item in dataModel.GetType().GetProperties())
            {
                string name = item.Name;
                object value = item.GetValue(dataModel, null);

                if(value==null)
                {
                    continue;
                }

                col.AppendFormat("`{0}`,", name);

                if(item.PropertyType.ToString().Contains("Int"))
                {
                    val.AppendFormat("{0},", value.ToString());
                }else
                {
                    val.AppendFormat("'{0}',", value.ToString());
                }
            }
            col.Remove(col.Length-1, 1);
            val.Remove(val.Length-1, 1);

            sql = sql.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2});",tableName,col.ToString(),val.ToString());

            System.Diagnostics.Debug.WriteLine(sql.ToString());
            
            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;

        }

        public override int UpdateSingleLine<T>(string table, T data, string where)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder col = new StringBuilder();

            //UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson' 

            foreach (PropertyInfo item in data.GetType().GetProperties())
            {
                string name = item.Name;
                object value = item.GetValue(data, null);

                if(value==null)
                {
                    continue;
                }

                if (item.PropertyType.ToString().Contains("Int"))
                {
                    col.AppendFormat("`{0}`={1},",name,value.ToString());
                }
                else
                {
                    col.AppendFormat("`{0}`='{1}',", name, value.ToString());
                }

            }
            col.Remove(col.Length - 1, 1);


            sql.AppendFormat("UPDATE {0} SET {1}",table,col.ToString());

            if(!string.IsNullOrWhiteSpace(where))
            {
                sql.AppendFormat(" WHERE {0}",where);
            }

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;
        }


        public override int DeleteRecord(string table, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("DELETE FROM `{0}`", table);

            if (!string.IsNullOrWhiteSpace(where))
            {
                sql.AppendFormat(" WHERE {0}", where);
            }

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;
        }

        public override int CallProcedure(string ProcedureName, Hashtable ParametersList)
        {
            
            List<MySqlParameter> mysqlParameterList = new List<MySqlParameter>();

            foreach(DictionaryEntry de in ParametersList)
            {
                mysqlParameterList.Add(new MySqlParameter(de.Key.ToString(), de.Value));

            }

            MySqlParameter[] mysqlParameterArray = mysqlParameterList.ToArray();

            _ExecuteReader(CommandType.StoredProcedure, ProcedureName, mysqlParameterArray);

            return 0;
        }
        public override int BeginTransaction()
        {
            string sql = "START TRANSACTION;";

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;
        }

        public override int CommitTransaction()
        {
            string sql = "COMMIT;";

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;
        }

        public override int RollbackTransaction()
        {
            string sql = "ROLLBACK;";

            System.Diagnostics.Debug.WriteLine(sql.ToString());

            int ret = this._ExecuteNonQuery(CommandType.Text, sql.ToString(), null);

            return ret;
        }

        public override uint GetLastInsertID()
        {
            string sql = "SELECT LAST_INSERT_ID() AS LAST_INERT_ID;";
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            object ret = this._ExecuteScalar(CommandType.Text, sql, null);

            return uint.Parse(ret.ToString());
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="conn">一个有效的MySql连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        private DataSet _GetDataSet(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //System.Diagnostics.Debug.WriteLine(cmdText);

            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, _conn, null, cmdType, cmdText, commandParameters);

                //调用 MySqlCommand  的 ExecuteReader 方法
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                return ds;
            
            }catch(MySqlException ex)
            {
                throw new Database.Exception(ex.Number, ex.Message);
            }catch (System.Exception e)
            {
                throw e;
            }finally
            {
                adapter.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                
            }
            
            
        }



        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="conn">一个有效的MySql连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public object _ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            try
            {

                    PrepareCommand(cmd, _conn, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    return val;
                
            }
            catch (MySqlException ex)
            {
                throw new Database.Exception(ex.Number, ex.Message);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();

            }

        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="commandParameters">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 找回缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        private Hashtable ClsToTable<T>(T cls)
        {
            Hashtable table = new Hashtable();

            Type t = cls.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(cls, null);

                table.Add(name, value);
            }

            return table;
        }

        private string ParseWhere<W>(W where)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" WHERE ");

            foreach (PropertyInfo p in where.GetType().GetProperties())
            {
                string name = p.Name;
                object value = p.GetValue(where, null);

                if (p.PropertyType.ToString().Contains("Int"))
                {
                    sql.AppendFormat("{0}={1} ", name.ToString(), value.ToString());
                }else
                {
                    sql.AppendFormat("{0}='{1}' ", name.ToString(), value.ToString());
                }

                sql.Append("AND ");
                
            }

            sql.Remove(sql.Length - 4, 4);

            return sql.ToString();
        }

        private string ParseWhereExpr(string col,string expr)
        {
            StringBuilder sql = new StringBuilder();

            //取出中括号内字符串
            string mc1 = MatchReged(col, @"(?<=\{)(.*)(?=\})");

            //取出方括号内字符串
            string mc2 = MatchReged(col, @"(?<=\[)(.*)(?=\])");

            MatchCollection mc = Regex.Matches(col, @"(?<=\{)(.*)(?=\})");
            if(mc.Count==1)
            {
                string m = mc[0].ToString();
                if(m.Equals("&&"))
                {
                    sql.AppendFormat("{0}");
                }
            }else
            {
                System.Diagnostics.Debug.WriteLine(expr);
                throw new ExceptionMessage("/language/database/where_error");
            }

            return null;

        }

        private string MatchReged(string text, string expr)
        {
            MatchCollection mc = Regex.Matches(text, expr);
            Match m = mc[0];
            return m.ToString();
        }

        private List<V> FillData<V>(DataTable table)
        {
            List<V> list = new List<V>();

            foreach (DataRow row in table.Rows)
            {
                V view = Activator.CreateInstance<V>();
                foreach (PropertyInfo p in view.GetType().GetProperties())
                {
                    string colName = p.Name;
                    if (row[colName] != System.DBNull.Value)
                    {
                        p.SetValue(view, row[colName], null);
                    }
                    
                }
                
                list.Add(view);
            }
            return list;
        }

    }
}

