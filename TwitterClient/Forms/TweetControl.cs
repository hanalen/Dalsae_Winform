using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public enum eTweet
	{
		NONE,
		HOME,
		MENTION,
		RETWEET,
		DM,
		HIGHRIGHT,
		DEHWA,
	}

	public partial class TweetControl : UserControl
	{
		private static readonly object lockObject = new object();

		public delegate void DeleEnter();
		public delegate void DeleLeave();
		public delegate void DeleUpdateFavorite(ClientTweet tweet, bool isCreateFav);
		public delegate void DeleUpdateRetweet(bool isRetweet);
		public delegate void DeleDeleteTweet();
		public delegate void DeleChangeFont();
		public delegate void DeleChangeSkin(Color color);
		public delegate void DeleChangeTweet(ClientTweet tweet);
		private Color myClolor;
		private Bitmap profileBitmap;
		private Bitmap retweetBitmap;

		private List<ToolStripMenuItem> listStripItem = new List<ToolStripMenuItem>();
		public eTweet etweet { get; private set; }
		public bool isDM { get; private set; } = false;
		public ClientTweet tweet { get; private set; }
		public ClientDirectMessage clientdm { get; private set; }
		public TweetControl parentControl { get; set; }
		private TweetControl _childControl;
		private TweetControl _quoteControl;
		public TweetControl quoteControl { get { return _quoteControl; } set { SetQuoteControl(value); } }
		public TweetControl childControl { get { return _childControl; } set { SetChildControl(value); } }

		private void SetQuoteControl(TweetControl control)
		{
			_quoteControl = control;
			if (control == null)
				replyLabel.Text = "↪";
			else
				replyLabel.Text = "↓";
		}

		private void SetChildControl(TweetControl control)
		{
			_childControl = control;
			if (control == null)
				replyLabel.Text = "↪";
			else
				replyLabel.Text = "↓";
		}

		public TweetControl(ClientTweet tweet, int width, Color color, eTweet etweet)
		{
			InitializeComponent();
			HideControls();
			
			if (tweet == null) return;

			this.tweet = tweet;
			
			//Size = new Size(width, Height);

			Anchor = (AnchorStyles.Left);
			this.etweet = etweet;

		


			if (tweet.isRetweet&&tweet.isMention)
				this.etweet = eTweet.MENTION;

			ChangeSkin(color);
			SetUrlAndTLMenu();
			SetMenuEnable();
			if (tweet.isRetweet)//일반트윗 표시
				ShowRetweet();
			else//리트윗 표시
				ShowTweet(); 
			SetFont();
			ControlResize(width);
			SetHashtags();

			if (DataInstence.CheckIsMe(tweet.user.id) == false)
				삭제ToolStripMenuItem.Enabled = false;
		}

		public TweetControl(ClientDirectMessage clientdm, int width, Color color)
		{
			InitializeComponent();
			HideControls();

			this.clientdm = clientdm;
			isDM = true;
			ChangeSkin(color);
			//Size = new Size(width, Height);
			Anchor = (AnchorStyles.Left);

			SetDMEnable();
			ShowDirectMessage(clientdm);

			SetUrlAndTLMenuDM();
			ControlResize(width);
			삭제ToolStripMenuItem.Enabled = false;
			웹에서보기ToolStripMenuItem.Enabled = false;
			SetFont();
		}

		private void HideControls()
		{
			TabStop = false;
			retweetProfilePicture.Hide();
			retweetNameLabel.Hide();
			gongsikPictureBox.Hide();
			lockPictureBox.Hide();
			textFav.Hide();
			textRT.Hide();
			replyLabel.Hide();
		}
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------직접 함수--------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		public void ControlLeave(bool isClear)//인풋박스 들어갈 때 색 남기는 용도
		{
			if(isClear)
				BackColor = myClolor;
			else
				BackColor = DataInstence.skin.leaveColor;
		}

		public void ChangeTweet(ClientTweet tweet)
		{
			this.tweet = tweet;
			HideControls();
			if (tweet.isRetweet)
				ShowRetweet();
			else
				ShowTweet();
		}

		public void ChangeSkin(Color backColor)
		{
			BackColor = myClolor = backColor;
			replyLabel.ForeColor = DataInstence.skin.mention;
			nameLabel.ForeColor = DataInstence.skin.tweet;

			if (etweet == eTweet.MENTION || etweet == eTweet.HIGHRIGHT)
				tweetLabel.ForeColor = DataInstence.skin.mention;
			else
				tweetLabel.ForeColor = DataInstence.skin.tweet;
			if (isDM == false)
				if (tweet.isRetweet)
					tweetLabel.ForeColor = DataInstence.skin.retweet;
		}


		//관심글 업데이트 함수
		//tweet: 관심글이 된 트윗, isCreateFav: true-관심글/false-해제
		public void UpdateFav(ClientTweet tweet, bool isCreateFav)//delegate로 구현 해야함
		{
			this.tweet = tweet;
			tweet.originalTweet.favorited = isCreateFav;
			if (tweet.isRetweet)
				ShowRetweet();
			else
				ShowTweet();
			//if (tweet.retweeted_status == null)//일반트윗 표시
			//{
			//	tweet.favorited = isCreateFav;
			//	ShowTweet();
			//}
			//else//리트윗 표시
			//{
			//	tweet.retweeted_status.favorited = isCreateFav;
			//	ShowRetweet();
			//}
		}

		//리트윗, 리트윗 취소 시 rt아이콘 표시/리트윗여부 flag설정
		//isRetweet: true-리트윗/false-취소
		public void UpdateRetweet(bool isRetweet)//delegate로 구현해야함
		{
			tweet.originalTweet.retweeted = isRetweet;
			//tweet.isRetweet = isRetweet;
			//if (this.tweet.retweeted_status == null)//일반트윗일 경우 업데이트
			//	this.tweet.retweeted = isRetweet;
			//else//리트윗일 경우 업데이트
			//	this.tweet.retweeted_status.retweeted = isRetweet;

			if (isRetweet)
				textRT.Show();
			else
				textRT.Hide();
		}

		//우클릭 했을 때 보여줄 메뉴
		public void ShowContextMenu()
		{
			contextMenuStrip1.Show(this, new Point(10, 10));
		}


		//모든 컨트롤에서 클릭 시 선택 되게 함
		private void TweetControl_Click(object sender, EventArgs e)
		{
			//ClientInctence.Debug(tweet.id+"\r");
			MouseEventArgs me = (MouseEventArgs)e;
			if (me.Button == MouseButtons.Left || me.Button == MouseButtons.Right)
			{
				Select();
			}
		}

		public void DeleteTweet()
		{
			tweetLabel.Font = new Font(tweetLabel.Font, FontStyle.Strikeout);
			textRT.Hide();
			tweet.originalTweet.retweeted = false;
			//if (tweet.retweeted == false)
			//{
			//	textRT.Hide();
			//	tweet.retweeted = false;
			//}
			//else if (tweet.retweeted_status != null)
			//{
			//	if (tweet.retweeted_status.retweeted)
			//	{
			//		textRT.Hide();
			//		tweet.retweeted = false;
			//	}
			//}
		}

		public void ChangeFont()
		{
			SetFont();
		}

		//오브젝트 파괴 시 인장 관련 클리어 작업
		public void Clear()
		{
			if (isDM)
			{
				//profileBitmap.Dispose();
				profilePicture.Image = null;
				//InjangManager.GetInstence().DeleteInjang(clientdm.sender.profile_image_url);
			}
			else
			{
				profilePicture.Image = null;
				retweetProfilePicture.Image = null;
				//if (tweet.retweeted_status == null)
				//{
				//	//profileBitmap.Dispose();
				//	profilePicture.Image = null;
				//	//InjangManager.GetInstence().DeleteInjang(tweet.user.profile_image_url);
				//}
				//else
				//{
				//	//profileBitmap.Dispose();
				//	//retweetBitmap.Dispose();

				//	profilePicture.Image = null;
				//	retweetProfilePicture.Image = null;

				//	//InjangManager.GetInstence().DeleteInjang(tweet.user.profile_image_url);
				//	//InjangManager.GetInstence().DeleteInjang(tweet.retweeted_status.user.profile_image_url);
				//}
			}
		}

		public void CopyTweet()
		{
			string copy = string.Empty;
			if (isDM)
			{
				copy = clientdm.text;
				if (clientdm.entities.urls.Count != 0)
					for (int i = 0; i < clientdm.entities.urls.Count; i++)
						copy = copy.Replace(clientdm.entities.urls[i].display_url, clientdm.entities.urls[i].expanded_url);
			}
			else
			{
				copy = string.Empty;
				copy = Generate.ReplaceTextExpend(tweet.originalTweet);
			}

			if (copy != string.Empty)
			{
				copy = copy.Replace("\n", Environment.NewLine);
				Clipboard.SetText(copy);
				ClientInctence.ShowMessageBox("트윗 내용이 클립보드에 복사되었습니다.", "알림", MessageBoxIcon.Information);
			}
		}

		//private string ReplaceText(ClientTweet tweet)
		//{
		//	string ret = tweet.text;
		//	if(tweet.isUrl)
		//		for (int i = 0; i < tweet.listUrl.Count; i++)
		//			ret = ret.Replace(tweet.listUrl[i].display_url, tweet.listUrl[i].expanded_url);

		//	if(tweet.isMedia)
		//		foreach(ClientMedia item in tweet.dicMedia.Values)
		//			ret = ret.Replace(item.display_url, item.expanded_url);
			
		//	return ret;
		//}

		//private string ReplaceURL(ClientEntities entities, ClientTweet tweet)
		//{
		//	string ret = tweet.text;
		//	if (entities.urls != null)//url이 있을 경우 변경
		//		for (int i = 0; i < entities.urls.Count; i++)
		//			ret = tweet.text.Replace(entities.urls[i].display_url, entities.urls[i].expanded_url);

		//	return ret;
		//}

		public void ControlResize(int parentWidth)
		{
			tweetLabel.MaximumSize = new Size(parentWidth - 100, 0);//이방식은 매우 느림
			//dateLabel.MaximumSize = new Size(parentWidth - 100, 0);//날짜 라벨도 필요

			if (isDM)
				Size = new Size(parentWidth, 42 + tweetLabel.Size.Height + dateLabel.Height);
			else if (tweet.isRetweet)
				Size = new Size(parentWidth, 42 + tweetLabel.Size.Height + retweetProfilePicture.Height + dateLabel.Height);
			else
				Size = new Size(parentWidth, 42 + tweetLabel.Size.Height + dateLabel.Height);
		}

		private void SetDMEnable()
		{
			리트윗ToolStripMenuItem.Enabled = false;
			관심글ToolStripMenuItem.Enabled = false;
			삭제ToolStripMenuItem.Enabled = false;
			리트윗끄기ToolStripMenuItem.Enabled = false;

			if (clientdm.entities == null)
				if (clientdm.entities.urls.Count == 0)
					URLToolStripMenuItem.Enabled = false;
				else
					URLToolStripMenuItem.Enabled = true;
		}

		private void SetUrlAndTLMenuDM()
		{
			AddUrl(clientdm.entities.urls);

			ToolStripMenuItem item = new ToolStripMenuItem(clientdm.sender.screen_name);
			item.Click += new EventHandler(UserTL_MenuStripClick);
			유저타임라인ToolStripMenuItem.DropDownItems.Add(item);

			ToolStripMenuItem item2 = new ToolStripMenuItem(clientdm.recipient.screen_name);
			item2.Click += new EventHandler(UserTL_MenuStripClick);
			유저타임라인ToolStripMenuItem.DropDownItems.Add(item2);
			listStripItem.Add(item);
			listStripItem.Add(item2);
		}

		//URL정보를 갖고 있을 경우 URL메뉴에 URL을 띄우기 위한 함수
		private void SetUrlAndTLMenu()
		{
			AddUrl(tweet.listUrl);
			AddMedia(tweet.dicMedia);
			//if (tweet.retweeted_status == null)
			//	AddUrl(tweet.dicUrl);
			//else
			//	AddUrl(tweet.retweeted_status.dicUrl);

			HashSet<string> hashUser = new HashSet<string>();

			//if (tweet.retweeted_status != null)//리트윗
			//{
			hashUser.Add(tweet.user.screen_name);//리트윗 유저
			hashUser.Add(tweet.originalTweet.user.screen_name);//리트윗 원본 작성자
			foreach (string name in tweet.hashMention)
				hashUser.Add(name);
			//for (int i = 0; i < tweet.hashMention.Count; i++)//리트윗 트윗에 낑긴 떼멘
			//	hashUser.Add(tweet.hashMention[i].screen_name);
			//}
			//else//일반
			//{
			//	listUser.Add(tweet.user.screen_name);//트윗 쓴 사람
			//	for (int i = 0; i < tweet.originalTweet.entities.user_mentions.Count; i++)//떼멘에 낑긴 사람들
			//		listUser.Add(tweet.entities.user_mentions[i].screen_name);
			//}

			foreach(string name in hashUser)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(name);
				item.Click += new EventHandler(UserTL_MenuStripClick);
				유저타임라인ToolStripMenuItem.DropDownItems.Add(item);

				ToolStripMenuItem item2 = new ToolStripMenuItem(name);
				item2.Click += new EventHandler(UserMute_MenuStripClick);
				유저뮤트ToolStripMenuItem.DropDownItems.Add(item2);
				listStripItem.Add(item);
				listStripItem.Add(item2);
			}
			//for (int i = 0; i < hashUser.Count; i++)//메뉴 아이템 추가
			//{
			//	ToolStripMenuItem item = new ToolStripMenuItem(hashUser.);
			//	item.Click += new EventHandler(UserTL_MenuStripClick);
			//	사용자타임라인ToolStripMenuItem.DropDownItems.Add(item);
			//}
		}

		private void AddUrl(List<ClientURL> listUrl)
		{
			for (int i = 0; i < listUrl.Count; i++)//URL이 있을 경우 메뉴에 추가
			{
				ToolStripMenuItem item = new ToolStripMenuItem(listUrl[i].display_url);
				item.Click += new EventHandler(URL_MenuStripClick);
				URLToolStripMenuItem.DropDownItems.Add(item);
				listStripItem.Add(item);
			}
		}

		private void AddMedia(List<ClientMedia> listMedia)
		{
			for (int i = 0; i < listMedia.Count; i++)//URL이 있을 경우 메뉴에 추가
			{
				if (listMedia[i].type != "photo")
				{
					ToolStripMenuItem item = new ToolStripMenuItem(listMedia[i].display_url);
					item.Click += new EventHandler(URL_MenuStripClick);
					URLToolStripMenuItem.DropDownItems.Add(item);
					listStripItem.Add(item);
				}
			}
		}

		private void AddMedia(Dictionary<string, ClientMedia> dicMedia)//일반용
		{
			foreach (ClientMedia media in dicMedia.Values)//URL이 있을 경우 메뉴에 추가
			{
				if (media.type != "photo")
				{
					ToolStripMenuItem item = new ToolStripMenuItem(media.display_url);
					item.Click += new EventHandler(URL_MenuStripClick);
					URLToolStripMenuItem.DropDownItems.Add(item);
					listStripItem.Add(item);
				}
			}
		}

		private void SetFont()
		{
			Option option = DataInstence.option;
			tweetLabel.Font = option.font;
			retweetNameLabel.Font = option.dateFont;
			dateLabel.Font = option.font;
			nameLabel.Font = DataInstence.option.boldFont;
			tweetLabel.Location = new Point(tweetLabel.Location.X, nameLabel.Size.Height+5);//(int)(option.font.Size * 0.9));
			dateLabel.Location = new Point(dateLabel.Location.X, Bottom - option.font.Height + 2);
		}

		//플텍이면 리트윗 비활성화, url이 없으면 url칸 비활성화 등
		//조리돌림으로 갑자기 플텍 걸 경우가 문제가 생길 수 있으나 상관없을듯
		private void SetMenuEnable()
		{
			if (tweet.isRetweet)//일반 트윗
			{
				리트윗ToolStripMenuItem.Enabled = true;
			}
			else//리트윗은 항상 true어야됨
			{
				리트윗ToolStripMenuItem.Enabled = true;
			}
			if (tweet.listUrl.Count == 0 && tweet.dicMedia.Count == 0)
			{
				URLToolStripMenuItem.Dispose();
				toolstripStripeURL.Dispose();
				이미지ToolStripMenuItem.Dispose();
			}
			else if (tweet.dicMedia.Count == 0)
				이미지ToolStripMenuItem.Dispose();
			else
			{
				bool isShowUrl = false;
				bool isShowPhoto = false;
				foreach(ClientMedia item in tweet.dicMedia.Values)
				{
					if (item.type == "photo")
					{
						이미지ToolStripMenuItem.Text = item.display_url;
						isShowPhoto = true;
					}
					else
						isShowUrl = true;
				}
				if (isShowUrl == false && tweet.listUrl.Count == 0)
					URLToolStripMenuItem.Dispose();
				if (isShowPhoto == false)
					이미지ToolStripMenuItem.Dispose();
			}

			if (tweet.lastEntities.hashtags.Count == 0)
			{
				해시태그ToolStripMenuItem.Dispose();
				hashStrip.Dispose();
			}
		}

		private void ShowDirectMessage(ClientDirectMessage clientdm)
		{
			DateTime date = (DateTime)clientdm.created_at;
			dateLabel.Text = date.ToString("yyyy년 MMM월 dd일 dddd HH:mm:ss");
			tweetLabel.Text = clientdm.text;
			nameLabel.Text = string.Format("From {0} / {1} To {2} / {3}", clientdm.sender.screen_name, clientdm.sender.name,
									clientdm.recipient.screen_name, clientdm.recipient.name);
			lock (lockObject)
			{
				profileBitmap = InjangManager.GetInstence().GetInjang(clientdm.sender.profile_image_url, 6);
				profilePicture.Image = profileBitmap;
			}
		}

		private void ShowTweet()
		{
			if (tweet == null) return;

			nameLabel.Text = tweet.user.screen_name + " / " + tweet.user.name;
			tweetLabel.MaximumSize = new Size(Width - 100, 0);
			tweetLabel.Text = tweet.text;
			
			DateTime date = (DateTime)tweet.created_at;
			dateLabel.Text = date.ToString("yyyy년 MMM월 dd일 dddd HH:mm:ss") + " / " + "via " + tweet.source;

			if (tweet.isReply || tweet.isQTRetweet)
				replyLabel.Show();

			if (tweet.favorited)
				textFav.Show();
			else
				textFav.Hide();
			if (tweet.retweeted)
				textRT.Show();
			else
				textRT.Hide();

			if (tweet.user.verified)
			{
				gongsikPictureBox.Parent = profilePicture;
				gongsikPictureBox.BackColor = Color.Transparent;
				gongsikPictureBox.Location = new Point(43, 30);
				gongsikPictureBox.Show();
			}
			if (tweet.user.Protected)
				lockPictureBox.Show();
			lock (lockObject)
			{
				profileBitmap = InjangManager.GetInstence().GetInjang(tweet.user.profile_image_url, 6);
				profilePicture.Image = profileBitmap;
			}
		}

		private void ShowRetweet()
		{
			nameLabel.Text = tweet.originalTweet.user.screen_name + " / " + tweet.originalTweet.user.name;
			tweetLabel.MaximumSize = new Size(Width - 100, 0);
			tweetLabel.Text = tweet.originalTweet.text;
			
			DateTime date = (DateTime)tweet.originalTweet.created_at;
			dateLabel.Text = date.ToString("yyyy년 MMM월 dd일 dddd HH:mm:ss") + " / " + "via " + tweet.originalTweet.source;
			retweetNameLabel.Text = "Retweet By " + tweet.user.screen_name + " / " + tweet.user.name;
			retweetNameLabel.Show();

			if (tweet.isReply || tweet.isQTRetweet)
				replyLabel.Show();

			if (tweet.originalTweet.favorited)
				textFav.Show();
			else
				textFav.Hide();
			if (tweet.originalTweet.retweeted)
				textRT.Show();
			else
				textRT.Hide();

			if (tweet.originalTweet.user.verified)
				gongsikPictureBox.Show();
			lock (lockObject)
			{
				retweetBitmap = InjangManager.GetInstence().GetInjang(tweet.user.profile_image_url, 2);
				retweetProfilePicture.Image = retweetBitmap;
				retweetProfilePicture.Show();


				profileBitmap = InjangManager.GetInstence().GetInjang(tweet.originalTweet.user.profile_image_url, 6);
				profilePicture.Image = profileBitmap;
			}
		}

		private void SetHashtags()
		{
			List<ClientHashtag> listHashtag = tweet.lastEntities.hashtags;
			for (int i = 0; i < listHashtag.Count; i++)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(listHashtag[i].text);
				item.Click += new EventHandler(HashTag_MenuStripClick);
				해시태그ToolStripMenuItem.DropDownItems.Add(item);
				listStripItem.Add(item);
			}
		}

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------이벤트 처리-------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Right:
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
				case Keys.Tab:
					return true;
					//default:
					//	return true;
					//case Keys.Shift | Keys.Right:
					//case Keys.Shift | Keys.Left:
					//case Keys.Shift | Keys.Up:
					//case Keys.Shift | Keys.Down:
					//	return true;
			}
			return base.IsInputKey(keyData);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			switch (e.KeyCode)
			{
				case Keys.Left:
				case Keys.Right:
				case Keys.Up:
				case Keys.Down:
					if (e.Shift)
					{

					}
					else
					{
					}
					break;
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Select();
			base.OnMouseDown(e);
		}

		private void TweetControl_Enter(object sender, EventArgs e)
		{
			BackColor = DataInstence.skin.select;
			tweetLabel.ForeColor = DataInstence.skin.tweet;
			ClientInctence.SelectControl(this);
		}

		private void TweetControl_Leave(object sender, EventArgs e)
		{
			if (isDM)
				tweetLabel.ForeColor = DataInstence.skin.tweet;
			else
			{
				if (tweet.isRetweet)
					tweetLabel.ForeColor = DataInstence.skin.retweet;
				else
					tweetLabel.ForeColor = DataInstence.skin.tweet;
			}
			if (etweet == eTweet.MENTION || etweet == eTweet.HIGHRIGHT)
				tweetLabel.ForeColor = DataInstence.skin.mention;
			else if (etweet == eTweet.RETWEET)
				tweetLabel.ForeColor = DataInstence.skin.retweet;
			BackColor = myClolor;
		}

		private void UserTL_MenuStripClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item == null) return;

			ClientInctence.LoadUserTweet(item.Text);
		}

		private void UserMute_MenuStripClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item == null) return;

			DataInstence.option.AddMuteUser(item.Text);
			ClientInctence.ShowMessageBox($"{item.Text}유저가 뮤트목록에 추가되었습니다",
										"알림", MessageBoxIcon.Information);
		}

		//URL 클릭 할 경우 웹페이지를 열어주는 함수
		private void URL_MenuStripClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item == null) return;
			if (isDM)
				ClickURL_DM(item);
			else
				ClickURL(tweet, item);
		}

		private void HashTag_MenuStripClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item == null) return;
			ClientInctence.AddHashTag(item.Text);
		}

		private void ClickURL_DM(ToolStripItem item)
		{
			for (int i = 0; i < clientdm.entities.media.Count; i++)
			{
				if (clientdm.entities.media[i].display_url == item.Text && clientdm.entities.media[i].type == "photo")
				{
					ImageForm form = new ImageForm();
					form.SetImageURL(clientdm.entities.media, true);
					form.Show();
					ImageForm.DeleLoadImage dele = new ImageForm.DeleLoadImage(form.LoadImage);
					form.BeginInvoke(dele);
					return;
				}
			}

			for (int i = 0; i < clientdm.entities.urls.Count; i++)
				if (clientdm.entities.urls[i].display_url == item.Text)
					System.Diagnostics.Process.Start(clientdm.entities.urls[i].expanded_url);
		}

		private void ClickURL(ClientTweet _tweet, ToolStripMenuItem item)
		{
			List<ClientMedia> listMedia = new List<TwitterClient.ClientMedia>();
			for (int i = 0; i < _tweet.mediaEntities.media.Count; i++)
				if (_tweet.mediaEntities.media[i].display_url == item.Text && _tweet.mediaEntities.media[i].type == "photo")
					listMedia.Add(_tweet.mediaEntities.media[i]);

			if (listMedia.Count>0)
			{
				ImageForm form = new ImageForm();

				form.SetImageURL(listMedia, false);
				form.Show();
				ImageForm.DeleLoadImage dele = new ImageForm.DeleLoadImage(form.LoadImage);
				form.BeginInvoke(dele);
				return;
			}
			

			bool isOpenUrl = false;
			foreach (ClientURL url in _tweet.listUrl)
			{
				if (item.Text == url.display_url)
				{
					List<ClientTweet> listTweet = new List<ClientTweet>();
					listTweet.Add(_tweet);
					ClientInctence.AddOpenURL(listTweet);
					System.Diagnostics.Process.Start(url.expanded_url);
					isOpenUrl = true;
					break;
				}
			}

			if (isOpenUrl) return;

			foreach (ClientMedia media in _tweet.dicMedia.Values)
			{
				if (item.Text == media.display_url)
				{
					List<ClientTweet> listTweet = new List<ClientTweet>();
					listTweet.Add(_tweet);
					ClientInctence.AddOpenURL(listTweet);
					System.Diagnostics.Process.Start(media.expanded_url);
					break;
				}
			}
		}

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------메뉴 작동---------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		private void 리트윗ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.Retweet(tweet);
		}

		private void 관심글ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.Favorites(tweet);
		}

		private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.DeleteTweet(tweet.originalTweet);
			//if (tweet.retweeted_status == null)
			//	ClientInctence.DeleteTweet(tweet);
			//else
			//	ClientInctence.DeleteTweet(tweet.retweeted_status);
		}

		private void contextMenuStrip_Opening(object sender, EventArgs e)
		{
			if (isDM == false)
			{
				if (DataInstence.hashRetweetOff.Contains(tweet.user.id))
					리트윗끄기ToolStripMenuItem.Text = $"{tweet.user.screen_name}의 리트윗 켜기";
				else
					리트윗끄기ToolStripMenuItem.Text = $"{tweet.user.screen_name}의 리트윗 끄기";
			}
			Select();
		}

		private void 복사ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyTweet();
		}

		private void 쪽지DMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (isDM)
			{
				ClientInctence.AddDMId(clientdm.sender.screen_name);
			}
			else
			{
				ClientInctence.AddDMId(tweet.originalTweet.user.screen_name);
				//if (tweet.retweeted_status == null)
				//	ClientInctence.AddDMId(tweet.user.screen_name);
				//else
				//	ClientInctence.AddDMId(tweet.retweeted_status.user.screen_name);
			}
		}

		private void tweetControl_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				TweetControl_Enter(null, null);
		}

		private void 웹에서보기ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = string.Empty;
			//if (tweet.retweeted_status == null)
			//	url = $"https://twitter.com/{tweet.user.screen_name}/status/{tweet.id}";
			//else
				url = $"https://twitter.com/{tweet.originalTweet.user.screen_name}/status/{tweet.originalTweet.id}";
			System.Diagnostics.Process.Start(url);
		}

		private void 입력하기ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.EnterInputTweet();
		}

		private void 답글ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.AddMentionIDS(false, tweet);
		}

		private void 모두에게답글ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.AddMentionIDS(true, tweet);
		}

		private void reply_MouseClick(object sender, MouseEventArgs e)
		{
			if (_childControl == null)
				ClientInctence.LoadSingleTweet(this);
			else
			{
				//Select();
				ClientInctence.HideSingleTweet(this);
			}
		}

		private void 인용하기QTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.AddQTRetweet(tweet);
		}

		private void 리트윗끄기ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			ClientInctence.FollowingUpdate(tweet.user.id);
		}

		private void 클라이언트뮤트ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataInstence.option.AddMuteClient(tweet.originalTweet.source);
			ClientInctence.ShowMessageBox($"클라이언트 {tweet.originalTweet.source} 가 뮤트에 추가되었습니다.", "알림", MessageBoxIcon.Information);
		}
		~TweetControl()
		{
			for (int i = 0; i < listStripItem.Count; i++)
				listStripItem[i].Dispose();
			listStripItem.Clear();
			유저타임라인ToolStripMenuItem.DropDownItems.Clear();
			URLToolStripMenuItem.DropDownItems.Clear();
			해시태그ToolStripMenuItem.DropDownItems.Clear();
		}
	}//class
}