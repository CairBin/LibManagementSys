using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    internal class ChangePassword
    {
        private LinkMysql link_mysql = new LinkMysql();

        public ChangePassword(string tb)
        {
            Console.Clear();

            Console.Write("这是个危险的操作，是否继续？[Y/N]: ");
            string user_choose = Console.ReadLine();

            if (user_choose == "Y" || user_choose == "y")
                ChangePsw(tb);

        }

        private void ChangePsw(string tb)
        {
            int cnt = 0;
            while(true)
            {
                cnt++;
                if(cnt == 4)
                {
                    Console.WriteLine("------------------");
                    Console.WriteLine("输入次数过多，请返回重试！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("请输入原密码:");
                string psw = Console.ReadLine();
                if(psw == String.Empty)
                {
                    Console.WriteLine("原密码不为空！");
                    continue;
                }

                string sql = "select count(*) from " + tb + " where id=" + UserInfo.id
                + " and password = '" + HashEncryption.HashText(psw) + "'";

                int num = link_mysql.MySqlExec(sql);

                if(num == 1)
                {
                    Console.Write("请输入新密码：");
                    string new_psw = Console.ReadLine();
                    if(new_psw == String.Empty)
                    {
                        Console.WriteLine("新密码不为空！");
                        continue;
                    }

                    Console.Write("请再次输入新密码：");
                    string new_psw_again = Console.ReadLine();
                    if (new_psw == String.Empty)
                    {
                        Console.WriteLine("新密码不为空！");
                        continue;
                    }

                    if(new_psw_again != new_psw)
                    {
                        Console.WriteLine("两次输入密码不一致！");
                        continue;
                    }

                    //数据库修改密码
                    if(!link_mysql.ChangeData(tb, "id", UserInfo.id.ToString(), "password", HashEncryption.HashText(new_psw)))
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine("修改数据失败，请联系管理员检查数据库！");
                        Console.Write("按回车键继续...");
                        Console.ReadLine();
                        return;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("---------------");
                    Console.WriteLine("原密码错误！");
                    Console.Write("按回车键继续...");
                    Console.Read();
                    return;
                }


            }

        }

    }
}
