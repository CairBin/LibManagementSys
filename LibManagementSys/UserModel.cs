using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    static class UserModelMenuText
    {
        public static string SEARCH_BOOK = "1";
        public static string BORROW_BOOK = "2";
        public static string CHANGE_PASSWORD = "3";
        public static string LOGOUT = "4";
    }

    internal class UserModel
    {
        private string[] array_user_model_menu_value = { "查询图书", "借书还书", "更改密码","注销登录" };

        public UserModel()
        {
            while(true)
            {
                Console.Clear();
                LoginMsg();
                InitMenu();

                Console.Write("请输入序号:");
                string user_choose = Console.ReadLine();

                if (user_choose == UserModelMenuText.SEARCH_BOOK)
                {
                    //图书查询
                    BookSearch book_search = new BookSearch();
                }
                else if (user_choose == UserModelMenuText.BORROW_BOOK)
                {
                    //借书还书
                    BorrowBook book_search = new BorrowBook();
                }
                else if (user_choose == UserModelMenuText.CHANGE_PASSWORD)
                {
                    //更改密码
                    ChangePassword change_password = new ChangePassword(UserManage.tb_user);

                }
                else if (user_choose == UserModelMenuText.LOGOUT)
                    break;

            }
        }

        //登录信息
        private void LoginMsg()
        {
            Console.WriteLine("********************");
            Console.WriteLine(String.Format("*你好，用户 {0}(ID:{1})", UserInfo.username, UserInfo.id));
            Console.WriteLine("********************");
        }

        //初始化菜单
        private void InitMenu()
        {
            Console.WriteLine("--------------------");
            for(int i = 0; i < array_user_model_menu_value.Length; i++)
                Console.WriteLine(i+1+"."+array_user_model_menu_value[i]);
            Console.WriteLine("--------------------");
        }


    }
}
