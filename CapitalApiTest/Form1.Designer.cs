namespace CapitalApiTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtCapitalPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCapitalAcct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvBest5Merge = new System.Windows.Forms.DataGridView();
            this.gcBest5BidQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcBest5BidPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcBest5AskPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcBest5AskQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGetBest5 = new System.Windows.Forms.Button();
            this.gvQuote = new System.Windows.Forms.DataGridView();
            this.QuoteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuoteValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetQuote = new System.Windows.Forms.Button();
            this.labConnResult = new System.Windows.Forms.Label();
            this.btnConn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtKdParam = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gvKlineKD = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcKd_K = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcKd_D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvKline = new System.Windows.Forms.DataGridView();
            this.gcKlineTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcHigh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcLow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCalcKD = new System.Windows.Forms.Button();
            this.btnCalc1K = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBest5Merge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuote)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvKlineKD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKline)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.txtCapitalPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCapitalAcct);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. 登入";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(16, 86);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(151, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登入群益";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtCapitalPwd
            // 
            this.txtCapitalPwd.Location = new System.Drawing.Point(67, 57);
            this.txtCapitalPwd.Name = "txtCapitalPwd";
            this.txtCapitalPwd.PasswordChar = '*';
            this.txtCapitalPwd.Size = new System.Drawing.Size(100, 23);
            this.txtCapitalPwd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "密碼";
            // 
            // txtCapitalAcct
            // 
            this.txtCapitalAcct.Location = new System.Drawing.Point(67, 28);
            this.txtCapitalAcct.Name = "txtCapitalAcct";
            this.txtCapitalAcct.PasswordChar = '*';
            this.txtCapitalAcct.Size = new System.Drawing.Size(100, 23);
            this.txtCapitalAcct.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "帳號";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Location = new System.Drawing.Point(12, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 341);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "訊息";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 22);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(265, 304);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvBest5Merge);
            this.groupBox3.Controls.Add(this.btnGetBest5);
            this.groupBox3.Controls.Add(this.gvQuote);
            this.groupBox3.Controls.Add(this.txtSymbol);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnGetQuote);
            this.groupBox3.Controls.Add(this.labConnResult);
            this.groupBox3.Controls.Add(this.btnConn);
            this.groupBox3.Location = new System.Drawing.Point(295, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 474);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2. 取得報價";
            // 
            // gvBest5Merge
            // 
            this.gvBest5Merge.AllowUserToAddRows = false;
            this.gvBest5Merge.AllowUserToDeleteRows = false;
            this.gvBest5Merge.AllowUserToResizeColumns = false;
            this.gvBest5Merge.AllowUserToResizeRows = false;
            this.gvBest5Merge.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvBest5Merge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvBest5Merge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gcBest5BidQty,
            this.gcBest5BidPrice,
            this.gcBest5AskPrice,
            this.gcBest5AskQty});
            this.gvBest5Merge.Location = new System.Drawing.Point(6, 282);
            this.gvBest5Merge.Name = "gvBest5Merge";
            this.gvBest5Merge.ReadOnly = true;
            this.gvBest5Merge.RowHeadersWidth = 20;
            this.gvBest5Merge.RowTemplate.Height = 25;
            this.gvBest5Merge.Size = new System.Drawing.Size(318, 166);
            this.gvBest5Merge.TabIndex = 16;
            // 
            // gcBest5BidQty
            // 
            this.gcBest5BidQty.DataPropertyName = "Best5BidQty";
            this.gcBest5BidQty.HeaderText = "委買量";
            this.gcBest5BidQty.Name = "gcBest5BidQty";
            this.gcBest5BidQty.ReadOnly = true;
            this.gcBest5BidQty.Width = 68;
            // 
            // gcBest5BidPrice
            // 
            this.gcBest5BidPrice.DataPropertyName = "Best5BidPrice";
            this.gcBest5BidPrice.HeaderText = "委買價";
            this.gcBest5BidPrice.Name = "gcBest5BidPrice";
            this.gcBest5BidPrice.ReadOnly = true;
            this.gcBest5BidPrice.Width = 68;
            // 
            // gcBest5AskPrice
            // 
            this.gcBest5AskPrice.DataPropertyName = "Best5AskPrice";
            this.gcBest5AskPrice.HeaderText = "委賣價";
            this.gcBest5AskPrice.Name = "gcBest5AskPrice";
            this.gcBest5AskPrice.ReadOnly = true;
            this.gcBest5AskPrice.Width = 68;
            // 
            // gcBest5AskQty
            // 
            this.gcBest5AskQty.DataPropertyName = "Best5AskQty";
            this.gcBest5AskQty.HeaderText = "委賣量";
            this.gcBest5AskQty.Name = "gcBest5AskQty";
            this.gcBest5AskQty.ReadOnly = true;
            this.gcBest5AskQty.Width = 68;
            // 
            // btnGetBest5
            // 
            this.btnGetBest5.Location = new System.Drawing.Point(6, 253);
            this.btnGetBest5.Name = "btnGetBest5";
            this.btnGetBest5.Size = new System.Drawing.Size(151, 23);
            this.btnGetBest5.TabIndex = 13;
            this.btnGetBest5.Text = "取得最佳五檔";
            this.btnGetBest5.UseVisualStyleBackColor = true;
            this.btnGetBest5.Click += new System.EventHandler(this.btnGetBest5_Click);
            // 
            // gvQuote
            // 
            this.gvQuote.AllowUserToAddRows = false;
            this.gvQuote.AllowUserToDeleteRows = false;
            this.gvQuote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvQuote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvQuote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuoteName,
            this.QuoteValue});
            this.gvQuote.Location = new System.Drawing.Point(6, 81);
            this.gvQuote.Name = "gvQuote";
            this.gvQuote.ReadOnly = true;
            this.gvQuote.RowHeadersWidth = 20;
            this.gvQuote.RowTemplate.Height = 25;
            this.gvQuote.Size = new System.Drawing.Size(318, 166);
            this.gvQuote.TabIndex = 12;
            // 
            // QuoteName
            // 
            this.QuoteName.DataPropertyName = "QuoteName";
            this.QuoteName.HeaderText = "欄位";
            this.QuoteName.Name = "QuoteName";
            this.QuoteName.ReadOnly = true;
            this.QuoteName.Width = 56;
            // 
            // QuoteValue
            // 
            this.QuoteValue.DataPropertyName = "QuoteValue";
            this.QuoteValue.HeaderText = "值";
            this.QuoteValue.Name = "QuoteValue";
            this.QuoteValue.ReadOnly = true;
            this.QuoteValue.Width = 44;
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(224, 52);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(100, 23);
            this.txtSymbol.TabIndex = 11;
            this.txtSymbol.Text = "TX00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "商品代碼";
            // 
            // btnGetQuote
            // 
            this.btnGetQuote.Location = new System.Drawing.Point(6, 51);
            this.btnGetQuote.Name = "btnGetQuote";
            this.btnGetQuote.Size = new System.Drawing.Size(151, 23);
            this.btnGetQuote.TabIndex = 7;
            this.btnGetQuote.Text = "取得商品報價";
            this.btnGetQuote.UseVisualStyleBackColor = true;
            this.btnGetQuote.Click += new System.EventHandler(this.btnGetQuote_Click);
            // 
            // labConnResult
            // 
            this.labConnResult.AutoSize = true;
            this.labConnResult.Location = new System.Drawing.Point(163, 26);
            this.labConnResult.Name = "labConnResult";
            this.labConnResult.Size = new System.Drawing.Size(55, 15);
            this.labConnResult.TabIndex = 6;
            this.labConnResult.Text = "連線結果";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(6, 22);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(151, 23);
            this.btnConn.TabIndex = 5;
            this.btnConn.Text = "報價連線";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtKdParam);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.gvKlineKD);
            this.groupBox4.Controls.Add(this.gvKline);
            this.groupBox4.Controls.Add(this.btnCalcKD);
            this.groupBox4.Controls.Add(this.btnCalc1K);
            this.groupBox4.Location = new System.Drawing.Point(647, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(434, 474);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3. 技術指標";
            // 
            // txtKdParam
            // 
            this.txtKdParam.Location = new System.Drawing.Point(209, 199);
            this.txtKdParam.Name = "txtKdParam";
            this.txtKdParam.Size = new System.Drawing.Size(45, 23);
            this.txtKdParam.TabIndex = 29;
            this.txtKdParam.Text = "9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "參數";
            // 
            // gvKlineKD
            // 
            this.gvKlineKD.AllowUserToAddRows = false;
            this.gvKlineKD.AllowUserToDeleteRows = false;
            this.gvKlineKD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvKlineKD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvKlineKD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.gcKd_K,
            this.gcKd_D});
            this.gvKlineKD.Location = new System.Drawing.Point(6, 228);
            this.gvKlineKD.Name = "gvKlineKD";
            this.gvKlineKD.RowHeadersWidth = 20;
            this.gvKlineKD.RowTemplate.Height = 25;
            this.gvKlineKD.Size = new System.Drawing.Size(422, 231);
            this.gvKlineKD.TabIndex = 27;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "KlineTime";
            this.dataGridViewTextBoxColumn1.HeaderText = "時間";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 56;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Open";
            this.dataGridViewTextBoxColumn2.HeaderText = "開";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 44;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "High";
            this.dataGridViewTextBoxColumn3.HeaderText = "高";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 44;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Low";
            this.dataGridViewTextBoxColumn4.HeaderText = "低";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 44;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Close";
            this.dataGridViewTextBoxColumn5.HeaderText = "收";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 44;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn6.HeaderText = "量";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 44;
            // 
            // gcKd_K
            // 
            this.gcKd_K.DataPropertyName = "Kd_K";
            this.gcKd_K.HeaderText = "K";
            this.gcKd_K.Name = "gcKd_K";
            this.gcKd_K.Width = 39;
            // 
            // gcKd_D
            // 
            this.gcKd_D.DataPropertyName = "Kd_D";
            this.gcKd_D.HeaderText = "D";
            this.gcKd_D.Name = "gcKd_D";
            this.gcKd_D.Width = 41;
            // 
            // gvKline
            // 
            this.gvKline.AllowUserToAddRows = false;
            this.gvKline.AllowUserToDeleteRows = false;
            this.gvKline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvKline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvKline.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gcKlineTime,
            this.gcOpen,
            this.gcHigh,
            this.gcLow,
            this.gcClose,
            this.gcQty});
            this.gvKline.Location = new System.Drawing.Point(6, 51);
            this.gvKline.Name = "gvKline";
            this.gvKline.RowHeadersWidth = 20;
            this.gvKline.RowTemplate.Height = 25;
            this.gvKline.Size = new System.Drawing.Size(422, 142);
            this.gvKline.TabIndex = 26;
            // 
            // gcKlineTime
            // 
            this.gcKlineTime.DataPropertyName = "KlineTime";
            this.gcKlineTime.HeaderText = "時間";
            this.gcKlineTime.Name = "gcKlineTime";
            this.gcKlineTime.Width = 56;
            // 
            // gcOpen
            // 
            this.gcOpen.DataPropertyName = "Open";
            this.gcOpen.HeaderText = "開";
            this.gcOpen.Name = "gcOpen";
            this.gcOpen.Width = 44;
            // 
            // gcHigh
            // 
            this.gcHigh.DataPropertyName = "High";
            this.gcHigh.HeaderText = "高";
            this.gcHigh.Name = "gcHigh";
            this.gcHigh.Width = 44;
            // 
            // gcLow
            // 
            this.gcLow.DataPropertyName = "Low";
            this.gcLow.HeaderText = "低";
            this.gcLow.Name = "gcLow";
            this.gcLow.Width = 44;
            // 
            // gcClose
            // 
            this.gcClose.DataPropertyName = "Close";
            this.gcClose.HeaderText = "收";
            this.gcClose.Name = "gcClose";
            this.gcClose.Width = 44;
            // 
            // gcQty
            // 
            this.gcQty.DataPropertyName = "Qty";
            this.gcQty.HeaderText = "量";
            this.gcQty.Name = "gcQty";
            this.gcQty.Width = 44;
            // 
            // btnCalcKD
            // 
            this.btnCalcKD.Location = new System.Drawing.Point(6, 199);
            this.btnCalcKD.Name = "btnCalcKD";
            this.btnCalcKD.Size = new System.Drawing.Size(151, 23);
            this.btnCalcKD.TabIndex = 21;
            this.btnCalcKD.Text = "計算 KD 指標";
            this.btnCalcKD.UseVisualStyleBackColor = true;
            this.btnCalcKD.Click += new System.EventHandler(this.btnCalcKD_Click);
            // 
            // btnCalc1K
            // 
            this.btnCalc1K.Location = new System.Drawing.Point(6, 22);
            this.btnCalc1K.Name = "btnCalc1K";
            this.btnCalc1K.Size = new System.Drawing.Size(151, 23);
            this.btnCalc1K.TabIndex = 8;
            this.btnCalc1K.Text = "計算 1 分 K 線";
            this.btnCalc1K.UseVisualStyleBackColor = true;
            this.btnCalc1K.Click += new System.EventHandler(this.btnCalc1K_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 539);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "群益 Api 教學範例";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBest5Merge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuote)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvKlineKD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKline)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button btnLogin;
        private TextBox txtCapitalPwd;
        private Label label2;
        private TextBox txtCapitalAcct;
        private Label label1;
        private GroupBox groupBox2;
        private RichTextBox txtLog;
        private GroupBox groupBox3;
        private DataGridView gvQuote;
        private DataGridViewTextBoxColumn QuoteName;
        private DataGridViewTextBoxColumn QuoteValue;
        private TextBox txtSymbol;
        private Label label4;
        private Button btnGetQuote;
        private Label labConnResult;
        private Button btnConn;
        private GroupBox groupBox4;
        private Button btnCalc1K;
        private System.Windows.Forms.Timer timer1;
        private Button btnGetBest5;
        private Button btnCalcKD;
        private DataGridView gvBest5Merge;
        private DataGridViewTextBoxColumn gcBest5BidQty;
        private DataGridViewTextBoxColumn gcBest5BidPrice;
        private DataGridViewTextBoxColumn gcBest5AskPrice;
        private DataGridViewTextBoxColumn gcBest5AskQty;
        private DataGridView gvKline;
        private DataGridView gvKlineKD;
        private DataGridViewTextBoxColumn gcKlineTime;
        private DataGridViewTextBoxColumn gcOpen;
        private DataGridViewTextBoxColumn gcHigh;
        private DataGridViewTextBoxColumn gcLow;
        private DataGridViewTextBoxColumn gcClose;
        private DataGridViewTextBoxColumn gcQty;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn gcKd_K;
        private DataGridViewTextBoxColumn gcKd_D;
        private TextBox txtKdParam;
        private Label label3;
    }
}