using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LibManagementSys
{
    static class BookSearchMenuText
    {
        public const string AS_BOOKNAME = "1";
        public const string AS_CLASS = "2";
        public const string AS_AUTHOR = "3";
        public const string AS_PUBLISHER = "4";
        public const string AS_DATE = "5";
        public const string BACK = "6";
    }


    internal class BookSearch
    {
        private string[] array_book_serch_menu_text = { "按书名查询", "按分类查询", "按作者查询", "按出版社查询", "按出版时间查询","返回" };

        private LinkMysql link_mysql = new LinkMysql();

        public BookSearch()
        {
            while(true)
            {
                Console.Clear();
                InitMenu();
                Console.Write("请输入序号:");
                string user_choose = Console.ReadLine();

                if (user_choose == BookSearchMenuText.AS_BOOKNAME)
                {
                    //按书名查询
                    SearchBookAsKeyword(BookManage.tb_book,"bookname","书名");
                }
                else if (user_choose == BookSearchMenuText.AS_CLASS)
                {
                    //按分类查询
                    SearchBookAsKeyword(BookManage.tb_book, "bookclass", "分类");
                }
                else if (user_choose == BookSearchMenuText.AS_AUTHOR)
                {
                    //按作者查询
                    SearchBookAsKeyword(BookManage.tb_book, "author", "作者");
                }
                else if (user_choose == BookSearchMenuText.AS_PUBLISHER)
                {
                    //按出版社查询
                    SearchBookAsKeyword(BookManage.tb_book, "publisher", "出版社");
                }
                else if (user_choose == BookSearchMenuText.AS_DATE)
                {
                    //按出版日期
                    Console.WriteLine("******************");
                    Console.WriteLine("*（注意）按照出版日期查询需要您使用正确格式: XXXX-XX-XX");
                    Console.WriteLine("******************");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    SearchBookAsKeyword(BookManage.tb_book, "date", "出版日期");
                }
                else if (user_choose == BookSearchMenuText.BACK)
                    break;


            }
        }


        private void InitMenu()
        {
            Console.WriteLine("----------------");
            for(int i = 0; i < array_book_serch_menu_text.Length; i++)
                Console.WriteLine(i+1+"."+array_book_serch_menu_text[i]);
            Console.WriteLine("----------------");
        }

        private void SearchBookAsKeyword(string tb, string keyword,string tip_word)
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
                Console.Write("请输入"+ tip_word +":");
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

                //输出图书信息
                string sql = String.Format("select bookname,author,publisher,price from {0} where {1}='{2}';", tb ,keyword ,input_str);

                MySqlDataReader rd = link_mysql.MySqlExecCol(sql);
                //输出图书信息
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

    }
}
