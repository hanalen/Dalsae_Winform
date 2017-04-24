using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TwitterClient.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public enum ePanel
	{
		HOME,
		MENTION,
		DM,
		FAVORITE,
		MY_TL,
		USER_TL,
		OPEN_URL,
	}



	public partial class FlowPanelManager
	{
		private static readonly object lockObject = new object();
		public TweetControl selectControl { get; private set; }
		private TweetControl colorControl;//인풋 박스 들어갈 때 색 바뀐놈

		public bool isShow { get; private set; } = true;
		private const int CON_MAX_TWEET = 100;
		private FlowLayoutPanelCustom flowPanel;
		private Panel panel = new Panel();
		private Dictionary<long, ClientTweet> dicTweet = new Dictionary<long, ClientTweet>();
		private Dictionary<long, ClientDirectMessage> dicDM = new Dictionary<long, ClientDirectMessage>();
		private List<TweetControl> listTweetControl = new List<TweetControl>();
		private bool isChangeColor = true;
		private bool isChangeColorDeahwa = true;
		private ePanel epanel;
		private LoadControl loadControl;
		private Rectangle panelRectangle;
		public delegate void DeleScroll(MouseEventArgs e);

		private VScrollBar scrollBar;// = new VScrollBar();

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------외부 기능--------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		//생성자, 생성자는 이거만 사용
		//mainForm: 메인폼, parent: flowLayoutPanel을 포함할 기본 패널, ePanel: 멘션/탐라/dm/유저탐라등 구분
		public FlowPanelManager(MainForm mainForm, Panel parent, ePanel epanel)
		{
			panel.Parent = parent;
			panel.Size = panel.Parent.Size;
			panel.Anchor = parent.Anchor;
			panel.BackColor = DataInstence.skin.blockOne;
			panel.AutoScroll = false;
			scrollBar = new VScrollBar();
			scrollBar.ValueChanged += ScrollBar_ValueChanged;
			panel.Controls.Add(scrollBar);
			//scrollBar.Parent = panel;
			scrollBar.Dock = DockStyle.Right;
			scrollBar.LargeChange = 350;
			scrollBar.SmallChange = 200;

			Point panelPoint = panel.PointToScreen(Point.Empty);
			panelRectangle = new Rectangle(panelPoint.X, panelPoint.Y, panel.Size.Width, panel.Size.Height);

			flowPanel = new FlowLayoutPanelCustom(new DeleScroll(MoveScroll));
			flowPanel.Parent = panel;
			flowPanel.AutoScroll = false;
			this.flowPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
			this.flowPanel.Location = new System.Drawing.Point(0, 0);
			this.flowPanel.Size = new System.Drawing.Size(parent.Width, 2);
			this.flowPanel.TabStop = false;
			this.flowPanel.TabIndex = 0;
			this.flowPanel.WrapContents = false;
			this.flowPanel.Location = new Point(0, 0);
			this.epanel = epanel;
			flowPanel.BackColor = DataInstence.skin.blockOne;
			switch (epanel)
			{
				case ePanel.HOME:				this.flowPanel.Name = "timeLinePanel";		break;
				case ePanel.MENTION:		this.flowPanel.Name = "mentionPanel";		break;
				case ePanel.DM:				this.flowPanel.Name = "dmPanel";				break;
				case ePanel.USER_TL:			this.flowPanel.Name = "userTLPanel";			break;
				case ePanel.OPEN_URL:		this.flowPanel.Name = "OpenURLPanel";		break;
			}
			if (epanel != ePanel.HOME && epanel != ePanel.DM && epanel != ePanel.OPEN_URL)
			{
				loadControl = new LoadControl(this, flowPanel.Width);
				flowPanel.Controls.Add(loadControl);
				flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + loadControl.Height);
			}
		}

		

		public void MoreLoadTweet()
		{
			if (listTweetControl.Count < 1) return;

			BaseParameter parameter = null;
			switch (epanel)
			{
				case ePanel.USER_TL:
					ParameterUserTimeLine userParam = new ParameterUserTimeLine();
					userParam.max_id = listTweetControl[0].tweet.id;
					userParam.screen_name = listTweetControl[0].tweet.user.screen_name;
					parameter = userParam;
					break;
				case ePanel.MENTION:
					ParameterMentionTimeLine mentionParam = new ParameterMentionTimeLine();
					mentionParam.max_id = listTweetControl[0].tweet.id;
					parameter = mentionParam;
					break;
				case ePanel.FAVORITE:
					ParameterFavoritesList favParam = new ParameterFavoritesList();
					favParam.max_id = listTweetControl[0].tweet.id;
					parameter = favParam;
					break;
			}
			ClientInctence.LoadMoreTweet(parameter);
			//listTweetControl[0].tweet.id
		}

		//public int GetLastControlHeight()
		//{
		//	if (listTweetControl.Count > 1)
		//		return listTweetControl[listTweetControl.Count - 1].Height;
		//	else
		//		return 0;
		//}

		

		public void ChangeSkin()
		{
			lock (lockObject)
			{
				for (int i = 0; i < listTweetControl.Count; i++)
				{
					TweetControl.DeleChangeSkin dele = new TweetControl.DeleChangeSkin(listTweetControl[i].ChangeSkin);
					if (listTweetControl[i].etweet == eTweet.DEHWA)
					{
						if (isChangeColorDeahwa)
							listTweetControl[i].BeginInvoke(dele, new object[] { DataInstence.skin.mentionOne });
						else
							listTweetControl[i].BeginInvoke(dele, new object[] { DataInstence.skin.mentionTwo });
						isChangeColorDeahwa = !isChangeColorDeahwa;
					}
					else
					{
						if (isChangeColor)
							listTweetControl[i].BeginInvoke(dele, new object[] { DataInstence.skin.blockOne });
						else
							listTweetControl[i].BeginInvoke(dele, new object[] { DataInstence.skin.blockTwo });
						isChangeColor = !isChangeColor;
					}
				}
			}
		}

		public void ShowContextMenu()
		{
			if (selectControl != null)
				selectControl.ShowContextMenu();
		}

		public void AddTweet(List<ClientTweet> listTweet, bool isMore, eTweet etweet)
		{
			List<ClientTweet> showTweet = new List<TwitterClient.ClientTweet>();
			lock (lockObject)
			{
				for (int i = 0; i < listTweet.Count; i++)
				{
					if (dicTweet.ContainsKey(listTweet[i].id) == false)//중복이 아닐 경우에만 추가 처리
					{
						listTweet[i].Init();
						if(isShowTweet(listTweet[i]))
						{
							showTweet.Add(listTweet[i]);
							dicTweet.Add(listTweet[i].id, listTweet[i]);
						}
						//if (listTweet[i].retweeted_status == null)//일반 트윗
						//{
						//	if (isShowTweet(listTweet[i]))//뮤트 체크
						//	{
						//		listTweet[i].Init();
						//		showTweet.Add(listTweet[i]);
						//		dicTweet.Add(listTweet[i].id, listTweet[i]);
						//	}
						//}
						//else//리트윗
						//{
						//	if (isShowTweet(listTweet[i].retweeted_status))//뮤트 체크
						//	{
						//		listTweet[i].Init();
						//		showTweet.Add(listTweet[i]);
						//		dicTweet.Add(listTweet[i].id, listTweet[i]);
						//	}
						//}
					}
				}
			}
			showTweet.Reverse();
			AddControl(showTweet, isMore, etweet);
		}

		//패널에 트윗을 추가 할 경우 사용, 중복 검사 후 추가 될 트윗을 AddContorl에 가져간다
		//listTweet: API콜 하고 받은 트윗 list
		public void AddTweet(List<ClientTweet> listTweet, eTweet etweet)
		{
			List<ClientTweet> showTweet = new List<TwitterClient.ClientTweet>();
			lock (lockObject)
			{
				for (int i = 0; i < listTweet.Count; i++)
				{
					if (dicTweet.ContainsKey(listTweet[i].id) == false)//중복이 아닐 경우에만 추가 처리
					{
						listTweet[i].Init();
						if (isShowTweet(listTweet[i]))//뮤트 체크
						{
							showTweet.Add(listTweet[i]);
							dicTweet.Add(listTweet[i].id, listTweet[i]);
						}
						//if (listTweet[i].retweeted_status == null)//일반 트윗
						//{
						//	if (isShowTweet(listTweet[i]))//뮤트 체크
						//	{
						//		listTweet[i].Init();
						//		showTweet.Add(listTweet[i]);
						//		dicTweet.Add(listTweet[i].id, listTweet[i]);
						//	}
						//}
						//else//리트윗
						//{
						//	if (isShowTweet(listTweet[i].retweeted_status))//뮤트 체크
						//	{
						//		listTweet[i].Init();
						//		showTweet.Add(listTweet[i]);
						//		dicTweet.Add(listTweet[i].id, listTweet[i]);
						//	}
						//}
					}
				}
			}
			AddControl(showTweet, false, etweet);
		}

		//유저 스트리밍에서 사용할 단일 트윗 추가용
		//tweet: 추가 될 트윗, return: 멘션함에 추가할지 여부
		public bool AddTweet(ClientTweet tweet, eTweet etweet)
		{
			bool ret = false;
			bool showTweet = false;
			bool isRetweeted = false;
			lock (lockObject)
			{
				DataInstence.UpdateFollowingName(tweet.user.id, tweet.user.name);
				if (dicTweet.ContainsKey(tweet.id) == false)//dictionary에 없을 경우 추가
				{
					tweet.Init();
					if (tweet.isRetweet)
					{
						showTweet = isShowTweet(tweet);
						if(showTweet)
						{
							if(DataInstence.CheckIsMe(tweet.originalTweet.user.id) &&
								DataInstence.option.isShowRetweet == false)
							{
								showTweet = false;
							}
						}
						ret = DataInstence.option.MatchHighlight(tweet.originalTweet.text);
						if (ret == false && DataInstence.option.isShowRetweet)//하이라이트가 아닐 경우 리트윗인지 체크
						{
							if(DataInstence.CheckIsMe(tweet.originalTweet.user.screen_name)&&
								DataInstence.option.isNotiRetweet)//멘션함으로 들어가는 여부 옵션 체크
							{
								ret = true;
								isRetweeted = true;
							}
							else
								ret = false;
						}
					}
					else
					{
						showTweet = isShowTweet(tweet);
						ret = DataInstence.option.MatchHighlight(tweet.text);
					}
				}

				if (showTweet)
				{
					//tweet.Init();
					if (ret == false)//하이라이트가 없을 경우 멘션인지 판단
						ret = tweet.isMention;
					dicTweet.Add(tweet.id, tweet);
					if (ret && isRetweeted == false)
						AddControl(tweet, eTweet.HIGHRIGHT);
					else if (tweet.isRetweet)
						AddControl(tweet, eTweet.RETWEET);
					else
						AddControl(tweet, etweet);
				}
				else
					ret = false;
			}
			return ret;
		}

		public void AddDirectMessage(ClientDirectMessage clientdm)
		{
			if (dicDM.ContainsKey(clientdm.id) == false)
			{
				dicDM.Add(clientdm.id, clientdm);
				AddControl(clientdm, eTweet.DM);
			}
		}

		public void ChangeFont()
		{
			lock (lockObject)
			{
				Generate.SuspendDrawing(flowPanel);
				//flowPanel.SuspendLayout();
				foreach (TweetControl control in listTweetControl)
				{
					TweetControl.DeleChangeFont dele = new TweetControl.DeleChangeFont(control.ChangeFont);
					control.BeginInvoke(dele);
				}
				Resize(false);
				Generate.ResumeDrawing(flowPanel);
			}
		}

		public void AddDirectMessage(List<ClientDirectMessage> listdm)
		{
			List<ClientDirectMessage> showdm = new List<TwitterClient.ClientDirectMessage>();
			lock (lockObject)
			{
				for (int i = 0; i < listdm.Count; i++)
				{
					if (dicDM.ContainsKey(listdm[i].id) == false)
					{
						showdm.Add(listdm[i]);
						dicDM.Add(listdm[i].id, listdm[i]);
					}
				}
			}
			AddControl(showdm, eTweet.DM);
		}

		//리트윗, 관심글을 사용하여 트윗의 정보가 바뀔 경우 UI갱신하기 위함
		//tweet: 정보가 바뀐 트윗
		public void UpdateFavorite(ClientTweet tweet, bool isCreateFav)
		{
			for (int i = 0; i < listTweetControl.Count; i++)
			{
				if (listTweetControl[i].tweet.originalTweet.id == tweet.id)
				{
					UpdateControlFavorite(listTweetControl[i], isCreateFav);
				}
			}
			Resize(false);
		}

		public void UpdateRetweet(ClientTweet tweet, bool isRetweet)
		{
			//tweet.Init();
			for (int i = 0; i < listTweetControl.Count; i++)
			{
				TweetControl updateCon = null;
				
				if (listTweetControl[i].tweet.retweeted_status == null)//일반트윗 오브젝트
				{
					if (listTweetControl[i].tweet.id == tweet.id)//취소
						updateCon = listTweetControl[i];
					else if (tweet.retweeted_status != null)
						if (listTweetControl[i].tweet.id == tweet.retweeted_status.id)//리트윗
							updateCon = listTweetControl[i];
				}
				else//리트윗 오브젝트
				{
					if (listTweetControl[i].tweet.retweeted_status.id == tweet.id)
						updateCon = listTweetControl[i];
					else if (tweet.retweeted_status != null)
						if (listTweetControl[i].tweet.retweeted_status.id == tweet.retweeted_status.id)
							updateCon = listTweetControl[i];
				}
				if (updateCon != null)
				{
					TweetControl.DeleUpdateRetweet dele = new TweetControl.DeleUpdateRetweet(updateCon.UpdateRetweet);
					updateCon.BeginInvoke(dele, new object[] { isRetweet });
				}
			}
			Resize(false);
		}

		
		public bool AddSingleTweet(TweetControl control, ClientTweet tweet)
		{
			bool ret = true;
			int index = listTweetControl.IndexOf(control);
			//ClientInctence.Debug(index);
			if (index == -1) return false;
			tweet.Init();
			if (control.childControl != null)
			{
				if (string.IsNullOrEmpty(control.tweet.quoted_status_id_str))
				{
					selectControl = control.childControl;
					selectControl.Select();
					//return;
				}
				else if (control.quoteControl != null)
				{
					selectControl = control.quoteControl;
					selectControl.Select();
				}
				else
				{
					if (DataInstence.blockList.hashBlockUsers.Contains(tweet.user.id) == false)
					{
						TweetControl con = AddControl(tweet, index);
						control.quoteControl = con;
						con.parentControl = control;
					}
					else
						ret = false;
				}
			}
			else
			{
				if (DataInstence.blockList.hashBlockUsers.Contains(tweet.user.id) == false)
				{
					TweetControl con = AddControl(tweet, index);
					control.childControl = con;
					con.parentControl = control;
				}
				else
					ret = false;
			}
			return ret;
		}

		public void DeleteTweet(long id)
		{
			lock (lockObject)
			{
				for (int i = 0; i < listTweetControl.Count; i++)
				{
					if (listTweetControl[i].tweet.id == id)
					{
						if (dicTweet.ContainsKey(id)) dicTweet.Remove(id);

						UpdateControlDelete(listTweetControl[i]);
					}
					else if (listTweetControl[i].tweet.originalTweet.id == id)
					{
						if (dicTweet.ContainsKey(id)) dicTweet.Remove(id);

						UpdateControlDelete(listTweetControl[i]);
					}
				}
			}
		}
		//패널의 사이즈가 바뀌었을 경우 사이즈 갱신
		public void Resize(bool isResizedMainform)
		{
			//if (isResizedMainform)
			//{
				panel.Size = new Size(panel.Parent.Width, panel.Height);
				ResizePanel();
			//}
			int panelHeight = 0;
			for (int i = 0; i < listTweetControl.Count; i++)
			{
				//if (isResizedMainform)
				listTweetControl[i].ControlResize(panel.Width);
				panelHeight += listTweetControl[i].Size.Height;
			}
			if (loadControl != null)
				panelHeight += loadControl.Size.Height + 8;
			flowPanel.Size = new Size(flowPanel.Size.Width, panelHeight);
			if (isResizedMainform)
				SetScrollSize(0);
		}

		public void ResizePanel()
		{
			Point panelPoint = panel.PointToScreen(Point.Empty);
			panelRectangle = new Rectangle(panelPoint.X, panelPoint.Y, panel.Size.Width, panel.Size.Height);
		}

		//컨트롤을 직접 클릭했을 경우 설정해줌
		//control: 선택 한 컨트롤
		public void SelectControl(TweetControl control)
		{
			int index = listTweetControl.IndexOf(control);
			if (index > -1)
			{
				selectControl = control;
				MoveScroll();
			}
			if (colorControl != null)
			{
				if (selectControl != colorControl)
				{
					colorControl.ControlLeave(true);
					colorControl = null;
				}
			}
		}

		//home키를 눌러 첫 트윗을 선택해야 할 경우 사용
		public void SelectFirst()
		{
			if (listTweetControl.Count > 0)
			{
				selectControl = listTweetControl[listTweetControl.Count - 1];
				selectControl.Select();
				scrollBar.Value = 0;
			}
		}

		//end키를 눌러 마지막 트윗을 선택해야 할 경우 사용
		public void SelectEnd()
		{
			if (listTweetControl.Count > 0)
			{
				selectControl = listTweetControl[0];
				selectControl.Select();
				scrollBar.Value = scrollBar.Maximum;
			}
		}

		public TweetControl SelectControl()
		{
			if (selectControl == null && listTweetControl.Count > 0)
			{
				selectControl = listTweetControl[listTweetControl.Count - 1];
			}
			if (selectControl != null)
				selectControl.Select();
			return selectControl;
		}

		//이전 트윗 선택(화살표 아래 키)
		public void PrevSelect()
		{
			if (selectControl == null && listTweetControl.Count > 0)//선택된 게 하나도 없을 경우
			{
				selectControl = listTweetControl[listTweetControl.Count - 1];
			}
			else
			{
				int indexControl = listTweetControl.IndexOf(selectControl);
				if (indexControl - 1 >= 0)
				{
					selectControl = listTweetControl[indexControl - 1];
				}
			}

			selectControl.Select();
		}

		public void 인풋박스_들어갈때_색바꾸는함수()
		{
			if (selectControl != null)
			{
				colorControl = selectControl;
				colorControl.ControlLeave(false);
			}
		}

		//다음 트윗 선택(화살표 위 키)
		public bool NextSelect()//ret: 끝까지 올라왔는지 판단
		{
			bool ret = false;

			if (selectControl == null)//맨 위라고 판단
			{
				ret = true;
			}
			else
			{
				int indexControl = listTweetControl.IndexOf(selectControl);
				if (listTweetControl.Count - 1 == indexControl)//맨위에서 또 누를 경우
				{
					ret = true;
					//selectControl = null;
				}
				else
				{
					selectControl = listTweetControl[indexControl + 1];
					selectControl.Select();
				}
			}
			return ret;
		}

		//선택 트윗 밑으로 자식 컨트롤을 다 지우는 재귀
		private void DeleteChildControls(TweetControl control)
		{
			if (control.quoteControl != null)
			{
				DeleteChildControls(control.quoteControl);
				listTweetControl.Remove(control.quoteControl);
				dicTweet.Remove(control.quoteControl.tweet.id);
				control.quoteControl.Dispose();
				control.quoteControl = null;
			}
			if (control.childControl != null)
			{
				DeleteChildControls(control.childControl);
				listTweetControl.Remove(control.childControl);
				dicTweet.Remove(control.childControl.tweet.id);
				control.childControl.Dispose();
				control.childControl = null;
			}
		}

		public void HideDeahwa(TweetControl control)
		{
			if (control == null) return;
			Generate.SuspendDrawing(flowPanel);
			DeleteChildControls(control);
			selectControl = control;
			selectControl.Select();

			Resize(false);
			Generate.ResumeDrawing(flowPanel);
		}

		//패널을 숨길 때 사용
		public void Hide()
		{
			if (isShow == false) return;

			isShow = false;
			panel.Hide();
		}

		//패널을 보여줄 때 사용
		public void Show()
		{
			if (isShow) return;

			isShow = true;
			flowPanel.Location = new Point(0, 0);
			panel.Show();
		}


		public void Clear()
		{
			lock(lockObject)
			{
				//Generate.SuspendDrawing(flowPanel);
				flowPanel.SuspendLayout();
				selectControl = null;
				colorControl = null;
				dicDM.Clear();
				dicTweet.Clear();
				listTweetControl.Clear();
				foreach(Control item in flowPanel.Controls)
				{
					if (item == loadControl)
						continue;
					item.Dispose();
				}
				flowPanel.Controls.Clear();
				
				
				if (epanel != ePanel.HOME && epanel != ePanel.DM && epanel != ePanel.OPEN_URL)
				{
					loadControl = new LoadControl(this, flowPanel.Width);
					flowPanel.Controls.Add(loadControl);
					flowPanel.Size = new Size(flowPanel.Width, loadControl.Height + 2);
				}
				else
					flowPanel.Size = new System.Drawing.Size(flowPanel.Parent.Width, 2);

				SetScrollSize(0);


				flowPanel.ResumeLayout();
				//Generate.ResumeDrawing(flowPanel);
			}
		}

	}//class
}
