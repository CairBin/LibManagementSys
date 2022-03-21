using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    static class AdminMenuText
    {
        public const string BOOK_MANAGE = "1";
        public const string USER_MANAGE = "2";
        public const string CHANGE_PASSWORD = "3";
        public const string LOGOUT = "4";
    }

    internal class AdminModel
    {
        private string[] array_admin_menu_text = {"图书信息管理","用户信息管理","更改密码(本账户)","注销登录"};

        public AdminModel()
        {
            while(true)
            {
                Console.Clear();
                LoginMsg(); //登陆消息
                InitMenu(); //加载菜单

                string user_choose = String.Empty;
                Console.Write("请输入序号:");
                user_choose = Console.ReadLine();

                if(user_choose == AdminMenuText.BOOK_MANAGE)
                {
                    //图书管理
                    BookManage book_manage = new BookManage();
                }
                else if(user_choose == AdminMenuText.USER_MANAGE)
                {
                    //普通用户管理
                    UserManage user_manage = new UserManage();
                }
                else if(user_choose==AdminMenuText.CHANGE_PASSWORD)
                {
                    //修改本账户密码
                    ChangePassword change_password = new ChangePassword("tb_admin");
                }
                else if(user_choose==AdminMenuText.LOGOUT)
                {
                    break;
                }

            }
        }


        //登录消息
        private void LoginMsg()
        {
            Console.WriteLine("*****************");
            string str = String.Format("*你好，管理员 {0}(ID:{1})", UserInfo.username, UserInfo.id);
            Console.WriteLine(str);
            Console.WriteLine("*****************");
        }

        //初始化菜单
        private void InitMenu()
        {
            Console.WriteLine(" ");
            Console.WriteLine("------------------");
            for(int i = 0; i < array_admin_menu_text.Length; i++)
                Console.WriteLine(i+1+"."+array_admin_menu_text[i]);
            Console.WriteLine("------------------");
        }

    }
}
