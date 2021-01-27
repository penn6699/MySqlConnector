using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// 
/// </summary>
public class MySqlConnector : IDisposable
{
    #region 字段
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    private string _ConnectionString;
    /// <summary>
    /// 数据库连接
    /// </summary>
    private MySqlConnection conn = null;

    #endregion

    #region 属性

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public string ConnectionString
    {
        get { return _ConnectionString; }
    }
    /// <summary>
    /// 数据库连接
    /// </summary>
    public MySqlConnection Connection
    {
        get
        {
            if (conn == null)
            {
                throw new Exception("数据库连接对象为 null ");
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }

            return conn;
        }
    }


    #endregion
    
    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public MySqlConnector()
    {
        _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySql_ETL"].ConnectionString;
        conn = new MySqlConnection(ConnectionString);
        conn.Open();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="ConnectionString">数据库连接字符串</param>
    public MySqlConnector(string ConnectionString)
    {
        _ConnectionString = ConnectionString;
        conn = new MySqlConnection(ConnectionString);
        conn.Open();
    }

    #endregion

    #region 数据库连接管理

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseConnection()
    {
        if (conn != null && conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
    }

    /// <summary>
    /// 打开数据库连接
    /// </summary>
    public void OpenConnection()
    {
        if (conn == null)
        {
            throw new Exception("数据库连接对象为 null ");
        }

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        else if (conn.State == ConnectionState.Broken)
        {
            conn.Close();
            conn.Open();
        }
    }

    #endregion
    
    #region MySqlCommand

    /// <summary>
    /// 创建数据库命令
    /// </summary>
    /// <param name="SQL">sql语句</param>
    /// <param name="pars">sql参数</param>
    /// <param name="CommandTimeout">超时时间。以秒为单位</param>
    /// <returns></returns>
    public MySqlCommand CreateSqlCommand(string SQL, MySqlParameter[] pars, CommandType cmdType, int CommandTimeout = 60)
    {
        MySqlCommand _SqlCommand = new MySqlCommand();
        _SqlCommand.Connection = Connection;
        _SqlCommand.CommandType = cmdType;

        if (!string.IsNullOrEmpty(SQL))
        {
            _SqlCommand.CommandText = SQL;
        }
        //以秒为单位
        _SqlCommand.CommandTimeout = CommandTimeout;

        _SqlCommand.Parameters.Clear();
        if (pars != null && pars.Length > 0)
        {
            _SqlCommand.Parameters.AddRange(pars);
        }
        return _SqlCommand;
    }

    /// <summary>
    /// 创建数据库命令
    /// </summary>
    /// <param name="SQL">sql语句</param>
    /// <param name="CommandTimeout">超时时间。以秒为单位</param>
    /// <returns></returns>
    public MySqlCommand CreateSqlCommand(string SQL, CommandType cmdType, int CommandTimeout = 60)
    {
        return CreateSqlCommand(SQL, null, cmdType, CommandTimeout);
    }

    /// <summary>
    /// 创建数据库命令
    /// </summary>
    /// <param name="CommandTimeout">超时时间。以秒为单位</param>
    /// <returns></returns>
    public MySqlCommand CreateSqlCommand(CommandType cmdType, int CommandTimeout = 60)
    {
        return CreateSqlCommand(null, null, cmdType, CommandTimeout);
    }

    /// <summary>
    /// 创建数据库命令
    /// </summary>
    /// <returns></returns>
    public MySqlCommand CreateSqlCommand()
    {
        MySqlCommand _SqlCommand = new MySqlCommand();
        _SqlCommand.Connection = Connection;
        return _SqlCommand;
    }


    #endregion






















    #region 释放资源

    private bool disposedValue = false; // 要检测冗余调用
    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        if (!disposedValue)
        {
            //释放数据库连接对象
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
            _ConnectionString = null;


            disposedValue = true;
        }
    }
    
    #endregion

}