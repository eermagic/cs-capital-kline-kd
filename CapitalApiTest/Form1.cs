using CapitalApiTest.Models;
using MySqlConnector;
using SKCOMLib;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CapitalApiTest
{
    public partial class Form1 : Form
    {
        #region 屬性
        SKCenterLib m_pSKCenter = new SKCenterLib(); //登入&環境物件
        SKReplyLib m_pSKReply = new SKReplyLib(); //回應物件
        SKOrderLib m_pSKOrder = new SKOrderLib(); //下單物件
        SKQuoteLib m_SKQuoteLib = new SKQuoteLib();// 國內報價物件

        int nCode;
        DataTable dtQuote = null;// 報價資料
        double dDigitNum = 100;
        short sBest5Page = -1; // 報價頁編號
        DataTable dtBest5 = null;// Best5 資料
        bool isConn = false;
        List<ClosePriceModel> listPriceCollect = new List<ClosePriceModel>(); //搜集收盤價
        List<KlineModel> listKline = new List<KlineModel>(); //搜集k線
        #endregion 屬性

        #region 建構子
        public Form1()
        {
            InitializeComponent();
            gvQuote.AutoGenerateColumns = false;
            gvBest5Merge.AutoGenerateColumns = false;
            gvKline.AutoGenerateColumns = false;
            gvKlineKD.AutoGenerateColumns = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 註冊公告事件
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.m_pSKReply_OnAnnouncement);

            // 註冊國內報價連線狀態事件
            m_SKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);

            // 註冊國內報價回傳事件
            m_SKQuoteLib.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKQuoteLib_OnNotifyQuoteLONG);

            // 註冊國內 Best5 回傳事件
            m_SKQuoteLib.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKQuoteLib_OnNotifyBest5);
        }
        #endregion 建構子

        #region 動作
        /// <summary>
        /// 登入群益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 不用 SGX DMA
            m_pSKCenter.SKCenterLib_SetAuthority(1);

            // 呼叫群益帳號登入
            nCode = m_pSKCenter.SKCenterLib_Login(txtCapitalAcct.Text.Trim().ToUpper(), txtCapitalPwd.Text.Trim());

            // 取得回傳訊息
            GetMessage("登入", nCode);

            if (nCode != 0 && nCode != 2003)
            {
                // 失敗
                txtLog.AppendText("登入失敗");
                return;
            }
        }

        /// <summary>
        /// 報價連線
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            // 國內報價連線 由 OnConnection 事件回報
            nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONG();
            GetMessage("國內報價連線", nCode);
        }

        /// <summary>
        /// 取得商品報價
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetQuote_Click(object sender, EventArgs e)
        {

            // 取回商品報價的相關資訊
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();
            nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(txtSymbol.Text, ref pSKStockLONG);
            GetMessage("取回商品報價的相關資訊", nCode);

            // 將報價資訊物件輸出在 DataGridView
            onUpdateQuote(pSKStockLONG);

            if (nCode != 0)
            {
                // 發生錯誤
                return;
            }



            //訂閱商品即時報價，訂閱後等待 OnNotifyQuoteLONG 事件回報
            short sPage = -1; // 報價頁編號
            nCode = m_SKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, txtSymbol.Text);
            GetMessage("訂閱商品即時報價", nCode);
        }

        /// <summary>
        /// 取得最佳五檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetBest5_Click(object sender, EventArgs e)
        {


            //訂閱 Tick & Best5，訂閱後等待 OnNotifyTicks 及 OnNotifyBest5 事件回報
            sBest5Page += 1;
            nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(ref sBest5Page, txtSymbol.Text);
            GetMessage("訂閱 Tick & Best5", nCode);
        }

        /// <summary>
        /// 計算 1 分 K 線
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc1K_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }

        /// <summary>
        /// 計算 KD 指標
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalcKD_Click(object sender, EventArgs e)
        {
            CalcKD_K(listKline, Convert.ToInt32(txtKdParam.Text));
            CalcKD_D(listKline, Convert.ToInt32(txtKdParam.Text));
            gvKlineKD.DataSource = listKline.ToList();
        }
        #endregion 動作

        #region 方法
        /// <summary>
        /// 取得群益api回傳訊息說明
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="nCode"></param>
        /// <param name="strMessage"></param>
        private void GetMessage(string strType, int nCode)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo = "【" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "】";

            txtLog.AppendText("【" + strType + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo + "\n");

        }

        /// <summary>
        /// 更新最新報價
        /// </summary>
        private void onUpdateQuote(SKSTOCKLONG pSKStockLONG)
        {
            if (dtQuote == null)
            {
                // 報價物件寫入 Datatable
                dtQuote = new DataTable();
                dtQuote.Columns.Add("QuoteName");
                dtQuote.Columns.Add("QuoteValue");
                DataRow drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "代碼";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockNo;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "名稱";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockName;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "開盤價";
                drNew["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "最高價";
                drNew["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "最低價";
                drNew["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "成交價";
                drNew["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "單量";
                drNew["QuoteValue"] = pSKStockLONG.nTickQty;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "昨收價";
                drNew["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "買價";
                drNew["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "買量";
                drNew["QuoteValue"] = pSKStockLONG.nBc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "賣價";
                drNew["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "賣量";
                drNew["QuoteValue"] = pSKStockLONG.nAc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "總量";
                drNew["QuoteValue"] = pSKStockLONG.nTQty;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "時間";
                drNew["QuoteValue"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dtQuote.Rows.Add(drNew);

                //輸出 GridView
                gvQuote.DataSource = dtQuote;
                gvQuote.Refresh();
            }
            else
            {
                // 報價物件更新 Datatable
                DataRow dr = dtQuote.Select("QuoteName='開盤價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='最高價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='最低價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='成交價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='單量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTickQty;

                dr = dtQuote.Select("QuoteName='昨收價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='買價'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='買量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nBc;

                dr = dtQuote.Select("QuoteName='賣價'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='賣量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nAc;

                dr = dtQuote.Select("QuoteName='總量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTQty;

                dr = dtQuote.Select("QuoteName='時間'")[0];
                dr["QuoteValue"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }


            // 搜集報價
            listPriceCollect.Add(new ClosePriceModel
            {
                Datetime = DateTime.Now,
                Price = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal)),
                Qty = pSKStockLONG.nTickQty
            });
        }

        /// <summary>
        /// 計算 KD 的 K 值
        /// </summary>
        /// <param name="ary"></param>
        /// <param name="rownum"></param>
        /// <returns></returns>
        private void CalcKD_K(List<KlineModel> listKline, int days)
        {
            if (listKline.Count < days)
            {
                return;
            }

            /*
             *  第n天收盤價-最近n天內最低價
                RSV ＝────────────────×100
                最近n天內最高價-最近n天內最低價
                計算出RSV之後，再來計算K值與D值。
                當日K值(%K)= 2/3 前一日 K值 + 1/3 RSV
             */

            for (int i = 0; i < listKline.Count; i++)
            {
                if ((i + 1) - days < 0)
                {
                    continue;
                }
                double val = 0;
                double beforeK = 0;

                // 取得前一天K值
                if (listKline[i - 1].Kd_K == null)
                {
                    beforeK = 50;
                }
                else
                {
                    beforeK = Convert.ToDouble(listKline[i - 1].Kd_K);
                }

                // 取當天收盤價
                double nowClose = listKline[i].Close;

                //取最近n天內最低價
                double minPrice = listKline.Where(w => w.idx > i - days && w.idx <= i).Min(s => s.Low);
                if (minPrice == 0)
                {
                    continue;
                }

                //取最近n天內最高價
                double maxPrice = listKline.Where(w => w.idx > i - days && w.idx <= i).Max(s => s.High);

                double RSV = ((nowClose - minPrice) / (maxPrice - minPrice) * 100);

                if (double.IsNaN(RSV) == false)
                {
                    val = ((beforeK * 2 / 3) + (RSV / 3));
                    val = Math.Round(val, 2);
                }
                else
                {
                    val = beforeK;
                }
                if (val > 0)
                {
                    listKline[i].Kd_K = val;
                }
            }
        }

        /// <summary>
        /// 計算 KD 的 D 值
        /// </summary>
        private void CalcKD_D(List<KlineModel> listKline, int days)
        {
            if (listKline.Count < days)
            {
                return;
            }

            /*
                當日D值(%D)= 2/3 前一日 D值＋ 1/3 當日K值
             */

            double val = 0;
            double beforeD = 0;
            for (int i = 0; i < listKline.Count; i++)
            {
                if (i + 1 < days)
                {
                    continue;
                }
                // 取得前一天D值
                if (listKline[i - 1].Kd_D == null)
                {
                    beforeD = 50;
                }
                else
                {
                    beforeD = (double)listKline[i - 1].Kd_D;
                }

                // 取當日K值
                if (listKline[i].Kd_K == null)
                {
                    continue;
                }
                double K9 = (double)listKline[i].Kd_K;

                val = (beforeD * 2 / 3) + (K9 / 3);
                if (val > 0)
                {
                    val = Math.Round(val, 2);
                    listKline[i].Kd_D = val;
                }
            }

        }
        #endregion 方法

        #region 事件
        /// <summary>
        /// 關閉視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConn)
            {
                nCode = m_SKQuoteLib.SKQuoteLib_LeaveMonitor();
            }

        }

        /// <summary>
        /// 群益公告事件
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="bstrMessage"></param>
        /// <param name="nConfirmCode"></param>
        void m_pSKReply_OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;

        }

        /// <summary>
        /// 國內報價連線回應事件
        /// </summary>
        /// <param name="nKind"></param>
        /// <param name="nCode"></param>
        void m_SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {
                // 連線中
                labConnResult.Text = "連線狀態：連線中";
            }
            else if (nKind == 3002)
            {
                // 連線中斷
                labConnResult.Text = "連線狀態：中斷";
            }
            else if (nKind == 3003)
            {
                // 連線成功
                labConnResult.Text = "連線狀態：正常";
                isConn = true;
            }
            else if (nKind == 3021)
            {
                //網路斷線
                labConnResult.Text = "連線狀態：網路斷線";
            }
        }

        /// <summary>
        /// 國內報價回應事件
        /// </summary>
        /// <param name="sMarketNo"></param>
        /// <param name="nStockIdx"></param>
        void m_SKQuoteLib_OnNotifyQuoteLONG(short sMarketNo, int nStockIdx)
        {
            // 報價資訊物件
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

            // 取得最新報價寫入報價資訊物件
            m_SKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

            // 將報價資訊物件輸出在 DataGridView
            onUpdateQuote(pSKStockLONG);
        }

        /// <summary>
        /// 國內 Best5 回傳事件
        /// </summary>
        void m_SKQuoteLib_OnNotifyBest5(short sMarketNo, int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
        {
            DataRow dr = null;

            if (dtBest5 == null)
            {
                // 報價物件寫入 Datatable
                dtBest5 = new DataTable();
                dtBest5.Columns.Add("Seq");
                dtBest5.Columns.Add("Best5BidQty");
                dtBest5.Columns.Add("Best5BidPrice");
                dtBest5.Columns.Add("Best5AskPrice");
                dtBest5.Columns.Add("Best5AskQty");

                // 價格除以 dDigitNum，是因為來的資料裡面會多 2 個 0，而預設 dDigitNum 是 100，所以要除掉 2 個 0

                dr = dtBest5.NewRow();
                dr["Seq"] = "1";
                dr["Best5BidQty"] = nBestBidQty1;
                dr["Best5BidPrice"] = nBestBid1 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty1;
                dr["Best5AskPrice"] = nBestAsk1 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "2";
                dr["Best5BidQty"] = nBestBidQty2;
                dr["Best5BidPrice"] = nBestBid2 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty2;
                dr["Best5AskPrice"] = nBestAsk2 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "3";
                dr["Best5BidQty"] = nBestBidQty3;
                dr["Best5BidPrice"] = nBestBid3 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty3;
                dr["Best5AskPrice"] = nBestAsk3 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "4";
                dr["Best5BidQty"] = nBestBidQty4;
                dr["Best5BidPrice"] = nBestBid4 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty4;
                dr["Best5AskPrice"] = nBestAsk4 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "5";
                dr["Best5BidQty"] = nBestBidQty5;
                dr["Best5BidPrice"] = nBestBid5 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty5;
                dr["Best5AskPrice"] = nBestAsk5 / dDigitNum;
                dtBest5.Rows.Add(dr);

                //輸出 GridView
                gvBest5Merge.DataSource = dtBest5;
            }
            else
            {
                // 報價物件更新 Datatable
                dr = dtBest5.Select("Seq='1'")[0];
                dr["Best5BidQty"] = nBestBidQty1;
                dr["Best5BidPrice"] = nBestBid1 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty1;
                dr["Best5AskPrice"] = nBestAsk1 / dDigitNum;

                dr = dtBest5.Select("Seq='2'")[0];
                dr["Best5BidQty"] = nBestBidQty2;
                dr["Best5BidPrice"] = nBestBid2 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty2;
                dr["Best5AskPrice"] = nBestAsk2 / dDigitNum;

                dr = dtBest5.Select("Seq='3'")[0];
                dr["Best5BidQty"] = nBestBidQty3;
                dr["Best5BidPrice"] = nBestBid3 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty3;
                dr["Best5AskPrice"] = nBestAsk3 / dDigitNum;

                dr = dtBest5.Select("Seq='4'")[0];
                dr["Best5BidQty"] = nBestBidQty4;
                dr["Best5BidPrice"] = nBestBid4 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty4;
                dr["Best5AskPrice"] = nBestAsk4 / dDigitNum;

                dr = dtBest5.Select("Seq='5'")[0];
                dr["Best5BidQty"] = nBestBidQty5;
                dr["Best5BidPrice"] = nBestBid5 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty5;
                dr["Best5AskPrice"] = nBestAsk5 / dDigitNum;
            }
        }

        /// <summary>
        /// 定時 1 秒事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("ss") == "00")
            {
                // 計算上一分鐘K線
                DateTime lastMinute = DateTime.Now.AddMinutes(-1);

                List<ClosePriceModel> items = listPriceCollect.Where(m => m.Datetime >= lastMinute).ToList();
                if (items.Count() > 0)
                {
                    // 搜集k線
                    listKline.Add(new KlineModel()
                    {
                        idx = listKline.Count,
                        KlineTime = lastMinute,
                        Open = items.First().Price,
                        High = items.Max(m => m.Price),
                        Low = items.Min(m => m.Price),
                        Close = items.Last().Price,
                        Qty = items.Sum(m => m.Qty)
                    });

                    // 輸出至 DataGridView
                    gvKline.DataSource = listKline.ToList();
                }
            }
        }
        #endregion 事件
    }
}