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
        #region �ݩ�
        SKCenterLib m_pSKCenter = new SKCenterLib(); //�n�J&���Ҫ���
        SKReplyLib m_pSKReply = new SKReplyLib(); //�^������
        SKOrderLib m_pSKOrder = new SKOrderLib(); //�U�檫��
        SKQuoteLib m_SKQuoteLib = new SKQuoteLib();// �ꤺ��������

        int nCode;
        DataTable dtQuote = null;// �������
        double dDigitNum = 100;
        short sBest5Page = -1; // �������s��
        DataTable dtBest5 = null;// Best5 ���
        bool isConn = false;
        List<ClosePriceModel> listPriceCollect = new List<ClosePriceModel>(); //�j�����L��
        List<KlineModel> listKline = new List<KlineModel>(); //�j��k�u
        #endregion �ݩ�

        #region �غc�l
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
            // ���U���i�ƥ�
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.m_pSKReply_OnAnnouncement);

            // ���U�ꤺ�����s�u���A�ƥ�
            m_SKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);

            // ���U�ꤺ�����^�Ǩƥ�
            m_SKQuoteLib.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKQuoteLib_OnNotifyQuoteLONG);

            // ���U�ꤺ Best5 �^�Ǩƥ�
            m_SKQuoteLib.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKQuoteLib_OnNotifyBest5);
        }
        #endregion �غc�l

        #region �ʧ@
        /// <summary>
        /// �n�J�s�q
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ���� SGX DMA
            m_pSKCenter.SKCenterLib_SetAuthority(1);

            // �I�s�s�q�b���n�J
            nCode = m_pSKCenter.SKCenterLib_Login(txtCapitalAcct.Text.Trim().ToUpper(), txtCapitalPwd.Text.Trim());

            // ���o�^�ǰT��
            GetMessage("�n�J", nCode);

            if (nCode != 0 && nCode != 2003)
            {
                // ����
                txtLog.AppendText("�n�J����");
                return;
            }
        }

        /// <summary>
        /// �����s�u
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            // �ꤺ�����s�u �� OnConnection �ƥ�^��
            nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONG();
            GetMessage("�ꤺ�����s�u", nCode);
        }

        /// <summary>
        /// ���o�ӫ~����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetQuote_Click(object sender, EventArgs e)
        {

            // ���^�ӫ~������������T
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();
            nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(txtSymbol.Text, ref pSKStockLONG);
            GetMessage("���^�ӫ~������������T", nCode);

            // �N������T�����X�b DataGridView
            onUpdateQuote(pSKStockLONG);

            if (nCode != 0)
            {
                // �o�Ϳ��~
                return;
            }



            //�q�\�ӫ~�Y�ɳ����A�q�\�ᵥ�� OnNotifyQuoteLONG �ƥ�^��
            short sPage = -1; // �������s��
            nCode = m_SKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, txtSymbol.Text);
            GetMessage("�q�\�ӫ~�Y�ɳ���", nCode);
        }

        /// <summary>
        /// ���o�̨Τ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetBest5_Click(object sender, EventArgs e)
        {


            //�q�\ Tick & Best5�A�q�\�ᵥ�� OnNotifyTicks �� OnNotifyBest5 �ƥ�^��
            sBest5Page += 1;
            nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(ref sBest5Page, txtSymbol.Text);
            GetMessage("�q�\ Tick & Best5", nCode);
        }

        /// <summary>
        /// �p�� 1 �� K �u
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc1K_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }

        /// <summary>
        /// �p�� KD ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalcKD_Click(object sender, EventArgs e)
        {
            CalcKD_K(listKline, Convert.ToInt32(txtKdParam.Text));
            CalcKD_D(listKline, Convert.ToInt32(txtKdParam.Text));
            gvKlineKD.DataSource = listKline.ToList();
        }
        #endregion �ʧ@

        #region ��k
        /// <summary>
        /// ���o�s�qapi�^�ǰT������
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="nCode"></param>
        /// <param name="strMessage"></param>
        private void GetMessage(string strType, int nCode)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo = "�i" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "�j";

            txtLog.AppendText("�i" + strType + "�j�i" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "�j" + strInfo + "\n");

        }

        /// <summary>
        /// ��s�̷s����
        /// </summary>
        private void onUpdateQuote(SKSTOCKLONG pSKStockLONG)
        {
            if (dtQuote == null)
            {
                // ��������g�J Datatable
                dtQuote = new DataTable();
                dtQuote.Columns.Add("QuoteName");
                dtQuote.Columns.Add("QuoteValue");
                DataRow drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�N�X";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockNo;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�W��";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockName;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�}�L��";
                drNew["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�̰���";
                drNew["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�̧C��";
                drNew["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�����";
                drNew["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "��q";
                drNew["QuoteValue"] = pSKStockLONG.nTickQty;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�Q����";
                drNew["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�R��";
                drNew["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�R�q";
                drNew["QuoteValue"] = pSKStockLONG.nBc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "���";
                drNew["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "��q";
                drNew["QuoteValue"] = pSKStockLONG.nAc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�`�q";
                drNew["QuoteValue"] = pSKStockLONG.nTQty;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "�ɶ�";
                drNew["QuoteValue"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dtQuote.Rows.Add(drNew);

                //��X GridView
                gvQuote.DataSource = dtQuote;
                gvQuote.Refresh();
            }
            else
            {
                // ���������s Datatable
                DataRow dr = dtQuote.Select("QuoteName='�}�L��'")[0];
                dr["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='�̰���'")[0];
                dr["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='�̧C��'")[0];
                dr["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='�����'")[0];
                dr["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='��q'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTickQty;

                dr = dtQuote.Select("QuoteName='�Q����'")[0];
                dr["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='�R��'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='�R�q'")[0];
                dr["QuoteValue"] = pSKStockLONG.nBc;

                dr = dtQuote.Select("QuoteName='���'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='��q'")[0];
                dr["QuoteValue"] = pSKStockLONG.nAc;

                dr = dtQuote.Select("QuoteName='�`�q'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTQty;

                dr = dtQuote.Select("QuoteName='�ɶ�'")[0];
                dr["QuoteValue"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }


            // �j������
            listPriceCollect.Add(new ClosePriceModel
            {
                Datetime = DateTime.Now,
                Price = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal)),
                Qty = pSKStockLONG.nTickQty
            });
        }

        /// <summary>
        /// �p�� KD �� K ��
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
             *  ��n�Ѧ��L��-�̪�n�Ѥ��̧C��
                RSV �עw�w�w�w�w�w�w�w�w�w�w�w�w�w�w�w��100
                �̪�n�Ѥ��̰���-�̪�n�Ѥ��̧C��
                �p��XRSV����A�A�ӭp��K�ȻPD�ȡC
                ���K��(%K)= 2/3 �e�@�� K�� + 1/3 RSV
             */

            for (int i = 0; i < listKline.Count; i++)
            {
                if ((i + 1) - days < 0)
                {
                    continue;
                }
                double val = 0;
                double beforeK = 0;

                // ���o�e�@��K��
                if (listKline[i - 1].Kd_K == null)
                {
                    beforeK = 50;
                }
                else
                {
                    beforeK = Convert.ToDouble(listKline[i - 1].Kd_K);
                }

                // ����Ѧ��L��
                double nowClose = listKline[i].Close;

                //���̪�n�Ѥ��̧C��
                double minPrice = listKline.Where(w => w.idx > i - days && w.idx <= i).Min(s => s.Low);
                if (minPrice == 0)
                {
                    continue;
                }

                //���̪�n�Ѥ��̰���
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
        /// �p�� KD �� D ��
        /// </summary>
        private void CalcKD_D(List<KlineModel> listKline, int days)
        {
            if (listKline.Count < days)
            {
                return;
            }

            /*
                ���D��(%D)= 2/3 �e�@�� D�ȡ� 1/3 ���K��
             */

            double val = 0;
            double beforeD = 0;
            for (int i = 0; i < listKline.Count; i++)
            {
                if (i + 1 < days)
                {
                    continue;
                }
                // ���o�e�@��D��
                if (listKline[i - 1].Kd_D == null)
                {
                    beforeD = 50;
                }
                else
                {
                    beforeD = (double)listKline[i - 1].Kd_D;
                }

                // �����K��
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
        #endregion ��k

        #region �ƥ�
        /// <summary>
        /// ��������
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
        /// �s�q���i�ƥ�
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="bstrMessage"></param>
        /// <param name="nConfirmCode"></param>
        void m_pSKReply_OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;

        }

        /// <summary>
        /// �ꤺ�����s�u�^���ƥ�
        /// </summary>
        /// <param name="nKind"></param>
        /// <param name="nCode"></param>
        void m_SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {
                // �s�u��
                labConnResult.Text = "�s�u���A�G�s�u��";
            }
            else if (nKind == 3002)
            {
                // �s�u���_
                labConnResult.Text = "�s�u���A�G���_";
            }
            else if (nKind == 3003)
            {
                // �s�u���\
                labConnResult.Text = "�s�u���A�G���`";
                isConn = true;
            }
            else if (nKind == 3021)
            {
                //�����_�u
                labConnResult.Text = "�s�u���A�G�����_�u";
            }
        }

        /// <summary>
        /// �ꤺ�����^���ƥ�
        /// </summary>
        /// <param name="sMarketNo"></param>
        /// <param name="nStockIdx"></param>
        void m_SKQuoteLib_OnNotifyQuoteLONG(short sMarketNo, int nStockIdx)
        {
            // ������T����
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

            // ���o�̷s�����g�J������T����
            m_SKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

            // �N������T�����X�b DataGridView
            onUpdateQuote(pSKStockLONG);
        }

        /// <summary>
        /// �ꤺ Best5 �^�Ǩƥ�
        /// </summary>
        void m_SKQuoteLib_OnNotifyBest5(short sMarketNo, int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
        {
            DataRow dr = null;

            if (dtBest5 == null)
            {
                // ��������g�J Datatable
                dtBest5 = new DataTable();
                dtBest5.Columns.Add("Seq");
                dtBest5.Columns.Add("Best5BidQty");
                dtBest5.Columns.Add("Best5BidPrice");
                dtBest5.Columns.Add("Best5AskPrice");
                dtBest5.Columns.Add("Best5AskQty");

                // ���氣�H dDigitNum�A�O�]���Ӫ���Ƹ̭��|�h 2 �� 0�A�ӹw�] dDigitNum �O 100�A�ҥH�n���� 2 �� 0

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

                //��X GridView
                gvBest5Merge.DataSource = dtBest5;
            }
            else
            {
                // ���������s Datatable
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
        /// �w�� 1 ��ƥ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("ss") == "00")
            {
                // �p��W�@����K�u
                DateTime lastMinute = DateTime.Now.AddMinutes(-1);

                List<ClosePriceModel> items = listPriceCollect.Where(m => m.Datetime >= lastMinute).ToList();
                if (items.Count() > 0)
                {
                    // �j��k�u
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

                    // ��X�� DataGridView
                    gvKline.DataSource = listKline.ToList();
                }
            }
        }
        #endregion �ƥ�
    }
}