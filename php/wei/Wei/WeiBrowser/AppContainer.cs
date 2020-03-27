using EasyTabs;

namespace WeiBrowser
{
    public partial class AppContainer : TitleBarTabs
    {
        public AppContainer()
        {
            InitializeComponent();
            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            Icon = Properties.Resources.WeiChrome;
        }

        public override TitleBarTab CreateTab()
        {
            TitleBarTab tb = new TitleBarTab(this);
            //tb.ShowCloseButton = false;
            tb.Content = new FrmBrowser(tb);
            tb.Content.Text = "新标签";
            return tb;
        }
    }
}
