using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Jlink_Tool
{
    internal class JlinkDll
    {
        public void JLINKARM_Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        /// <summary>
        /// 打开JLINK设备
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_Open();

        /// <summary>
        /// 关闭JLINK设备
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_Close();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_Connect();

        [DllImport("JLinkARM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINK_RTTERMINAL_Control(UInt32 CMD, UInt32 Size);

        [DllImport("JLinkARM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINK_RTTERMINAL_Read(int terminal, [Out(), MarshalAs(UnmanagedType.LPArray)] byte[] rxBuffer, UInt32 size);

        static string gdb_ansi(string str)
        {
            return "kaka";
        }

        public static string JLINKARM_ReadRTT_String()
        {
            try
            {
                byte[] aa = new byte[1000];
                JLINK_RTTERMINAL_Read(0, aa, 1000);

                ASCIIEncoding kk = new ASCIIEncoding();
                UTF8Encoding uu = new UTF8Encoding();

                string ss = uu.GetString(aa);
                if (ss.Length > 1)
                {
                    Console.Write(ss);
                }
                return ss;

            }
            catch { }
            return String.Empty;
        }


        [DllImport("JLinkARM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINK_RTTERMINAL_Write(int terminal, [In(), MarshalAs(UnmanagedType.LPArray)] byte[] txBuffer, UInt32 size);


        /// <summary>
        /// 系统复位
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_Reset();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_GoAllowSim();

        /// <summary>
        /// 执行程序
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_Go();

        /// <summary>
        /// 中断程序执行
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool JLINKARM_Halt();

        /// <summary>
        /// 单步执行
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool JLINKARM_Step();


        /// <summary>
        /// 清除错误信息
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ClrError();

        /// <summary>
        /// 设置JLINK接口速度
        /// </summary>
        /// <param name="speed"></param>
        /// <remarks>0为自动调整</remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_SetSpeed(int speed);


        /// <summary>
        /// 设置JTAG为最高速度
        /// </summary>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_SetMaxSpeed();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt16 JLINKARM_GetSpeed();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_GetVoltage();

        /// <summary>
        /// 当前MCU是否处于停止状态
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool JLINKARM_IsHalted();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool JLINKARM_IsConnected();

        /// <summary>
        /// JLINK是否已经可以操作了
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool JLINKARM_IsOpen();

        /// <summary>
        /// 取消程序断点
        /// </summary>
        /// <param name="index">断点序号</param>
        /// <remarks>配合JLINKARM_SetBP()使用</remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ClrBP(UInt32 index);

        /// <summary>
        /// 设置程序断点
        /// </summary>
        /// <param name="index">断点序号</param>
        /// <param name="addr">目标地址</param>
        /// <remarks>建议使用JLINKARM_SetBPEx()替代</remarks>
        [DllImport("JLinkARM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_SetBP(UInt32 index, UInt32 addr);

        /// <summary>
        /// 设置程序断点
        /// </summary>
        /// <param name="addr">目标地址</param>
        /// <param name="mode">断点类型</param>
        /// <returns>Handle,提供给JLINKARM_ClrBPEx()使用</returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int JLINKARM_SetBPEx(UInt32 addr, BP_MODE mode);

        /// <summary>
        /// 取消程序断点
        /// </summary>
        /// <param name="handle"></param>
        /// <remarks>配合JLINKARM_SetBPEx()使用</remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ClrBPEx(int handle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int JLINKARM_SetWP(UInt32 addr, UInt32 addrmark, UInt32 dat, UInt32 datmark, byte ctrl, byte ctrlmark);

        /// <summary>
        /// 取消数据断点
        /// </summary>
        /// <param name="handle"></param>
        /// <remarks>配合JLINKARM_SetWP()使用</remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ClrWP(int handle);

        /// <summary>
        /// 设置寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dat"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteReg(ARM_REG index, UInt32 dat);

        /// <summary>
        /// 读取寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_ReadReg(ARM_REG index);

        /// <summary>
        /// 写入一段数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="size"></param>
        /// <param name="buf"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteMem(UInt32 addr, UInt32 size, byte[] buf);

        /// <summary>
        /// 读取一段数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="size"></param>
        /// <param name="buf"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ReadMem(UInt32 addr, UInt32 size, [Out(), MarshalAs(UnmanagedType.LPArray)] byte[] buf);

        /// <summary>
        /// 从调试通道获取一串数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="size">需要获取的数据长度</param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ReadDCCFast([Out(), MarshalAs(UnmanagedType.LPArray)] UInt32[] buf, UInt32 size);

        /// <summary>
        /// 从调试通道获取一串数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="size">希望获取的数据长度</param>
        /// <param name="timeout"></param>
        /// <returns>实际获取的数据长度</returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_ReadDCC([Out(), MarshalAs(UnmanagedType.LPArray)] UInt32[] buf, UInt32 size, int timeout);

        /// <summary>
        /// 向调试通道写入一串数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="size">需要写入的数据长度</param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteDCCFast(UInt32[] buf, UInt32 size);

        /// <summary>
        /// 向调试通道写入一串数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="size">希望写入的数据长度</param>
        /// <param name="timeout"></param>
        /// <returns>实际写入的数据长度</returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_WriteDCC(UInt32[] buf, UInt32 size, int timeout);

        /// <summary>
        /// 获取JLINK的DLL版本号
        /// </summary>
        /// <returns></returns>
        /// <remarks>使用10进制数表示</remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_GetDLLVersion();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_ExecCommand([In(), MarshalAs(UnmanagedType.LPArray)] byte[] oBuffer, int a, int b);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_TIF_Select(int type);

        /// <summary>
        /// 获取JLINK的固件版本号
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_GetHardwareVersion();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void JLINKARM_GetFeatureString([Out(), MarshalAs(UnmanagedType.LPArray)] byte[] oBuffer);

        [DllImport("JLinkARM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void JLINKARM_GetOEMString([Out(), MarshalAs(UnmanagedType.LPArray)] byte[] oBuffer);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_SetLogFile([In(), MarshalAs(UnmanagedType.LPArray)] byte[] oBuffer);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern StringBuilder JLINKARM_GetCompileDateTime();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_GetSN();

        /// <summary>
        /// 获取当前MCU的ID号
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern UInt32 JLINKARM_GetId();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void JLINKARM_ReadMemU32(UInt32 addr, UInt32 leng, ref UInt32 buf, ref byte status);

        /// <summary>
        /// 写入32位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dat"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteU32(UInt32 addr, UInt32 dat);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void JLINKARM_ReadMemU16(UInt32 addr, UInt32 leng, ref UInt16 buf, ref byte status);

        /// <summary>
        /// 写入16位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dat"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteU16(UInt32 addr, UInt16 dat);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void JLINKARM_ReadMemU8(UInt32 addr, UInt32 leng, ref byte buf, ref byte status);

        /// <summary>
        /// 写入8位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dat"></param>
        /// <remarks></remarks>
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void JLINKARM_WriteU8(UInt32 addr, byte dat);

        /// <summary>
        /// 读取32位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public UInt32 JLINKARM_ReadU32(UInt32 addr)
        {
            UInt32 dat = 0;
            byte stu = 0;
            JLINKARM_ReadMemU32(addr, 1, ref dat, ref stu);
            return dat;
        }

        /// <summary>
        /// 读取16位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public UInt16 JLINKARM_ReadU16(UInt32 addr)
        {
            UInt16 dat = 0;
            byte stu = 0;
            JLINKARM_ReadMemU16(addr, 1, ref dat, ref stu);
            return dat;
        }

        /// <summary>
        /// 读取8位的数据
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public byte JLINKARM_ReadU8(UInt32 addr)
        {
            byte dat = 0;
            byte stu = 0;
            JLINKARM_ReadMemU8(addr, 1, ref dat, ref stu);
            return dat;
        }

        /// <summary>
        /// 设置数据断点
        /// </summary>
        /// <param name="addr">目标地址</param>
        /// <param name="addrmark">地址屏蔽位</param>
        /// <param name="dat">目标数据</param>
        /// <param name="datmark">数据屏蔽位</param>
        /// <param name="mode">触发模式</param>
        /// <returns>Handle,提供给JLINKARM_ClrWP()函数使用</returns>
        /// <remarks>当前数值除了屏蔽位以外的数据位,与目标数据除了屏蔽位以外的数据位,一致即可产生触发</remarks>
        public int JLINKARM_SetWP(UInt32 addr, UInt32 addrmark, UInt32 dat, UInt32 datmark, WP_MODE mode)
        {
            switch (mode)
            {
                case WP_MODE.READ_WRITE:
                    return JLINKARM_SetWP(addr, addrmark, dat, datmark, 0x8, 0xf7);
                case WP_MODE.READ:
                    return JLINKARM_SetWP(addr, addrmark, dat, datmark, 0x8, 0xf6);
                case WP_MODE.WRITE:
                    return JLINKARM_SetWP(addr, addrmark, dat, datmark, 0x9, 0xf6);
                default:
                    {
                        return 0xff;
                    }
            }
        }

        public string JLINKARM_StringFeature()
        {
            byte[] aa = new byte[1000];
            JLINKARM_GetFeatureString(aa);
            ASCIIEncoding kk = new ASCIIEncoding();
            string ss = kk.GetString(aa);
            return ss;
        }

        public string JLINKARM_StringOEM()
        {
            byte[] aa = new byte[1000];
            JLINKARM_GetOEMString(aa);
            ASCIIEncoding kk = new ASCIIEncoding();
            string ss = kk.GetString(aa);
            return ss;
        }

        public void JLINKARM_Setup(string cmdstr)
        {
            //RmAnnotate rm = new RmAnnotate();
            //rm.MarkDosMode = false;
            //rm.MarkStartSpaceToTab = 0;
            //cmdstr = rm.Convert(cmdstr);
            cmdstr = cmdstr.Replace(" ", "");
            cmdstr = cmdstr.Replace("\t", "");
            cmdstr = cmdstr.Replace("\r", "");
            cmdstr = cmdstr.Replace("\n", "");
            string[] cmd = cmdstr.Split(';');
            for (int i = 0; i <= cmd.Length - 1; i++)
            {
                _setupDoCmd(cmd[i]);
            }
        }
        private void _setupDoCmd(string cmdstr)
        {
            string cmd = cmdstr.ToLower();
            cmd = cmd.Replace("(", ",");
            cmd = cmd.Replace(")", "");
            cmd = cmd.TrimEnd(',');
            string[] arg = cmd.Split(',');

            UInt32 val1;
            UInt32 val2;
            if (arg.Length == 3)
            {
                cmd = arg[0];
                val1 = _setupGetVal(arg[1]);
                val2 = _setupGetVal(arg[2]);
            }
            else if (arg.Length == 2)
            {
                cmd = arg[0];
                val1 = _setupGetVal(arg[1]);
                val2 = 0;
            }
            else if (arg.Length == 1)
            {
                cmd = arg[0];
                val1 = 0;
                val2 = 0;
            }
            else
            {
                cmd = "";
                val1 = 0;
                val2 = 0;
            }

            if (cmd != "")
            {
                Debug.WriteLine("Do CMD: " + cmdstr);
                switch (cmd)
                {
                    case "setjtagspeed":
                        JLINKARM_SetSpeed((int)val1);
                        break;
                    case "delay":
                        JLINKARM_Sleep((int)val1);
                        break;
                    case "disablemmu":
                        Console.WriteLine("...........................CMD not Supported");
                        break;
                    case "go":
                        JLINKARM_Go();
                        break;
                    case "halt":
                        JLINKARM_Halt();
                        break;
                    case "reset":
                        JLINKARM_Reset();
                        if (val1 > 0)
                        {
                            JLINKARM_Sleep((int)val1);
                        }
                        break;
                    case "resetbp0":
                        Console.WriteLine("...........................CMD not Supported");
                        break;
                    case "ResetADI":
                        Console.WriteLine("...........................CMD not Supported");
                        break;
                    case "read8":
                        JLINKARM_ReadU8(val1);
                        break;
                    case "read16":
                        JLINKARM_ReadU16(val1);
                        break;
                    case "read32":
                        JLINKARM_ReadU32(val1);
                        break;
                    case "verify8":
                        do
                        {
                            byte aa = JLINKARM_ReadU8(val1);
                            if (aa == ((byte)val2 & 0xff))
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "verify16":
                        do
                        {
                            ushort aa = JLINKARM_ReadU16(val1);
                            if (aa == ((ushort)val2 & 0xffff))
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "verify32":
                        do
                        {
                            uint aa = JLINKARM_ReadU32(val1);
                            if (aa == val2)
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "write8":
                        JLINKARM_WriteU8(val1, (byte)val2);
                        break;
                    case "write16":
                        JLINKARM_WriteU16(val1, (ushort)val2);
                        break;
                    case "write32":
                        JLINKARM_WriteU32(val1, val2);
                        break;
                    case "writeverify8":
                        do
                        {
                            JLINKARM_WriteU8(val1, (byte)val2);
                            byte aa = JLINKARM_ReadU8(val1);
                            if (aa == ((byte)val2 & 0xff))
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "writeverify16":
                        do
                        {
                            JLINKARM_WriteU16(val1, (ushort)val2);
                            ushort aa = JLINKARM_ReadU16(val1);
                            if (aa == ((ushort)val2 & 0xffff))
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "writeverify32":
                        do
                        {
                            JLINKARM_WriteU32(val1, val2);
                            uint aa = JLINKARM_ReadU32(val1);
                            if (aa == val2)
                            {
                                break; // TODO: might not be correct. Was : Exit Do
                            }
                            JLINKARM_Sleep(1);
                        } while (true);
                        break;

                    case "writeregister":
                        JLINKARM_WriteReg((ARM_REG)val1, val2);
                        break;
                    case "writejtag_ir":
                        Console.WriteLine("...........................CMD not Supported");
                        break;
                    case "writejtag_dr":
                        Console.WriteLine("...........................CMD not Supported");
                        break;
                    default:
                        Console.WriteLine("...........................Unkonwned CMD");
                        break;
                }
            }
        }
        private UInt32 _setupGetVal(string str)
        {
            UInt32 dd;
            if (str.StartsWith("0x") && str.Length >= 3)
            {
                dd = Convert.ToUInt32(str.Substring(2), 16);
            }
            else
            {
                dd = Convert.ToUInt32(str);
            }
            return dd;
        }

        /// <summary>
        /// ARM内部寄存器
        /// </summary>
        /// <remarks></remarks>
        public enum ARM_REG : UInt32
        {
            R0,
            R1,
            R2,
            R3,
            R4,
            R5,
            R6,
            R7,
            CPSR,
            R15,
            R8_USR,
            R9_USR,
            R10_USR,
            R11_USR,
            R12_USR,
            R13_USR,
            R14_USR,
            SPSR_FIQ,
            R8_FIQ,
            R9_FIQ,
            R10_FIQ,
            R11_FIQ,
            R12_FIQ,
            R13_FIQ,
            R14_FIQ,
            SPSR_SVC,
            R13_SVC,
            R14_SVC,
            SPSR_ABT,
            R13_ABT,
            R14_ABT,
            SPSR_IRQ,
            R13_IRQ,
            R14_IRQ,
            SPSR_UND,
            R13_UND,
            R14_UND,
            SPSR_SYS,
            R13_SYS,
            R14_SYS,
            PC = 9
        }

        /// <summary>
        /// 程序断点模式
        /// </summary>
        /// <remarks></remarks>
        public enum BP_MODE : UInt32
        {
            ARM = 1,
            THUMB = 2,
            HARD_ARM = 0xffffff01u,
            HARD_THUMB = 0xffffff02u,
            SOFT_ARM = 0xf1u,
            SOFT_THUMB = 0xf2u
        }

        /// <summary>
        /// 数据断点模式
        /// </summary>
        /// <remarks></remarks>
        public enum WP_MODE : UInt32
        {
            READ_WRITE,
            READ,
            WRITE
        }
    }
}
