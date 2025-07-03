namespace MiniDownloadManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonDownload = new Button();
            labelTitle = new Label();
            labelFileTitle = new Label();
            pictureBoxImage = new PictureBox();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();

            // labelFileTitle
            labelFileTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelFileTitle.Location = new Point(0, 90);
            labelFileTitle.Name = "labelFileTitle";
            labelFileTitle.Size = new Size(800, 25);
            labelFileTitle.TabIndex = 4;
            labelFileTitle.Text = ""; // יוזן בזמן ריצה
            labelFileTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonDownload
            // 
            buttonDownload.Location = new Point(356, 320); // Centered horizontally
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(88, 30); // Slightly larger button
            buttonDownload.TabIndex = 0;
            buttonDownload.Text = "Download";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point); // Larger, bold, and underlined
            labelTitle.Location = new Point(0, 50); // Positioned near the top
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(800, 30);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "Mini-Download Manager";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter; // Center aligned text
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = (Image)resources.GetObject("pictureBoxImage.Image"); // Assuming the apple image is in resources
            pictureBoxImage.Location = new Point(300, 180); // Centered and positioned below "Apple"
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(200, 120); // Larger size for the image
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom; // To fit the image within bounds
            pictureBoxImage.TabIndex = 2;
            pictureBoxImage.TabStop = false;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(200, 370); // Positioned below the download button
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(400, 23); // Wider progress bar
            progressBar.TabIndex = 3;
            progressBar.Visible = false; // Initially invisible
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar);
            Controls.Add(labelFileTitle);
            Controls.Add(pictureBoxImage);
            Controls.Add(labelTitle);
            Controls.Add(buttonDownload);
            Name = "Form1";
            Text = "MainWindow"; // Changed title to match the image
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonDownload;
        private Label labelTitle;
        private PictureBox pictureBoxImage;
        private ProgressBar progressBar;
        private Label labelFileTitle;
    }
}