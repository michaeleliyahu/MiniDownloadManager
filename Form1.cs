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
                var bestItem = items.OrderByDescending(i => i.Score).FirstOrDefault();

                if (bestItem != null)
                {
                    labelTitle.Text = bestItem.Title;
                    pictureBoxImage.Load(bestItem.ImageURL);
                }
                else
                {
                    labelTitle.Text = "No items available";
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

                MessageBox.Show("Download complete!");

                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
                Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during download: " + ex.Message);
            }
        }
    }
}
