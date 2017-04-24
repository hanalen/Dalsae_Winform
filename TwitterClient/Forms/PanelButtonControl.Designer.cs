namespace TwitterClient.Forms
{
	partial class PanelButtonControl
	{
		/// <summary> 
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelButtonControl));
			this.homeLabel = new System.Windows.Forms.Label();
			this.mentionLabel = new System.Windows.Forms.Label();
			this.dmLabel = new System.Windows.Forms.Label();
			this.favLabel = new System.Windows.Forms.Label();
			this.urlLabel = new System.Windows.Forms.Label();
			this.urlPicture = new System.Windows.Forms.PictureBox();
			this.favPicture = new System.Windows.Forms.PictureBox();
			this.notiPicture = new System.Windows.Forms.PictureBox();
			this.homePicture = new System.Windows.Forms.PictureBox();
			this.dmPicture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.urlPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.favPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.notiPicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.homePicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dmPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// homeLabel
			// 
			this.homeLabel.AutoSize = true;
			this.homeLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.homeLabel.Location = new System.Drawing.Point(50, 10);
			this.homeLabel.Name = "homeLabel";
			this.homeLabel.Size = new System.Drawing.Size(21, 17);
			this.homeLabel.TabIndex = 3;
			this.homeLabel.Text = "홈";
			this.homeLabel.Click += new System.EventHandler(this.homeLabel_Click);
			// 
			// mentionLabel
			// 
			this.mentionLabel.AutoSize = true;
			this.mentionLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.mentionLabel.Location = new System.Drawing.Point(128, 10);
			this.mentionLabel.Name = "mentionLabel";
			this.mentionLabel.Size = new System.Drawing.Size(34, 17);
			this.mentionLabel.TabIndex = 4;
			this.mentionLabel.Text = "알림";
			this.mentionLabel.Click += new System.EventHandler(this.mentionLabel_Click);
			// 
			// dmLabel
			// 
			this.dmLabel.AutoSize = true;
			this.dmLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.dmLabel.Location = new System.Drawing.Point(211, 10);
			this.dmLabel.Name = "dmLabel";
			this.dmLabel.Size = new System.Drawing.Size(34, 17);
			this.dmLabel.TabIndex = 5;
			this.dmLabel.Text = "쪽지";
			this.dmLabel.Click += new System.EventHandler(this.dmLabel_Click);
			// 
			// favLabel
			// 
			this.favLabel.AutoSize = true;
			this.favLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.favLabel.Location = new System.Drawing.Point(290, 10);
			this.favLabel.Name = "favLabel";
			this.favLabel.Size = new System.Drawing.Size(47, 17);
			this.favLabel.TabIndex = 7;
			this.favLabel.Text = "관심글";
			this.favLabel.Click += new System.EventHandler(this.favLabel_Click);
			// 
			// urlLabel
			// 
			this.urlLabel.AutoSize = true;
			this.urlLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.urlLabel.Location = new System.Drawing.Point(380, 3);
			this.urlLabel.Name = "urlLabel";
			this.urlLabel.Size = new System.Drawing.Size(52, 34);
			this.urlLabel.TabIndex = 9;
			this.urlLabel.Text = "최근 연\r\n  링크";
			this.urlLabel.Click += new System.EventHandler(this.urlLabel_Click);
			// 
			// urlPicture
			// 
			this.urlPicture.BackColor = System.Drawing.Color.Transparent;
			this.urlPicture.Image = ((System.Drawing.Image)(resources.GetObject("urlPicture.Image")));
			this.urlPicture.Location = new System.Drawing.Point(338, 3);
			this.urlPicture.Name = "urlPicture";
			this.urlPicture.Size = new System.Drawing.Size(36, 32);
			this.urlPicture.TabIndex = 8;
			this.urlPicture.TabStop = false;
			this.urlPicture.Click += new System.EventHandler(this.urlPicture_Click);
			// 
			// favPicture
			// 
			this.favPicture.BackColor = System.Drawing.Color.Transparent;
			this.favPicture.Image = ((System.Drawing.Image)(resources.GetObject("favPicture.Image")));
			this.favPicture.Location = new System.Drawing.Point(248, 3);
			this.favPicture.Name = "favPicture";
			this.favPicture.Size = new System.Drawing.Size(36, 32);
			this.favPicture.TabIndex = 6;
			this.favPicture.TabStop = false;
			this.favPicture.Click += new System.EventHandler(this.favPicture_Click);
			// 
			// notiPicture
			// 
			this.notiPicture.BackColor = System.Drawing.Color.Transparent;
			this.notiPicture.Image = global::TwitterClient.Properties.Resources.noti_icon;
			this.notiPicture.Location = new System.Drawing.Point(87, 3);
			this.notiPicture.Name = "notiPicture";
			this.notiPicture.Size = new System.Drawing.Size(36, 32);
			this.notiPicture.TabIndex = 2;
			this.notiPicture.TabStop = false;
			this.notiPicture.Click += new System.EventHandler(this.notiPicture_Click);
			// 
			// homePicture
			// 
			this.homePicture.BackColor = System.Drawing.Color.Transparent;
			this.homePicture.Image = global::TwitterClient.Properties.Resources.home_icon;
			this.homePicture.Location = new System.Drawing.Point(3, 3);
			this.homePicture.Name = "homePicture";
			this.homePicture.Size = new System.Drawing.Size(36, 32);
			this.homePicture.TabIndex = 1;
			this.homePicture.TabStop = false;
			this.homePicture.Click += new System.EventHandler(this.homePicture_Click);
			// 
			// dmPicture
			// 
			this.dmPicture.BackColor = System.Drawing.Color.Transparent;
			this.dmPicture.Image = global::TwitterClient.Properties.Resources.dm_icon;
			this.dmPicture.Location = new System.Drawing.Point(172, 3);
			this.dmPicture.Name = "dmPicture";
			this.dmPicture.Size = new System.Drawing.Size(36, 32);
			this.dmPicture.TabIndex = 0;
			this.dmPicture.TabStop = false;
			this.dmPicture.Click += new System.EventHandler(this.dmPicture_Click);
			// 
			// PanelButtonControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Controls.Add(this.urlLabel);
			this.Controls.Add(this.urlPicture);
			this.Controls.Add(this.favLabel);
			this.Controls.Add(this.favPicture);
			this.Controls.Add(this.dmLabel);
			this.Controls.Add(this.mentionLabel);
			this.Controls.Add(this.homeLabel);
			this.Controls.Add(this.notiPicture);
			this.Controls.Add(this.homePicture);
			this.Controls.Add(this.dmPicture);
			this.Name = "PanelButtonControl";
			this.Size = new System.Drawing.Size(544, 38);
			((System.ComponentModel.ISupportInitialize)(this.urlPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.favPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.notiPicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.homePicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dmPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox dmPicture;
		private System.Windows.Forms.PictureBox homePicture;
		private System.Windows.Forms.PictureBox notiPicture;
		private System.Windows.Forms.Label homeLabel;
		private System.Windows.Forms.Label mentionLabel;
		private System.Windows.Forms.Label dmLabel;
		private System.Windows.Forms.PictureBox favPicture;
		private System.Windows.Forms.Label favLabel;
		private System.Windows.Forms.PictureBox urlPicture;
		private System.Windows.Forms.Label urlLabel;
	}
}
