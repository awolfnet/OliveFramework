﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OliveFramework.tool
{
    public abstract class Database: IDisposable
    {
        /// <summary>
        /// 链接到数据库
        /// </summary>
        /// <param name="server">服务器地址</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="db">数据库</param>
        /// <param name="timeout">链接超时</param>
        abstract public void Connect(string server, string uid, string pwd, string db, uint timeout);

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        abstract public void Close();

        /// <summary>
        /// 执行带返回结果集的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>查询结果集</returns>
        abstract public DataSet GetDataSet(string sql);


        abstract public List<V> SelectData<V>(string table, string join, string where,bool distinct, string orderby);

        /// <summary>
        /// 选择数据
        /// </summary>
        /// <typeparam name="V">视图模型</typeparam>
        /// <param name="table">表名</param>
        /// <param name="join">关联表</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        abstract public List<V> SelectData<V>(string table, string join, string where);

        /// <summary>
        /// 选择数据
        /// </summary>
        /// <typeparam name="V">视图模型</typeparam>
        /// <param name="table">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        abstract public List<V> SelectData<V>(string table, string where);

        /// <summary>
        /// 选择数据
        /// </summary>
        /// <typeparam name="V">试图模型</typeparam>
        /// <param name="table">表名</param>
        /// <returns></returns>
        abstract public List<V> SelectData<V>(string table);

        /// <summary>
        /// 向数据库插入一行数据
        /// </summary>
        /// <typeparam name="T">数据模型类</typeparam>
        /// <param name="t">数据</param>
        /// <returns>插入ID</returns>
        abstract public int InsertSingleLine<T>(string tableName,T dataModel);

        /// <summary>
        /// 更新一行数据
        /// </summary>
        /// <typeparam name="T">数据模型类</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="dataModel">数据</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        abstract public int UpdateSingleLine<T>(string table, T data, string where);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        abstract public int DeleteRecord(string table, string where);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="ParametersList"></param>
        /// <returns></returns>
        abstract public int CallProcedure(string ProcedureName, Hashtable ParametersList);

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        abstract public int BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        abstract public int CommitTransaction();


        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        abstract public int RollbackTransaction();


        /// <summary>
        /// 获取最后插入的自增长ID
        /// </summary>
        /// <returns></returns>
        abstract public uint GetLastInsertID();
        public abstract void Dispose();

        public class Exception:System.Exception
        {
            public int Code;
            public new String Message="";

            public Exception(int Code,String Message)
            {
                this.Code = Code;
                this.Message = Message;

            }
                
        }

    }
}