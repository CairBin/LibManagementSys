using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    internal class BorrowBookInfo:UserInfo
    {
        static public string ISBN = String.Empty;           //书号
        static public string bookname = String.Empty;       //书名
        static public string author = String.Empty;         //作者 
        static public string publisher = String.Empty;      //出版社
        static public string bookclass = String.Empty;      //图书分类
    }
}
