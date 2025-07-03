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
            var bestItem = items.OrderByDescending(i => i.Score).FirstOrDefault();

            if (bestItem == null)
            {
                MessageBox.Show("No item available to download.");
                return;
            }

            string tempPath = Path.GetTempPath();
            string fileName = Path.GetFileName(bestItem.FileURL);
            string fullPath = Path.Combine(tempPath, fileName);

            if (File.Exists(fullPath))
            {
                MessageBox.Show($"File already downloaded:\n{fullPath}");
                Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
                return;
            }

            try
            {
                await DownloadFileWithProgress(bestItem.FileURL, fullPath);

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

        private async Task DownloadFileWithProgress(string url, string destinationFilePath)
        {
            using HttpClient client = new HttpClient();
            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var canReportProgress = totalBytes != -1;

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None);

            var buffer = new byte[8192];
            long totalRead = 0;
            int read;

            progressBar.Invoke((Action)(() =>
            {
                progressBar.Value = 0;
                progressBar.Visible = true;
                progressBar.Maximum = 100;
            }));

            while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                totalRead += read;

                if (canReportProgress)
                {
                    int progress = (int)((totalRead * 100L) / totalBytes);
                    progressBar.Invoke((Action)(() =>
                    {
                        progressBar.Value = progress;
                    }));
                }
            }

            progressBar.Invoke((Action)(() =>
            {
                progressBar.Visible = false;
            }));
        }

    }
}
