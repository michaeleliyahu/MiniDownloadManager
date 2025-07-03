using MiniDownloadManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MiniDownloadManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var items = await FetchDownloadItemsAsync();

                var filteredItems = items.Where(i => IsValid(i)).ToList();

                var bestItem = filteredItems.OrderByDescending(i => i.Score).FirstOrDefault();

                if (bestItem != null)
                {
                    labelFileTitle.Text = bestItem.Title;
                    pictureBoxImage.Load(bestItem.ImageURL);
                }
                else
                {
                    labelTitle.Text = "No compatible items available";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message);
            }
        }
        public async Task<List<DownloadItem>> FetchDownloadItemsAsync()
        {
            string url = "https://4qgz7zu7l5um367pzultcpbhmm0thhhg.lambda-url.us-west-2.on.aws/";

            using HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<DownloadItem> items = JsonSerializer.Deserialize<List<DownloadItem>>(json, options);
            return items;
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            var items = await FetchDownloadItemsAsync();

            // סינון לפי Validators
            var filteredItems = items.Where(i => IsValid(i)).ToList();

            var bestItem = filteredItems.OrderByDescending(i => i.Score).FirstOrDefault();

            if (bestItem == null)
            {
                MessageBox.Show("No compatible item available to download.");
                return;
            }

            try
            {
                string tempPath = Path.GetTempPath();
                string fileName = Path.GetFileName(bestItem.FileURL);
                string fullPath = Path.Combine(tempPath, fileName);

                if (File.Exists(fullPath))
                {
                    MessageBox.Show($"File already downloaded:\n{fullPath}");
                    Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
                    return;
                }

                using HttpClient client = new HttpClient();
                using var response = await client.GetAsync(bestItem.FileURL, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await stream.CopyToAsync(fs);
                }

                MessageBox.Show("Download complete!");

                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
                Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during download: " + ex.Message);
            }
        }
        private bool IsValid(DownloadItem item)
        {
            if (item.Validators == null)
                return true; // אין מגבלות, מתאים אוטומטית

            // 1. בדיקת RAM
            if (item.Validators.Ram.HasValue)
            {
                var availableRamMb = GetAvailableRamInMB();
                if (availableRamMb < item.Validators.Ram.Value)
                    return false;
            }

            // 2. בדיקת OS
            if (!string.IsNullOrEmpty(item.Validators.Os))
            {
                var minVersion = new Version(item.Validators.Os);
                var currentVersion = GetOSVersion();
                if (currentVersion < minVersion)
                    return false;
            }

            // 3. בדיקת Disk (פנוי ב־temp)
            if (item.Validators.Disk.HasValue)
            {
                var freeBytes = GetFreeDiskSpaceInBytes(Path.GetTempPath());
                if (freeBytes < item.Validators.Disk.Value)
                    return false;
            }

            return true;
        }

        private int GetAvailableRamInMB()
        {
            // פשוט השיטה - מושך זיכרון פנוי במגה בייט:
            var availableBytes = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
            return (int)(availableBytes / (1024 * 1024));
        }

        private Version GetOSVersion()
        {
            return Environment.OSVersion.Version;
        }

        private long GetFreeDiskSpaceInBytes(string folderPath)
        {
            var drive = new DriveInfo(Path.GetPathRoot(folderPath));
            return drive.AvailableFreeSpace;
        }
    }
}
