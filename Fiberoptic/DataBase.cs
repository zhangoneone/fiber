using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;

namespace FiberopticServer
{
    class DataBase
    {
       string ConString="asdfsaf";//连接字符串
       short[] data;
       public void initDatabase()//初始化数据库
       {
           OleDbConnection Conn = new OleDbConnection(ConString);
           OleDbCommand comm = new OleDbCommand();
           OleDbDataReader dr;
           Conn.Open();
           comm.Connection = Conn;
           comm.CommandText = "";//sql语句
           dr = comm.ExecuteReader();
           while (dr.Read())
           {
               
           }
           dr.Close();
           Conn.Close();

       }
       public void StoreData(short []data)//保存数据
       {

       }
       public short[] LoadData()//读取数据
       {
           return data;
       }
       
    }
}
