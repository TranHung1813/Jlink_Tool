using Jlink_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jlink_Tool
{
    public class JlinkHandler
    {
        public string DeviceName { get; set; }
        public int Speed { get; set; }
        private Thread jlinklog_trd;
        public JlinkHandler(string _DeviceName, int _speed)
        {
            DeviceName = _DeviceName;
            Speed = _speed;

            jlinklog_trd = new Thread(new ThreadStart(() => JlinkLog_Thread()));
            jlinklog_trd.IsBackground = true;
        }
        public void StartLog()
        {
            if (string.IsNullOrEmpty(GetJlink_PortName()))
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng kiểm tra lại kết nối Jlink", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(DeviceName) | Speed == 0)
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng kiểm tra lại kết nối Jlink", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //rTb_LogTerminal.AppendText("Stop: " + JlinkHandler.JLINK_RTTERMINAL_Control(1, 0));
            try
            {
                JlinkDll.JLINKARM_Close();
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra");
            }

            JlinkDll.JLINKARM_SetLogFile(Encoding.UTF8.GetBytes("this_log.txt"));
            JlinkDll.JLINKARM_Open();

            string message = "DLL version: " + JlinkDll.JLINKARM_GetDLLVersion() + ", ";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);
            message = "SN:" + JlinkDll.JLINKARM_GetSN() + ", ";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);
            message = "HW Ver:" + JlinkDll.JLINKARM_GetHardwareVersion() + "\r\n";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);

            JlinkDll.JLINKARM_ExecCommand(Encoding.UTF8.GetBytes($"device = {DeviceName}"), 0, 0);
            JlinkDll.JLINKARM_TIF_Select(1);
            JlinkDll.JLINKARM_SetSpeed(Speed);

            //rTb_LogTerminal.AppendText("CPU ID: " + JlinkHandler.JLINKARM_GetId());
            JlinkDll.JLINK_RTTERMINAL_Control(0, 0);
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Connect();
            JlinkDll.JLINKARM_Reset();
            if (JlinkDll.JLINKARM_IsHalted())
            {

            }
            else
            {

            }
            JlinkDll.JLINKARM_Halt();
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Go();


            //rTb_LogTerminal.AppendText("\x1b[36mTEST\x1b[0m");
            //JlinkHandler.JLINKARM_Reset();
            try
            {
                jlinklog_trd?.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("StartLog_Error: " + ex.Message);

            }
            jlinklog_trd = new Thread(new ThreadStart(JlinkLog_Thread));
            jlinklog_trd.IsBackground = true;
            jlinklog_trd.Start();
        }

        public void StartLog_without_Halt()
        {
            if (string.IsNullOrEmpty(GetJlink_PortName()))
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng kiểm tra lại kết nối Jlink", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(DeviceName) | Speed == 0)
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng kiểm tra lại kết nối Jlink", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //rTb_LogTerminal.AppendText("Stop: " + JlinkHandler.JLINK_RTTERMINAL_Control(1, 0));
            JlinkDll.JLINKARM_Close();
            Thread.Sleep(100);

            JlinkDll.JLINKARM_SetLogFile(Encoding.UTF8.GetBytes("this_log.txt"));
            JlinkDll.JLINKARM_Open();

            string message = "DLL version: " + JlinkDll.JLINKARM_GetDLLVersion() + ", ";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);
            message = "SN:" + JlinkDll.JLINKARM_GetSN() + ", ";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);
            message = "HW Ver:" + JlinkDll.JLINKARM_GetHardwareVersion() + "\r\n";
            OnNotifyRecvPacket(message, message.Length, Color.DodgerBlue);

            JlinkDll.JLINKARM_ExecCommand(Encoding.UTF8.GetBytes($"device = {DeviceName}"), 0, 0);
            JlinkDll.JLINKARM_TIF_Select(1);
            JlinkDll.JLINKARM_SetSpeed(Speed);

            //rTb_LogTerminal.AppendText("CPU ID: " + JlinkHandler.JLINKARM_GetId());
            JlinkDll.JLINK_RTTERMINAL_Control(0, 0);
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Connect();
            //JlinkDll.JLINKARM_Reset();
            //if (JlinkDll.JLINKARM_IsHalted())
            //{

            //}
            //else
            //{

            //}
            //JlinkDll.JLINKARM_Halt();
            //Thread.Sleep(100);
            //JlinkDll.JLINKARM_Go();


            //rTb_LogTerminal.AppendText("\x1b[36mTEST\x1b[0m");
            //JlinkHandler.JLINKARM_Reset();
            try
            {
                jlinklog_trd?.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("StartLog_Error: " + ex.Message);

            }
            jlinklog_trd = new Thread(new ThreadStart(JlinkLog_Thread));
            jlinklog_trd.IsBackground = true;
            jlinklog_trd.Start();

            JlinkDll.JLINKARM_SetLogFile(Encoding.UTF8.GetBytes("temp.txt"));
        }

        Color txtColor = Color.FromArgb(0xCC, 0xCC, 0xCC);// Color.White;
        
        private void JlinkLog_Thread()
        {
            int countNoData = 0;
            int countDisconnect = 0;
            string mess;
            bool isReconnected = false;
            while (true)
            {
                try
                {
                    if (JlinkDll.JLINKARM_IsConnected())
                    {
                        mess = JlinkDll.JLINKARM_ReadRTT_String();
                        string recvText = "";
                        if (mess.Replace("\0", "").Length > 0)
                        {
                            AppendAnsiColoredText(mess.Replace("\0", ""), ref recvText, ref txtColor);
                            //if (mess.Replace("\0", "").Length > 0)
                            //{
                            //    string pattern = @"(\r\n)";
                            //    string tmp_mess = mess
                            //                .Replace("\u001b[2;32m", "").Replace("\u001b[32m", "[I]")
                            //                .Replace("\u001b[2;31m", "").Replace("\u001b[31m", "[E]")
                            //                .Replace("\u001b[2;33m", "").Replace("\u001b[33m", "[W]")
                            //                .Replace("\u001b[2;35m", "").Replace("\u001b[35m", "[V]")
                            //                //.Replace("\u001b[0m", "")
                            //                .Replace("\0", "");
                            //    string[] mess_buff = Regex
                            //                .Split(tmp_mess, pattern)
                            //                .Where(i => i != "").ToArray();
                            //    foreach (string s in mess_buff)
                            //    {
                            //        if (s.Contains("[E]"))
                            //            txtColor = Color.Red;
                            //        else if (s.Contains("[I]"))
                            //            txtColor = Color.Lime;
                            //        else if (s.Contains("[W]"))
                            //            txtColor = Color.Yellow;
                            //        else if (s.Contains("[V]"))
                            //            txtColor = Color.Fuchsia;
                            //        else if(s.Contains("\u001b[0m"))
                            //            txtColor = Color.White;

                            //        string msg = s.Replace("\u001b[0m", "");
                            //OnNotifyRecvPacket(recvText, recvText.Length, textColor);
                            //    }
                            //    countNoData = 0;
                            //}
                            //else
                            //{
                            //    if (++countNoData % 50 == 0)
                            //    {
                            //        // NO DATA for long time
                            //        //OnNotifyRecvPacket("NO DATA\r\n", "NO DATA\r\n".Length, Color.Red);
                            //        if (countNoData >= 200)
                            //        {
                            //            countNoData = 0;
                            //            // Reconnect
                            //            if (!string.IsNullOrEmpty(GetJlink_PortName()))
                            //            {
                            //                //JlinkReconnect();
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                    else if (++countDisconnect >= 30)
                    {
                        countDisconnect = 0;
                        OnNotifyRecvPacket("DISCONNECTED\r\n", "DISCONNECTED\r\n".Length, Color.Red);
                        // Reconnect
                        if (!string.IsNullOrEmpty(GetJlink_PortName()))
                        {
                            //JlinkReconnect_without_Reset();
                            //isReconnected = false;
                        }

                    }
                }
                catch
                {
                    OnNotifyRecvPacket("ERROR\r\n", "ERROR\r\n".Length, Color.Red);
                }
                Thread.Sleep(100);
            }
        }

        private void AppendAnsiColoredText(string text, ref string outText, ref Color outColor)
        {
            // Regex to match ANSI escape codes
            string ansiRegex = @"\x1B\[(?<code>[0-9;]*)m";
            var matches = Regex.Matches(text, ansiRegex);

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                // Add text before the ANSI code
                if (match.Index > lastIndex)
                {
                    outText = (text.Substring(lastIndex, match.Index - lastIndex));
                }
                else outText = "";

                //
                OnNotifyRecvPacket(outText, outText.Length, outColor);

                // Parse the ANSI code and get the color
                string ansiCode = match.Groups["code"].Value;
                ApplyAnsiCode(ansiCode, ref outColor);

                lastIndex = match.Index + match.Length;
            }

            // Add remaining text after the last ANSI code
            if (lastIndex < text.Length)
            {
                outText = (text.Substring(lastIndex));

                OnNotifyRecvPacket(outText, outText.Length, outColor);
            }
        }

        private void ApplyAnsiCode(string ansiCode, ref Color textColor)
        {
            string[] codes = ansiCode.Split(';');
            foreach (string code in codes)
            {
                if (int.TryParse(code, out int codeInt))
                {
                    switch (codeInt)
                    {
                        case 0:
                            // Reset to default color
                            textColor = Color.FromArgb(0xCC, 0xCC, 0xCC); //Color.White;
                            break;
                        case 2:
                            // Dim (handled by applying a lighter color later)
                            break;
                        case 30:
                            textColor = Color.Black;
                            break;
                        case 31:
                            textColor = Color.FromArgb(0xC5, 0x0F, 0x1F); //Color.Red;
                            break;
                        case 32:
                            textColor = Color.FromArgb(0x13, 0xA1, 0x0E); //Color.Green;
                            break;
                        case 33:
                            textColor = Color.FromArgb(0xC1, 0x9C, 0x00); //Color.Yellow;
                            break;
                        case 34:
                            textColor = Color.Blue;
                            break;
                        case 35:
                            textColor = Color.FromArgb(0x88, 0x17, 0x98); //Color.Magenta;
                            break;
                        case 36:
                            textColor = Color.Cyan;
                            break;
                        case 37:
                            textColor = Color.FromArgb(0xCC, 0xCC, 0xCC); //Color.White;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void JlinkReconnect()
        {
            JlinkDll.JLINK_RTTERMINAL_Control(1, 0);
            JlinkDll.JLINKARM_Close();
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Open();
            JlinkDll.JLINKARM_ExecCommand(Encoding.UTF8.GetBytes($"device = {DeviceName}"), 0, 0);
            JlinkDll.JLINKARM_TIF_Select(1);
            JlinkDll.JLINKARM_SetSpeed(Speed);
            JlinkDll.JLINK_RTTERMINAL_Control(0, 0);
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Reset();
            JlinkDll.JLINKARM_Halt();
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Go();
        }

        private void JlinkReconnect_without_Reset()
        {
            JlinkDll.JLINK_RTTERMINAL_Control(1, 0);
            JlinkDll.JLINKARM_Close();
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Open();
            JlinkDll.JLINKARM_ExecCommand(Encoding.UTF8.GetBytes($"device = {DeviceName}"), 0, 0);
            JlinkDll.JLINKARM_TIF_Select(1);
            JlinkDll.JLINKARM_SetSpeed(Speed);
            JlinkDll.JLINK_RTTERMINAL_Control(0, 0);
            Thread.Sleep(100);
            JlinkDll.JLINKARM_Connect();
        }

        public void StopLog()
        {
            try
            {
                jlinklog_trd?.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("StopLog_Error: " + ex.Message);

            }
            if (!string.IsNullOrEmpty(GetJlink_PortName()))
            {
                Task.Run(() =>
                {
                    JlinkDll.JLINK_RTTERMINAL_Control(1, 0);
                    JlinkDll.JLINKARM_Close();
                });
            }

        }

        public static string GetJlink_PortName()
        {
            string Jlink_Port = "";
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = portnames.Select(n => ports.FirstOrDefault(s => s.Contains($"({n})"))).ToList();
                portList.RemoveAll(i => i == null);

                Jlink_Port = portList.FirstOrDefault(n => n.Contains("JLink"))?.ToString();
            }
            return Jlink_Port;
        }

        private event EventHandler<RecvPacket> _NotifyRecvPacket;
        public event EventHandler<RecvPacket> NotifyRecvPacket
        {
            add
            {
                _NotifyRecvPacket += value;
            }
            remove
            {
                _NotifyRecvPacket -= value;
            }
        }
        protected virtual void OnNotifyRecvPacket(string datarecv, int length, Color color = default)
        {
            if (_NotifyRecvPacket != null)
            {
                _NotifyRecvPacket(this, new RecvPacket(datarecv, length, color));
            }
        }
    }
    public class RecvPacket : EventArgs
    {
        public string DataRecv;
        public int Length;
        [DefaultValue(typeof(Color), "255, 0, 0")]
        public Color TextColor;
        public RecvPacket(string datarecv, int length, Color color = default)
        {
            DataRecv = datarecv;
            Length = length;
            TextColor = color;
        }
    }
}
