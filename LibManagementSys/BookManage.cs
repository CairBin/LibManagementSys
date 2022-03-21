using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibManagementSys
{
    static class BookManageText
    {
        public const string ADD_BOOK = "1";
        public const string CHANGE_BOOK = "2";
        public const string REMOVE_BOOK = "3";
        public const string SEARCH_BOOK = "4";
        public const string BACK = "5";
    }

    internal class BookManage
    {
        private string[] array_book_manage_menu_text = { "增加图书信息", "修改图书信息", "删除图书信息","查询图书信息","返回"};

        private const string date_regex_str = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";

        static public string tb_book = "tb_book";

        private LinkMysql link_mysql = new LinkMysql(); //数据库操作对象

        //图书管理菜单
        public BookManage()
        {
            string user_choose = String.Empty;
            while(true)
            {
                Console.Clear();    //清屏
                Console.WriteLine("---------------------");
                for(int i=0;i<array_book_manage_menu_text.Length;i++)
                    Console.WriteLine(i+1+"."+array_book_manage_menu_text[i]);
                Console.WriteLine("---------------------");
                Console.Write("请选择序号:");
                user_choose = Console.ReadLine();

                if(user_choose == BookManageText.ADD_BOOK)
                {
                    //增加图书
                    AddBookInfo();
                }
                else if(user_choose == BookManageText.CHANGE_BOOK)
                {
                    //修改图书
                    ChangeBookInfo();
                }
                else if(user_choose==BookManageText.REMOVE_BOOK)
                {
                    //删除图书
                    DelBookInfo();
                }
                else if(user_choose == BookManageText.SEARCH_BOOK)
                {
                    BookSearch book_search = new BookSearch();
                }
                else if(user_choose==BookManageText.BACK)
                {
                    //返回
                    break;
                }


            }
        }


        //增加图书信息
        private void AddBookInfo()
        {
            Console.Clear();    //清屏
            int cnt = 0;
            while(true)
            {
                cnt++;
                if(cnt==4)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine("操作错误次数过多，请返回重新选择！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                }

                Console.Write("请输入书号:");
                BookInfo.ISBN = Console.ReadLine();
                if(BookInfo.ISBN == String.Empty)
                {
                    Console.WriteLine("书号不为空！");
                    continue;
                }

                Console.Write("请输入书名:");
                BookInfo.bookname = Console.ReadLine();
                if(BookInfo.bookname == String.Empty)
                {
                    Console.WriteLine("书名不为空！");
                    continue;
                }

                Console.Write("请输入作者:");
                BookInfo.author = Console.ReadLine();
                if (BookInfo.author == String.Empty)
                {
                    Console.WriteLine("作者名不为空！");
                    continue;
                }

                Console.Write("请输入出版社:");
                BookInfo.publisher = Console.ReadLine();
                if (BookInfo.publisher == String.Empty)
                {
                    Console.WriteLine("出版社不为空！");
                    continue;
                }

                Console.Write("请输入图书分类:");
                BookInfo.bookclass = Console.ReadLine();
                if (BookInfo.bookclass == String.Empty)
                {
                    Console.WriteLine("图书分类不为空！");
                    continue;
                }

                Console.Write("请输入库存量:");
                string temp = Console.ReadLine();
                if(temp == String.Empty || !Regex.IsMatch(temp, @"^[+-]?\d*[.]?\d*$") || Convert.ToInt32(temp)<0)
                {
                    Console.WriteLine("库存量不为空且为不小于零的整数！");
                    continue;
                }
                BookInfo.stock_num = Convert.ToInt32(temp);

                temp = String.Empty;
                Console.Write("请输入总量:");
                temp = Console.ReadLine();
                if (temp == String.Empty || !Regex.IsMatch(temp, @"^[+-]?\d*[.]?\d*$") || Convert.ToInt32(temp) < 0)
                {
                    Console.WriteLine("总量不为空且为不小于零的整数！");
                    continue;
                }
                BookInfo.total_num = Convert.ToInt32(temp);

                temp = String.Empty;
                Console.Write("请输入单价:");
                temp = Console.ReadLine();
                if (temp == String.Empty || !Regex.IsMatch(temp, @"^[+-]?\d*[.]?\d*$") || Convert.ToDouble(temp) < 0)
                {
                    Console.WriteLine("总量不为空且不小于零！");
                    continue;
                }
                BookInfo.price = Convert.ToDouble(temp);


                Regex date_regex = new Regex(date_regex_str);
                Console.Write("请输入出版日期（格式：XXXX-XX-XX）:");
                string date = Console.ReadLine();
                if(date == String.Empty || !date_regex.IsMatch(date))
                {
                    Console.WriteLine("输入为空或格式不正确！");
                    continue;
                }
                BookInfo.date = date;

                //查询是否已有该图书
                if(link_mysql.DataIsHere(tb_book, "ISBN", BookInfo.ISBN))
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("数据库中已有该书记录！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                //添加图书到数据库
                if(!AddBookInSql())
                {
                    Console.WriteLine("-----------------");
                    Console.WriteLine("添加或修改图书信息失败，请检查数据库！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("---------------");
                Console.WriteLine("操作成功！");
                Console.Write("按回车键继续！");
                Console.ReadLine();

                break;

            }


        }

        //修改图书信息
        private void ChangeBookInfo()
        {
            Console.Clear();    //清屏
            int cnt = 0;        //操作次数记录
            bool end_flag = false;
            while(!end_flag)
            {
                cnt++;
                if (cnt == 4)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine("操作错误次数过多，请返回重新选择！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                }

                string user_input = String.Empty;
                Console.Write("请输入所修改图书的书号:");
                user_input = Console.ReadLine();

                if(user_input == String.Empty)
                {
                    Console.WriteLine("书号不为空！");
                    continue;
                }

                if (link_mysql.DataIsHere(tb_book, "ISBN", user_input))
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("修改图书信息将先删除原有信息，是否删除？[Y/N]:");
                        string user_adjust = String.Empty;
                        user_adjust = Console.ReadLine();
                        if (user_adjust == "Y" || user_adjust == "y")
                        {
                            //删除
                            if(!DelBookInSql(user_input))
                            {
                                Console.WriteLine("--------------------");
                                Console.WriteLine("删除图书信息失败！");
                                Console.Write("按回车键继续...");
                                Console.ReadLine();
                                end_flag = true;    // 退出修改信息的大循环
                                break;
                            }
                            //增加信息
                            AddBookInfo();
                            end_flag=true;
                            break;

                        }
                        else if (user_adjust == "N" || user_adjust == "n")
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("修改失败：数据库中不存在该图书！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }


            }
            
        }

        //删除图书信息
        private void DelBookInfo()
        {
            Console.Clear();
            while(true)
            {
                string user_input = String.Empty;
                Console.WriteLine("请输入所删除图书书号:");
                user_input = Console.ReadLine();

                if(user_input == String.Empty)
                {
                    Console.WriteLine("输入不为空！");
                    continue;
                }

                if(!link_mysql.DataIsHere(tb_book,"ISBN",user_input))
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("图书不存在！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                if(!link_mysql.DelData(tb_book,"ISBN",user_input))
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("图书删除失败！");
                    Console.Write("按回车键继续...");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("---------------");
                Console.WriteLine("操作成功！");
                Console.Write("按回车键继续！");
                Console.ReadLine();

                break;

            }
        }



        //数据库添加图书
        private bool AddBookInSql()
        {
            List<TableValue> lst_table = new List<TableValue>
            {
                new TableValue("ISBN",BookInfo.ISBN),
                new TableValue("bookname",BookInfo.bookname),
                new TableValue("author",BookInfo.author),
                new TableValue("publisher",BookInfo.publisher),
                new TableValue("bookclass",BookInfo.bookclass),
                new TableValue("stock_num",BookInfo.stock_num),
                new TableValue("total_num",BookInfo.total_num),
                new TableValue("price",BookInfo.price),
                new TableValue("date",BookInfo.date)
            };

            return link_mysql.InsetSql(tb_book, lst_table);
        }

        //数据库中删除图书
        private bool DelBookInSql(string ISBN)
        {
            return link_mysql.DelData(tb_book, "ISBN", ISBN);
        }

    }
}
