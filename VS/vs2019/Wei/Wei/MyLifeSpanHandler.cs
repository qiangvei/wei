using CefSharp;
using CefSharp.WinForms;
using EasyTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei
{
    class MyLifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            if(browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            //在原窗口加载跳转的页面
            //var br = (ChromiumWebBrowser)chromiumWebBrowser;
            //br.Load(targetUrl);
            if (Config.container.InvokeRequired)
            {
                Config.container.Invoke(new Action(() =>
                {
                    TitleBarTab tb = new TitleBarTab(Config.container);
                    tb.Content = new FrmBrowser(tb,targetUrl);
                    tb.Content.Text = "跳转中";
                    Config.container.Tabs.Add(tb);
                    Config.container.SelectedTabIndex = Config.container.Tabs.Count -1;
                }));
            }
            return true; //Return true to cancel the popup creation copyright by codebye.com.
        }
    }
}
