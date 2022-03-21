


namespace LibManagementSys
{
    class Program
    {
        static void Main(string[] args)
        {

            LinkMysql link_sql = new LinkMysql();

            Console.WriteLine("检查数据库中...");
            Thread.Sleep(1500);

            if(link_sql.CheckMysql())
            {
                Console.WriteLine("加载成功，请稍等...");
                Thread.Sleep(1000);
                MainMenu main_menu = new MainMenu();
            }
            else
            {
                Console.WriteLine("数据库连接失败...");
                Console.WriteLine("按回车键退出程序...");
                Console.ReadLine();
            }
            


        }
    }
}

