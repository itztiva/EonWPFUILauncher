using DiscordRPC;
using Luna_Launch;
using Luxury;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UiDesktopApp1.ViewModels.Pages;
using Wpf.Ui.Controls;
using System.Windows.Input;
using System.ComponentModel;
using UiDesktopApp1.Views.Windows;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Windows.Shapes;
using UiDesktopApp1.Utils;

namespace UiDesktopApp1.Views.Pages
{


    public partial class DashboardPage : Page
    {
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = null;
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private string projectDir;

        public DashboardViewModel ViewModel { get; }

        internal static void DownloadFile(string URL, string path)
        {
            new WebClient().DownloadFile(URL, path);
        }

        public static void SafeKillProcess(string processName)
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(processName);
                for (int i = 0; i < processesByName.Length; i++)
                {
                    processesByName[i].Kill();
                }
            }
            catch
            {
            }
        }
        public static void Inject(int pid, string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            IntPtr hProcess = Win32.OpenProcess(1082, false, pid);
            IntPtr procAdress = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            uint num = checked((uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))));
            IntPtr intPtr = Win32.VirtualAllocEx(hProcess, IntPtr.Zero, num, 12288U, 4U);
            UIntPtr uintPtr;
            Win32.WriteProcessMemory(hProcess, intPtr, Encoding.Default.GetBytes(path), num, out uintPtr);
            Win32.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAdress, intPtr, 0U, IntPtr.Zero);

            Console.WriteLine("Injected");
        }

        public DashboardPage(DashboardViewModel viewModel)
        {
            RPC.rpctimestamp = Timestamps.Now;
            RPC.InitializeRPC();
            RPC.SetState("In Launcher", true);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.ResizeMode = ResizeMode.CanMinimize;
            }
        }

        private void button12_Click_1(object sender, RoutedEventArgs e)
        {
            RPC.SetState("Starting the game...", true);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eon");
            string filePath = System.IO.Path.Combine(folderPath, "Settings.ini");

            string email = "";
            string password = "";
            string directory = "";

            if (!File.Exists(filePath))
            {
                System.Windows.MessageBox.Show("Settings not found.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (File.Exists(filePath))
                {
                    var lines2 = File.ReadAllLines(filePath).ToList();
                    for (int i = 0; i < lines2.Count; i++)
                    {
                        if (lines2[i].StartsWith("Directory="))
                        {
                            directory = lines2[i].Substring("Directory=".Length);
                        }
                    }
                }

                if (!Directory.Exists(directory + "\\FortniteGame") || !Directory.Exists(directory + "\\Engine"))
                {
                    System.Windows.MessageBox.Show("FortniteGame and/or Engine not found. Make sure it's the valid directory.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                   
                    string SplashPath = System.IO.Path.Combine(directory, "FortniteGame", "Content", "Splash", "Splash.bmp");

                    //             if (File.Exists(SplashPath))
                    //           {
                    //             File.Delete(SplashPath);
                    //                DownloadFile("https://cdn.discordapp.com/attachments/814917879458824236/1191438802418401420/56v56488.bmp", SplashPath);
                    //         }

                    /* if (!Directory.Exists(Path.Combine(projectDir, "Luna_.dll")))
            {
                DownloadFile("https://cdn.discordapp.com/attachments/1149445201690120222/1151259961620430980/Aurora.Runtime.dll", Path.Combine(projectDir, "Luna_.dll"));
            }; */

                    if (!Directory.Exists(directory + "\\Redirect.dll"))
                    {
                        DownloadFile("https://api.lunafn.xyz/redirect", directory + "\\Redirect.dll");
                    };

                    string directoryPath = directory + "\\";
                    string RarPath = directoryPath + "\\EonEAC.rar";
                    string extractedFolderPath = directoryPath + "\\";

                    string RarUrl = "https://api.lunafn.xyz/EasyAntiCheat";

                    try
                    {
                        DownloadFile(RarUrl, RarPath);
                        Console.WriteLine("RAR file downloaded successfully.");

                        ExtractRAR(RarPath, extractedFolderPath);
                        Console.WriteLine("RAR file extracted successfully.");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error handling RAR: " + ex.Message, "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                        RPC.SetState("In Launcher", true);
                    }

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = System.IO.Path.Combine(directoryPath, "EasyAntiCheat", "EasyAntiCheat_EOS_Setup.exe"),
                        WorkingDirectory = directoryPath,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = System.IO.Path.Combine(directoryPath, "EonEAC.exe"),
                        WorkingDirectory = directoryPath,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });



                    bool EmailPassError = false;

                    {
                        var lines = File.ReadAllLines(filePath).ToList();
                        for (int i = 0; i < lines.Count; i++)
                        {
                            if (lines[i].StartsWith("Email="))
                            {
                                email = lines[i].Substring("Email=".Length);
                            }
                            else if (lines[i].StartsWith("Password="))
                            {
                                password = lines[i].Substring("Password=".Length);
                            }
                        }

                        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                        {
                            EmailPassError = true;
                        }
                    }

                    using (FileStream fs = File.Create(filePath))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.WriteLine("[Account]");
                            writer.WriteLine($"Email={email}");
                            writer.WriteLine($"Password={password}");
                            writer.WriteLine($"Directory={directory}");
                            writer.WriteLine($"AccountId={textBox5.Text}");
                        };
                    };

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = directory + "\\FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = directory + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });

                    foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC"))
                    {
                        process.Kill();
                    }

                    SafeKillProcess("FortniteClient-Win64-Shipping_BE");
                    SafeKillProcess("FortniteLauncher");
                    SafeKillProcess("FortniteClient-Win64-Shipping");
                    SafeKillProcess("CrashReportClient");

                    Process proc = new Process();
                    proc.StartInfo.FileName = directory + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe";
                    proc.StartInfo.Arguments = $"-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -noeac -fromfl=be -fltoken=7673cbd7dbe3e7fdfag3a4b2 -AUTH_LOGIN={email} -AUTH_PASSWORD={password} -AUTH_TYPE=epic";
                    FakeAC.Start(directory, "FortniteClient-Win64-Shipping_BE.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "r");
                    FakeAC.Start(directory, "FortniteLauncher.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "dsf");
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    try
                    {
                        FakeAC._FNLauncherProcess.Close();
                        FakeAC._FNAntiCheatProcess.Close();
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("There Been A Error Closing");
                    }

                    Inject(proc.Id, directory + "\\Redirect.dll");
                    Inject(proc.Id, directory + "\\Engine\\Binaries\\ThridParty\\NVIDIA\\NVaftermath\\Win64\\GFSDK_Aftermath_Lib.x64.dll");

                    if (EmailPassError)
                    {
                        foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
                        {
                            process.Kill();
                        }

                        foreach (Process process in Process.GetProcessesByName("FortniteLauncher"))
                        {
                            process.Kill();
                        }

                        foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC"))
                        {
                            process.Kill();
                        }
                        System.Windows.MessageBox.Show("Email and/or password and/or directory is empty.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (Directory.GetFiles(directoryPath).Length > 54)
                        {
                            {
                                button13.Visibility = Visibility.Hidden;
                                button12.Visibility = Visibility.Visible;

                                RPC.SetState("In Launcher", true);

                                foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
                                {
                                    process.Kill();
                                }

                                foreach (Process process in Process.GetProcessesByName("FortniteLauncher"))
                                {
                                    process.Kill();
                                }

                                foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC"))
                                {
                                    process.Kill();
                                }
                            }
                            RPC.SetState("In Launcher", true);
                            System.Windows.MessageBox.Show("Anti Cheat triggered. Please remove your unoffical Paks from \"FortniteGame\\Content\\Paks\"", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        else
                        {
                            button12.Visibility = Visibility.Hidden;
                            button13.Visibility = Visibility.Visible;

                            RPC.SetState("In Game", true);
                        }
                    }
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWebContent();
        }
        public async void LoadWebContent()
        {
            using (HttpClient client = new HttpClient())
            {
                bool success = false;
                int retryCount = 3; 

                while (!success && retryCount > 0)
                {
                    try
                    {
                        string url = "http://api.lunafn.xyz/News";
                        string content = await client.GetStringAsync(url);

                        Dispatcher.Invoke(() =>
                        {
                            Label2.Content = content;
                        });

                        success = true;
                    }
                    catch (HttpRequestException e)
                    {
                        await Task.Delay(2000); 
                        retryCount--;

                        if (retryCount == 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Label2.Content = "Unable to connect to Eon\n\nIf you see this that means that Eon\nIs Most Likely Offline\nCheck the Discord for more information.";
                            });
                            RPC.SetState("Unable to Connect To API", true);
                        }
                    }
                }
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            button13.Visibility = Visibility.Hidden;
            button12.Visibility = Visibility.Visible;

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            RPC.SetState("In Launcher", true);

            foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
            {
                process.Kill();
            }

            foreach (Process process in Process.GetProcessesByName("FortniteLauncher"))
            {
                process.Kill();
            }

            foreach (Process process in Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC"))
            {
                process.Kill();
            }
            System.Windows.MessageBox.Show("Fortnite has been successfully shut down!", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://cdn.fnbuilds.services/9.10.rar",
                UseShellExecute = true
            });
        }

        private void button22_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://dsc.gg/luxuryfn",
                UseShellExecute = true
            });
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eon");
            string filePath = System.IO.Path.Combine(folderPath, "Settings.ini");
            string directory = "";

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("Directory="))
                    {
                        directory = lines[i].Substring("Directory=".Length);
                    }
                }
            }

            string SplashPath = System.IO.Path.Combine(directory, "FortniteGame", "Content", "Splash", "Splash.bmp");
            string directoryPath = directory + "\\";
            string RarPath = directoryPath + "\\EonEAC.rar";
            //      string pakFilePath1 = directoryPath + "\\pakchunkLuxury-WindowsClient.pak";
            //    string sigFilePath1 = directoryPath + "\\pakchunkLuxury-WindowsClient.sig";

            if (Directory.Exists(directory))
            {
                if (!Directory.Exists(directory + "\\FortniteGame") || !Directory.Exists(directory + "\\Engine"))
                {
                    System.Windows.MessageBox.Show("FortniteGame and/or Engine not found. Make sure it's the valid directory.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool fileExists = false;

                    // Remove dll
                    if (File.Exists(directory + "\\LuxuryServer.dll"))
                    {
                        File.Delete(directory + "\\LuxuryServer.dll");
                        fileExists = true;
                    }

                    // Remove pak files
                    //        if (File.Exists(pakFilePath1))
                    //  {
                    //        File.Delete(pakFilePath1);
                    //          fileExists = true;
                    //          }

                    //     if (File.Exists(sigFilePath1))
                    //        {
                    //       File.Delete(sigFilePath1);
                    //            fileExists = true;
                    //           }

                    // Remove rar
                    if (File.Exists(RarPath))
                    {
                        File.Delete(RarPath);
                        fileExists = true;
                    }

                    //Downloading Original Splash

                    if (!File.Exists(SplashPath))
                    {
                        DownloadFile("https://cdn.discordapp.com/attachments/814917879458824236/1190676471807754312/Splash.bmp", SplashPath);
                        fileExists = true;
                    }

                    if (!fileExists)
                    {
                        System.Windows.MessageBox.Show("There aren't Eon files in v12.41 folder.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Eon files have been successfully removed from the v12.41 folder!", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Directory not found.", "Eon Launcher", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Darkbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Darkbox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ExtractRAR(string rarFilePath, string extractionPath)
        {
            using (var archive = RarArchive.Open(rarFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(extractionPath, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });

                    }
                }
            }
        }

    }
}
