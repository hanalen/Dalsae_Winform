namespace TwitterClient
{
	partial class OptionForm
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
			this.checkMuteMention = new System.Windows.Forms.CheckBox();
			this.checkRetweetNoti = new System.Windows.Forms.CheckBox();
			this.checkShowRetweet = new System.Windows.Forms.CheckBox();
			this.checkShowCount = new System.Windows.Forms.CheckBox();
			this.checkYesNo = new System.Windows.Forms.CheckBox();
			this.checkSendEnter = new System.Windows.Forms.CheckBox();
			this.fontLabel = new System.Windows.Forms.Label();
			this.fontButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.highLightList = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.muteWordList = new System.Windows.Forms.ListBox();
			this.highLightTextBox = new System.Windows.Forms.TextBox();
			this.highOkButton = new System.Windows.Forms.Button();
			this.highRemButton = new System.Windows.Forms.Button();
			this.muteWordText = new System.Windows.Forms.TextBox();
			this.muteWordOKBut = new System.Windows.Forms.Button();
			this.muteWordRemBut = new System.Windows.Forms.Button();
			this.muteUserList = new System.Windows.Forms.ListBox();
			this.muteClientList = new System.Windows.Forms.ListBox();
			this.muteUserText = new System.Windows.Forms.TextBox();
			this.muteClientText = new System.Windows.Forms.TextBox();
			this.muteUserOKBut = new System.Windows.Forms.Button();
			this.muteClientOKBut = new System.Windows.Forms.Button();
			this.muteUserRemBut = new System.Windows.Forms.Button();
			this.muteClientRemBut = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.skinComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkRtProtecd = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.soundComboBox = new System.Windows.Forms.ComboBox();
			this.checkNotiPlay = new System.Windows.Forms.CheckBox();
			this.checkSmallUI = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.imageButton = new System.Windows.Forms.Button();
			this.labelImagePath = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkMuteMention
			// 
			this.checkMuteMention.AutoSize = true;
			this.checkMuteMention.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkMuteMention.Location = new System.Drawing.Point(13, 13);
			this.checkMuteMention.Name = "checkMuteMention";
			this.checkMuteMention.Size = new System.Drawing.Size(130, 19);
			this.checkMuteMention.TabIndex = 0;
			this.checkMuteMention.Text = "멘션함도 뮤트 작동";
			this.checkMuteMention.UseVisualStyleBackColor = true;
			this.checkMuteMention.CheckedChanged += new System.EventHandler(this.checkBox_Changed);
			// 
			// checkRetweetNoti
			// 
			this.checkRetweetNoti.AutoSize = true;
			this.checkRetweetNoti.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkRetweetNoti.Location = new System.Drawing.Point(12, 59);
			this.checkRetweetNoti.Name = "checkRetweetNoti";
			this.checkRetweetNoti.Size = new System.Drawing.Size(154, 34);
			this.checkRetweetNoti.TabIndex = 1;
			this.checkRetweetNoti.Text = "리트윗 알림을 멘션함에\r\n표시합니다.";
			this.checkRetweetNoti.UseVisualStyleBackColor = true;
			this.checkRetweetNoti.CheckedChanged += new System.EventHandler(this.checkRetweetNoti_CheckedChanged);
			// 
			// checkShowRetweet
			// 
			this.checkShowRetweet.AutoSize = true;
			this.checkShowRetweet.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkShowRetweet.Location = new System.Drawing.Point(12, 38);
			this.checkShowRetweet.Name = "checkShowRetweet";
			this.checkShowRetweet.Size = new System.Drawing.Size(185, 19);
			this.checkShowRetweet.TabIndex = 2;
			this.checkShowRetweet.Text = "리트윗 알림을 TL에 띄웁니다.";
			this.checkShowRetweet.UseVisualStyleBackColor = true;
			this.checkShowRetweet.CheckedChanged += new System.EventHandler(this.checkShowRetweet_CheckedChanged);
			// 
			// checkShowCount
			// 
			this.checkShowCount.AutoSize = true;
			this.checkShowCount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkShowCount.Location = new System.Drawing.Point(12, 92);
			this.checkShowCount.Name = "checkShowCount";
			this.checkShowCount.Size = new System.Drawing.Size(240, 19);
			this.checkShowCount.TabIndex = 3;
			this.checkShowCount.Text = "트윗에 리트윗, 관심글 수를 표시합니다.";
			this.checkShowCount.UseVisualStyleBackColor = true;
			this.checkShowCount.Visible = false;
			this.checkShowCount.CheckedChanged += new System.EventHandler(this.checkBox_Changed);
			// 
			// checkYesNo
			// 
			this.checkYesNo.AutoSize = true;
			this.checkYesNo.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkYesNo.Location = new System.Drawing.Point(13, 114);
			this.checkYesNo.Name = "checkYesNo";
			this.checkYesNo.Size = new System.Drawing.Size(205, 19);
			this.checkYesNo.TabIndex = 4;
			this.checkYesNo.Text = "트윗 등록 시 확인 창을 띄웁니다.";
			this.checkYesNo.UseVisualStyleBackColor = true;
			this.checkYesNo.CheckedChanged += new System.EventHandler(this.checkBox_Changed);
			// 
			// checkSendEnter
			// 
			this.checkSendEnter.AutoSize = true;
			this.checkSendEnter.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkSendEnter.Location = new System.Drawing.Point(13, 136);
			this.checkSendEnter.Name = "checkSendEnter";
			this.checkSendEnter.Size = new System.Drawing.Size(184, 34);
			this.checkSendEnter.TabIndex = 5;
			this.checkSendEnter.Text = "Enter키로 트윗을 등록합니다.\r\n(해제 시 Ctrl+Enter로 등록)";
			this.checkSendEnter.UseVisualStyleBackColor = true;
			this.checkSendEnter.CheckedChanged += new System.EventHandler(this.checkBox_Changed);
			// 
			// fontLabel
			// 
			this.fontLabel.AutoSize = true;
			this.fontLabel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.fontLabel.Location = new System.Drawing.Point(13, 172);
			this.fontLabel.Name = "fontLabel";
			this.fontLabel.Size = new System.Drawing.Size(39, 15);
			this.fontLabel.TabIndex = 6;
			this.fontLabel.Text = "label1";
			// 
			// fontButton
			// 
			this.fontButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.fontButton.Location = new System.Drawing.Point(13, 191);
			this.fontButton.Name = "fontButton";
			this.fontButton.Size = new System.Drawing.Size(75, 23);
			this.fontButton.TabIndex = 7;
			this.fontButton.Text = "폰트 설정";
			this.fontButton.UseVisualStyleBackColor = true;
			this.fontButton.Click += new System.EventHandler(this.fontButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cancelButton.Location = new System.Drawing.Point(387, 603);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "취소";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.okButton.Location = new System.Drawing.Point(295, 603);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "확인";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// highLightList
			// 
			this.highLightList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.highLightList.FormattingEnabled = true;
			this.highLightList.ItemHeight = 15;
			this.highLightList.Location = new System.Drawing.Point(13, 324);
			this.highLightList.Name = "highLightList";
			this.highLightList.Size = new System.Drawing.Size(204, 79);
			this.highLightList.TabIndex = 10;
			this.highLightList.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedChange);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(15, 291);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(207, 30);
			this.label1.TabIndex = 11;
			this.label1.Text = "단어 알림(해당 단어가 들어간 트윗은\r\n멘션함으로 들어옵니다)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(13, 446);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(207, 30);
			this.label2.TabIndex = 12;
			this.label2.Text = "단어 뮤트(해당 단어가 들어간 트윗은\r\n타임라인에 표기되지 않습니다)";
			// 
			// muteWordList
			// 
			this.muteWordList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteWordList.FormattingEnabled = true;
			this.muteWordList.ItemHeight = 15;
			this.muteWordList.Location = new System.Drawing.Point(15, 478);
			this.muteWordList.Name = "muteWordList";
			this.muteWordList.Size = new System.Drawing.Size(202, 79);
			this.muteWordList.Sorted = true;
			this.muteWordList.TabIndex = 13;
			this.muteWordList.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedChange);
			// 
			// highLightTextBox
			// 
			this.highLightTextBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.highLightTextBox.Location = new System.Drawing.Point(13, 415);
			this.highLightTextBox.Name = "highLightTextBox";
			this.highLightTextBox.Size = new System.Drawing.Size(95, 23);
			this.highLightTextBox.TabIndex = 14;
			// 
			// highOkButton
			// 
			this.highOkButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.highOkButton.Location = new System.Drawing.Point(113, 415);
			this.highOkButton.Name = "highOkButton";
			this.highOkButton.Size = new System.Drawing.Size(50, 23);
			this.highOkButton.TabIndex = 15;
			this.highOkButton.Text = "추가";
			this.highOkButton.UseVisualStyleBackColor = true;
			this.highOkButton.Click += new System.EventHandler(this.highOkButton_Click);
			// 
			// highRemButton
			// 
			this.highRemButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.highRemButton.Location = new System.Drawing.Point(171, 415);
			this.highRemButton.Name = "highRemButton";
			this.highRemButton.Size = new System.Drawing.Size(50, 23);
			this.highRemButton.TabIndex = 16;
			this.highRemButton.Text = "제거";
			this.highRemButton.UseVisualStyleBackColor = true;
			this.highRemButton.Click += new System.EventHandler(this.highRemButton_Click);
			// 
			// muteWordText
			// 
			this.muteWordText.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteWordText.Location = new System.Drawing.Point(15, 569);
			this.muteWordText.Name = "muteWordText";
			this.muteWordText.Size = new System.Drawing.Size(93, 23);
			this.muteWordText.TabIndex = 17;
			// 
			// muteWordOKBut
			// 
			this.muteWordOKBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteWordOKBut.Location = new System.Drawing.Point(115, 569);
			this.muteWordOKBut.Name = "muteWordOKBut";
			this.muteWordOKBut.Size = new System.Drawing.Size(50, 23);
			this.muteWordOKBut.TabIndex = 18;
			this.muteWordOKBut.Text = "추가";
			this.muteWordOKBut.UseVisualStyleBackColor = true;
			this.muteWordOKBut.Click += new System.EventHandler(this.muteWordOKBut_Click);
			// 
			// muteWordRemBut
			// 
			this.muteWordRemBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteWordRemBut.Location = new System.Drawing.Point(171, 569);
			this.muteWordRemBut.Name = "muteWordRemBut";
			this.muteWordRemBut.Size = new System.Drawing.Size(50, 23);
			this.muteWordRemBut.TabIndex = 19;
			this.muteWordRemBut.Text = "제거";
			this.muteWordRemBut.UseVisualStyleBackColor = true;
			this.muteWordRemBut.Click += new System.EventHandler(this.muteWordRemBut_Click);
			// 
			// muteUserList
			// 
			this.muteUserList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteUserList.FormattingEnabled = true;
			this.muteUserList.ItemHeight = 15;
			this.muteUserList.Location = new System.Drawing.Point(248, 324);
			this.muteUserList.Name = "muteUserList";
			this.muteUserList.Size = new System.Drawing.Size(204, 79);
			this.muteUserList.TabIndex = 20;
			this.muteUserList.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedChange);
			// 
			// muteClientList
			// 
			this.muteClientList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteClientList.FormattingEnabled = true;
			this.muteClientList.ItemHeight = 15;
			this.muteClientList.Location = new System.Drawing.Point(250, 478);
			this.muteClientList.Name = "muteClientList";
			this.muteClientList.Size = new System.Drawing.Size(202, 79);
			this.muteClientList.Sorted = true;
			this.muteClientList.TabIndex = 21;
			this.muteClientList.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedChange);
			// 
			// muteUserText
			// 
			this.muteUserText.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteUserText.Location = new System.Drawing.Point(248, 414);
			this.muteUserText.Name = "muteUserText";
			this.muteUserText.Size = new System.Drawing.Size(95, 23);
			this.muteUserText.TabIndex = 22;
			// 
			// muteClientText
			// 
			this.muteClientText.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteClientText.Location = new System.Drawing.Point(250, 568);
			this.muteClientText.Name = "muteClientText";
			this.muteClientText.Size = new System.Drawing.Size(95, 23);
			this.muteClientText.TabIndex = 23;
			// 
			// muteUserOKBut
			// 
			this.muteUserOKBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteUserOKBut.Location = new System.Drawing.Point(349, 412);
			this.muteUserOKBut.Name = "muteUserOKBut";
			this.muteUserOKBut.Size = new System.Drawing.Size(50, 23);
			this.muteUserOKBut.TabIndex = 24;
			this.muteUserOKBut.Text = "추가";
			this.muteUserOKBut.UseVisualStyleBackColor = true;
			this.muteUserOKBut.Click += new System.EventHandler(this.muteUserOKBut_Click);
			// 
			// muteClientOKBut
			// 
			this.muteClientOKBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteClientOKBut.Location = new System.Drawing.Point(351, 566);
			this.muteClientOKBut.Name = "muteClientOKBut";
			this.muteClientOKBut.Size = new System.Drawing.Size(50, 23);
			this.muteClientOKBut.TabIndex = 25;
			this.muteClientOKBut.Text = "추가";
			this.muteClientOKBut.UseVisualStyleBackColor = true;
			this.muteClientOKBut.Click += new System.EventHandler(this.muteClientOKBut_Click);
			// 
			// muteUserRemBut
			// 
			this.muteUserRemBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteUserRemBut.Location = new System.Drawing.Point(406, 412);
			this.muteUserRemBut.Name = "muteUserRemBut";
			this.muteUserRemBut.Size = new System.Drawing.Size(50, 23);
			this.muteUserRemBut.TabIndex = 26;
			this.muteUserRemBut.Text = "제거";
			this.muteUserRemBut.UseVisualStyleBackColor = true;
			this.muteUserRemBut.Click += new System.EventHandler(this.muteUserRemBut_Click);
			// 
			// muteClientRemBut
			// 
			this.muteClientRemBut.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.muteClientRemBut.Location = new System.Drawing.Point(411, 565);
			this.muteClientRemBut.Name = "muteClientRemBut";
			this.muteClientRemBut.Size = new System.Drawing.Size(50, 23);
			this.muteClientRemBut.TabIndex = 27;
			this.muteClientRemBut.Text = "제거";
			this.muteClientRemBut.UseVisualStyleBackColor = true;
			this.muteClientRemBut.Click += new System.EventHandler(this.muteClientRemBut_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(246, 291);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(191, 30);
			this.label3.TabIndex = 28;
			this.label3.Text = "유저 뮤트(해당 유저는 타임라인에\r\n표시되지 않습니다)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(246, 446);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(215, 30);
			this.label4.TabIndex = 29;
			this.label4.Text = "클라이언트 뮤트(특정 클라이언트\r\n트윗을 타임라인에 표시하지 않습니다)";
			// 
			// skinComboBox
			// 
			this.skinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.skinComboBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.skinComboBox.FormattingEnabled = true;
			this.skinComboBox.Location = new System.Drawing.Point(304, 191);
			this.skinComboBox.Name = "skinComboBox";
			this.skinComboBox.Size = new System.Drawing.Size(121, 23);
			this.skinComboBox.TabIndex = 30;
			this.skinComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label5.Location = new System.Drawing.Point(266, 195);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 15);
			this.label5.TabIndex = 31;
			this.label5.Text = "스킨";
			// 
			// checkRtProtecd
			// 
			this.checkRtProtecd.AutoSize = true;
			this.checkRtProtecd.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkRtProtecd.Location = new System.Drawing.Point(248, 12);
			this.checkRtProtecd.Name = "checkRtProtecd";
			this.checkRtProtecd.Size = new System.Drawing.Size(198, 34);
			this.checkRtProtecd.TabIndex = 32;
			this.checkRtProtecd.Text = "프로텍트 유저 글도 리트윗 하기\r\n(아이디는 ***처리)";
			this.checkRtProtecd.UseVisualStyleBackColor = true;
			this.checkRtProtecd.CheckedChanged += new System.EventHandler(this.checkBox_Changed);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label6.Location = new System.Drawing.Point(240, 163);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 15);
			this.label6.TabIndex = 34;
			this.label6.Text = "알림 소리";
			// 
			// soundComboBox
			// 
			this.soundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.soundComboBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.soundComboBox.FormattingEnabled = true;
			this.soundComboBox.Location = new System.Drawing.Point(304, 160);
			this.soundComboBox.Name = "soundComboBox";
			this.soundComboBox.Size = new System.Drawing.Size(121, 23);
			this.soundComboBox.TabIndex = 35;
			this.soundComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// checkNotiPlay
			// 
			this.checkNotiPlay.AutoSize = true;
			this.checkNotiPlay.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkNotiPlay.Location = new System.Drawing.Point(248, 136);
			this.checkNotiPlay.Name = "checkNotiPlay";
			this.checkNotiPlay.Size = new System.Drawing.Size(106, 19);
			this.checkNotiPlay.TabIndex = 36;
			this.checkNotiPlay.Text = "알림 소리 재생";
			this.checkNotiPlay.UseVisualStyleBackColor = true;
			this.checkNotiPlay.CheckedChanged += new System.EventHandler(this.notiCheckBox_CheckedChanged);
			// 
			// checkSmallUI
			// 
			this.checkSmallUI.AutoSize = true;
			this.checkSmallUI.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.checkSmallUI.Location = new System.Drawing.Point(248, 48);
			this.checkSmallUI.Name = "checkSmallUI";
			this.checkSmallUI.Size = new System.Drawing.Size(169, 34);
			this.checkSmallUI.TabIndex = 37;
			this.checkSmallUI.Text = "트윗 입력창을 작게 하고\r\n인장을 표시하지 않습니다.";
			this.checkSmallUI.UseVisualStyleBackColor = true;
			this.checkSmallUI.CheckedChanged += new System.EventHandler(this.checkSmallUI_CheckedChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label7.Location = new System.Drawing.Point(11, 219);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(106, 15);
			this.label7.TabIndex = 38;
			this.label7.Text = "이미지 저장 폴더: ";
			// 
			// imageButton
			// 
			this.imageButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.imageButton.Location = new System.Drawing.Point(12, 239);
			this.imageButton.Name = "imageButton";
			this.imageButton.Size = new System.Drawing.Size(49, 23);
			this.imageButton.TabIndex = 39;
			this.imageButton.Text = "선택";
			this.imageButton.UseVisualStyleBackColor = true;
			this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
			// 
			// labelImagePath
			// 
			this.labelImagePath.AutoSize = true;
			this.labelImagePath.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelImagePath.Location = new System.Drawing.Point(115, 219);
			this.labelImagePath.Name = "labelImagePath";
			this.labelImagePath.Size = new System.Drawing.Size(39, 15);
			this.labelImagePath.TabIndex = 40;
			this.labelImagePath.Text = "label8";
			// 
			// OptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 638);
			this.Controls.Add(this.labelImagePath);
			this.Controls.Add(this.imageButton);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.checkSmallUI);
			this.Controls.Add(this.checkNotiPlay);
			this.Controls.Add(this.soundComboBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.checkRtProtecd);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.skinComboBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.muteClientRemBut);
			this.Controls.Add(this.muteUserRemBut);
			this.Controls.Add(this.muteClientOKBut);
			this.Controls.Add(this.muteUserOKBut);
			this.Controls.Add(this.muteClientText);
			this.Controls.Add(this.muteUserText);
			this.Controls.Add(this.muteClientList);
			this.Controls.Add(this.muteUserList);
			this.Controls.Add(this.muteWordRemBut);
			this.Controls.Add(this.muteWordOKBut);
			this.Controls.Add(this.muteWordText);
			this.Controls.Add(this.highRemButton);
			this.Controls.Add(this.highOkButton);
			this.Controls.Add(this.highLightTextBox);
			this.Controls.Add(this.muteWordList);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.highLightList);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.fontButton);
			this.Controls.Add(this.fontLabel);
			this.Controls.Add(this.checkSendEnter);
			this.Controls.Add(this.checkYesNo);
			this.Controls.Add(this.checkShowCount);
			this.Controls.Add(this.checkShowRetweet);
			this.Controls.Add(this.checkRetweetNoti);
			this.Controls.Add(this.checkMuteMention);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "OptionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "설정";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.optionForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkMuteMention;
		private System.Windows.Forms.CheckBox checkRetweetNoti;
		private System.Windows.Forms.CheckBox checkShowRetweet;
		private System.Windows.Forms.CheckBox checkShowCount;
		private System.Windows.Forms.CheckBox checkYesNo;
		private System.Windows.Forms.CheckBox checkSendEnter;
		private System.Windows.Forms.Label fontLabel;
		private System.Windows.Forms.Button fontButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ListBox highLightList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox muteWordList;
		private System.Windows.Forms.TextBox highLightTextBox;
		private System.Windows.Forms.Button highOkButton;
		private System.Windows.Forms.Button highRemButton;
		private System.Windows.Forms.TextBox muteWordText;
		private System.Windows.Forms.Button muteWordOKBut;
		private System.Windows.Forms.Button muteWordRemBut;
		private System.Windows.Forms.ListBox muteUserList;
		private System.Windows.Forms.ListBox muteClientList;
		private System.Windows.Forms.TextBox muteUserText;
		private System.Windows.Forms.TextBox muteClientText;
		private System.Windows.Forms.Button muteUserOKBut;
		private System.Windows.Forms.Button muteClientOKBut;
		private System.Windows.Forms.Button muteUserRemBut;
		private System.Windows.Forms.Button muteClientRemBut;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox skinComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkRtProtecd;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox soundComboBox;
		private System.Windows.Forms.CheckBox checkNotiPlay;
		private System.Windows.Forms.CheckBox checkSmallUI;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button imageButton;
		private System.Windows.Forms.Label labelImagePath;
	}
}