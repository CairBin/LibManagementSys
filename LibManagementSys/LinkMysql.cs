using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LibManagementSys
{
    internal class LinkMysql
    {
        static private string server = "127.0.0.1";
        static private int port = 3306;
        static private string user = "libsys";
        static private string password = "123456";
        static private string database = "db_libsys";

        //数据库连接字符串
        static private string connString = "server=" + server + ";" +
                                            "port=" + port + ";" +
                                            "user=" + user + ";" +
                                            "password=" + password + ";" +
                                            "database=" + database + ";";

        //数据库连接对象
        static public MySqlConnection connection = new MySqlConnection(connString);

        public int MySqlExec(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, connection);   //执行MySql语句
            if (connection.State == System.Data.ConnectionState.Closed)  //若处于关闭状态
                connection.Open();  //则打开数据库
            int num = Convert.ToInt32(cmd.ExecuteScalar()); //执行查询,返回的时执行结果中的第一行第一列
            connection.Close();
            return num; //返回查询结果
        }

        public int MySqlExecRes(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            int res = cmd.ExecuteNonQuery();    //返回受影响的函数
            connection.Close();

            return res;
        }

        //获取数据
        //使用MySqlExecCol函数一定要关闭数据库
        public MySqlDataReader MySqlExecCol(string sql)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader rd = cmd.ExecuteReader();
            //此处一定不要关数据库连接，否则会读取报错
            //connection.Close();
            return rd;
        }

        //返回第一行第一列的数据
        public object MySqlExecFirstRowCol(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            object result = cmd.ExecuteScalar();
            connection.Close();

            return result;
        }

        public bool InsetSql(string sql_tb, List<TableValue> lst)
        {
            //数据库语句
            if (sql_tb == String.Empty || lst.Count == 0) return false;
            string sql = "insert into " + sql_tb + "(";
            for(int i=0;i<lst.Count;i++)
            {
                if (i < lst.Count - 1)
                    sql += lst[i].name + ",";
                else
                    sql += lst[i].name + ") ";
            }

            sql += "values(";
            for(int i=0; i<lst.Count;i++)
            {
                if (i < lst.Count - 1)
                    sql += "'" + lst[i].value + "',";
                else
                    sql += "'" + lst[i].value + "'); ";
            }

            MySqlCommand cmd = new MySqlCommand(sql, LinkMysql.connection);
            if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                LinkMysql.connection.Open();
            int res = cmd.ExecuteNonQuery();

            connection.Close();

            if(res != 1) return false;


            return true;
        }


        //检查记录存在
        public bool DataIsHere(string tb, string where, string value)
        {
            string sql = String.Format("select COUNT({0}) from {1} where {2}='{3}';", where, tb, where, value);
            MySqlCommand cmd = new MySqlCommand(sql, LinkMysql.connection);
            if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                LinkMysql.connection.Open();

            int res = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            if (res == 0) return false;

            return true;
        }

        //删除记录 
        public bool DelData(string tb, string where, string value)
        {
            string sql = String.Format("delete from {0} where {1}={2};", tb, where, value);
            MySqlCommand cmd = new MySqlCommand(sql, LinkMysql.connection);
            if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                LinkMysql.connection.Open();

            int res = cmd.ExecuteNonQuery();
            connection.Close();
            if (res == 0) return false;

            return true;
        }

        //修改数据
        public bool ChangeData(string tb, string where, string where_value, string change_where, string change_where_value)
        {
            string sql = String.Format("update {0} set {1}='{2}' where {3}='{4}';",tb,change_where,change_where_value,where,where_value);
            MySqlCommand cmd = new MySqlCommand(sql, LinkMysql.connection);
            if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                LinkMysql.connection.Open();

            int res = cmd.ExecuteNonQuery();
            connection.Close();
            if (res == 0) return false;

            return true;
        }

        public bool DelDataTwoWhere(string tb, string where, string value, string where2, string value2)
        {
            string sql = String.Format("delete from {0} where {1}='{2}' and {3}='{4}';", tb, where, value,where2,value2);
            MySqlCommand cmd = new MySqlCommand(sql, LinkMysql.connection);
            if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                LinkMysql.connection.Open();

            int res = cmd.ExecuteNonQuery();
            connection.Close();
            if (res == 0) return false;

            return true;
        }

        public bool CheckMysql()
        {
            bool flag = true;

            try
            {
                if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                    LinkMysql.connection.Open();

                if(LinkMysql.connection.State == System.Data.ConnectionState.Open)
                {
                    
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch
            {
                flag = false;
            }
            finally
            {
                LinkMysql.connection.Close();
            }

            return flag;
            
        }

    }
}
