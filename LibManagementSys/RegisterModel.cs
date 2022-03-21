using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace LibManagementSys
{
    internal class RegisterModel
    {

        public RegisterModel(string tb)
        {
            int cnt = 0;
            string user = String.Empty;
            string password = String.Empty;
            string password_again = String.Empty;
            Console.Clear();
            while (true)
            {
                cnt++;
                if(cnt == 4)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine("您输入错误次数过多！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    cnt = 0;

                    break;
                }
                Console.WriteLine("请输入账户:");
                user = Console.ReadLine();
                if (user == String.Empty)
                {
                    Console.WriteLine("用户名不为空！");
                    continue;
                }

                Console.WriteLine("请输入密码:");
                password = Console.ReadLine();

                Console.WriteLine("请确定密码:");
                password_again = Console.ReadLine();

                if (password == String.Empty || password_again == String.Empty)
                {
                    Console.WriteLine("密码不为空");
                    continue;
                }

                if(password != password_again)
                {
                    Console.WriteLine("两次密码不一致，请重新输入！");
                    continue;
                }

                if (password == password_again)
                {
                    string msg = String.Empty;
                    int user_id = 0;

                    //将数据写入数据库

                    //数据库语句
                    string sql = String.Format("insert into {2}(username,password) values('{0}','{1}'); select @@Identity from {2}",
                                                user,
                                                HashEncryption.HashText(password),
                                                tb);

                    MySqlCommand cmd = new MySqlCommand(sql,LinkMysql.connection);
                    if (LinkMysql.connection.State == System.Data.ConnectionState.Closed)
                        LinkMysql.connection.Open();

                    int res = cmd.ExecuteNonQuery();
                    if (res == 1)
                    {
                        sql = String.Format("select @@Identity from {0}",tb);
                        cmd = new MySqlCommand(sql, LinkMysql.connection);
                        user_id = Convert.ToInt32(cmd.ExecuteScalar());
                        msg = "注册成功！您的ID为:" + user_id;
                    }
                    else
                        msg = "注册失败，请管理员检查数据库连接！";

                    LinkMysql.connection.Close();   //关闭数据库连接

                    Console.WriteLine("-----------------");
                    Console.WriteLine(msg);
                    Console.Write("请按回车键继续...");
                    Console.ReadLine();

                    break;
                }


            }
        }


    }
}
