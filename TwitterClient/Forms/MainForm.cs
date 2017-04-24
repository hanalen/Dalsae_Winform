using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	/*
		[0:04][자유부대]<I'> <나라하> 제일 밑에 스크롤베이스 패널을 테이블레이아웃으로 만들고		[0:04][자유부대]<I'> <나라하> 그걸 좌, 우로 나눕니다		[0:04][자유부대]<I'> <나라하> 우측은 딱 스크롤바 사이즈 크기로 고정시키고 좌측은 상대적 사이즈로 설정		[0:05][자유부대]<I'> <나라하> 그리고 좌측에는 일반 패널을  Dock.Fill 상태로 놓고, 우측에는 스크롤 바를 설치		[0:05][자유부대]<하날엔> ngui가 진짜 잘돼있긴 한거구나		[0:05][자유부대]<I'> <나라하> 일반 패널(베이스패널) 위에 또 다른 패널 하나를 앵커를 이용해서 올려두고		[0:06][자유부대]<I'> <나라하> 내용물은 그 패널위에 추가합니다		[0:06][자유부대]<I'> <나라하> 그리고, 내용물이 추가되는 이벤트 때 마다 스크롤 바의 Range 값을 변경
		[0:06][자유부대]<I'> <나라하> 그리고 스크롤 바 이벤트가 발생하면, 내용물 담고 있는 패널의 Top 값을 변경해줍니다		[0:07][자유부대]<I'> <나라하> 이런 식이 되면, 구조는 좀 복잡한데 나름 유연한 형태가 되요	
		[0:08][자유부대]<I'> <나라하> 제가 저런 방식을 택한 이유가		[0:09][자유부대]<I'> <나라하> 각 컨트롤의 onDraw를 가로채서 이미지를 입히는 식으로 하다보니까		[0:09][자유부대]<I'> <나라하> 전체를 하나로 묶기 힘들어서 아예 기능별 분리해 버린 거라서요
		[0:09][자유부대]<I'> <나라하> 1. 바탕패널		[0:10][자유부대]<I'> <나라하> 2.스크롤바		[0:10][자유부대]<I'> <나라하> 3.내용물 올리는 패널
	*/
	public partial class MainForm : Form
	{
		public enum eSelectMenu
		{
			TIME_LINE,
			MENTION,
			DM,
			FAVORITE,
			MY_TL,
			USER,
			OPEN_URL,
		}

		public enum eTaskBar
		{
			STREAMING,
		}

		private Regex UrlMatch = new Regex(@"[(http|ftp|https):\/\/]*[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
		private int tweetLength;
		private eSelectMenu eselectMenu = eSelectMenu.TIME_LINE;
		private List<PictureBox> listPictureBox = new List<PictureBox>();//이미지 첨부 용 리스트
		private FlowPanelManager homePanel;
		private FlowPanelManager mentionPanel;
		private FlowPanelManager dmPanel;
		private FlowPanelManager favPanel;
		private FlowPanelManager myPanel;
		private FlowPanelManager userPanel;
		private FlowPanelManager urlPanel;
		private Dictionary<string, FlowPanelManager> dicUserPanel = new Dictionary<string, FlowPanelManager>();//유저 타임라인
		private List<Picture> listFiles = new List<Picture>();
		private bool isMention = false;//멘션 입력 중일 경우
		private bool isTab = false;//탭키 입력을 막기위해 사용
		private int mentionIndex = 0;
		private Bitmap profileBitmap;
		private TweetControl loadTweetControl;
		//private bool isLoadQuote = false;
		private bool isTwoLoadTweet = false;

		private bool isAddedGifAndVideo = false;
		private string pathGifAndVideo = string.Empty;

		private ClientTweet replyTweet;

		public delegate void DeleLog(string text);
		public delegate void DeleDebug(object text);
		public delegate void DeleAddTweetList(List<ClientTweet> listTweet, eSelectMenu eselect);
		public delegate void DeleAddTweet(ClientTweet tweet);
		public delegate void DeleAddDirectMessageList(List<ClientDirectMessage> listdm);
		public delegate void DeleAddDirectMessage(ClientDirectMessage clientdm);
		public delegate void DeleUpdateProfilePicture(string url);
		public delegate void DeleUpdateTweet(ClientTweet tweet, bool isCreateFav);
		public delegate void DeleShowUserTimeLine(List<ClientTweet> listTweet, string name);
		public delegate void DeleDeleteTweet(ClientStreamDelete streamDelete);
		public delegate void DeleTaskBarUpdate(eTaskBar etask, string text);
		public delegate void DeleShowUserPanel(string name);
		public delegate void DeleEnterInput();
		public delegate void DeleAddDMId(string name);
		public delegate void DeleMainFormResize();
		public delegate void DeleChangeFont();
		public delegate void DeleChangeSkin();
		public delegate void DeleUpdateMentionID();
		public delegate void DeleShowPanel(eSelectMenu eselect);
		public delegate void DeleShowMessageBox(string text, string title,
																MessageBoxButtons button, MessageBoxIcon icon);
		public delegate void DeleShowMessageBoxCallBack(string text, string title,
																MessageBoxButtons button, MessageBoxIcon icon, Delegate dele);
		public delegate void DeleAddMentionId(bool isReplyAll, ClientTweet tweet);
		public delegate void DeleChangeSmallUI(bool isSmall);
		public delegate void DeleQTRetweet(ClientTweet tweet);
		public delegate void DeleLoadAccount();
		public delegate void DeleAddHashtag(string text);

		private FormWindowState prevWindowState;
		public MainForm()
		{
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			UpdateStyles();

			Init();
		}

		private void Init()
		{
			listPictureBox.Add(imageBox1);
			listPictureBox.Add(imageBox2);
			listPictureBox.Add(imageBox3);
			listPictureBox.Add(imageBox4);

			Size = DataInstence.option.sizeMainForm;
			Location = DataInstence.option.pointMainForm;

			homePanel = new TwitterClient.FlowPanelManager(this, panel1, ePanel.HOME);
			mentionPanel = new FlowPanelManager(this, panel1, ePanel.MENTION);
			dmPanel = new FlowPanelManager(this, panel1, ePanel.DM);
			urlPanel = new FlowPanelManager(this, panel1, ePanel.OPEN_URL);
			favPanel = new FlowPanelManager(this, panel1, ePanel.FAVORITE);
			this.AutoScaleMode = AutoScaleMode.Dpi;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			if (DataInstence.option.isSmallUI)
				ChangeSmallUI(DataInstence.option.isSmallUI);

			mentionPanel.Hide();
			mentionListBox.Hide();
			DoubleBuffered = true;
			
			if (Generate.IsOnScreen(this) == false)
				Location = new Point(500, 100);

			newMention.Hide();
			newDM.Hide();
			ChangeSkin();

			prevWindowState = WindowState;
		}
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------외부 호출 함수---------------------------------------------
		//----------------------------------------------------------------------------------------------------

		public void ShowMessageBox(string text, string title, MessageBoxButtons button, MessageBoxIcon icon)
		{
			MessageBox.Show(this, text, title, button, icon);
		}

		public void ShowMessageBox(string text, string title, MessageBoxButtons button, MessageBoxIcon icon, Delegate dele)
		{
			DialogResult dr = MessageBox.Show(this, text, title, button, icon);
			dele.DynamicInvoke(new object[] { dr });
		}

		public void AddHashtag(string text)
		{
			int selectIndex = inputTweet.Text.Length;
			inputTweet.Text += $" #{text}";
			inputTweet.Select(selectIndex, 0);
			EnterInputTweet();
		}

		public void LoadAccounts()
		{
			List<string> listAccount = FileInstence.GetAccountArray();
			
			foreach (object vitem in 계정선택AToolStripMenuItem.DropDownItems)//계정 전환 시 목록 남기기위한 코드
			{
				ToolStripMenuItem item = vitem as ToolStripMenuItem;
				if (item == null) continue;

				if (item != 계정추가ToolStripMenuItem && item != 현재계정삭제ToolStripMenuItem)
					listAccount.Remove(item.Text);
			}

			foreach (string account in listAccount)
			{
				
				ToolStripMenuItem item = new ToolStripMenuItem(account);
				item.Click += new EventHandler(account_Click);
				계정선택AToolStripMenuItem.DropDownItems.Add(item);
			}
		}

		//처음 실행 후 인장 업데이트 할 때 사용
		public void UpdateProfilePicture(string url)
		{
			//UI옵션에서 인장을 꺼놨어도 다시 켰을 때 인장을 표시하기 위해 해당 코드는 돌아감
			profileBitmap = InjangManager.GetInstence().GetInjang(url, 6);
			profilePictureBox.Image = profileBitmap;
			//profilePictureBox.Image = InjangManager.GetInstence().GetInjang(url);
		}

		//통신 로그용 함수
		public void Log(object text)
		{

		}

		public void ChangeSmallUI(bool isSmall)
		{
			if (isSmall)
			{
				newMention.Location = new Point(newMention.Location.X, 31);
				newDM.Location = new Point(newDM.Location.X, 51);
				inputTweet.Size = new Size(inputTweet.Size.Width, 23);
				mentionListBox.Location = new Point(mentionListBox.Location.X, 64);
				tweetButton.Location = new Point(tweetButton.Location.X, 64);
				countLabel.Location = new Point(countLabel.Location.X, 69);
				panel1.Location = new Point(panel1.Location.X, 100);
				panel1.Size = new Size(panel1.Size.Width, panel1.Size.Height + 38);
				profilePictureBox.Hide();
				for (int i = 0; i < listPictureBox.Count; i++)
				{
					listPictureBox[i].Location = new Point(listPictureBox[i].Location.X, 64);
				}
			}
			else
			{
				newMention.Location = new Point(newMention.Location.X, 91);
				newDM.Location = new Point(newDM.Location.X, 111);
				inputTweet.Size = new Size(inputTweet.Size.Width, 63);
				mentionListBox.Location = new Point(mentionListBox.Location.X, 102);
				tweetButton.Location = new Point(tweetButton.Location.X, 103);
				countLabel.Location = new Point(countLabel.Location.X, 111);
				panel1.Location = new Point(panel1.Location.X, 138);
				panel1.Size = new Size(panel1.Size.Width, panel1.Size.Height - 38);
				profilePictureBox.Show();
				for (int i = 0; i < listPictureBox.Count; i++)
				{
					listPictureBox[i].Location = new Point(listPictureBox[i].Location.X, 102);
				}
			}
			MainForm_ResizeEnd(null, null);
		}

		//디버깅용 함수
		public void Debug(object text)
		{
			//debugTextBox.Text += Environment.NewLine;
		}

		//스킨 변경
		public void ChangeSkin()
		{
			panel1.BackColor = DataInstence.skin.blockOne;
			BackColor = DataInstence.skin.defaultColor;
			menuStrip1.BackColor = DataInstence.skin.topbar;
			panelButtonControl1.BackColor = DataInstence.skin.bottomBar;

			newMention.ForeColor = DataInstence.skin.menuColor;
			newDM.ForeColor = DataInstence.skin.menuColor;
			countLabel.ForeColor = DataInstence.skin.menuColor;
			panelButtonControl1.ForeColor = DataInstence.skin.menuColor;

			homePanel.ChangeSkin();
			mentionPanel.ChangeSkin();
			dmPanel.ChangeSkin();
			favPanel.ChangeSkin();
			if (myPanel != null)
				myPanel.ChangeSkin();
			foreach(FlowPanelManager item in dicUserPanel.Values)
			{
				item.ChangeSkin();
			}
		}

		//폰트 변경
		public void ChangeFont()
		{
			dmPanel.ChangeFont();
			homePanel.ChangeFont();
			mentionPanel.ChangeFont();
			urlPanel.ChangeFont();
			foreach(FlowPanelManager item in dicUserPanel.Values)
			{
				item.ChangeFont();
			}
		}

		public void AddMentionIds(bool isReplyAll, ClientTweet tweet)
		{
			if(isReplyAll)
			{
				ClearInputBox(true);
				replyTweet = tweet;
				ReplyAll();
			}
			else
			{
				if (replyTweet == null)
					replyTweet = tweet;

				Reply();
			}
		}

		public void ClearLoadTweet()
		{
			isTwoLoadTweet = false;
			loadTweetControl = null;
		}

		//대화 불러오기용 함수
		public void AddSingleTweet(ClientTweet tweet)
		{
			if (tweet == null || loadTweetControl == null)
			{
				loadTweetControl = null;
				return;
			}
			bool isAdded = false;
			switch(eselectMenu)
			{
				case eSelectMenu.TIME_LINE:			isAdded = homePanel.AddSingleTweet(loadTweetControl, tweet);			break;
				case eSelectMenu.MENTION:			isAdded = mentionPanel.AddSingleTweet(loadTweetControl, tweet);		break;
				case eSelectMenu.USER:				isAdded = userPanel.AddSingleTweet(loadTweetControl, tweet);			break;
				case eSelectMenu.MY_TL:				isAdded = myPanel.AddSingleTweet(loadTweetControl, tweet);				break;
				case eSelectMenu.FAVORITE:			isAdded = favPanel.AddSingleTweet(loadTweetControl, tweet);				break;
				//case eSelectMenu.OPEN_URL:		urlPanel.Resize();						break;
			}
			if (isTwoLoadTweet)
				isTwoLoadTweet = false;
			else
				loadTweetControl = null;

			if (isAdded == false)
				ShowMessageBox("블락한 유저의 트윗입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}


		//스트리밍용 추가 트윗
		public void AddTweetStreaming(ClientTweet tweet)
		{
			if (DataInstence.CheckIsMe(tweet.user.id))//인장 변경 체크
			{
				ChangeProfilePicture(tweet.user.profile_image_url);
				if (DataInstence.CheckIsMe(tweet.user.screen_name) == false)//내 screen_name이 바뀔 경우 정보 갱신
					DataInstence.UpdateMe(tweet.user.screen_name);
			}

			if (homePanel.AddTweet(tweet, eTweet.HOME))//홈패널에 추가 후 하이라이트 적중 시 추가
			{
				if(mentionPanel.AddTweet(tweet, eTweet.MENTION))
				{
					ClientInctence.PlaySoundNoti();
					if (eselectMenu != eSelectMenu.MENTION)//알림 표시
					{
						newMention.Show();
						if (this.Focused == false && inputTweet.Focused == false)
							Generate.FlashWindowEx(this);
					}
				}
			}
			if (tweet.isRetweet)
			{
				//if (DataInstence.hashRetweetOff.Contains(tweet.user.id) == false)//리트윗 끈 유저 체크
				//{
					//내가 한 리트윗일 경우 오브젝트 갱신
					if (tweet.user.screen_name == DataInstence.userInfo.myUser.screen_name)
					{
						UpdateTweetRetweet(tweet, true);
					}
				//}
			}
		}

		//유저스트리밍에서 부르는 DM 추가
		//clientdm: dm정보 클래스
		public void AddDirectMessage(ClientDirectMessage clientdm)
		{
			dmPanel.AddDirectMessage(clientdm);
			if (DataInstence.CheckIsMe(clientdm.sender.screen_name) == false)
			{
				ClientInctence.PlaySoundNoti();
				if (eselectMenu != eSelectMenu.DM)
				{
					newDM.Show();
					if (this.Focused == false && inputTweet.Focused == false)
						Generate.FlashWindowEx(this);
				}
			}
			else
			{
				if(DataInstence.CheckIsMe(clientdm.recipient.screen_name))
					ChangeProfilePicture(clientdm.recipient.profile_image_url);//인장 변경 체크
			}
		}

		//API콜로 불러오는 dm정보
		public void AddDirectMessage(List<ClientDirectMessage> listdm)
		{
			dmPanel.AddDirectMessage(listdm);
			ClientInctence.EndLoadingTweet();
		}

		//트윗 추가
		//listTweet: 추가 될 트윗 리스트, eselect: 메뉴 분류
		public void AddTweet(List<ClientTweet> listTweet, eSelectMenu eselect)
		{
			switch (eselect)
			{
				case eSelectMenu.TIME_LINE:
					homePanel.AddTweet(listTweet, eTweet.HOME);
					ClientInctence.EndLoadStartTweet();
					break;
				case eSelectMenu.MENTION:
					mentionPanel.AddTweet(listTweet, eTweet.MENTION);
					ClientInctence.EndLoadStartMention();
					break;
				case eSelectMenu.FAVORITE:			favPanel.AddTweet(listTweet, eTweet.NONE);			break;
				case eSelectMenu.MY_TL:				myPanel.AddTweet(listTweet, eTweet.NONE);			break;
				case eSelectMenu.OPEN_URL:		urlPanel.AddTweet(listTweet, eTweet.NONE);			break;
			}
			ClientInctence.EndLoadingTweet();
		}

		//더불러오기에 사용
		public void AddMoreTweet(List<ClientTweet> listTweet, eSelectMenu eselct)
		{
			switch(eselct)
			{
				case eSelectMenu.MY_TL:
					myPanel.AddTweet(listTweet, true, eTweet.HOME);
					break;
				case eSelectMenu.FAVORITE:
					favPanel.AddTweet(listTweet, true, eTweet.HOME);
					break;
				case eSelectMenu.MENTION:
					mentionPanel.AddTweet(listTweet, true, eTweet.MENTION);
					break;
			}
		}

		//관글,리트윗 등으로 트윗 정보가 바뀔 때 호출
		//tweet: 바뀐 트윗 정보
		public void UpdateTweetFavorite(ClientTweet tweet, bool isCreateFav)
		{
			homePanel.UpdateFavorite(tweet, isCreateFav);
			mentionPanel.UpdateFavorite(tweet, isCreateFav);
		}

		//리트윗이 들어올 경우 갱신
		public void UpdateTweetRetweet(ClientTweet tweet, bool isRetweet)
		{
			homePanel.UpdateRetweet(tweet, isRetweet);
			mentionPanel.UpdateRetweet(tweet, isRetweet);
		}

		//delete이벤트 오면 사용
		public void DeleteTweet(ClientStreamDelete streamDelete)
		{
			homePanel.DeleteTweet(streamDelete.delete.status.id);
			mentionPanel.DeleteTweet(streamDelete.delete.status.id);
		}

		//유저 트윗 볼 때 사용
		//listTweet: 트윗 목록, name: 유저 screen_name
		public void AddUserTimeLine(List<ClientTweet> listTweet, string name)
		{
			if (dicUserPanel.ContainsKey(name))//기존에 불러온 게 있을 경우 보기
			{
				dicUserPanel[name].AddTweet(listTweet, eTweet.NONE);
				dicUserPanel[name].Show();
				userPanel = dicUserPanel[name];
			}
			else
			{
				FlowPanelManager panel = new FlowPanelManager(this, panel1, ePanel.USER_TL);
				dicUserPanel.Add(name, panel);
				panel.AddTweet(listTweet, eTweet.NONE);
				ToolStripMenuItem item = new ToolStripMenuItem(name);
				item.Name = name;
				item.Click += new EventHandler(UserTL_MenuStripClick);
				유저타임라인ToolStripMenuItem.DropDownItems.Add(item);
				UserTL_MenuStripClick(item, null);
				userPanel = dicUserPanel[name];
			}
			ClientInctence.EndLoadingTweet();
			ResumeLayout();
		}

		//유저 글 더 불러오기 할 때
		public void AddUserMoreTimeLine(List<ClientTweet> listTweet, string name)
		{
			if (dicUserPanel.ContainsKey(name))
			{
				dicUserPanel[name].AddTweet(listTweet, true, eTweet.NONE);
			}
		}


		//하단 바 변경 함수, 유저스트리밍 on/off표시용
		public void UpdateTaskBar(eTaskBar etask, string text)
		{
			switch (etask)
			{
				case eTaskBar.STREAMING:
					if (text.IndexOf("ON") > -1)
						연결ToolStripMenuItem.Enabled = false;
					else if (text.IndexOf("OFF") > -1)
						연결ToolStripMenuItem.Enabled = true;
					toolStrip_UserStream.Text = text;
					break;
			}
		}

		//외부에서 선택 패널 바꿀 떄 사용
		public void ShowPanelDele(eSelectMenu eselect)
		{
			ShowPanel(eselect);
		}

		//유저 패널을 보여줄 때 사용, 외부 호출용
		public void ShowUserPanel(string name)
		{
			HidePanels();
			eselectMenu = eSelectMenu.USER;
			toolStrip_Panal.Text = "유저 트윗";

			if(dicUserPanel.ContainsKey(name))
			{
				userPanel = dicUserPanel[name];
				userPanel.Show();
			}
		}

		//현재 선택 중인 컨트롤을 얻는 함수
		private TweetControl GetSelectControl()
		{
			TweetControl selectControl = null;
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE:			selectControl = homePanel.SelectControl();			break;
				case eSelectMenu.MENTION:			selectControl = mentionPanel.SelectControl();		break;
				case eSelectMenu.DM:					selectControl = dmPanel.SelectControl();			break;
				case eSelectMenu.FAVORITE:			selectControl = favPanel.SelectControl();				break;
				case eSelectMenu.MY_TL:				selectControl = myPanel.SelectControl();				break;
				case eSelectMenu.USER:				selectControl = userPanel.SelectControl();			break;
				case eSelectMenu.OPEN_URL:		selectControl = urlPanel.SelectControl();				break;
			}
			return selectControl;
		}

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------UI 처리-----------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		//트윗 컨트롤 클릭 했을 떄 선택 컨트롤을 바꾸는 함수
		public void SelectControl(TweetControl control)
		{
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE:
					homePanel.SelectControl(control);
					break;
				case eSelectMenu.DM:
					dmPanel.SelectControl(control);
					break;
				case eSelectMenu.FAVORITE:
					favPanel.SelectControl(control);
					break;
				case eSelectMenu.MENTION:
					mentionPanel.SelectControl(control);
					break;
				case eSelectMenu.MY_TL:
					myPanel.SelectControl(control);
					break;
				case eSelectMenu.OPEN_URL:
					urlPanel.SelectControl(control);
					break;
				case eSelectMenu.USER:
					userPanel.SelectControl(control);
					break;
			}
		}

		//자신의 인장이 바뀌었을 때 인장 바꾸는 함수
		private void ChangeProfilePicture(string url)
		{
			//UI옵션에서 인장을 꺼놨어도 다시 켰을 때 인장을 표시하기 위해 해당 코드는 돌아감
			User user = DataInstence.userInfo.myUser;
			if (user.profile_image_url != url)
			{
				//InjangManager.GetInstence().DeleteInjang(user.profile_image_url);
				user.profile_image_url = url;
				//profileBitmap.Dispose();
				profilePictureBox.Image = null;

				profileBitmap = InjangManager.GetInstence().GetInjang(url, 6);
				profilePictureBox.Image = profileBitmap;
			}
		}

		//패널을 보여줄 때 사용, 내부함수
		private void ShowPanel(eSelectMenu eselect)
		{
			//if (inputTweet.Focused) return;
			if (eselect != eSelectMenu.USER && eselect == eselectMenu) return;//같은 패널이면 작동x
			if (loadTweetControl != null) return;//대화 불러오기 중이면 취소

			ActiveControl = null;
			Generate.SuspendDrawing(panel1);
			//panel1.SuspendLayout();
			HidePanels();
			eselectMenu = eselect;
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: homePanel.Show(); toolStrip_Panal.Text = "타임라인"; break;
				case eSelectMenu.MY_TL: myPanel.Show(); toolStrip_Panal.Text = "내 트윗"; break;
				case eSelectMenu.FAVORITE: favPanel.Show(); toolStrip_Panal.Text = "관심글"; break;
				case eSelectMenu.OPEN_URL: urlPanel.Show(); toolStrip_Panal.Text = "최근 열은 링크"; break;
				case eSelectMenu.USER: userPanel.Show(); toolStrip_Panal.Text = "유저 트윗"; break;
				case eSelectMenu.DM:
					dmPanel.Show();
					toolStrip_Panal.Text = "메시지";
					newDM.Hide();
					break;
				case eSelectMenu.MENTION:
					mentionPanel.Show(); toolStrip_Panal.Text = "멘션";
					newMention.Hide();
					break;
			}
			Generate.ResumeDrawing(panel1);
		}

		//모든 패널을 숨기는 함수
		private void HidePanels()
		{
			homePanel.Hide();
			mentionPanel.Hide();
			urlPanel.Hide();
			dmPanel.Hide();
			favPanel.Hide();
			if (myPanel != null) myPanel.Hide();
			if (userPanel != null)
			{
				userPanel.Hide();
				userPanel = null;
			}
		}

		//@ 입력 시 아이디 자동완성 박스 띄우는 기능
		private void ShowMentionBox()
		{
			if (mentionIndex == inputTweet.SelectionStart) return; //@막 입력했을 때는 체크 안 하게
			if (inputTweet.Text.Length == 0)
			{
				mentionListBox.Hide();
				return;
			}
			string id = string.Empty;
			//끝까지 혹은 스페이스 입력 전까지의 모든 문자열을 검색 해야함, 좌우 키로 아이디 변경 시 필요함
			try
			{
				int endIndex = inputTweet.Text.IndexOf(' ', mentionIndex) - 1;//실제 칸이 스페이스일경우 -1해야함
				if (endIndex < 0) endIndex = inputTweet.Text.Length - 1;//스페이스가 없을 경우는 더 문자가 없는 거라 끝index
				id = inputTweet.Text.Substring(mentionIndex, endIndex - mentionIndex + 1);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.Assert(false, e.ToString());
			}

			List<string> listString = new List<string>();
			Dictionary<long, UserSemi> dicUser = DataInstence.dicFollwing;
			foreach (UserSemi item in dicUser.Values)
			{
				listString.Add(item.screen_name + " / " + item.name);
			}

			List<string> find = listString.FindAll(s => s.IndexOf(id, StringComparison.CurrentCultureIgnoreCase) > -1);
			find.Sort();
			mentionListBox.Items.Clear();
			mentionListBox.Items.AddRange(find.ToArray());
			if (mentionListBox.Items.Count > 0)
			{
				mentionListBox.SelectedItem = mentionListBox.Items[0];
			}
		}

		//멘션 자동완성 박스 숨기는 기능
		private void HideMentionBox()
		{
			mentionListBox.Items.Clear();
			mentionIndex = 0;
			isMention = false;
			mentionListBox.Hide();
		}

		//아래 키 눌렀을 때 나갈 수 있는지 체크하는거
		private bool IsLeaveInputTweet()
		{
			bool ret = false;
			int indexText = this.inputTweet.GetLineFromCharIndex(this.inputTweet.SelectionStart);
			if (indexText == inputTweet.Lines.Length - 1 || inputTweet.Lines.Length == 0)//마지막줄일 경우
			{
				ret = true;
			}
			return ret;
		}

		//밖에서 인풋 박스로 들어갈 때 사용
		private void EnterInputTweet()
		{
			inputTweet.Focus();

			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE:			homePanel.인풋박스_들어갈때_색바꾸는함수();			break;
				case eSelectMenu.MENTION:			mentionPanel.인풋박스_들어갈때_색바꾸는함수();		break;
				case eSelectMenu.DM:					dmPanel.인풋박스_들어갈때_색바꾸는함수();			break;
				case eSelectMenu.FAVORITE:			favPanel.인풋박스_들어갈때_색바꾸는함수();				break;
				case eSelectMenu.MY_TL:				myPanel.인풋박스_들어갈때_색바꾸는함수();				break;
				case eSelectMenu.OPEN_URL:		urlPanel.인풋박스_들어갈때_색바꾸는함수();				break;
				case eSelectMenu.USER:
					if (userPanel!=null)
						userPanel.인풋박스_들어갈때_색바꾸는함수();
					break;
			}

		}

		//메뉴&드래그로 파일 추가 할 때 사용
		private void AddFile(string[] listFile)
		{
			int addedFilesCount = listFiles.Count;
			for (int i = 0; i < listFile.Length; i++, addedFilesCount++)
			{
				Bitmap temp = new Bitmap(listFile[i]);
				listPictureBox[addedFilesCount].Image = temp;
				//listPictureBox[addedFilesCount].Load(listFile[i]);
				Picture pic = new Picture(listFile[i], temp);
				listFiles.Add(pic);
			}
		}

		//클립보드에서 추가하는 거!
		private void AddFile(Image image)
		{
			if (listFiles.Count < 4)
			{
				Bitmap bitmap = image as Bitmap;
				Picture pic = new Picture(string.Empty, bitmap);
				listPictureBox[listFiles.Count].Image = bitmap;
				listFiles.Add(pic);
			}
			else
			{
				ShowMessageBox("이미지를 더 이상 추가할 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		//외부에서 트윗 입력창에 포커스 둘 때 사용
		public void EnterInput()
		{
			EnterInputTweet();
		}

		//esc를 누르거나 트윗을 보냈을 때 트윗 입력칸 초기화
		//isClear: 전송이 아닌, esc같은 거로 취소할 때 부르는 함수. bitmap을 전송부에서 dispose하기 때문
		public void ClearInputBox(bool isClear)
		{
			inputTweet.Text = string.Empty;
			countLabel.Text = "(0/140)";
			replyTweet = null;
			tweetButton.Text = "트윗 하기";
			for (int i = 0; i < listFiles.Count; i++)//업로드 이미지 청소
			{
				listPictureBox[i].Image = null;
				if(isClear)
					listFiles[i].bitmap.Dispose();
			}
			listFiles.Clear();
			if (isAddedGifAndVideo)
			{
				listPictureBox[0].Image = null;
				isAddedGifAndVideo = false;
				pathGifAndVideo = string.Empty;
			}
			ActiveControl = null;
			//GetSelectControl();
		}

		//키 입력 시 스크롤바 생성관련 부분
		//TODO, 나중에 크기가 늘어나게 할지 스크롤로 할지 고민좀
		private void inputTweet_TextChanged(object sender, EventArgs e)
		{
		
			if (inputTweet.ScrollBars == ScrollBars.None)
			{
				if (inputTweet.Lines.Length > 3)
					inputTweet.ScrollBars = ScrollBars.Vertical;
			}
			else if (inputTweet.ScrollBars == ScrollBars.Vertical)
			{
				if (inputTweet.Lines.Length < 3)
					inputTweet.ScrollBars = ScrollBars.None;
			}
			if (inputTweet.Text.Length == 0)
			{
				//if(replyTweet!=null)
				//{
				//	replyTweet = null;
				//	tweetButton.Text = "트윗 하기";
				//}
				countLabel.Text = "(0/140)";
			}
			else
			{
				MatchCollection mt = UrlMatch.Matches(inputTweet.Text);
				tweetLength = 0;
				for (int i = 0; i < mt.Count; i++)
					tweetLength += mt[i].Length;
				
				tweetLength = inputTweet.Text.Length - tweetLength + 23 * mt.Count - inputTweet.Lines.Length + 1;
				if (tweetLength > 140)
					inputTweet.BackColor = Color.FromArgb(0xff, 0xa7, 0xa7);
				else
					inputTweet.BackColor = Color.White;

				countLabel.Text = $"({tweetLength}/140)";
			}
		}

		//아이디 자동완성 기능에서 멘션 ID 추가할때 사용
		private void AddMentionID()
		{
			if (mentionListBox.Items.Count == 0)
			{
				HideMentionBox();
				return;
			}

			string mentionId = mentionListBox.SelectedItem.ToString();
			mentionId = mentionId.Remove(mentionId.IndexOf('/'));
			int endIndex = inputTweet.Text.IndexOf(' ', mentionIndex);
			if (endIndex == -1) endIndex = inputTweet.Text.Length - 1;
			StringBuilder sb = new StringBuilder(inputTweet.Text);
			sb.Remove(mentionIndex, endIndex - mentionIndex + 1);
			sb.Insert(mentionIndex, mentionId);
			inputTweet.Text = sb.ToString();
			inputTweet.SelectionStart = inputTweet.Text.Length;

			HideMentionBox();

			inputTweet.Focus();
		}

		//트윗 전송 시 호출
		private void SendTweet()
		{
			if (listFiles.Count == 0 && inputTweet.Text.Length == 0) return;
			//bool isSend = true;
			bool isDm = false;
			if(inputTweet.Text.Length>2)
			{
				if(inputTweet.Text[0] == 'd' && inputTweet.Text[1]==' ')
				{
					isDm = true;
				}
			}
			if (DataInstence.option.isYesnoTweet)
			{
				DialogResult result;
				if (isDm)
				{
					result = MessageBox.Show(this, "트윗을 등록 하시겠습니까?",
									"등록한다", MessageBoxButtons.YesNo, MessageBoxIcon.None);
				}
				else
				{
					result = MessageBox.Show(this, "쪽지를 보내시겠습니까?",
									"보낸다", MessageBoxButtons.YesNo, MessageBoxIcon.None);
				}
				if (result == DialogResult.No)
					return;
			}
			if(isDm)
			{
				bool isSendDM = true;
				int index = inputTweet.Text.IndexOf(' ');
				if (inputTweet.Text.Length > index)
					index = inputTweet.Text.IndexOf(' ', index + 1);
				else
					isSendDM = false;
				if (index == -1)
					isSendDM = false;
				else
				{
					if (inputTweet.Text.Length - 1 <= index)
						isSendDM = false;
				}
				if (isSendDM == false)
				{
					MessageBox.Show(this, "쪽지에 내용이 없습니다.\r이미지만 보내기는 안 됩니다.",
								   "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}

			if (tweetLength > 140 && isDm == false)
			{
				MessageBox.Show(this, "트윗이 140자를 넘어 전송할 수 없습니다.",
								   "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			ParameterUpdate parameter = new ParameterUpdate();
			parameter.status = inputTweet.Text;
			if (replyTweet != null)
			{
				parameter.in_reply_to_status_id = replyTweet.id.ToString();
			}
			if (isAddedGifAndVideo)
			{
				ClientInctence.SendMultimedia(parameter, pathGifAndVideo);
				ClearInputBox(true);
			}
			else
			{
				List<Bitmap> files = new List<Bitmap>();
				for (int i = 0; i < listFiles.Count; i++)
					files.Add(listFiles[i].bitmap);
				ClientInctence.SendTweet(parameter, files.ToArray());
				ClearInputBox(false);
			}
		}

		//파일 미리보기 추가 할 때 사용
		private class Picture
		{
			public Picture(string path, Bitmap bitmap)
			{
				this.path = path;
				this.bitmap = bitmap;
			}
			public string path { get; set; }
			public Bitmap bitmap { get; set; }
		}

		private void 관심글저장도구ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TwitterClient.Forms.FavoriteForm form = new Forms.FavoriteForm();
			form.Show();
		}

		private void 현재계정삭제ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClearMainform();
			ClientInctence.ClearClient();
			string delScreenName = FileInstence.DeleteSelectAccount();
			if (string.IsNullOrEmpty(delScreenName)) return;

			foreach (ToolStripItem item in 계정선택AToolStripMenuItem.DropDownItems)
			{
				if (item.Text == delScreenName)
				{
					item.Dispose();
					break;
				}
			}
		}
	}//mainform


	

}
