using System;
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
	public partial class FlowPanelManager
	{
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------내부 기능--------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		private void MoveScroll(MouseEventArgs e)
		{
			int increaseValue = 0;
			if (e.Delta < 0)
			{
				increaseValue = (scrollBar.Value + scrollBar.SmallChange > scrollBar.Maximum)
					? scrollBar.Maximum - scrollBar.Value
					: scrollBar.SmallChange;
			}
			else if (e.Delta > 0)
			{
				increaseValue = -((scrollBar.Value - scrollBar.SmallChange < 0)
					? scrollBar.Value : scrollBar.SmallChange);
			}
			scrollBar.Value += increaseValue;
		}

		private void ScrollBar_ValueChanged(object sender, System.EventArgs e)
		{
			flowPanel.Top = -scrollBar.Value;
		}

		private void SetScrollSize(int height)
		{
			if (scrollBar.Value != 0/* && selectControl != null*/)
			{
				//flowPanel.SuspendLayout();
				int value = scrollBar.Value + height;
				if (value > scrollBar.Maximum)
					value = scrollBar.Maximum;
				else if (value < 0)
					value = 0;
				scrollBar.Value = value;
				//flowPanel.ResumeLayout();
			}
			int scrollSize = flowPanel.Size.Height - panel.Size.Height;
			if (scrollSize < 0) scrollSize = 0;
			scrollBar.Maximum = scrollSize;
		}

		private void MoveScroll()
		{
			if (isOnPanel() == false)
			{
				int value = selectControl.Location.Y;
				if (value > scrollBar.Maximum) value = scrollBar.Maximum;
				scrollBar.Value = value;
			}
			else if (selectControl.Bottom + flowPanel.Top > panel.Bottom)
			{
				int value = selectControl.Size.Height + scrollBar.Value;
				if (value > scrollBar.Maximum)
					value = scrollBar.Maximum;
				scrollBar.Value = value;
			}
			else
			{
				Point conPoint = selectControl.PointToScreen(Point.Empty);
				Point panelPoint = panel.PointToScreen(Point.Empty);
				if (conPoint.Y <= panelPoint.Y)
				{
					int value = scrollBar.Value - selectControl.Size.Height;
					if (value < 0)
						value = 0;
					scrollBar.Value = value;
				}
			}
		}

		private bool isOnPanel()
		{
			Point conPoint = selectControl.PointToScreen(Point.Empty);
			Rectangle conRect = new Rectangle(conPoint.X, conPoint.Y, selectControl.Size.Width, selectControl.Size.Height);

			bool ret = panelRectangle.IntersectsWith(conRect);
			return ret;
		}
		
		private void UpdateControlFavorite(TweetControl con, bool isFav)
		{
			TweetControl.DeleUpdateFavorite dele = new TweetControl.DeleUpdateFavorite(con.UpdateFav);
			con.BeginInvoke(dele, new object[] { con.tweet, isFav });
		}

		private void UpdateControlRetweet(TweetControl con, bool isRetweet)
		{
			TweetControl.DeleUpdateRetweet dele = new TweetControl.DeleUpdateRetweet(con.UpdateRetweet);
			con.BeginInvoke(dele, new object[] { isRetweet });
		}

		private void UpdateControlDelete(TweetControl con)
		{
			TweetControl.DeleDeleteTweet dele = new TweetControl.DeleDeleteTweet(con.DeleteTweet);
			con.BeginInvoke(dele);
		}

		//AddTweet에서 호출
		//listTweet 추가할 트윗 목록, isMore: 더 불러오기를 했을 경우
		private void AddControl(List<ClientTweet> listTweet, bool isMore, eTweet etweet)
		{
			//Generate.SuspendDrawing(flowPanel);
			flowPanel.SuspendLayout();
			for (int i = listTweet.Count - 1; i > -1; i--)
			{
				if (listTweetControl.Count + 1 > CON_MAX_TWEET && isMore == false)//최대 표시수 넘으면 삭제
				{
					TweetControl delControl = listTweetControl[0];
					delControl.Clear();
					listTweetControl.Remove(delControl);
					flowPanel.Controls.Remove(delControl);
					if (delControl == selectControl)
						selectControl = null;
					delControl.Dispose();
					dicTweet.Remove(delControl.tweet.id);
				}

				TweetControl con;
				if (isChangeColor)
				{
					con = new TweetControl(listTweet[i], flowPanel.Width, DataInstence.skin.blockOne, etweet);
					isChangeColor = !isChangeColor;
				}
				else
				{
					con = new TweetControl(listTweet[i], flowPanel.Width, DataInstence.skin.blockTwo, etweet);
					isChangeColor = !isChangeColor;
				}
				flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + con.Height);
				flowPanel.Controls.Add(con);
				if (isMore)//더 불러오기를 했을 경우 추가 index가 다르다
				{
					flowPanel.Controls.SetChildIndex(con, 1);
					listTweetControl.Insert(0, con);
				}
				else
					listTweetControl.Add(con);
				SetScrollSize(con.Height);
				con.Show();
			}
			Resize(false);
			//SetScrollSize();
			flowPanel.ResumeLayout();
			//Generate.ResumeDrawing(flowPanel);
			//ResizeMainForm();
		}

		//대화 불러오기에 사용되는 함수
		private TweetControl AddControl(ClientTweet tweet, int index)
		{
			TweetControl con;
			lock (lockObject)
			{
				Generate.SuspendDrawing(flowPanel);
				tweet.Init();
				
				if (isChangeColorDeahwa)
				{
					con = new TweetControl(tweet, flowPanel.Width, DataInstence.skin.mentionOne, eTweet.DEHWA);
					isChangeColorDeahwa = !isChangeColorDeahwa;
				}
				else
				{
					con = new TweetControl(tweet, flowPanel.Width, DataInstence.skin.mentionTwo, eTweet.DEHWA);
					isChangeColorDeahwa = !isChangeColorDeahwa;
				}
				
				flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + con.Height);
				flowPanel.Controls.Add(con);
				if (loadControl != null)
					flowPanel.Controls.SetChildIndex(con, index + 1);
				else
					flowPanel.Controls.SetChildIndex(con, index);
				listTweetControl.Insert(index, con);
				con.Show();

				Resize(false);
				SetScrollSize(con.Height);
				con.Show();

				Generate.ResumeDrawing(flowPanel);
			}
			
			return con;
		}

		private void AddControl(List<ClientDirectMessage> listdm, eTweet etweet)
		{
			//Generate.SuspendDrawing(flowPanel);
			flowPanel.SuspendLayout();
			for (int i = listdm.Count - 1; i > -1; i--)
			{
				listdm[i].Init();
				TweetControl con;
				if (isChangeColor)
				{
					con = new TweetControl(listdm[i], flowPanel.Width, DataInstence.skin.blockOne);
					isChangeColor = !isChangeColor;
				}
				else
				{
					con = new TweetControl(listdm[i], flowPanel.Width, DataInstence.skin.blockTwo);
					isChangeColor = !isChangeColor;
				}
				flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + con.Height);
				flowPanel.Controls.Add(con);
				listTweetControl.Add(con);
				SetScrollSize(con.Height);
				con.Show();
			}
			Resize(false);
			//SetScrollSize();
			flowPanel.ResumeLayout();
			//Generate.ResumeDrawing(flowPanel);
		}

		private void AddControl(ClientDirectMessage clientdm, eTweet etweet)
		{
			flowPanel.SuspendLayout();
			//Generate.SuspendDrawing(flowPanel);
			clientdm.Init();
			TweetControl con;
			if (isChangeColor)
			{
				con = new TweetControl(clientdm, flowPanel.Width, DataInstence.skin.blockOne);
				isChangeColor = !isChangeColor;
			}
			else
			{
				con = new TweetControl(clientdm, flowPanel.Width, DataInstence.skin.blockTwo);
				isChangeColor = !isChangeColor;
			}
			flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + con.Height);
			flowPanel.Controls.Add(con);
			listTweetControl.Add(con);
			con.Show();

			Resize(false);
			SetScrollSize(con.Height);
			con.Show();
			flowPanel.ResumeLayout();
			//Generate.ResumeDrawing(flowPanel);
		}


		//AddTweet(단일) 에서 호출, 트윗 박스 추가
		//tweet: 언제나의 그 트윗
		private void AddControl(ClientTweet tweet, eTweet etweet)
		{
			lock (lockObject)
			{
				if (scrollBar.Value != 0 && isShow)
					Generate.SuspendDrawing(flowPanel);
				else
					flowPanel.SuspendLayout();
				if (listTweetControl.Count + 1 > CON_MAX_TWEET)//최대 표시수 넘으면 삭제
				{
					TweetControl delControl = listTweetControl[0];
					delControl.Clear();
					listTweetControl.Remove(delControl);
					flowPanel.Controls.Remove(delControl);
					if (delControl == selectControl)
						selectControl = null;
					delControl.Dispose();
					dicTweet.Remove(delControl.tweet.id);
				}

				TweetControl con;
				if (isChangeColor)
				{
					con = new TweetControl(tweet, flowPanel.Width, DataInstence.skin.blockOne, etweet);
					isChangeColor = !isChangeColor;
				}
				else
				{
					con = new TweetControl(tweet, flowPanel.Width, DataInstence.skin.blockTwo, etweet);
					isChangeColor = !isChangeColor;
				}
				//flowPanel.Size = new Size(flowPanel.Width, flowPanel.Height + con.Height);
				
				flowPanel.Controls.Add(con);
				listTweetControl.Add(con);
				Resize(false);
				SetScrollSize(con.Height);
				con.Show();
				if (scrollBar.Value != 0 && isShow)
					Generate.ResumeDrawing(flowPanel);
				else
					flowPanel.ResumeLayout();
				//flowPanel.ResumeLayout();
			}
		}

		//뮤트 체크
		//tweet: 체크 할 트윗
		private bool isShowTweet(ClientTweet tweet)
		{
			bool ret = true;

			if (DataInstence.isBlockUser(tweet.originalTweet.user.id))
				return false;

			if (epanel == ePanel.MENTION)
			{
				if (DataInstence.option.isMuteMention)//멘션함 뮤트일경우 체크체크
				{
					if (DataInstence.option.MatchMuteWord(tweet.originalTweet.text))
						ret = false;
					else if (DataInstence.option.MatchMuteClient(tweet.originalTweet.source))
						ret = false;
					else if (DataInstence.option.MatchMuteUser(tweet.originalTweet.user.screen_name))
						ret = false;
				}
				if (DataInstence.isBlockUser(tweet.entities.user_mentions))
					ret = false;
			}
			else if (epanel == ePanel.DM)
			{
				ret = true;
			}
			else
			{
				if (DataInstence.option.MatchMuteWord(tweet.originalTweet.text))
					ret = false;
				else if (DataInstence.option.MatchMuteClient(tweet.originalTweet.source))
					ret = false;
				else if (DataInstence.option.MatchMuteUser(tweet.originalTweet.user.screen_name))
					ret = false;
				else if (DataInstence.isRetweetOffUser(tweet.user.id) && tweet.isRetweet)
					ret = false;
				else if (DataInstence.isBlockUser(tweet.entities.user_mentions))
					ret = false;
			}
			return ret;
		}
		private class FlowLayoutPanelCustom : FlowLayoutPanel
		{
			private DeleScroll dele;

			public FlowLayoutPanelCustom(DeleScroll dele)
			{
				this.dele = dele;
			}

			protected override void OnMouseWheel(MouseEventArgs e)
			{
				base.OnMouseWheel(e);
				dele.Invoke(e);
			}
		}
	}
}
