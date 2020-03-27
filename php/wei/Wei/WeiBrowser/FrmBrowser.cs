using CefSharp;
using CefSharp.WinForms;
using EasyTabs;
using System;
using System.Windows.Forms;

namespace WeiBrowser
{
    public partial class FrmBrowser : Form
    {
        public delegate void UiHandle(object obj);
        public ChromiumWebBrowser thisBrowser;
        private TitleBarTab thisTab;
        public FrmBrowser(TitleBarTab tabObj)
        {
            InitializeComponent();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh-CN";
            if (!Cef.IsInitialized) Cef.Initialize(settings);
            thisTab = tabObj;
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            string indexUrl = "www.baidu.com";
            txtUrl.Text = indexUrl;
            thisBrowser = new ChromiumWebBrowser(indexUrl);
            thisBrowser.Dock = DockStyle.Fill;
            thisBrowser.LifeSpanHandler = new MyLifeSpanHandler();
            thisBrowser.FrameLoadEnd += LoadEnd;
            thisBrowser.TitleChanged += TitleChanged;
            splitContainer1.Panel2.Controls.Add(thisBrowser);
        }
        public void TabTitleSeting(object obj)
        {
            if (thisTab.Content.InvokeRequired)
            {
                UiHandle uh = new UiHandle(TabTitleSeting);
                thisTab.Content.BeginInvoke(uh, obj);
                //SetTabTitle stt = new SetTabTitle(e.Title.ToString());
            }
            else
            {
                thisTab.Content.Text = obj.ToString();
            }
        }
        private void TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.TabTitleSeting(e.Title);
        }

        private void LoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //MessageBox.Show(this.Text);
            //thisBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('kw').value='cef';");
            //thisBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('su').click();");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (thisBrowser.CanGoBack) thisBrowser.Back();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (thisBrowser.CanGoForward) thisBrowser.Forward();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUrl.Text))
                thisBrowser.Load(txtUrl.Text);
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                thisBrowser.Load(txtUrl.Text);
            }
        }

        private void FrmBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
