using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LibManagementSys
{
    internal class LoginModel
    {
        private LinkMysql link_sql = new LinkMysql();

        private string USER = "1";
        private string ADMIN = "2";

        public bool LoginId(string login_kind)
        {
            Console.Clear();    //清屏

            string sql_tb = String.Empty;
            if (login_kind == USER)
                sql_tb = "tb_user";
            else if (login_kind == ADMIN)
                sql_tb = "tb_admin";
            else
                return false;

            int user_id = 0;
            string password = String.Empty;

            int cnt = 0;

            while(true)
            {
                cnt++;
                if (cnt == 4)
                {
                    Console.WriteLine("-----------");
                    Console.WriteLine("输入错误次数过多！");
                    Console.Write("按回车键继续！");
                    Console.ReadLine();
                }


                Console.Write("请输入ID:");
                string temp = Console.ReadLine();
                if(temp == String.Empty)
                {
                    Console.WriteLine("ID不能为空！");
                    continue;
                }
                user_id = int.Parse(temp);

                Console.Write("请输入密码:");
                password = Console.ReadLine();
                if(password == String.Empty)
                {
                    Console.WriteLine("密码不能为空!");
                    continue;
                }

                string sql = "select count(*) from " + sql_tb + " where id=" + user_id 
                + " and password = '" + HashEncryption.HashText(password) + "'";

                int num = link_sql.MySqlExec(sql);

                if (num == 1)
                {
                    UserInfo.id = user_id;

                    // 管理员标识
                    if (login_kind == ADMIN) UserInfo.is_admin = true;
                    else UserInfo.is_admin = false;

                    sql = String.Format("select username from {0} where id={1} and password='{2}'", sql_tb, user_id, HashEncryption.HashText(password));
                    UserInfo.username = link_sql.MySqlExecFirstRowCol(sql).ToString();
                    LinkMysql.connection.Close();   //关闭数据库连接
                    break;
                }
                else
                    return false;

                

            }


            return true;
        }


    }
}
