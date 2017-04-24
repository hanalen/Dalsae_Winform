namespace TwitterClient
{
	partial class TweetControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TweetControl));
			this.tweetLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.dateLabel = new System.Windows.Forms.Label();
			this.retweetNameLabel = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.입력하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.이미지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.URLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolstripStripeURL = new System.Windows.Forms.ToolStripSeparator();
			this.답글ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.모두에게답글ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.사용자기능ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.유저타임라인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.리트윗끄기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.유저뮤트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.클라이언트뮤트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.해시태그ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hashStrip = new System.Windows.Forms.ToolStripSeparator();
			this.웹에서보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.리트윗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.인용하기QTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.쪽지DMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.관심글ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.복사ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gongsikPictureBox = new System.Windows.Forms.PictureBox();
			this.retweetProfilePicture = new System.Windows.Forms.PictureBox();
			this.profilePicture = new System.Windows.Forms.PictureBox();
			this.lockPictureBox = new System.Windows.Forms.PictureBox();
			this.textFav = new System.Windows.Forms.Label();
			this.textRT = new System.Windows.Forms.Label();
			this.replyLabel = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gongsikPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.retweetProfilePicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.profilePicture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lockPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// tweetLabel
			// 
			this.tweetLabel.AutoEllipsis = true;
			this.tweetLabel.AutoSize = true;
			this.tweetLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tweetLabel.Location = new System.Drawing.Point(60, 21);
			this.tweetLabel.Name = "tweetLabel";
			this.tweetLabel.Size = new System.Drawing.Size(464, 17);
			this.tweetLabel.TabIndex = 0;
			this.tweetLabel.Text = "AutoSize AutoSize AutoSize AutoSize AutoSize AutoSize AutoSize AutoSize ";
			this.tweetLabel.UseMnemonic = false;
			this.tweetLabel.Click += new System.EventHandler(this.TweetControl_Click);
			this.tweetLabel.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.tweetLabel.Leave += new System.EventHandler(this.TweetControl_Leave);
			this.tweetLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tweetControl_MouseClick);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.nameLabel.Location = new System.Drawing.Point(60, 6);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(50, 13);
			this.nameLabel.TabIndex = 2;
			this.nameLabel.Text = "label1";
			this.nameLabel.UseMnemonic = false;
			this.nameLabel.Click += new System.EventHandler(this.TweetControl_Click);
			this.nameLabel.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.nameLabel.Leave += new System.EventHandler(this.TweetControl_Leave);
			// 
			// dateLabel
			// 
			this.dateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new System.Drawing.Point(61, 79);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(60, 12);
			this.dateLabel.TabIndex = 3;
			this.dateLabel.Text = "dateLabel";
			this.dateLabel.UseMnemonic = false;
			this.dateLabel.Click += new System.EventHandler(this.TweetControl_Click);
			this.dateLabel.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.dateLabel.Leave += new System.EventHandler(this.TweetControl_Leave);
			// 
			// retweetNameLabel
			// 
			this.retweetNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.retweetNameLabel.AutoSize = true;
			this.retweetNameLabel.Location = new System.Drawing.Point(86, 53);
			this.retweetNameLabel.Name = "retweetNameLabel";
			this.retweetNameLabel.Size = new System.Drawing.Size(111, 12);
			this.retweetNameLabel.TabIndex = 5;
			this.retweetNameLabel.Text = "retweetNameLabel";
			this.retweetNameLabel.Click += new System.EventHandler(this.TweetControl_Click);
			this.retweetNameLabel.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.retweetNameLabel.Leave += new System.EventHandler(this.TweetControl_Leave);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.입력하기ToolStripMenuItem,
            this.toolStripSeparator4,
            this.이미지ToolStripMenuItem,
            this.URLToolStripMenuItem,
            this.toolstripStripeURL,
            this.답글ToolStripMenuItem,
            this.모두에게답글ToolStripMenuItem,
            this.toolStripSeparator6,
            this.사용자기능ToolStripMenuItem,
            this.toolStripSeparator1,
            this.해시태그ToolStripMenuItem,
            this.hashStrip,
            this.웹에서보기ToolStripMenuItem,
            this.리트윗ToolStripMenuItem,
            this.인용하기QTToolStripMenuItem,
            this.쪽지DMToolStripMenuItem,
            this.관심글ToolStripMenuItem,
            this.toolStripSeparator2,
            this.복사ToolStripMenuItem,
            this.삭제ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 370);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// 입력하기ToolStripMenuItem
			// 
			this.입력하기ToolStripMenuItem.Name = "입력하기ToolStripMenuItem";
			this.입력하기ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.입력하기ToolStripMenuItem.Text = "입력하기";
			this.입력하기ToolStripMenuItem.Click += new System.EventHandler(this.입력하기ToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
			// 
			// 이미지ToolStripMenuItem
			// 
			this.이미지ToolStripMenuItem.Name = "이미지ToolStripMenuItem";
			this.이미지ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.이미지ToolStripMenuItem.Text = "이미지";
			this.이미지ToolStripMenuItem.Click += new System.EventHandler(this.URL_MenuStripClick);
			// 
			// URLToolStripMenuItem
			// 
			this.URLToolStripMenuItem.Name = "URLToolStripMenuItem";
			this.URLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.URLToolStripMenuItem.Text = "URL";
			// 
			// toolstripStripeURL
			// 
			this.toolstripStripeURL.Name = "toolstripStripeURL";
			this.toolstripStripeURL.Size = new System.Drawing.Size(149, 6);
			// 
			// 답글ToolStripMenuItem
			// 
			this.답글ToolStripMenuItem.Name = "답글ToolStripMenuItem";
			this.답글ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.답글ToolStripMenuItem.Text = "답글";
			this.답글ToolStripMenuItem.Click += new System.EventHandler(this.답글ToolStripMenuItem_Click);
			// 
			// 모두에게답글ToolStripMenuItem
			// 
			this.모두에게답글ToolStripMenuItem.Name = "모두에게답글ToolStripMenuItem";
			this.모두에게답글ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.모두에게답글ToolStripMenuItem.Text = "모두에게 답글";
			this.모두에게답글ToolStripMenuItem.Click += new System.EventHandler(this.모두에게답글ToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
			// 
			// 사용자기능ToolStripMenuItem
			// 
			this.사용자기능ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.유저타임라인ToolStripMenuItem,
            this.리트윗끄기ToolStripMenuItem,
            this.유저뮤트ToolStripMenuItem,
            this.클라이언트뮤트ToolStripMenuItem});
			this.사용자기능ToolStripMenuItem.Name = "사용자기능ToolStripMenuItem";
			this.사용자기능ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.사용자기능ToolStripMenuItem.Text = "사용자 기능";
			// 
			// 유저타임라인ToolStripMenuItem
			// 
			this.유저타임라인ToolStripMenuItem.Name = "유저타임라인ToolStripMenuItem";
			this.유저타임라인ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.유저타임라인ToolStripMenuItem.Text = "유저 타임라인";
			// 
			// 리트윗끄기ToolStripMenuItem
			// 
			this.리트윗끄기ToolStripMenuItem.Name = "리트윗끄기ToolStripMenuItem";
			this.리트윗끄기ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.리트윗끄기ToolStripMenuItem.Text = "리트윗 끄기";
			this.리트윗끄기ToolStripMenuItem.Click += new System.EventHandler(this.리트윗끄기ToolStripMenuItem_Click_1);
			// 
			// 유저뮤트ToolStripMenuItem
			// 
			this.유저뮤트ToolStripMenuItem.Name = "유저뮤트ToolStripMenuItem";
			this.유저뮤트ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.유저뮤트ToolStripMenuItem.Text = "유저 뮤트";
			// 
			// 클라이언트뮤트ToolStripMenuItem
			// 
			this.클라이언트뮤트ToolStripMenuItem.Name = "클라이언트뮤트ToolStripMenuItem";
			this.클라이언트뮤트ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.클라이언트뮤트ToolStripMenuItem.Text = "클라이언트 뮤트";
			this.클라이언트뮤트ToolStripMenuItem.Click += new System.EventHandler(this.클라이언트뮤트ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// 해시태그ToolStripMenuItem
			// 
			this.해시태그ToolStripMenuItem.Name = "해시태그ToolStripMenuItem";
			this.해시태그ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.해시태그ToolStripMenuItem.Text = "해시태그";
			// 
			// hashStrip
			// 
			this.hashStrip.Name = "hashStrip";
			this.hashStrip.Size = new System.Drawing.Size(149, 6);
			// 
			// 웹에서보기ToolStripMenuItem
			// 
			this.웹에서보기ToolStripMenuItem.Name = "웹에서보기ToolStripMenuItem";
			this.웹에서보기ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.웹에서보기ToolStripMenuItem.Text = "웹에서 보기";
			this.웹에서보기ToolStripMenuItem.Click += new System.EventHandler(this.웹에서보기ToolStripMenuItem_Click);
			// 
			// 리트윗ToolStripMenuItem
			// 
			this.리트윗ToolStripMenuItem.Name = "리트윗ToolStripMenuItem";
			this.리트윗ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.리트윗ToolStripMenuItem.Text = "리트윗(RT)";
			this.리트윗ToolStripMenuItem.Click += new System.EventHandler(this.리트윗ToolStripMenuItem_Click);
			// 
			// 인용하기QTToolStripMenuItem
			// 
			this.인용하기QTToolStripMenuItem.Name = "인용하기QTToolStripMenuItem";
			this.인용하기QTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.인용하기QTToolStripMenuItem.Text = "인용하기(QT)";
			// 
			// 쪽지DMToolStripMenuItem
			// 
			this.쪽지DMToolStripMenuItem.Name = "쪽지DMToolStripMenuItem";
			this.쪽지DMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.쪽지DMToolStripMenuItem.Text = "쪽지(DM)";
			this.쪽지DMToolStripMenuItem.Click += new System.EventHandler(this.쪽지DMToolStripMenuItem_Click);
			// 
			// 관심글ToolStripMenuItem
			// 
			this.관심글ToolStripMenuItem.Name = "관심글ToolStripMenuItem";
			this.관심글ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.관심글ToolStripMenuItem.Text = "관심글";
			this.관심글ToolStripMenuItem.Click += new System.EventHandler(this.관심글ToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// 복사ToolStripMenuItem
			// 
			this.복사ToolStripMenuItem.Name = "복사ToolStripMenuItem";
			this.복사ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.복사ToolStripMenuItem.Text = "복사";
			this.복사ToolStripMenuItem.Click += new System.EventHandler(this.복사ToolStripMenuItem_Click);
			// 
			// 삭제ToolStripMenuItem
			// 
			this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
			this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.삭제ToolStripMenuItem.Text = "삭제";
			this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
			// 
			// gongsikPictureBox
			// 
			this.gongsikPictureBox.BackColor = System.Drawing.Color.Transparent;
			this.gongsikPictureBox.Image = global::TwitterClient.Properties.Resources.gongSik_Small;
			this.gongsikPictureBox.Location = new System.Drawing.Point(43, 30);
			this.gongsikPictureBox.Name = "gongsikPictureBox";
			this.gongsikPictureBox.Size = new System.Drawing.Size(16, 16);
			this.gongsikPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.gongsikPictureBox.TabIndex = 7;
			this.gongsikPictureBox.TabStop = false;
			// 
			// retweetProfilePicture
			// 
			this.retweetProfilePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.retweetProfilePicture.BackColor = System.Drawing.Color.Transparent;
			this.retweetProfilePicture.Location = new System.Drawing.Point(60, 53);
			this.retweetProfilePicture.Name = "retweetProfilePicture";
			this.retweetProfilePicture.Size = new System.Drawing.Size(24, 24);
			this.retweetProfilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.retweetProfilePicture.TabIndex = 4;
			this.retweetProfilePicture.TabStop = false;
			this.retweetProfilePicture.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.retweetProfilePicture.Leave += new System.EventHandler(this.TweetControl_Leave);
			this.retweetProfilePicture.Click += new System.EventHandler(this.TweetControl_Click);
			// 
			// profilePicture
			// 
			this.profilePicture.BackColor = System.Drawing.Color.Transparent;
			this.profilePicture.Location = new System.Drawing.Point(10, 6);
			this.profilePicture.Name = "profilePicture";
			this.profilePicture.Size = new System.Drawing.Size(42, 42);
			this.profilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.profilePicture.TabIndex = 1;
			this.profilePicture.TabStop = false;
			this.profilePicture.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.profilePicture.Leave += new System.EventHandler(this.TweetControl_Leave);
			this.profilePicture.Click += new System.EventHandler(this.TweetControl_Click);
			this.profilePicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tweetControl_MouseClick);
			// 
			// lockPictureBox
			// 
			this.lockPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lockPictureBox.BackColor = System.Drawing.Color.Transparent;
			this.lockPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("lockPictureBox.Image")));
			this.lockPictureBox.Location = new System.Drawing.Point(464, 4);
			this.lockPictureBox.Name = "lockPictureBox";
			this.lockPictureBox.Size = new System.Drawing.Size(16, 18);
			this.lockPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.lockPictureBox.TabIndex = 8;
			this.lockPictureBox.TabStop = false;
			// 
			// textFav
			// 
			this.textFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textFav.AutoSize = true;
			this.textFav.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textFav.ForeColor = System.Drawing.Color.Red;
			this.textFav.Location = new System.Drawing.Point(464, 68);
			this.textFav.Name = "textFav";
			this.textFav.Size = new System.Drawing.Size(19, 15);
			this.textFav.TabIndex = 9;
			this.textFav.Text = "♥";
			// 
			// textRT
			// 
			this.textRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textRT.AutoSize = true;
			this.textRT.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textRT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.textRT.Location = new System.Drawing.Point(437, 70);
			this.textRT.Name = "textRT";
			this.textRT.Size = new System.Drawing.Size(23, 12);
			this.textRT.TabIndex = 10;
			this.textRT.Text = "RT";
			// 
			// replyLabel
			// 
			this.replyLabel.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.replyLabel.Location = new System.Drawing.Point(28, 49);
			this.replyLabel.Name = "replyLabel";
			this.replyLabel.Size = new System.Drawing.Size(20, 20);
			this.replyLabel.TabIndex = 11;
			this.replyLabel.Text = "↪";
			this.replyLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.reply_MouseClick);
			// 
			// TweetControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Controls.Add(this.replyLabel);
			this.Controls.Add(this.textRT);
			this.Controls.Add(this.textFav);
			this.Controls.Add(this.lockPictureBox);
			this.Controls.Add(this.gongsikPictureBox);
			this.Controls.Add(this.retweetNameLabel);
			this.Controls.Add(this.retweetProfilePicture);
			this.Controls.Add(this.dateLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.profilePicture);
			this.Controls.Add(this.tweetLabel);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TweetControl";
			this.Size = new System.Drawing.Size(500, 94);
			this.Click += new System.EventHandler(this.TweetControl_Click);
			this.Enter += new System.EventHandler(this.TweetControl_Enter);
			this.Leave += new System.EventHandler(this.TweetControl_Leave);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gongsikPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.retweetProfilePicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.profilePicture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lockPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		

		#endregion

		private System.Windows.Forms.Label tweetLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label dateLabel;
		private System.Windows.Forms.PictureBox retweetProfilePicture;
		private System.Windows.Forms.Label retweetNameLabel;
		private System.Windows.Forms.PictureBox profilePicture;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem URLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 리트윗ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 쪽지DMToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 관심글ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 복사ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.PictureBox gongsikPictureBox;
		private System.Windows.Forms.PictureBox lockPictureBox;
		private System.Windows.Forms.Label textFav;
		private System.Windows.Forms.Label textRT;
		private System.Windows.Forms.ToolStripMenuItem 웹에서보기ToolStripMenuItem;
		private System.Windows.Forms.Label replyLabel;
		private System.Windows.Forms.ToolStripMenuItem 입력하기ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolstripStripeURL;
		private System.Windows.Forms.ToolStripMenuItem 답글ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 모두에게답글ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem 사용자기능ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 리트윗끄기ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 유저타임라인ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 인용하기QTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 유저뮤트ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 클라이언트뮤트ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 이미지ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 해시태그ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator hashStrip;
	}
}
