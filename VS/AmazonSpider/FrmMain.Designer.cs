namespace AmazonSpider
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.urlTxt = new System.Windows.Forms.TextBox();
            this.fetchBtn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.homePage = new System.Windows.Forms.TabPage();
            this.fetchDataBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // urlTxt
            // 
            this.urlTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTxt.Location = new System.Drawing.Point(2, 3);
            this.urlTxt.Name = "urlTxt";
            this.urlTxt.Size = new System.Drawing.Size(840, 21);
            this.urlTxt.TabIndex = 0;
            this.urlTxt.Text = "https://www.amazon.com/Fulfuloon-Swimming-Portable-Foldable-Bathing/product-revie" +
    "ws/B012D2PT76/ref=cm_cr_arp_d_viewopt_srt?ie=UTF8&reviewerType=all_reviews&sortB" +
    "y=recent&pageNumber=1";
            this.urlTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.urlTxt_KeyPress);
            // 
            // fetchBtn
            // 
            this.fetchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fetchBtn.Location = new System.Drawing.Point(846, 2);
            this.fetchBtn.Name = "fetchBtn";
            this.fetchBtn.Size = new System.Drawing.Size(100, 23);
            this.fetchBtn.TabIndex = 1;
            this.fetchBtn.Text = "运行";
            this.fetchBtn.UseVisualStyleBackColor = true;
            this.fetchBtn.Click += new System.EventHandler(this.fetchBtn_Click);
            // 
            // tabContainer
            // 
            this.tabContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabContainer.Controls.Add(this.homePage);
            this.tabContainer.Location = new System.Drawing.Point(3, 30);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(650, 732);
            this.tabContainer.TabIndex = 2;
            // 
            // homePage
            // 
            this.homePage.Location = new System.Drawing.Point(4, 22);
            this.homePage.Name = "homePage";
            this.homePage.Padding = new System.Windows.Forms.Padding(3);
            this.homePage.Size = new System.Drawing.Size(642, 706);
            this.homePage.TabIndex = 0;
            this.homePage.Text = "首页";
            this.homePage.UseVisualStyleBackColor = true;
            // 
            // fetchDataBtn
            // 
            this.fetchDataBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fetchDataBtn.Location = new System.Drawing.Point(952, 2);
            this.fetchDataBtn.Name = "fetchDataBtn";
            this.fetchDataBtn.Size = new System.Drawing.Size(100, 23);
            this.fetchDataBtn.TabIndex = 3;
            this.fetchDataBtn.Text = "获取数据";
            this.fetchDataBtn.UseVisualStyleBackColor = true;
            this.fetchDataBtn.Click += new System.EventHandler(this.fetchDataBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(659, 52);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(393, 706);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 774);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.fetchDataBtn);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.fetchBtn);
            this.Controls.Add(this.urlTxt);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "亚马逊爬虫";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.tabContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTxt;
        private System.Windows.Forms.Button fetchBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage homePage;
        private System.Windows.Forms.Button fetchDataBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

