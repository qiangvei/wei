using EasyTabs;

namespace Wei
{
    public partial class AppContainer : TitleBarTabs
    {
        public AppContainer()
        {
            InitializeComponent();
            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            Icon = Properties.Resources.vei;
        }

        public override TitleBarTab CreateTab()
        {
            TitleBarTab tb = new TitleBarTab(this);
            //tb.ShowCloseButton = false;
            tb.Content = new FrmBrowser(tb,string.Empty);
            tb.Content.Text = "新标签";
            return tb;
        }
    }
}
