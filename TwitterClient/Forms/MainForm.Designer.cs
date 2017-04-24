using System.Windows.Forms;

namespace TwitterClient
{
	partial class MainForm
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

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tweetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.이미지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gIF추가GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.홈HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.멘션RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.쪽지DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.내트윗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.관심글ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.팔로잉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.팔로워ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.유저타임라인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.없음ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.유저검색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.관심글저장도구ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.옵션OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.프로그램설정OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.단축키설정HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.아이디완성갱신NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.블락리스트갱신BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.계정선택AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.계정추가ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.현재계정삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.연결ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.프로그램정보HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.만든사람HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.inputTweet = new System.Windows.Forms.TextBox();
			this.tweetButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.mentionListBox = new System.Windows.Forms.ListBox();
			this.countLabel = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStrip_UserStream = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip_Panal = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.newMention = new System.Windows.Forms.Label();
			this.newDM = new System.Windows.Forms.Label();
			this.imageBox4 = new System.Windows.Forms.PictureBox();
			this.imageBox3 = new System.Windows.Forms.PictureBox();
			this.imageBox2 = new System.Windows.Forms.PictureBox();
			this.imageBox1 = new System.Windows.Forms.PictureBox();
			this.profilePictureBox = new System.Windows.Forms.PictureBox();
			this.panelButtonControl1 = new TwitterClient.Forms.PanelButtonControl();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tweetToolStripMenuItem,
            this.보기ToolStripMenuItem,
            this.옵션OToolStripMenuItem,
            this.프로그램정보HToolStripMenuItem});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(557, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tweetToolStripMenuItem
			// 
			this.tweetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.이미지ToolStripMenuItem,
            this.gIF추가GToolStripMenuItem});
			this.tweetToolStripMenuItem.Name = "tweetToolStripMenuItem";
			this.tweetToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			this.tweetToolStripMenuItem.Text = "업로드(&F)";
			// 
			// 이미지ToolStripMenuItem
			// 
			this.이미지ToolStripMenuItem.Name = "이미지ToolStripMenuItem";
			this.이미지ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.이미지ToolStripMenuItem.Text = "이미지 추가(&F)";
			this.이미지ToolStripMenuItem.Click += new System.EventHandler(this.이미지ToolStripMenuItem_Click);
			// 
			// gIF추가GToolStripMenuItem
			// 
			this.gIF추가GToolStripMenuItem.Name = "gIF추가GToolStripMenuItem";
			this.gIF추가GToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.gIF추가GToolStripMenuItem.Text = "GIF 추가(&G)";
			this.gIF추가GToolStripMenuItem.Click += new System.EventHandler(this.gIF추가GToolStripMenuItem_Click);
			// 
			// 보기ToolStripMenuItem
			// 
			this.보기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.홈HToolStripMenuItem,
            this.멘션RToolStripMenuItem,
            this.쪽지DToolStripMenuItem,
            this.toolStripSeparator4,
            this.내트윗ToolStripMenuItem,
            this.관심글ToolStripMenuItem,
            this.toolStripSeparator1,
            this.팔로잉ToolStripMenuItem,
            this.팔로워ToolStripMenuItem,
            this.toolStripSeparator2,
            this.유저타임라인ToolStripMenuItem,
            this.유저검색ToolStripMenuItem,
            this.toolStripSeparator9,
            this.관심글저장도구ToolStripMenuItem});
			this.보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
			this.보기ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.보기ToolStripMenuItem.Text = "보기(&V)";
			// 
			// 홈HToolStripMenuItem
			// 
			this.홈HToolStripMenuItem.Name = "홈HToolStripMenuItem";
			this.홈HToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.홈HToolStripMenuItem.Text = "홈(&H)";
			this.홈HToolStripMenuItem.Click += new System.EventHandler(this.홈HToolStripMenuItem_Click);
			// 
			// 멘션RToolStripMenuItem
			// 
			this.멘션RToolStripMenuItem.Name = "멘션RToolStripMenuItem";
			this.멘션RToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.멘션RToolStripMenuItem.Text = "알림(&R)";
			this.멘션RToolStripMenuItem.Click += new System.EventHandler(this.멘션RToolStripMenuItem_Click);
			// 
			// 쪽지DToolStripMenuItem
			// 
			this.쪽지DToolStripMenuItem.Name = "쪽지DToolStripMenuItem";
			this.쪽지DToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.쪽지DToolStripMenuItem.Text = "쪽지(&D)";
			this.쪽지DToolStripMenuItem.Click += new System.EventHandler(this.쪽지DToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
			// 
			// 내트윗ToolStripMenuItem
			// 
			this.내트윗ToolStripMenuItem.Name = "내트윗ToolStripMenuItem";
			this.내트윗ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.내트윗ToolStripMenuItem.Text = "내 트윗(&G)";
			this.내트윗ToolStripMenuItem.Click += new System.EventHandler(this.내트윗ToolStripMenuItem_Click);
			// 
			// 관심글ToolStripMenuItem
			// 
			this.관심글ToolStripMenuItem.Name = "관심글ToolStripMenuItem";
			this.관심글ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.관심글ToolStripMenuItem.Text = "관심글(&F)";
			this.관심글ToolStripMenuItem.Click += new System.EventHandler(this.관심글ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
			// 
			// 팔로잉ToolStripMenuItem
			// 
			this.팔로잉ToolStripMenuItem.Enabled = false;
			this.팔로잉ToolStripMenuItem.Name = "팔로잉ToolStripMenuItem";
			this.팔로잉ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.팔로잉ToolStripMenuItem.Text = "팔로잉";
			// 
			// 팔로워ToolStripMenuItem
			// 
			this.팔로워ToolStripMenuItem.Enabled = false;
			this.팔로워ToolStripMenuItem.Name = "팔로워ToolStripMenuItem";
			this.팔로워ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.팔로워ToolStripMenuItem.Text = "팔로워";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
			// 
			// 유저타임라인ToolStripMenuItem
			// 
			this.유저타임라인ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.없음ToolStripMenuItem});
			this.유저타임라인ToolStripMenuItem.Name = "유저타임라인ToolStripMenuItem";
			this.유저타임라인ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.유저타임라인ToolStripMenuItem.Text = "유저 타임라인";
			// 
			// 없음ToolStripMenuItem
			// 
			this.없음ToolStripMenuItem.Enabled = false;
			this.없음ToolStripMenuItem.Name = "없음ToolStripMenuItem";
			this.없음ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.없음ToolStripMenuItem.Text = "--검색한 유저목록--";
			// 
			// 유저검색ToolStripMenuItem
			// 
			this.유저검색ToolStripMenuItem.Name = "유저검색ToolStripMenuItem";
			this.유저검색ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.유저검색ToolStripMenuItem.Text = "유저 검색";
			this.유저검색ToolStripMenuItem.Click += new System.EventHandler(this.유저검색ToolStripMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(159, 6);
			// 
			// 관심글저장도구ToolStripMenuItem
			// 
			this.관심글저장도구ToolStripMenuItem.Name = "관심글저장도구ToolStripMenuItem";
			this.관심글저장도구ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.관심글저장도구ToolStripMenuItem.Text = "관심글 저장도구";
			this.관심글저장도구ToolStripMenuItem.Click += new System.EventHandler(this.관심글저장도구ToolStripMenuItem_Click);
			// 
			// 옵션OToolStripMenuItem
			// 
			this.옵션OToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.프로그램설정OToolStripMenuItem,
            this.단축키설정HToolStripMenuItem,
            this.toolStripSeparator3,
            this.아이디완성갱신NToolStripMenuItem,
            this.블락리스트갱신BToolStripMenuItem,
            this.toolStripSeparator5,
            this.계정선택AToolStripMenuItem,
            this.toolStripSeparator8,
            this.연결ToolStripMenuItem});
			this.옵션OToolStripMenuItem.Name = "옵션OToolStripMenuItem";
			this.옵션OToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.옵션OToolStripMenuItem.Text = "옵션(&O)";
			// 
			// 프로그램설정OToolStripMenuItem
			// 
			this.프로그램설정OToolStripMenuItem.Name = "프로그램설정OToolStripMenuItem";
			this.프로그램설정OToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.프로그램설정OToolStripMenuItem.Text = "프로그램 설정(&O)";
			this.프로그램설정OToolStripMenuItem.Click += new System.EventHandler(this.프로그램설정OToolStripMenuItem_Click);
			// 
			// 단축키설정HToolStripMenuItem
			// 
			this.단축키설정HToolStripMenuItem.Name = "단축키설정HToolStripMenuItem";
			this.단축키설정HToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.단축키설정HToolStripMenuItem.Text = "단축키 설정(&H)";
			this.단축키설정HToolStripMenuItem.Click += new System.EventHandler(this.단축키설정HToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
			// 
			// 아이디완성갱신NToolStripMenuItem
			// 
			this.아이디완성갱신NToolStripMenuItem.Name = "아이디완성갱신NToolStripMenuItem";
			this.아이디완성갱신NToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.아이디완성갱신NToolStripMenuItem.Text = "아이디 완성 갱신(&N)";
			this.아이디완성갱신NToolStripMenuItem.Click += new System.EventHandler(this.아이디완성갱신NToolStripMenuItem_Click);
			// 
			// 블락리스트갱신BToolStripMenuItem
			// 
			this.블락리스트갱신BToolStripMenuItem.Name = "블락리스트갱신BToolStripMenuItem";
			this.블락리스트갱신BToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.블락리스트갱신BToolStripMenuItem.Text = "블락 리스트 갱신(&B)";
			this.블락리스트갱신BToolStripMenuItem.Click += new System.EventHandler(this.블락리스트갱신BToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
			// 
			// 계정선택AToolStripMenuItem
			// 
			this.계정선택AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.계정추가ToolStripMenuItem,
            this.현재계정삭제ToolStripMenuItem,
            this.toolStripSeparator10});
			this.계정선택AToolStripMenuItem.Name = "계정선택AToolStripMenuItem";
			this.계정선택AToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.계정선택AToolStripMenuItem.Text = "계정 선택(&A)";
			// 
			// 계정추가ToolStripMenuItem
			// 
			this.계정추가ToolStripMenuItem.Name = "계정추가ToolStripMenuItem";
			this.계정추가ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.계정추가ToolStripMenuItem.Text = "계정 추가";
			this.계정추가ToolStripMenuItem.Click += new System.EventHandler(this.계정추가ToolStripMenuItem_Click);
			// 
			// 현재계정삭제ToolStripMenuItem
			// 
			this.현재계정삭제ToolStripMenuItem.Name = "현재계정삭제ToolStripMenuItem";
			this.현재계정삭제ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.현재계정삭제ToolStripMenuItem.Text = "현재 계정 삭제";
			this.현재계정삭제ToolStripMenuItem.Click += new System.EventHandler(this.현재계정삭제ToolStripMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(191, 6);
			// 
			// 연결ToolStripMenuItem
			// 
			this.연결ToolStripMenuItem.Name = "연결ToolStripMenuItem";
			this.연결ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.연결ToolStripMenuItem.Text = "유저 스트리밍 연결(&U)";
			// 
			// 프로그램정보HToolStripMenuItem
			// 
			this.프로그램정보HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.만든사람HToolStripMenuItem});
			this.프로그램정보HToolStripMenuItem.Name = "프로그램정보HToolStripMenuItem";
			this.프로그램정보HToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
			this.프로그램정보HToolStripMenuItem.Text = "프로그램 정보(&H)";
			// 
			// 만든사람HToolStripMenuItem
			// 
			this.만든사람HToolStripMenuItem.Name = "만든사람HToolStripMenuItem";
			this.만든사람HToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.만든사람HToolStripMenuItem.Text = "만든 사람(&H)";
			this.만든사람HToolStripMenuItem.Click += new System.EventHandler(this.만든사람HToolStripMenuItem_Click);
			// 
			// inputTweet
			// 
			this.inputTweet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputTweet.Location = new System.Drawing.Point(60, 34);
			this.inputTweet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.inputTweet.Multiline = true;
			this.inputTweet.Name = "inputTweet";
			this.inputTweet.Size = new System.Drawing.Size(485, 63);
			this.inputTweet.TabIndex = 2;
			this.inputTweet.TabStop = false;
			this.inputTweet.TextChanged += new System.EventHandler(this.inputTweet_TextChanged);
			this.inputTweet.Enter += new System.EventHandler(this.inputTweet_Enter);
			this.inputTweet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTweet_KeyDown);
			this.inputTweet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputTweet_KeyPress);
			this.inputTweet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputTweet_KeyUp);
			this.inputTweet.Leave += new System.EventHandler(this.inputTweet_Leave);
			this.inputTweet.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.inputTweet_KeyPreview);
			// 
			// tweetButton
			// 
			this.tweetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tweetButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tweetButton.Location = new System.Drawing.Point(459, 103);
			this.tweetButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tweetButton.Name = "tweetButton";
			this.tweetButton.Size = new System.Drawing.Size(90, 29);
			this.tweetButton.TabIndex = 3;
			this.tweetButton.TabStop = false;
			this.tweetButton.Text = "트윗 하기";
			this.tweetButton.UseVisualStyleBackColor = true;
			this.tweetButton.Click += new System.EventHandler(this.tweetButton_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel1.Location = new System.Drawing.Point(0, 138);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 543);
			this.panel1.TabIndex = 12;
			// 
			// mentionListBox
			// 
			this.mentionListBox.FormattingEnabled = true;
			this.mentionListBox.ItemHeight = 15;
			this.mentionListBox.Location = new System.Drawing.Point(58, 102);
			this.mentionListBox.Name = "mentionListBox";
			this.mentionListBox.Size = new System.Drawing.Size(246, 79);
			this.mentionListBox.TabIndex = 14;
			this.mentionListBox.TabStop = false;
			this.mentionListBox.DoubleClick += new System.EventHandler(this.doubleClick_mentionListBox);
			// 
			// countLabel
			// 
			this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.countLabel.AutoSize = true;
			this.countLabel.Location = new System.Drawing.Point(394, 111);
			this.countLabel.Name = "countLabel";
			this.countLabel.Size = new System.Drawing.Size(48, 15);
			this.countLabel.TabIndex = 13;
			this.countLabel.Text = "(0/140)";
			this.countLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_UserStream,
            this.toolStripSeparator6,
            this.toolStrip_Panal,
            this.toolStripSeparator7});
			this.toolStrip1.Location = new System.Drawing.Point(0, 719);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(557, 25);
			this.toolStrip1.TabIndex = 15;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStrip_UserStream
			// 
			this.toolStrip_UserStream.Name = "toolStrip_UserStream";
			this.toolStrip_UserStream.Size = new System.Drawing.Size(108, 22);
			this.toolStrip_UserStream.Text = "유저스트리밍(OFF)";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStrip_Panal
			// 
			this.toolStrip_Panal.Name = "toolStrip_Panal";
			this.toolStrip_Panal.Size = new System.Drawing.Size(55, 22);
			this.toolStrip_Panal.Text = "타임라인";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// newMention
			// 
			this.newMention.AutoSize = true;
			this.newMention.ForeColor = System.Drawing.SystemColors.ControlText;
			this.newMention.Location = new System.Drawing.Point(5, 91);
			this.newMention.Name = "newMention";
			this.newMention.Size = new System.Drawing.Size(50, 15);
			this.newMention.TabIndex = 16;
			this.newMention.Text = "새 알림!";
			// 
			// newDM
			// 
			this.newDM.AutoSize = true;
			this.newDM.Location = new System.Drawing.Point(5, 111);
			this.newDM.Name = "newDM";
			this.newDM.Size = new System.Drawing.Size(50, 15);
			this.newDM.TabIndex = 17;
			this.newDM.Text = "새 쪽지!";
			// 
			// imageBox4
			// 
			this.imageBox4.BackColor = System.Drawing.Color.Transparent;
			this.imageBox4.Location = new System.Drawing.Point(170, 102);
			this.imageBox4.Name = "imageBox4";
			this.imageBox4.Size = new System.Drawing.Size(30, 30);
			this.imageBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBox4.TabIndex = 11;
			this.imageBox4.TabStop = false;
			this.imageBox4.Click += new System.EventHandler(this.imageBox_Click);
			// 
			// imageBox3
			// 
			this.imageBox3.BackColor = System.Drawing.Color.Transparent;
			this.imageBox3.Location = new System.Drawing.Point(133, 102);
			this.imageBox3.Name = "imageBox3";
			this.imageBox3.Size = new System.Drawing.Size(30, 30);
			this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBox3.TabIndex = 10;
			this.imageBox3.TabStop = false;
			this.imageBox3.Click += new System.EventHandler(this.imageBox_Click);
			// 
			// imageBox2
			// 
			this.imageBox2.BackColor = System.Drawing.Color.Transparent;
			this.imageBox2.Location = new System.Drawing.Point(96, 102);
			this.imageBox2.Name = "imageBox2";
			this.imageBox2.Size = new System.Drawing.Size(30, 30);
			this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBox2.TabIndex = 9;
			this.imageBox2.TabStop = false;
			this.imageBox2.Click += new System.EventHandler(this.imageBox_Click);
			// 
			// imageBox1
			// 
			this.imageBox1.BackColor = System.Drawing.Color.Transparent;
			this.imageBox1.Location = new System.Drawing.Point(60, 102);
			this.imageBox1.Name = "imageBox1";
			this.imageBox1.Size = new System.Drawing.Size(30, 30);
			this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBox1.TabIndex = 8;
			this.imageBox1.TabStop = false;
			this.imageBox1.Click += new System.EventHandler(this.imageBox_Click);
			// 
			// profilePictureBox
			// 
			this.profilePictureBox.BackColor = System.Drawing.Color.Transparent;
			this.profilePictureBox.Location = new System.Drawing.Point(5, 34);
			this.profilePictureBox.Name = "profilePictureBox";
			this.profilePictureBox.Size = new System.Drawing.Size(50, 50);
			this.profilePictureBox.TabIndex = 7;
			this.profilePictureBox.TabStop = false;
			// 
			// panelButtonControl1
			// 
			this.panelButtonControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelButtonControl1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.panelButtonControl1.Location = new System.Drawing.Point(0, 681);
			this.panelButtonControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelButtonControl1.Name = "panelButtonControl1";
			this.panelButtonControl1.Size = new System.Drawing.Size(557, 38);
			this.panelButtonControl1.TabIndex = 0;
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(151, 6);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.ClientSize = new System.Drawing.Size(557, 744);
			this.Controls.Add(this.panelButtonControl1);
			this.Controls.Add(this.mentionListBox);
			this.Controls.Add(this.newDM);
			this.Controls.Add(this.newMention);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.countLabel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.imageBox4);
			this.Controls.Add(this.imageBox3);
			this.Controls.Add(this.imageBox2);
			this.Controls.Add(this.imageBox1);
			this.Controls.Add(this.profilePictureBox);
			this.Controls.Add(this.tweetButton);
			this.Controls.Add(this.inputTweet);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Dalsae";
			this.Activated += new System.EventHandler(this.mainForm_Actived);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_Closing);
			this.Shown += new System.EventHandler(this.mainForm_Shown);
			this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Move += new System.EventHandler(this.mainForm_Move);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.mainForm_KeyPreview);
			this.Resize += new System.EventHandler(this.mainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tweetToolStripMenuItem;
		private System.Windows.Forms.TextBox inputTweet;
		private System.Windows.Forms.Button tweetButton;
		private System.Windows.Forms.ToolStripMenuItem 이미지ToolStripMenuItem;
		private PictureBox profilePictureBox;
		private PictureBox imageBox1;
		private PictureBox imageBox2;
		private PictureBox imageBox3;
		private PictureBox imageBox4;
		private Panel panel1;
		private Label countLabel;
		private ToolStripMenuItem 보기ToolStripMenuItem;
		private ToolStripMenuItem 내트윗ToolStripMenuItem;
		private ToolStripMenuItem 관심글ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem 팔로잉ToolStripMenuItem;
		private ToolStripMenuItem 팔로워ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		public ToolStripMenuItem 유저타임라인ToolStripMenuItem;
		private ToolStripMenuItem 유저검색ToolStripMenuItem;
		private ToolStripMenuItem 옵션OToolStripMenuItem;
		private ToolStripMenuItem 프로그램설정OToolStripMenuItem;
		private ToolStripMenuItem 단축키설정HToolStripMenuItem;
		private ToolStripMenuItem 프로그램정보HToolStripMenuItem;
		private ToolStripMenuItem 만든사람HToolStripMenuItem;
		private ToolStripMenuItem 아이디완성갱신NToolStripMenuItem;
		private ToolStripMenuItem 블락리스트갱신BToolStripMenuItem;
		private ListBox mentionListBox;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStrip toolStrip1;
		private ToolStripLabel toolStrip_UserStream;
		private ToolStripMenuItem 없음ToolStripMenuItem;
		private ToolStripLabel toolStrip_Panal;
		private ToolStripMenuItem 멘션RToolStripMenuItem;
		private ToolStripMenuItem 쪽지DToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem 계정선택AToolStripMenuItem;
		private ToolStripMenuItem 홈HToolStripMenuItem;
		private ToolStripMenuItem 계정추가ToolStripMenuItem;
		private Label newMention;
		private Label newDM;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripSeparator toolStripSeparator7;
		private Forms.PanelButtonControl panelButtonControl1;
		private ToolStripMenuItem gIF추가GToolStripMenuItem;
		private ToolStripMenuItem 연결ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator8;
		private ToolStripSeparator toolStripSeparator9;
		private ToolStripMenuItem 관심글저장도구ToolStripMenuItem;
		private ToolStripMenuItem 현재계정삭제ToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator10;
	}
}

