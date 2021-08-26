using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace AmazonSpider
{
    public partial class FrmMain : Form
    {
        private delegate void UiHandler(object obj);
        private ChromiumWebBrowser _browser = null;
        public FrmMain()
        {
            InitializeComponent();
            InitializeChromium();
        }
        /// <summary>
        /// 初始化浏览器
        /// </summary>
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.Locale = "zh_CN";
            settings.CachePath = Application.StartupPath+ @"\Cache";
            settings.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36";
            if (!Cef.IsInitialized) Cef.Initialize(settings);
            //创建对象
            string url = this.urlTxt.Text;
            _browser = new ChromiumWebBrowser(url);
            tabContainer.TabPages["homePage"].Controls.Add(_browser);
            _browser.Dock = DockStyle.Fill;
            //装载事件
            ChromiumHandle();
        }
        public void ChromiumHandle() 
        {
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            _browser.LoadingStateChanged += _browser_LoadingStateChanged;
        }
        /// <summary>
        /// 通过状态来判断加载完资源，实际上是不准确的，每个frame请求的状态变更都会引触发这个方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            //Console.WriteLine("正在加载?:"+e.IsLoading.ToString()+" =>"+_browser.IsLoading.ToString());
            //是否正在加载
            if (e.IsLoading == false)
            {

            }
        }

        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                //_browser.ViewSource();
                var task02 = _browser.GetMainFrame().GetSourceAsync();
                task02.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        //执行获取数据的方法
                        fetchData();
                    }
                });
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void urlTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                LoadUrl();
            }
        }
        /// <summary>
        /// 加载链接的页面 , 不传入链接则取链接输入框的值
        /// </summary>
        /// <param name="url"></param>
        private void LoadUrl(string url=null)
        {
            if (url == null)
            {
                url = this.urlTxt.Text;
            }
            if (!string.IsNullOrEmpty(url))
            {
                _browser.Load(url);
            }
        }

        private void fetchBtn_Click(object sender, EventArgs e)
        {
            LoadUrl();
        }

        private void fetchDataBtn_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        private void fetchData()
        {
            try
            {
                string jsString = @"var reviewBox = document.getElementById('cm_cr-review_list');
                                        for (var i = 0; i < reviewBox.childNodes.length; i++)
                                            {
                                                alert(reviewBox.childNodes[i].getElementsByClassName('a-profile'))
                                            };
                                    ";
                jsString = @"(function () {
                                function getScript(url, success) {
                                    var script = document.createElement('script');
                                    script.src = url;
                                    var head = document.getElementsByTagName('head')[0],
                                        done = false;
                                    script.onload = script.onreadystatechange = function () {
                                        if (!done && (!this.readyState
                                            || this.readyState == 'loaded'
                                            || this.readyState == 'complete')) {
                                            done = true;
                                            success();
                                            script.onload = script.onreadystatechange = null;
                                            head.removeChild(script);
                                        }
                                    };
                                    head.appendChild(script);
                                }
                                getScript('https://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js', function () {
                                            if (typeof jQuery == 'undefined') {
                                                console.log('Sorry, but jQuery wasn\'t able to load');
                                            } else {
                                                $(document).ready(function () {
                                                    
                                                });
                                            }
                                });
})();
";
                _browser.GetMainFrame().ExecuteJavaScriptAsync(jsString);
                Thread.Sleep(4000);
                jsString = @"function getData(){   
                                var data =[];
                                $('div[data-hook=review]').each(function(i, item) {
                                    var date = $(item).find('[data-hook=review-date]')[0].innerText;
                                    var star = $(item).find('[data-hook=review-star-rating]')[0].innerText;
                                    var title = $(item).find('[data-hook=review-title]')[0].innerText;
                                    var body = $(item).find('[data-hook=review-body]')[0].innerText;
                                    var userName = $(item).find('.a-profile-name')[0].innerText;
                                    var userUrl = $($(item).find('.a-profile')[0]).attr('href');
                                    data.push({
                                        'date':date
                                        ,'userName':userName
                                        ,'userUrl':userUrl
                                        ,'star':star
                                        ,'title':title
                                        ,'body':body
                                        ,'host':document.domain
                                    })
                                })
                                return data.length>0?JSON.stringify(data):'';
            }getData(); ";
                var task = _browser.GetMainFrame().EvaluateScriptAsync(jsString);
                // 等待js 方法执行完后，获取返回值
                task.ContinueWith(t =>
                {
                    if (t.Result.Result!=null)
                    {
                        var response = t.Result;
                        string jsdata = response.Result.ToString();
                        //MessageBox.Show(response.Result.ToString());
                        //Console.WriteLine(resultStr);
                        //richTextBoxAdd(resultStr);
                        richTextBoxAdd(jsdata);
                    }
                });
                //Console.WriteLine("Frame数量：" + _browser.GetBrowser().GetFrameCount());
                //foreach(string n in _browser.GetBrowser().GetFrameNames())
                //{
                //    Console.WriteLine("Frame名："+n);
                //}
                //_browser.ViewSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void richTextBoxAdd(object obj)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new UiHandler(richTextBoxAdd), obj);
            }
            else
            {
                this.richTextBox1.AppendText("\r\n=============\r\n" + obj.ToString());
            }
        }
    }
}
