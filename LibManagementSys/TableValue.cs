using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementSys
{
    //作为元素，用于创建数据库表单的一栏
    internal class TableValue
    {
        public TableValue(string _name, object _value)
        {
            name = _name;
            value = _value;
        }

        public string name = String.Empty;
        public object value = String.Empty;
    }
}
