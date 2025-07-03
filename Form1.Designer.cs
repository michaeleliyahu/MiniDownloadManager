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
            buttonDownload = new Button();
            labelTitle = new Label();
            pictureBoxImage = new PictureBox();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // buttonDownload
            // 
            buttonDownload.Location = new Point(117, 113);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(75, 23);
            buttonDownload.TabIndex = 0;
            buttonDownload.Text = "Download";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Location = new Point(426, 201);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(38, 15);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "Waiting for data...";
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Location = new Point(220, 256);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(100, 50);
            pictureBoxImage.TabIndex = 2;
            pictureBoxImage.TabStop = false;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(227, 181);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(100, 23);
            progressBar.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar);
            Controls.Add(pictureBoxImage);
            Controls.Add(labelTitle);
            Controls.Add(buttonDownload);
            Name = "Form1";
            Text = "Form1";
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
    }


}
