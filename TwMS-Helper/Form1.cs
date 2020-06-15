using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utility.ModifyRegistry;

namespace TwMS_Helper
{
    public partial class Form1 : Form
    {
        private string dir_value_name = "Path";
        private string dir_reg = "HKEY_LOCAL_MACHINE\\SOFTWARE\\GAMANIA\\MapleStory";

        public Form1()
        {
            InitializeComponent();
            autoCopyPwd.Checked = bool.Parse(ConfigAppSettings.GetValue("AutoCopyPwd", "false"));
            msPath.Text = ConfigAppSettings.GetValue("msPath", "");
            if (msPath.Text == "")
            {
                string dir_reg_0 = dir_reg.Replace("HKEY_LOCAL_MACHINE\\", "");

                try
                {
                    ModifyRegistry myRegistry = new ModifyRegistry();
                    myRegistry.BaseRegistryKey = Registry.CurrentUser;
                    myRegistry.SubKey = dir_reg_0;
                    if (myRegistry.Read(dir_value_name) != "")
                    {
                        ConfigAppSettings.SetValue("msPath", myRegistry.Read(dir_value_name));
                        msPath.Text = myRegistry.Read(dir_value_name);
                    }
                }
                catch
                {
                }
            }
        }

        private void setHKBFEnvironment_Click(object sender, EventArgs e)
        {
            ModifyRegistry myRegistry = new ModifyRegistry();
            myRegistry.BaseRegistryKey = Registry.CurrentUser;

            // 允許運行BFServiceX的域名
            myRegistry.SubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Stats\\{8AFB38D0-67A4-49D3-8822-401755FC6573}\\iexplore\\AllowedDomains\\beanfun.com";
            myRegistry.CreateSubKey();
            myRegistry.SubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Stats\\{8AFB38D0-67A4-49D3-8822-401755FC6573}\\iexplore";
            myRegistry.DeleteKey("Blocked");
            myRegistry.DeleteKey("Flags");

            // 啟用BFServiceX元件
            myRegistry.SubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Settings\\{8AFB38D0-67A4-49D3-8822-401755FC6573}";
            myRegistry.DeleteSubKeyTree();

            // 相容性視圖的域名
            myRegistry.SubKey = "Software\\Policies\\Microsoft\\Internet Explorer\\BrowserEmulation\\PolicyList";
            myRegistry.Write("beanfun.com", "beanfun.com");

            MessageBox.Show("配置完成。");
        }

        private void setAccountTool_Click(object sender, EventArgs e)
        {
            string accountToolPath = string.Format("{0}\\AccountTool.exe", System.Environment.CurrentDirectory);
            if (accountToolPath == "" || !File.Exists(accountToolPath))
            {
                MessageBox.Show("帳密工具檔案丟失，請嘗試重新下載。");
                return;
            }
            string dir_reg_0 = dir_reg.Replace("HKEY_LOCAL_MACHINE\\", "");
            ModifyRegistry myRegistry = new ModifyRegistry();
            myRegistry.BaseRegistryKey = Registry.CurrentUser;
            myRegistry.SubKey = dir_reg_0;
            myRegistry.Write(dir_value_name, accountToolPath);

            MessageBox.Show("配置完成。");
        }

        private bool checkGame()
        {
            string gamePath = msPath.Text;
            if (gamePath == "" || !File.Exists(gamePath))
            {
                DialogResult result = MessageBox.Show("無法正確偵測遊戲安裝狀態。請按一下<是>來重新偵測。若未安裝遊戲，請按一下<否>開始下載。", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    msPath_Click(null, null);
                }
                else
                {
                    Process.Start("https://tw.beanfun.com/maplestory/dw_game.aspx");
                    return false;
                }
            }
            gamePath = msPath.Text;
            if (gamePath == "" || !File.Exists(gamePath))
            {
                return false;
            }
            return true;
        }

        private void autoCopyPwd_CheckedChanged(object sender, EventArgs e)
        {
            bool config = bool.Parse(ConfigAppSettings.GetValue("AutoCopyPwd", "false"));
            if (autoCopyPwd.Checked != config)
            {
                ConfigAppSettings.SetValue("AutoCopyPwd", autoCopyPwd.Checked.ToString());
            }
        }

