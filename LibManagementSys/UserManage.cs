using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    static class UserManageMenuText
    {
        public const string DEL_USER = "1";
        public const string SEARCH_USER = "2";
        public const string ADD_ADMIN = "3";
        public const string BACK = "4";
    }

    internal class UserManage
    {
        private string[] array_user_manage_menu_value = {"删除普通用户","查询普通用户","添加管理人员","返回"};

        //用户数据表名
        static public string tb_user = "tb_user";

        private const string tb_admin = "tb_admin";

        //数据库
        private LinkMysql link_sql = new LinkMysql();

        public UserManage()
        {
            while(true)
            {
                Console.Clear();
                InitMenu();
                Console.Write("请输入序号:");
                string user_choose = Console.ReadLine();

                if(user_choose == UserManageMenuText.DEL_USER)
                {
                    //删除用户
                    DelUser();
                }
                else if(user_choose == UserManageMenuText.SEARCH_USER)
                {
                    //查询用户
                    UserSearch user_manage = new UserSearch();
                }
                else if(user_choose == UserManageMenuText.ADD_ADMIN)
                {
                    //添加管理员用户
                    RegisterModel add_admin = new RegisterModel(tb_admin);
                }
                else if(user_choose== UserManageMenuText.BACK)
                {
                    break;
                }

            }
        }

        private void InitMenu()
        {
            Console.WriteLine("--------------------");
            for(int i = 0; i < array_user_manage_menu_value.Length; i++)
                Console.WriteLine(i+1+"."+array_user_manage_menu_value[i]);
            Console.WriteLine("--------------------");
        }


        private void DelUser()
        {
            Console.Clear();
            Console.Write("请输入所删除用户ID:");
            string user_id = Console.ReadLine();

            if(user_id == String.Empty)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("输入不为空！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            if(!link_sql.DataIsHere(tb_user,"id",user_id))
            {
                Console.WriteLine("----------------");
                Console.WriteLine("用户不存在！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            if(!link_sql.DelData(tb_user,"id",user_id))
            {
                Console.WriteLine("----------------");
                Console.WriteLine("删除失败，请检查数据库！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

        }

        

    }
}
