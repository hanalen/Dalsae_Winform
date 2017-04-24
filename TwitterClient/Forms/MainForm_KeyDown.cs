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
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public partial class MainForm
	{
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------키보드 함수------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		/*
		 1. bool isTab = false; 선언
		2. 텍스트뷰의 프리뷰에서, if (e.KeyCode == Keys.Tab) { isTab = true; }
		3. 텍스트뷰의 press 이벤트에서,
		if(isTab) { e.Handled = false;  isTab = false; }
		정리하면, 텍스트 값 입력보다 프리뷰가 먼저이고, 프레스 단계에서 키가 입력되는 거라고 추측해서
		*/
		//프리뷰->다운->프레스->업 순으로 작동. 키는 press에서 입력 됨
		//inputTweet에서 이벤트 다 처리 후 mainForm의 KeyDown으로 감
		private void inputTweet_KeyPreview(object sender, PreviewKeyDownEventArgs e)
		{
			if (isMention && e.KeyCode == Keys.Tab)
			{
				e.IsInputKey = true;
				isTab = true;
				AddMentionID();
			}
			else if (e.KeyCode == Keys.Tab)
			{
				e.IsInputKey = true;
			}
			else if (e.KeyCode == Keys.Escape && inputTweet.Focused)
			{
				e.IsInputKey = true;
				HideMentionBox();
				ClearInputBox(true);
			}
		}

		private void inputTweet_KeyDown(object sender, KeyEventArgs e)//화살표는 안 들어옴
		{
			switch (e.KeyCode)
			{
				case Keys.A://Ctrl+A, Mutiline일 경우 작동 안 해서 수동
					if (e.Control)
					{
						TextSelectAll();
						e.SuppressKeyPress = true;
						e.Handled = true;
					}
					else
					{
						CheckDeleteTextGolbengE();
					}
					break;
				case Keys.Enter://엔터키로 전송
					if (inputTweet.Text.Length == 0 && listFiles.Count == 0)
					{
						e.SuppressKeyPress = true;//키 무시
						e.Handled = true;
						ArrowDown();
					}
					else if (DataInstence.option.isSendEnter)
					{
						if (e.KeyCode == Keys.Enter && e.Control == false && e.Alt == false && e.Shift == false && isMention == false)
						{
							e.SuppressKeyPress = true;//키 무시
							e.Handled = true;
							SendTweet();
						}
						else if (isMention)
						{
							e.SuppressKeyPress = true;//키 무시
							e.Handled = true;
							AddMentionID();
						}
					}
					else if (e.Control && e.Alt == false && e.Shift == false)
					{
						e.SuppressKeyPress = true;//키 무시
						e.Handled = true;
						SendTweet();
					}

					else//아이디 입력 도중에 엔터 쳤을 경우 아이디 완성해준다
					{
						if (isMention)
						{
							isTab = false;
							e.SuppressKeyPress = true;
							e.Handled = true;
							AddMentionID();
						}
						else
							CheckDeleteTextGolbengE();
					}
					break;
				case Keys.D2://@입력
					if (e.Shift)
					{
						isMention = true;
						mentionIndex = inputTweet.SelectionStart + 1;
						mentionListBox.Show();
					}
					else
						CheckDeleteTextGolbengE();
					break;
				case Keys.Back://백스페이스
					if (isMention)
					{
						if (inputTweet.Text.Length > 1)
						{
							if (inputTweet.SelectionStart != 0)
								if (inputTweet.Text[inputTweet.SelectionStart - 1] == '@')
									HideMentionBox();
						}
						CheckDeleteTextGolbengE();
					}
					break;
				case Keys.Delete://딜리트
					if (inputTweet.Text.Length > inputTweet.SelectionStart)
					{
						if (inputTweet.Text[inputTweet.SelectionStart] == '@')
							HideMentionBox();
					}
					CheckDeleteTextGolbengE();
					break;
				case Keys.Tab://탭, 아이디 추가는 Preview에서 호출. 여기는 그냥 초기화만 함
					if (isTab)
					{
						isTab = false;
						e.SuppressKeyPress = true;
						e.Handled = true;
					}
					break;
				case Keys.Space://스페이스, 스페이스 키로 아이디 추가를 그만두는 걸 체크
					if (isMention)
						HideMentionBox();
					else
						CheckDeleteTextGolbengE();
					break;
				case Keys.Down:
					if (IsLeaveInputTweet())
					{
						e.SuppressKeyPress = true;
						e.Handled = true;
					}
					break;
				case Keys.V:
					if (e.Control)
					{
						Image image = Clipboard.GetImage();
						if (image != null)
						{
							AddFile(image);
						}
					}
					break;
				default:
					CheckDeleteTextGolbengE();
					break;
			}
		}

		private void CheckDeleteTextGolbengE()
		{
			if (inputTweet.SelectedText.Length > 0)
			{
				if (inputTweet.SelectedText.IndexOf("@") > -1)
				{
					HideMentionBox();
				}
			}
		}

		private void inputTweet_KeyPress(object sender, KeyPressEventArgs e)//up,down안들어옴
		{

		}

		private void inputTweet_KeyUp(object sender, KeyEventArgs e)
		{
			if (isMention)
			{
				if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)//아래위 화살표 막아야 함
					ShowMentionBox();
			}
		}

		private void mainForm_KeyPreview(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Tab)
			{
				e.IsInputKey = true;
			}
			else if (e.KeyCode == Keys.Down && inputTweet.Focused == false)
			{
				e.IsInputKey = true;
				GetSelectControl();
			}
			else if (e.KeyCode == Keys.Up && inputTweet.Focused == false)
			{
				e.IsInputKey = true;
				GetSelectControl();
			}
			
		}

		//MainForm단에서 키 처리 하는 거
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)//inputbox에 있어도 작동하는 기능
			{
				case Keys.Left: ArrowLeft(); break;
				case Keys.Right: ArrowRight(); break;
				case Keys.Up: ArrowUp(); break;
				case Keys.Down: ArrowDown(); break;
				case Keys.Tab:
					e.SuppressKeyPress = true;
					e.Handled = true;
					break;
			}

			if (e.KeyCode == Keys.Enter && inputTweet.Focused == false)
			{
				EnterInputTweet();
				return;
			}

			if (inputTweet.Focused) return;

			if (e.Control && e.KeyCode == Keys.C)
			{
				CopyTweet();
				return;
			}

			HotKeys hotKey = DataInstence.hotKey;
			if (e.KeyCode == hotKey.keyShowTL) ShowPanel(eSelectMenu.TIME_LINE);
			else if (e.KeyCode == hotKey.keyShowMention) ShowPanel(eSelectMenu.MENTION);
			else if (e.KeyCode == hotKey.keyShowDM) ShowPanel(eSelectMenu.DM);
			else if (e.KeyCode == hotKey.keyShowFavorite) ShowPanel(eSelectMenu.FAVORITE);
			else if (e.KeyCode == hotKey.keyDM) AddIdDm();
			else if (e.KeyCode == hotKey.keyShowOpendURL) ShowPanel(eSelectMenu.OPEN_URL);
			else if (e.KeyCode == hotKey.keyFavorite) AddFavorite();
			else if (e.KeyCode == hotKey.keyInput) EnterInputTweet();
			else if (e.KeyCode == hotKey.keyReply) Reply();
			else if (e.KeyCode == hotKey.keyReplyAll) ReplyAll();
			else if (e.KeyCode == hotKey.keyRetweet) Retweet();
			else if (e.KeyCode == hotKey.keyMenu) ShowMenu();
			else if (e.KeyCode == hotKey.keyGoHome) KeyHome();
			else if (e.KeyCode == hotKey.keyGoEnd) KeyEnd();
			else if (e.KeyCode == hotKey.keyDeleteTweet) DeleteTweet();
			else if (e.KeyCode == hotKey.keyHashTag) AddHashTag();
			else if (e.KeyCode == hotKey.keyLoad) LoadTweetByKey();
			else if (e.KeyCode == hotKey.keyQRetweet) QTRetweet();
		}
		private void CopyTweet()
		{
			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;
			selectControl.CopyTweet();
		}



		private void TextSelectAll()
		{
			inputTweet.Select(0, inputTweet.TextLength);
		}


		private void DeleteTweet()
		{
			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;
			if (selectControl.isDM) return;
			ClientInctence.DeleteTweet(selectControl.tweet.originalTweet);
		}

		private void Retweet()
		{
			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;
			if (selectControl.isDM) return;

			ClientInctence.Retweet(selectControl.tweet);
		}

		private void AddFavorite()
		{
			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;
			if (selectControl.isDM) return;

			ClientInctence.Favorites(selectControl.tweet);
		}



		private void AddHashTag()
		{

		}

		public void AddIdDm(string name)
		{
			tweetButton.Text = "쪽지 보내기";
			inputTweet.Clear();
			inputTweet.Text = "d " + name + " ";
			EnterInputTweet();
			inputTweet.SelectionStart = inputTweet.Text.Length;
		}

		private void AddIdDm()
		{
			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;
			tweetButton.Text = "쪽지 보내기";
			inputTweet.Clear();
			if (selectControl.isDM)
			{
				if (DataInstence.CheckIsMe(selectControl.clientdm.sender.screen_name))
					inputTweet.Text = "d " + selectControl.clientdm.recipient.screen_name + " ";
				else
					inputTweet.Text = "d " + selectControl.clientdm.sender.screen_name + " ";
			}
			else
			{
				inputTweet.Text = "d " + selectControl.tweet.originalTweet.user.screen_name + " ";
			}
			inputTweet.Select(inputTweet.Text.Length, 0);
			EnterInputTweet();
		}

		private void Reply()
		{
			HideMentionBox();

			TweetControl selectControl = GetSelectControl();
			if (selectControl == null) return;

			if (selectControl.isDM == false)//dm이 아닐경우
			{
				if (replyTweet == null)//첫 선택 한 트윗에 답변 가게 함
					replyTweet = selectControl.tweet;
				tweetButton.Text = "답변 하기";
				inputTweet.Text += "@" + selectControl.tweet.originalTweet.user.screen_name + " ";

				inputTweet.Select(inputTweet.Text.Length, 0);
				EnterInputTweet();
			}
			else
			{
				tweetButton.Text = "쪽지 보내기";
				inputTweet.Clear();
				if (DataInstence.CheckIsMe(selectControl.clientdm.sender.screen_name))
					inputTweet.Text = "d " + selectControl.clientdm.recipient.screen_name + " ";
				else
					inputTweet.Text = "d " + selectControl.clientdm.sender.screen_name + " ";

				inputTweet.Select(inputTweet.Text.Length, 0);
				EnterInputTweet();
			}
		}

		private void ReplyAll()
		{
			TweetControl selectControl = GetSelectControl();

			if (selectControl == null) return;

			if (selectControl.isDM)//dm에 답변하는 경우 reply로 보내버리기 그냥..
			{
				Reply();
				return;
			}

			inputTweet.Clear();//클리어 후 replytweet등록 해야함. 순서 문제
			HideMentionBox();
			replyTweet = selectControl.tweet;
			tweetButton.Text = "답변 하기";

			StringBuilder sb = new StringBuilder();
			sb.Append($"@{replyTweet.user.screen_name} ");
			if(replyTweet.isRetweet)
				sb.Append($"@{replyTweet.originalTweet.user.screen_name} ");
			foreach(string name in replyTweet.hashMention)
			{
				if(DataInstence.CheckIsMe(name)==false)
					sb.Append($"@{name} ");
			}
			inputTweet.Text = sb.ToString();

			inputTweet.Select(inputTweet.Text.Length, 0);
			EnterInputTweet();
		}

		private void ShowMenu()
		{
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: homePanel.ShowContextMenu(); break;
				case eSelectMenu.MENTION: mentionPanel.ShowContextMenu(); break;
				case eSelectMenu.DM: dmPanel.ShowContextMenu(); break;
				case eSelectMenu.FAVORITE: favPanel.ShowContextMenu(); break;
				case eSelectMenu.MY_TL: myPanel.ShowContextMenu(); break;
				case eSelectMenu.USER: userPanel.ShowContextMenu(); break;
				case eSelectMenu.OPEN_URL: urlPanel.ShowContextMenu(); break;
			}
		}

		private void KeyHome()
		{
			if (inputTweet.Focused) return;
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: homePanel.SelectFirst(); break;
				case eSelectMenu.MENTION: mentionPanel.SelectFirst(); break;
				case eSelectMenu.DM: dmPanel.SelectFirst(); break;
				case eSelectMenu.USER: userPanel.SelectFirst(); break;
				case eSelectMenu.FAVORITE: favPanel.SelectFirst(); break;
				case eSelectMenu.MY_TL: myPanel.SelectFirst(); break;
				case eSelectMenu.OPEN_URL: urlPanel.SelectFirst(); break;
			}
		}

		private void KeyEnd()
		{
			if (inputTweet.Focused) return;

			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: homePanel.SelectEnd(); break;
				case eSelectMenu.MENTION: mentionPanel.SelectEnd(); break;
				case eSelectMenu.DM: dmPanel.SelectEnd(); break;
				case eSelectMenu.USER: userPanel.SelectEnd(); break;
				case eSelectMenu.FAVORITE: favPanel.SelectEnd(); break;
				case eSelectMenu.MY_TL: myPanel.SelectEnd(); break;
				case eSelectMenu.OPEN_URL: urlPanel.SelectEnd(); break;
			}
		}

		private void QTRetweet()
		{
			TweetControl control = GetSelectControl();
			if (control != null)
				QTRetweet(control.tweet);
		}

		public void QTRetweet(ClientTweet tweet)
		{
			ClearInputBox(true);
			string url = " ";
			//if (tweet.retweeted_status == null)
			//	url += $"https://twitter.com/{tweet.user.screen_name}/status/{tweet.id}";
			//else
			//	url += $"https://twitter.com/{tweet.retweeted_status.user.screen_name}/status/{tweet.retweeted_status.id}";
			url += $"https://twitter.com/{tweet.originalTweet.user.screen_name}/status/{tweet.originalTweet.id}";
			inputTweet.Text = url;
			inputTweet.SelectionStart = 0;
			tweetButton.Text = "인용 하기";
			EnterInputTweet();
		}

		private void LoadTweetByKey()
		{
			if (inputTweet.Focused) return;

			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE:
					ClientInctence.LoadTweet(eSelectMenu.TIME_LINE, string.Empty);
					break;
				case eSelectMenu.MENTION:
					ClientInctence.LoadTweet(eSelectMenu.MENTION, string.Empty);
					break;
				case eSelectMenu.DM:
					ClientInctence.LoadTweet(eSelectMenu.DM, string.Empty);
					break;
				case eSelectMenu.USER:
					ClientInctence.LoadTweet(eSelectMenu.USER,
						dicUserPanel.FirstOrDefault(x => x.Value == userPanel).Key);
					break;
				case eSelectMenu.FAVORITE:
					ClientInctence.LoadTweet(eSelectMenu.FAVORITE, string.Empty);
					break;
				case eSelectMenu.MY_TL:
					ClientInctence.LoadTweet(eSelectMenu.MY_TL, string.Empty);
					break;
			}
		}

		private void KeyU()
		{
			EnterInputTweet();
		}

		private void ArrowUp()
		{
			if (isMention)
			{
				if (mentionListBox.SelectedIndex > 0 && mentionListBox.Items.Count > 0)//up key
				{
					mentionListBox.SelectedItem = mentionListBox.Items[mentionListBox.SelectedIndex - 1];
					return;
				}
			}
			if (inputTweet.Focused) return;
			bool isLast = false;
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: isLast = homePanel.NextSelect(); break;
				case eSelectMenu.MENTION: isLast = mentionPanel.NextSelect(); break;
				case eSelectMenu.DM: isLast = dmPanel.NextSelect(); break;
				case eSelectMenu.USER: isLast = userPanel.NextSelect(); break;
				case eSelectMenu.FAVORITE: isLast = favPanel.NextSelect(); break;
				case eSelectMenu.MY_TL: isLast = myPanel.NextSelect(); break;
				case eSelectMenu.OPEN_URL: isLast = urlPanel.NextSelect(); break;
				default: isLast = homePanel.NextSelect(); break;
			}
			if (isLast)
			{
				EnterInputTweet();
			}
		}

		private void ArrowDown()
		{
			if (isMention)
			{
				if (mentionListBox.SelectedIndex + 1 < mentionListBox.Items.Count && mentionListBox.Items.Count > 0)//down key
					mentionListBox.SelectedItem = mentionListBox.Items[mentionListBox.SelectedIndex + 1];

				return;
			}
			if (inputTweet.Focused)
			{
				int indexText = this.inputTweet.GetLineFromCharIndex(this.inputTweet.SelectionStart);
				if (indexText == inputTweet.Lines.Length - 1 || inputTweet.Lines.Length == 0)//마지막줄일 경우
				{
					GetSelectControl();
				}
			}
			else
			{
				switch (eselectMenu)
				{
					case eSelectMenu.TIME_LINE: homePanel.PrevSelect(); break;
					case eSelectMenu.MENTION: mentionPanel.PrevSelect(); break;
					case eSelectMenu.DM: dmPanel.PrevSelect(); break;
					case eSelectMenu.FAVORITE: favPanel.PrevSelect(); break;
					case eSelectMenu.MY_TL: myPanel.PrevSelect(); break;
					case eSelectMenu.OPEN_URL: urlPanel.PrevSelect(); break;
					case eSelectMenu.USER:
						if (userPanel != null)
							userPanel.PrevSelect();
						break;
					default: homePanel.PrevSelect(); break;
				}
			}
		}

		private void ArrowLeft()
		{
			if (inputTweet.Focused) return;
			TweetControl selectControl = GetSelectControl();
			if (selectControl.childControl == null && selectControl.parentControl != null)
				selectControl.parentControl.Select();
			else
				HideSingleTweet(selectControl);
		}

		private void ArrowRight()
		{
			if (inputTweet.Focused) return;
			if (loadTweetControl != null) return;//로딩중
			TweetControl selectControl = GetSelectControl();
			LoadSingleTweet(selectControl);
		}

		public void HideSingleTweet(TweetControl control)
		{
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE:			homePanel.HideDeahwa(control);		break;
				case eSelectMenu.MENTION:			mentionPanel.HideDeahwa(control);	break;
				//case eSelectMenu.DM:					dmPanel.HideDeahwa();			break;
				case eSelectMenu.USER:				userPanel.HideDeahwa(control);			break;
				case eSelectMenu.FAVORITE:			favPanel.HideDeahwa(control);			break;
				case eSelectMenu.MY_TL:				myPanel.HideDeahwa(control);			break;
				//case eSelectMenu.OPEN_URL:		urlPanel.HideDeahwa();			break;
				default: homePanel.PrevSelect(); break;
			}
		}

		public void LoadSingleTweet(TweetControl tweetControl)
		{
			if (eselectMenu == eSelectMenu.DM || tweetControl == null) return;
			ClientTweet tweet = tweetControl.tweet;

			bool isFirst = false;
			bool isSecond = false;

			if (tweet != null)
			{
				if (tweet.isReply)
				{
					isFirst = true;
					loadTweetControl = tweetControl;
					ClientInctence.LoadSingleTweet(tweet.originalTweet.in_reply_to_status_id_str);
				}
				if (tweet.isQTRetweet)
				{
					isFirst = true;
					loadTweetControl = tweetControl;
					ClientInctence.LoadSingleTweet(tweet.originalTweet.quoted_status_id_str);
				}
			}
			if (isFirst && isSecond)
			{
				isTwoLoadTweet = true;
			}
		}






	}//class
}//namespace
