using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    internal class BookInfo
    {
        static public string ISBN = String.Empty;           //书号
        static public string bookname = String.Empty;       //书名
        static public string author = String.Empty;         //作者 
        static public string publisher = String.Empty;      //出版社
        static public string bookclass = String.Empty;      //图书分类
        static public int stock_num = 0;                    //库存量
        static public int total_num = 0;                    //总量
        static public double price = 0;                     //单价 
        static public string date = "0000-00-00";           //出版日期
    }
}
