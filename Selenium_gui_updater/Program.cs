using System.Diagnostics;
using System.Net;
using System.IO.Compression;

namespace Selenium_gui_updater {
    class Program {
        static int Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine(@"Error: no path");
                return -1;
            } 
            var path = args[0];
            // Try to download latest zip from repo
            try {
                Process.Start(@"taskkill -f -im selenium_gui_winform.exe");
                File.Delete(path + @"\selenium_gui_winform.exe");
                File.Delete(path + @"\selenium_gui_winform.dll.config");
               
                Console.WriteLine(@"INFO: Start downloading latest release");
                WebClient wc = new WebClient();
                wc.DownloadFile(
                    "https://github.com/ksj-10th-a09/sqli-detection-gui/releases/latest/download/selenium_gui_Win32.zip",
                    path + @"\selenium_gui_Win32.zip");
            }
            catch (Exception ex) {
                Console.WriteLine(@"ERR: {ex}", ex);
                Console.WriteLine(@"Please Check your internet connection");
                return -1;
            }

            if (!File.Exists(path + @"selenium_gui_Win32.zip")) return -1;
            
            // Try to unzip what download from repo
            try {
                string zipFile = path + @"\selenium_gui_Win32.zip";

                ZipFile.ExtractToDirectory(zipFile, path);
            }
            catch (IOException ex) { Console.WriteLine(@"ERR: IO Error, " + ex); }
            catch (UnauthorizedAccessException) { Console.WriteLine(@"ERR: Can't access to " + path); }

            Process.Start(path + @"\selenium_gui_winform.exe");
            return 0;
        }
    }
}