        private void browseHKBF_Click(object sender, EventArgs e)
        {
            Process.Start("iexplore", "http://hk.beanfun.com/");
        }

        private void msPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "請選擇楓之谷主程式";
            ofd.Filter = "楓之谷主程式|MapleStory.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                msPath.Text = ofd.FileName;
                ConfigAppSettings.SetValue("msPath", msPath.Text);
            }
        }

        enum GameStartMode : int
        {
            Auto = 0,
            Normal = 1,
            LocaleEmulator = 2
        };

        private void startGame_Click(object sender, EventArgs e)
        {
            string gamePath = msPath.Text;
            if (gamePath == "" || !File.Exists(gamePath))
            {
                DialogResult result = MessageBox.Show("無法正確偵測遊戲安裝狀態。請按一下<是>來重新偵測。若未安裝遊戲，請按一下<否>開始下載。", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    msPath_Click(null, null);
                }
                else
                {
                    Process.Start("https://tw.beanfun.com/maplestory/dw_game.aspx");
                    return;
                }
            }
            gamePath = msPath.Text;
            if (gamePath == "" || !File.Exists(gamePath))
            {
                return;
            }
            gamePath = msPath.Text;
            bool findGame = false;
            string gameProcessName = "MapleStory";
            foreach (Process process in Process.GetProcessesByName(gameProcessName))
            {
                try
                {
                    if (process.MainModule.FileName == gamePath)
                    { findGame = true; break; }
                }
                catch { }
            }

            if (findGame)
            {
                DialogResult result = MessageBox.Show("遊戲已經運行,可能是客戶端問題導致未完全結束程序,是否要結束遊戲?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    foreach (Process process in Process.GetProcessesByName(gameProcessName))
                    {
                        try
                        {
                            if (process.MainModule.FileName == gamePath)
                                process.Kill();
                        }
                        catch { }
                    }
                }
            }

            try
            {
                Debug.WriteLine("try open game");
                int runMode = (int)GameStartMode.Auto;
                switch (WindowsAPI.GetSystemDefaultLocaleName())
                {
                    case "zh-Hant":
                    case "zh-CHT":
                    case "zh-TW":
                    case "zh-HK":
                    case "zh-MO":
                        runMode = (int)GameStartMode.Normal;
                        break;
                    default:
                        runMode = (int)GameStartMode.LocaleEmulator;
                        break;
                }

                if (runMode > (int)GameStartMode.LocaleEmulator) runMode = (int)GameStartMode.LocaleEmulator;

                string game_commandLine = "tw.login.maplestory.gamania.com 8484 BeanFun %s %s";
                string commandLine = "";
                if (game_commandLine != "")
                {
                    string account = null;
                    string password = null;
                    if (account != null && password != null && account != "" && password != "")
                    {
                        commandLine = game_commandLine;
                        Regex regex = new Regex("%s");
                        commandLine = regex.Replace(commandLine, account, 1);
                        commandLine = regex.Replace(commandLine, password, 1);
                    }
                }

                switch (runMode)
                {
                    case (int)GameStartMode.LocaleEmulator:
                        OperatingSystem os = System.Environment.OSVersion;
                        if (os.Platform == PlatformID.Win32NT && os.Version.Major < 6 && runMode != (int)GameStartMode.Normal)
                        {
                            MessageBox.Show("以非繁體語係系統啟動遊戲的方式不支援Windows XP。");
                            return;
                        }
                        startByLE(gamePath, commandLine);
                        break;
                    case (int)GameStartMode.Normal:
                        ProcessStartInfo startInfo = new ProcessStartInfo(gamePath);
                        startInfo.WorkingDirectory = Path.GetDirectoryName(gamePath);
                        startInfo.Arguments = commandLine;
                        Process.Start(startInfo);
                        break;
                }
                Debug.WriteLine("try open game done");
            }
            catch
            {
                MessageBox.Show("啟動失敗，請嘗試從桌面捷徑直接啟動遊戲。若您系統為非繁體語係系統，可能是Locale Emulator元件不支援您的系統。");
            }
        }

        private void startByLE(string path, string command)
        {
            if (!File.Exists(string.Format("{0}\\LocaleEmulator.dll", System.Environment.CurrentDirectory)) || !File.Exists(string.Format("{0}\\LoaderDll.dll", System.Environment.CurrentDirectory)))
            {
                MessageBox.Show("程式檢測到您當前運行的系統非繁體語係，但是非繁體語係啟動遊戲需要的檔案丟失，請嘗試重新下載。");
                return;
            }

            int CHINESEBIG5_CHARSET = 136;
            var commandLine = string.Empty;
            commandLine = path.StartsWith("\"")
                ? $"{path} "
                : $"\"{path}\" ";
            commandLine += command;
            System.Globalization.TextInfo culInfo = System.Globalization.CultureInfo.GetCultureInfo("zh-HK").TextInfo;

            var l = new LEProc.LoaderWrapper
            {
                ApplicationName = path,
                CommandLine = commandLine,
                CurrentDirectory = Path.GetDirectoryName(path),
                AnsiCodePage = (uint)culInfo.ANSICodePage,
                OemCodePage = (uint)culInfo.OEMCodePage,
                LocaleID = (uint)culInfo.LCID,
                DefaultCharset = (uint)CHINESEBIG5_CHARSET,
                HookUILanguageAPI = (uint)0,
                Timezone = "China Standard Time",
                NumberOfRegistryRedirectionEntries = 0,
                DebugMode = false
            };

            uint ret;
            if ((ret = l.Start()) != 0)
            {
                if (ret == 0xC00700C1)
                {
                    MessageBox.Show($"非繁體語係系統啟動遊戲失敗\r\n"
                                + $"錯誤碼: {Convert.ToString(ret, 16).ToUpper()}\r\n"
                                + "導致這個錯誤的原因可能是因為使用了遊戲的自動更新，導致遊戲損壞了。\r\n"
                    );
                }
                else
                {
                    MessageBox.Show($"非繁體語係系統啟動遊戲失敗\r\n"
                                + $"錯誤碼: {Convert.ToString(ret, 16).ToUpper()}\r\n"
                                + $"{string.Format($"{Environment.OSVersion} {(Is64BitOS() ? "x64" : "x86")}", Environment.OSVersion, Is64BitOS() ? "x64" : "x86")}\r\n"
                                + $"{GenerateSystemDllVersionList()}\r\n"
                                + "如果你有運行任何防毒軟體, 請關閉後再次嘗試。\r\n"
                                + "如果仍然顯示此視窗, 請嘗試以「安全模式」啟動程式。"
                                + "如果你進行了以上的嘗試仍然沒有一個有效，請隨時在後面的連結提交問題\r\n"
                                + "https://github.com/xupefei/Locale-Emulator/issues\r\n" + "\r\n" + "\r\n"
                                + "你可以按 CTRL+C 將此訊息複製到你的剪貼板。\r\n"
                    );
                }
            }
        }

        public static bool Is64BitOS()
        {
            if (IntPtr.Size == 8) // 64-bit programs run only on Win64
            {
                return true;
            }
            // Detect whether the current process is a 32-bit process 
            // running on a 64-bit system.
            bool flag;
            return DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
                   WindowsAPI.IsWow64Process(WindowsAPI.GetCurrentProcess(), out flag) && flag;
        }

        private static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            var moduleHandle = WindowsAPI.GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }
            return WindowsAPI.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
        }

        private static string GenerateSystemDllVersionList()
        {
            string[] dlls = { "NTDLL.DLL", "KERNELBASE.DLL", "KERNEL32.DLL", "USER32.DLL", "GDI32.DLL" };

            var result = new StringBuilder();

            foreach (var dll in dlls)
            {
                var version = FileVersionInfo.GetVersionInfo(
                                                             Path.Combine(
                                                                          Path.GetPathRoot(Environment.SystemDirectory),
                                                                          Is64BitOS()
                                                                              ? @"Windows\SysWOW64\"
                                                                              : @"Windows\System32\",
                                                                          dll));

                result.Append(dll);
                result.Append(": ");
                result.Append(
                              $"{version.FileMajorPart}.{version.FileMinorPart}.{version.FileBuildPart}.{version.FilePrivatePart}");
                result.Append("\r\n");
            }

            return result.ToString();
        }
    }
}
