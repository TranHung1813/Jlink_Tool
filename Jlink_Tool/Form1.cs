using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jlink_Tool
{
    public partial class Form1 : Form
    {
        JlinkHandler Jlink_handler;
        public Form1()
        {
            InitializeComponent();

            Jlink_handler = new JlinkHandler("N", 4000);
            Jlink_handler.NotifyRecvPacket += JLink_RecvPacket;
            richTextBox1.Text = "";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                bt_Connect_wthoutHalt_Click(null, null);
            }
            if (keyData == Keys.F3)
            {
                btDisconnect_Click(null, null);
            }
            if (keyData == Keys.F5)
            {
                btConnect_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            //try { Reset_Release_State(); } catch { }
            try
            {
                rTb_LogTerminal.AppendText("-------------------------------------------------------" +
                "-----------------------------------------------------------" +
                "-----------------------------------------------------------" +
                "\r\n");
                Jlink_handler.ChangeDeviceName(tbDeviceName.Text);
                Jlink_handler?.StopLog();
                //ReadFile();
                //logInfo = new Jlink_LogInfo
                //{
                //    FwType = "RELEASE"
                //};
                //backup = "";
                Jlink_handler?.StartLog();
                //if (timer_PrintLog.Enabled == false) timer_PrintLog.Start();
                rTb_LogTerminal.Focus();

                Read_Log();
            }
            catch { }
        }

        private void bt_Connect_wthoutHalt_Click(object sender, EventArgs e)
        {
            try
            {
                rTb_LogTerminal.AppendText("-------------------------------------------------------" +
                "-----------------------------------------------------------" +
                "-----------------------------------------------------------" +
                "\r\n");
                Jlink_handler.ChangeDeviceName(tbDeviceName.Text);
                Jlink_handler?.StopLog();

                Jlink_handler?.StartLog_without_Halt();
                //if (timer_PrintLog.Enabled == false) timer_PrintLog.Start();
                rTb_LogTerminal.Focus();

                Read_Log();
            }
            catch { }
        }

        private void JLink_RecvPacket(object sender, RecvPacket e)
        {
            try
            {
                if (e.Length > 0)
                {
                    var param = e;
                    richTextBox1.Invoke((MethodInvoker)delegate
                    {
                        RxTxBox_Write(param.DataRecv, e.TextColor);
                    });
                    
                }
            }
            catch { }
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Jlink_handler?.StopLog();
                RxTxBox_Write("[LOG]:   RTT Viewer disconnected\r\n", Color.Red);
            }
            catch { }
        }

        private static string GetInfo_fromString(string buff, string key, string endString)
        {
            string ret = "";
            try
            {
                if (buff.Contains(key))
                {
                    string temp = buff.Substring(buff.IndexOf(key));
                    int length = temp.IndexOf(endString) - key.Length;
                    ret = temp.Substring(key.Length, length);
                }
            }
            catch { }
            return ret;
        }

        private void Read_Log()
        {
            string filePath = "this_log.txt";

            Task.Run(() =>
            {
                // Open the file with read access and allow other processes to read or write to the file.
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        // Read the entire file content.
                        string content = reader.ReadToEnd();
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (content.Contains("Device "))
                            {
                                string device = GetInfo_fromString(content, "Device \"", "\" selected.");
                                tbDeviceName.Text = device;
                            }
                            //richTextBox1.Text = content;
                            //richTextBox1.SelectionStart = richTextBox1.TextLength;
                            //richTextBox1.ScrollToCaret();
                        });

                        if (JlinkDll.JLINKARM_IsConnected())
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //richTextBox1.AppendText("[LOG]: RTT Viewer connected\r\n");
                                RxTxBox_Write("[LOG]:   RTT Viewer connected\r\n", Color.MediumSpringGreen);
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                //richTextBox1.AppendText("[LOG]: RTT Viewer disconnected\r\n");
                                RxTxBox_Write("[LOG]:   RTT Viewer disconnected\r\n", Color.Red);
                            });
                        }
                    }
                }
            });
        }

        void RxTxBox_Write(String str, Color color)
        {
            String ShowStr;
            ShowStr = str;

            richTextBox1.SelectionColor = color;

            richTextBox1.ScrollToCaret();
            richTextBox1.SelectedText = ShowStr;
            richTextBox1.SelectionColor = Color.FromArgb(0xCC, 0xCC, 0xCC);//Color.White;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;

        }

        private void btRTT_Send_Click(object sender, EventArgs e)
        {
            if (JlinkDll.JLINKARM_IsConnected())
            {
                JlinkDll.JLINK_RTTERMINAL_Write(0, Encoding.ASCII.GetBytes($"{tbRTT_Send.Text}\r\n"), (uint)tbRTT_Send.Text.Length + 2);
                rTb_LogTerminal.Focus();
            }
        }
    }
}
