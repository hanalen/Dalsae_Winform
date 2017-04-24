namespace TwitterClient.Forms
{
	partial class FavoriteForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoriteForm));
			this.label1 = new System.Windows.Forms.Label();
			this.startButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.unfavSaveButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.prevButton = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.countLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.imageLabel = new System.Windows.Forms.ToolStripLabel();
			this.unfavButton = new System.Windows.Forms.Button();
			this.favButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(4, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(498, 153);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(211, 228);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "시작하기";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(0, 174);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(250, 250);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(256, 174);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(250, 250);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Visible = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(0, 430);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(250, 250);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 4;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Visible = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Location = new System.Drawing.Point(256, 430);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(250, 250);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox4.TabIndex = 5;
			this.pictureBox4.TabStop = false;
			this.pictureBox4.Visible = false;
			// 
			// unfavSaveButton
			// 
			this.unfavSaveButton.AutoSize = true;
			this.unfavSaveButton.Location = new System.Drawing.Point(379, 686);
			this.unfavSaveButton.Name = "unfavSaveButton";
			this.unfavSaveButton.Size = new System.Drawing.Size(123, 40);
			this.unfavSaveButton.TabIndex = 6;
			this.unfavSaveButton.Text = "저장 후 관심글 해제";
			this.unfavSaveButton.UseVisualStyleBackColor = true;
			this.unfavSaveButton.Visible = false;
			this.unfavSaveButton.Click += new System.EventHandler(this.unfavSaveButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.AutoSize = true;
			this.saveButton.Location = new System.Drawing.Point(294, 686);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(79, 40);
			this.saveButton.TabIndex = 7;
			this.saveButton.Text = "저장 하기";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Visible = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(389, 776);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(75, 40);
			this.nextButton.TabIndex = 8;
			this.nextButton.Text = "다음";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Visible = false;
			this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
			// 
			// prevButton
			// 
			this.prevButton.Location = new System.Drawing.Point(35, 776);
			this.prevButton.Name = "prevButton";
			this.prevButton.Size = new System.Drawing.Size(75, 40);
			this.prevButton.TabIndex = 9;
			this.prevButton.Text = "이전";
			this.prevButton.UseVisualStyleBackColor = true;
			this.prevButton.Visible = false;
			this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripSeparator1,
            this.countLabel,
            this.toolStripSeparator2,
            this.imageLabel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(514, 25);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(47, 22);
			this.statusLabel.Text = "대기 중";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// countLabel
			// 
			this.countLabel.Name = "countLabel";
			this.countLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// imageLabel
			// 
			this.imageLabel.Name = "imageLabel";
			this.imageLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// unfavButton
			// 
			this.unfavButton.AutoSize = true;
			this.unfavButton.Location = new System.Drawing.Point(109, 686);
			this.unfavButton.Name = "unfavButton";
			this.unfavButton.Size = new System.Drawing.Size(115, 40);
			this.unfavButton.TabIndex = 13;
			this.unfavButton.Text = "관심글만 해제하기";
			this.unfavButton.UseVisualStyleBackColor = true;
			this.unfavButton.Visible = false;
			this.unfavButton.Click += new System.EventHandler(this.unfavButton_Click);
			// 
			// favButton
			// 
			this.favButton.AutoSize = true;
			this.favButton.Location = new System.Drawing.Point(12, 686);
			this.favButton.Name = "favButton";
			this.favButton.Size = new System.Drawing.Size(91, 40);
			this.favButton.TabIndex = 14;
			this.favButton.Text = "관심글 재등록";
			this.favButton.UseVisualStyleBackColor = true;
			this.favButton.Visible = false;
			this.favButton.Click += new System.EventHandler(this.favButton_Click);
			// 
			// FavoriteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 840);
			this.Controls.Add(this.favButton);
			this.Controls.Add(this.unfavButton);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.prevButton);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.unfavSaveButton);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FavoriteForm";
			this.Text = "관심글 저장도구";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Button unfavSaveButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button prevButton;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel statusLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel countLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel imageLabel;
		private System.Windows.Forms.Button unfavButton;
		private System.Windows.Forms.Button favButton;
	}
}