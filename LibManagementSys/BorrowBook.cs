using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibManagementSys
{
    static class BorrowBookMenuText
    {
        public const string BORROW = "1";
        public const string RETURN = "2";
        public const string USER_BORROWED = "3";
        public const string BACK = "4";
    }

    internal class BorrowBook
    {
        private LinkMysql link_mysql = new LinkMysql();

        private string[] array_borrow_menu_value = { "我要借书","我要还书","已借图书","返回" };

        static public string tb_borrowed_book = "tb_borrowed_book";

        public BorrowBook()
        {
            BorrowBookInfo.id = UserInfo.id;
            BorrowBookInfo.username = UserInfo.username;

            //图书借还模块
            while(true)
            {
                Console.Clear();
                InitMenu();
                Console.Write("请选择序号:");
                string user_choose = Console.ReadLine();

                if (user_choose == BorrowBookMenuText.BORROW)
                {
                    //借书
                    BorrowBookInLib();
                }
                else if (user_choose == BorrowBookMenuText.RETURN)
                {
                    //还书
                    ReturnBook();
                }
                else if (user_choose == BorrowBookMenuText.USER_BORROWED)
                {
                    //借书列表
                    BorrowedBookList();
                }
                else if (user_choose == BorrowBookMenuText.BACK)
                    break;


            }
            

        }

        private void InitMenu()
        {
            Console.WriteLine("-----------------");
            for(int i=0;i<array_borrow_menu_value.Length;i++)
                Console.WriteLine(i+1+"."+array_borrow_menu_value[i]);
            Console.WriteLine("-----------------");

        }

        private void BorrowBookInLib()
        {
            BorrowBookInfo.id = UserInfo.id;
            BorrowBookInfo.username = UserInfo.username;

            Console.Clear();
            Console.Write("请输入书号:");
            string isbn = Console.ReadLine();

            if(isbn == String.Empty)
            {
                Console.WriteLine("----------");
                Console.WriteLine("输入不能为空！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            if(!link_mysql.DataIsHere(BookManage.tb_book,"ISBN",isbn))
            {
                Console.WriteLine("----------");
                Console.WriteLine("该图书不存在！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            if (link_mysql.DataIsHere(tb_borrowed_book, "ISBN", isbn))
            {
                Console.WriteLine("----------");
                Console.WriteLine("您不能借两本及其以上的相同图书！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            //获取图书数量
            string sql = String.Format("select stock_num from {0} where ISBN='{1}';", BookManage.tb_book, isbn);
            int book_num = Convert.ToInt32(link_mysql.MySqlExecFirstRowCol(sql));

            if(book_num == 0)
            {
                Console.WriteLine("----------");
                Console.WriteLine("该图书无库存或已经被借光！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            BorrowBookInfo.ISBN = isbn;
            sql = String.Format("select bookname,author,publisher,bookclass from {0} where ISBN='{1}';", BookManage.tb_book, isbn);

            //使用MySqlExecCol函数一定要关闭数据库
            MySqlDataReader rd = link_mysql.MySqlExecCol(sql);

            while(rd.Read())
            {
                BorrowBookInfo.bookname = rd.GetString(0);
                BorrowBookInfo.author = rd.GetString(1);
                BorrowBookInfo.publisher = rd.GetString(2);
                BorrowBookInfo.bookclass = rd.GetString(3);
            }
            //使用MySqlExecCol函数一定要关闭数据库
            LinkMysql.connection.Close();

            List<TableValue> lst_borrowed = new List<TableValue>
            {
                new TableValue("id",BorrowBookInfo.id),
                new TableValue("username",BorrowBookInfo.username),
                new TableValue("bookname",BorrowBookInfo.bookname),
                new TableValue("author",BorrowBookInfo.author),
                new TableValue("publisher",BorrowBookInfo.publisher),
                new TableValue("bookclass",BorrowBookInfo.bookclass),
                new TableValue("ISBN",BorrowBookInfo.ISBN)
            };

            link_mysql.InsetSql(tb_borrowed_book, lst_borrowed);

            //图书量减少1，并更新数据库信息
            book_num--;
            link_mysql.ChangeData(BookManage.tb_book, "ISBN", isbn, "stock_num", book_num.ToString());

            Console.WriteLine("------------");
            Console.WriteLine("操作完成！");
            Console.Write("按回车键继续...");
            Console.ReadLine();

        }

        private void ReturnBook()
        {
            Console.Clear();

            Console.Write("请输入书号:");
            string isbn = Console.ReadLine();

            if (isbn == String.Empty)
            {
                Console.WriteLine("----------");
                Console.WriteLine("输入不能为空！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            if (!link_mysql.DataIsHere(tb_borrowed_book, "ISBN", isbn))
            {
                Console.WriteLine("----------");
                Console.WriteLine("您并没有借该图书！");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            //获取图书数量
            string sql = String.Format("select stock_num from {0} where ISBN='{1}';", BookManage.tb_book, isbn);
            int book_num = Convert.ToInt32(link_mysql.MySqlExecFirstRowCol(sql));
            book_num++;
            //更新数据库信息
            if(!link_mysql.ChangeData(BookManage.tb_book,"ISBN",isbn,"stock_num",book_num.ToString()))
            {
                Console.WriteLine("----------------");
                Console.WriteLine("还书失败，请联系管理员!");
                Console.Write("按回车键继续...");
                Console.ReadLine();
                return;
            }

            link_mysql.DelDataTwoWhere(tb_borrowed_book, "id", UserInfo.id.ToString(), "ISBN",isbn);

            Console.WriteLine("------------");
            Console.WriteLine("操作完成！");
            Console.Write("按回车键继续...");
            Console.ReadLine();

        }

        private void BorrowedBookList()
        {
            string sql = String.Format("select bookname,author,publisher,bookclass from {0} where id='{1}';", tb_borrowed_book, UserInfo.id);
            MySqlDataReader rd = link_mysql.MySqlExecCol(sql);

            while(rd.Read())
            {
                Console.WriteLine("-------------------");
                for(int i = 0; i < rd.FieldCount; i++)
                    Console.WriteLine(rd.GetName(i)+": "+rd.GetString(i));
                Console.WriteLine("-------------------");
            }

            LinkMysql.connection.Close();

            Console.WriteLine("------------");
            Console.WriteLine("操作完成！");
            Console.Write("按回车键继续...");
            Console.ReadLine();

        }

    }
}
