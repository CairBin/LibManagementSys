using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    static class MainMenuText
    {
        public const string USER = "1";
        public const string ADMIN = "2";
        public const string REGISTER = "3";
        public const string EXIT = "4";
    }

    internal class MainMenu
    {
        private string[] array_main_menu_text = {"用户","管理员","注册","退出"};


        public MainMenu()
        {
            while(true)
            {
                System.Console.Clear(); //清屏
                string user_choose = String.Empty;
                InitMenuText();
                Console.Write("请输入序号:");
                user_choose = Console.ReadLine();

                if(user_choose == MainMenuText.USER)
                {
                    LoginModel login_model = new LoginModel();
                    if (!login_model.LoginId(MainMenuText.USER))
                    {
                        Console.WriteLine("---------------");
                        Console.WriteLine("登录失败！");
                        Console.Write("按回车键继续...");
                        Console.ReadLine();
                    }
                    else
                    {
                        UserModel user_model = new UserModel();
                    }
                }
                else if(user_choose == MainMenuText.ADMIN)
                {
                    LoginModel login_model = new LoginModel();
                    if (!login_model.LoginId(MainMenuText.ADMIN))
                    {
                        Console.WriteLine("---------------");
                        Console.WriteLine("登录失败！");
                        Console.Write("按回车键继续...");
                        Console.ReadLine();
                    }
                    else
                    {
                        AdminModel admin_model = new AdminModel();
                    }
                }
                else if(user_choose == MainMenuText.REGISTER)
                {
                    RegisterModel register_model = new RegisterModel(UserManage.tb_user);
                }
                else if(user_choose == MainMenuText.EXIT)
                {
                    Environment.Exit(0);
                }


            }
        }

        //输出主菜单内容
        private void InitMenuText()
        {
            Console.WriteLine("图书管理系统");
            Console.WriteLine("--------------------");
            for(int i=0; i<array_main_menu_text.Length; i++)
                Console.WriteLine(i+1+"."+array_main_menu_text[i]);
            Console.WriteLine("--------------------");
        }

    }
}
