using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using CefSharp;
using CefSharp.WinForms;
using EasyTabs;

namespace WeiBrowser
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh-CN";
            if(!Cef.IsInitialized)Cef.Initialize(settings);
            //txtUrl.Text = "https://sellercentral.amazon.com/gp/homepage.html";
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //if(!string.IsNullOrWhiteSpace(txtUrl.Text))
            //browser.Load(txtUrl.Text);
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //browser.Load(txtUrl.Text);
            } 
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //if (browser.CanGoBack) browser.Back();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //if (browser.CanGoForward) browser.Forward();
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
