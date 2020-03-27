using EasyTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wei
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Frm_Main());
            Config.container = new AppContainer();
            AppContainer container = Config.container;
            TitleBarTab tb = new TitleBarTab(container);
            tb.Content = new FrmBrowser(tb,string.Empty);
            tb.Content.Text = "首页";
            tb.ShowCloseButton = false;//禁止关闭首页
            container.Tabs.Add(tb);
            container.SelectedTabIndex = 0;
            TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
            applicationContext.Start(container);
            Application.Run(applicationContext);
        }
    }
}
