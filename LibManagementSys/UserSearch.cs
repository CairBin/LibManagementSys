using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LibManagementSys
{
    static class UserSearchMenuText
    {
        public const string SEARCH_AS_ID = "1";
        public const string SEARCH_AS_USERNAME = "2";
        public const string USER_TABLE = "3";
        public const string BACK = "4";
    }

    internal class UserSearch
    {
        private string[] array_user_search_menu_value = { "按ID查询", "按用户名查询", "输出用户表","返回" };

        //数据库
        private LinkMysql link_mysql = new LinkMysql();

        public UserSearch()
        {
            while(true)
            {
                Console.Clear();
                InitMenu();
                Console.Write("请输入序号:");
                string user_choose = Console.ReadLine();

                if (user_choose == UserSearchMenuText.SEARCH_AS_ID)
                {
                    //按ID查询
                    SearchUserAsKeyword(UserManage.tb_user, "id", "ID");
                }
                else if (user_choose == UserSearchMenuText.SEARCH_AS_USERNAME)
                {
                    //按用户名
                    SearchUserAsKeyword(UserManage.tb_user, "username", "用户名");
                }
                else if (user_choose == UserSearchMenuText.USER_TABLE)
                {
                    //输出普通用户表
                    PrintUserTable(UserManage.tb_user);
                }
                else if (user_choose == UserSearchMenuText.BACK)
                    break;

            }
        }

        private void InitMenu()
        {
            Console.WriteLine("------------------");
            for(int i = 0; i < array_user_search_menu_value.Length; i++)
                Console.WriteLine(i+1+"."+array_user_search_menu_value[i]);
            Console.WriteLine("------------------");
        }

        private void SearchUserAsKeyword(string tb, string keyword, string tip_word)
        {
            int cnt = 0;

            while (true)
            {
                cnt++;
                if (cnt == 4)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine("操作错误次数过多，请返回重新选择！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                Console.Clear();
                Console.Write("请输入" + tip_word + ":");
                string input_str = Console.ReadLine();
                if (input_str == String.Empty)
                {
                    Console.WriteLine("输入不为空！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    continue;
                }

                if (!link_mysql.DataIsHere(tb, keyword, input_str))
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("所查询不存在!");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine(" ");

                //输出信息
                string sql = String.Format("select username,id from {0} where {1}='{2}';", tb, keyword, input_str);

                MySqlDataReader rd = link_mysql.MySqlExecCol(sql);
                while (rd.Read())
                {

                    Console.WriteLine("-----------------");
                    for (int i = 0; i < rd.FieldCount; i++)
                        Console.WriteLine(rd.GetName(i) + ": " + rd.GetString(i));
                    Console.WriteLine("-----------------");
                }
                LinkMysql.connection.Close();
                Console.Write("按回车键继续...");
                Console.ReadLine();
                break;
            }
        }

        private void PrintUserTable(string tb)
        {
            Console.Clear();

            //输出信息
            string sql = String.Format("select username,id from {0};", tb);

            MySqlDataReader rd = link_mysql.MySqlExecCol(sql);
            while (rd.Read())
            {

                Console.WriteLine("-----------------");
                for (int i = 0; i < rd.FieldCount; i++)
                    Console.WriteLine(rd.GetName(i) + ": " + rd.GetString(i));
                Console.WriteLine("-----------------");
            }
            LinkMysql.connection.Close();
            Console.Write("按回车键继续...");
            Console.ReadLine();
        }

    }
}
