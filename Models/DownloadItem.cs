using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDownloadManager.Models
{
    public class DownloadItem
    {
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public string? FileURL { get; set; }
        public int Score { get; set; }
        public Validators? Validators { get; set; }
    }
}
