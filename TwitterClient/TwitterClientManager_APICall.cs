using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;
using static TwitterClient.MainForm;
using System.Text.RegularExpressions;

namespace TwitterClient
{
	public partial class TwitterClientManager
	{
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------API 요청----------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		//첫 실행 시 자기 정보 가져와서 인장 표시하기 위한 기능
		public void GetMyInfo()
		{
			ParameterVerifyCredentials parameter = new ParameterVerifyCredentials();
			WebInstence.RequestTwitter(parameter);
		}

		private void GetRetweetOffIds()
		{
			ParameterGetRetweetOffIds parameter = new TwitterClient.ParameterGetRetweetOffIds();
			WebInstence.RequestTwitter(parameter);
		}

		private void StartGetOAuth()
		{
			ParameterGetOAuth parameter = new ParameterGetOAuth();
			WebInstence.RequestOAuth(parameter);

			ShowInputPinForm();//핀 입력창 띄우기
		}

		private void GetAccessToken(string pin)
		{
			ParameterGetAccessToken parameter = new TwitterClient.ParameterGetAccessToken();
			parameter.oauth_verifier = pin;
			WebInstence.RequestOAuth(parameter);
		}

		//내 트윗 긁어오기
		private void GetMyTweet()
		{
			ParameterUserTimeLine parameter = new TwitterClient.ParameterUserTimeLine();
			parameter.screen_name = DataInstence.userInfo.myUser.screen_name;
			parameter.eresponse = eResponse.MY_TWEET;   //eResponse를 바꿔야 UserTL로 인식 안 함

			WebInstence.RequestTwitter(parameter);
		}

		//트윗 보내기
		//parameter: 기본 트윗 정보, MainForm에서 만들어서 옴, listFiles 이미지 전송 파일 선택 된 거
		public void SendTweet(ParameterUpdate parameter, Bitmap[] listBitmap)
		{
			ClientSendTweet tweet = new ClientSendTweet();
			tweet.SetTweet(parameter, listBitmap);

			ThreadPool.QueueUserWorkItem(new WaitCallback(SendTweet), tweet);
		}

		public void SendMultimedia(ParameterUpdate parameter, string filePath)
		{
			ClientSendTweet tweet = new ClientSendTweet();
			tweet.SetTweet(parameter, filePath);

			ThreadPool.QueueUserWorkItem(new WaitCallback(SendTweet), tweet);
		}

		//threadpool에서 스레드 꺼내서 트윗을 백그라운드로 전송하는 방식
		//obj: ClientSendTweet형 parameter, ParameterUpdate정보와 listFiles이 있다
		private void SendTweet(object obj)
		{
			ClientSendTweet tweet = obj as ClientSendTweet;
			if (tweet == null) return;

			if (tweet.listBitmap != null)
			{
				if (tweet.listBitmap.Length != 0)//media가 있을 경우
				{
					ImageConverter converter = new ImageConverter();
					for (int i = 0; i < tweet.listBitmap.Length; i++)
					{
						//Bitmap bit = new Bitmap("a");
						//(byte[])converter.ConvertTo(bit, typeof(byte[]));
						byte[] bytes = (byte[])converter.ConvertTo(tweet.listBitmap[i], typeof(byte[]));//File.ReadAllBytes(tweet.listBitmap[i]);
						string fileStr = Convert.ToBase64String(bytes);
						tweet.listBitmap[i].Dispose();
						ParameterMediaUpload parameter = new ParameterMediaUpload();
						parameter.media_data = fileStr;
						parameter.eresponse = eResponse.IMAGE;
						string response = WebInstence.SendMultimedia(parameter);
						if (string.IsNullOrEmpty(response) == false)
						{
							ClientMultimedia multi = JsonConvert.DeserializeObject<ClientMultimedia>(response);
							tweet.ResponseMedia(multi.media_id_string);
						}
					}
				}
			}
			else
			{
				if (string.IsNullOrEmpty(tweet.multiPath) == false)
				{
					byte[] bytes = File.ReadAllBytes(tweet.multiPath);
					string fileStr = Convert.ToBase64String(bytes);
					ParameterMediaUpload parameter = new ParameterMediaUpload();
					parameter.media_data = fileStr;
					parameter.eresponse = eResponse.IMAGE;
					string response = WebInstence.SendMultimedia(parameter);
					if (string.IsNullOrEmpty(response) == false)
					{
						ClientMultimedia multi = JsonConvert.DeserializeObject<ClientMultimedia>(response);
						tweet.ResponseMedia(multi.media_id_string);
					}
				}
			}
			WebInstence.RequestTwitter(tweet.parameter);//media가 있을 경우 media를 다 보내고 받은 후 전송
		}

		public void LoadMoreTweet(BaseParameter parameter)
		{
			WebInstence.RequestTwitter(parameter);
		}


		private void LoadTimeLine()
		{
			ParameterHomeTimeLine parameter = new TwitterClient.ParameterHomeTimeLine();
			WebInstence.RequestTwitter(parameter);
		}

		private void LoadMention()
		{
			ParameterMentionTimeLine parameter = new ParameterMentionTimeLine();
			WebInstence.RequestTwitter(parameter);
		}

		public void LoadSingleTweet(string idStr)
		{
			ParameterSingleTweet parameter = new ParameterSingleTweet(idStr);
			WebInstence.RequestTwitter(parameter);
		}

		//관심글 추가,해제
		//tweet: 트윗 정보
		public void Favorites(ClientTweet tweet)
		{
			//if (tweet.retweeted_status == null)//일반 트윗
			//{
				if (tweet.originalTweet.favorited)//이미 박은 거
				{
					ParameterFavorites_Destroy parameter = new ParameterFavorites_Destroy();
					parameter.id = tweet.originalTweet.id;
					WebInstence.RequestTwitter(parameter);
				}
				else//새로 추가
				{
					ParameterFavorites_Create parameter = new ParameterFavorites_Create();
					parameter.id = tweet.originalTweet.id;
					WebInstence.RequestTwitter(parameter);
				}
		}

		//트윗 삭제
		//tweet: 삭제 할 트윗 
		public void DeleteTweet(ClientTweet tweet)
		{
			if (tweet.user.screen_name != DataInstence.userInfo.myUser.screen_name) return;//자기 트윗 아니면 종료

			DialogResult result = MessageBox.Show(mainForm, "트윗을 삭제 하시겠습니까?",
								"트윗 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.None);
			if (result == DialogResult.Yes)
			{
				ParameterTweetDelete parameter = new ParameterTweetDelete(tweet.id);
				WebInstence.RequestTwitter(parameter);
			}
		}

		public void FollowingUpdate(long id)
		{
			ParameterUpdateFollowingData parameter = new TwitterClient.ParameterUpdateFollowingData();
			parameter.user_id = id;
			if (DataInstence.hashRetweetOff.Contains(id))
				parameter.retweets = true;
			else
				parameter.retweets = false;

			WebInstence.RequestTwitter(parameter);
		}

		//리트윗, 리트윗 취소
		//tweet: 트윗 정보
		public void Retweet(ClientTweet tweet)
		{
			BaseParameter parameter = null;

			if (tweet.user.Protected && tweet.isRetweet == false)//플텍유저일경우
			{
				if (DataInstence.CheckIsMe(tweet.user.id))
				{
					if (tweet.retweeted)//일반 트윗 리트윗 취소
						parameter = CreateRetweet(tweet.id, false);
					else if (IsSendRetweet())
						parameter = CreateRetweet(tweet.id, true);
				}
				else
				{
					if (DataInstence.option.isRetweetProtectUser)
					{
						if (IsSendRetweet())
						{
							RetweetProtectUser(tweet);
						}
					}
					else
					{
						ShowMessageBox("잠금 계정의 트윗은 리트윗 할 수 없습니다.\r", "알림");
					}
				}
			}
			else if (tweet.isRetweet == false)//일반 트윗
			{
				if (tweet.retweeted)//일반 트윗 리트윗 취소
					parameter = CreateRetweet(tweet.id, false);
				else if (IsSendRetweet())
					parameter = CreateRetweet(tweet.id, true);
			}
			else//리트윗
			{
				if (tweet.originalTweet.retweeted)//리트윗에 대고 취소<- 가 안 됨. 갱신이 안된다
					parameter = CreateRetweet(tweet.originalTweet.id, false);
				else if (IsSendRetweet())
					parameter = CreateRetweet(tweet.originalTweet.id, true);
			}
			if (parameter != null)
				WebInstence.RequestTwitter(parameter);
		}

		private void RetweetProtectUser(ClientTweet tweet)
		{
			if (tweet == null) return;

			if (tweet.isMedia)
			{
				ShowMessageBox("이미지가 있는 글이라 리트윗에 다소 시간이 걸립니다.", "알림", MessageBoxIcon.Information);
				ThreadPool.QueueUserWorkItem(ProtectUserImageLoad, tweet);
			}
			else
			{
				ParameterUpdate sendParameter = new ParameterUpdate();
				//sendParameter.status = "RT @**: " + tweet.text;
				sendParameter.status = Generate.ReplaceTextExpend(tweet.originalTweet);
				if (sendParameter.status.Length > 139)
					sendParameter.status = sendParameter.status.Substring(0, 139);
				sendParameter.status = sendParameter.status.Insert(0, "RT @**: ");
				SendTweet(sendParameter, null);
			}
		}

		private void ProtectUserImageLoad(object obj)
		{
			ClientTweet tweet = obj as ClientTweet;
			if (tweet == null) return;

			ParameterUpdate sendParameter = new ParameterUpdate();
			//sendParameter.status = "RT @**: " + tweet.text;
			//if (sendParameter.status.Length > 139)
			//	sendParameter.status = sendParameter.status.Substring(0, 139);
			sendParameter.status = ReplaceUrl(tweet.originalTweet);

			List<Bitmap> listBitmap = new List<Bitmap>();
			foreach (ClientMedia item in tweet.dicMedia.Values)
			{
				sendParameter.status = sendParameter.status.Replace(item.display_url, "");
				try
				{
					WebRequest request = WebRequest.Create(item.media_url_https);
					WebResponse response = request.GetResponse();
					using (Stream stream = response.GetResponseStream())
					{
						Bitmap bitmap = new Bitmap(stream);
						listBitmap.Add(bitmap);
					}
				}
				catch (Exception e) { }
			}
			sendParameter.status = sendParameter.status.Insert(0, "RT @**: ");

			Regex UrlMatch = new Regex(@"[(http|ftp|https):\/\/]*[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
			MatchCollection mt = UrlMatch.Matches(sendParameter.status);
			int tweetLength = 0;
			for (int i = 0; i < mt.Count; i++)
				tweetLength += mt[i].Length;

			tweetLength = sendParameter.status.Length - tweetLength + 23 * mt.Count + 1;

			if (tweetLength > 140)
				sendParameter.status = sendParameter.status.Substring(0, 139);
			
			SendTweet(sendParameter, listBitmap.ToArray());
		}

		private string ReplaceUrl(ClientTweet tweet)
		{
			string ret = tweet.text;
			if (tweet.isUrl)
				for (int i = 0; i < tweet.listUrl.Count; i++)
					ret = ret.Replace(tweet.listUrl[i].display_url, tweet.listUrl[i].expanded_url);

			return ret;
		}



		private bool IsSendRetweet()
		{
			DialogResult result = MessageBox.Show(mainForm, "선택한 트윗을 리트윗 하시겠습니까?",
								"리트윗 확인", MessageBoxButtons.YesNo, MessageBoxIcon.None);
			if (result == DialogResult.Yes)
				return true;
			else
				return false;
		}

		private BaseParameter CreateRetweet(long id, bool isRetweet)
		{
			if (isRetweet)
			{
				ParameterRetweet parameter = new ParameterRetweet(id);
				//parameter.id = id;
				return parameter;
			}
			else
			{
				ParameterUnRetweet parameter = new ParameterUnRetweet(id);
				//parameter.id = id;

				return parameter;
			}
		}


		//팔로 리스트 받는 함수, 스레드로 작동
		//obj: 사용안함
		public void GetFollowList(object obj)
		{
			Dictionary<long, UserSemi> dicUser = new Dictionary<long, TwitterClient.UserSemi>();
			string token = DataInstence.userInfo.Token;
			token = token.Substring(0, token.IndexOf('-'));
			ParameterFollowing parameter = new ParameterFollowing();
			parameter.user_id = token;
			//parameter.user_id = DataInstence.userInfo.myUser.id;
			parameter.count = 200.ToString();

			string json = WebInstence.SyncRequest(parameter);
			ClientUsers clientUsers = JsonConvert.DeserializeObject<ClientUsers>(json);

			for (int j = 0; j < clientUsers.users.Length; j++)
				dicUser.Add(clientUsers.users[j].id,
						new UserSemi(clientUsers.users[j].name, clientUsers.users[j].screen_name, clientUsers.users[j].id));

			int count = DataInstence.userInfo.myUser.friends_count;
			for (int i = count - clientUsers.users.Length; i > 0; i -= clientUsers.users.Length)//받는 리스트만큼 i감소
			{
				parameter = new ParameterFollowing();
				parameter.count = 200.ToString();
				parameter.user_id = DataInstence.userInfo.myUser.id;
				parameter.cursor = clientUsers.next_cursor.ToString();

				clientUsers = null;
				json = WebInstence.SyncRequest(parameter);
				if (string.IsNullOrEmpty(json))//리밋걸린거
				{
					Thread.Sleep(60000);
					continue;
				}
				clientUsers = JsonConvert.DeserializeObject<ClientUsers>(json);

				for (int j = 0; j < clientUsers.users.Length; j++)
					dicUser.Add(clientUsers.users[j].id,
						new UserSemi(clientUsers.users[j].name, clientUsers.users[j].screen_name, clientUsers.users[j].id));
			}

			DataInstence.SetFollowing(dicUser);
			FileInstence.UpdateFollowList(dicUser);//아이디가 많을 경우도 있어 저장해둔다

			MainForm.DeleShowMessageBox dele = new DeleShowMessageBox(mainForm.ShowMessageBox);
			mainForm.BeginInvoke(dele, new object[] { "팔로우 리스트를 가져왔습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information });
		}

		public void ConnectUserStreaming()
		{
			//if (WebInstence.isConnectedUserStreaming() == false)
			//{
				ParameterUserStream parameter = new TwitterClient.ParameterUserStream();
				ThreadPool.QueueUserWorkItem(WebInstence.ConnectUserStream, parameter);
			//}
		}

		public void GetBlockIds(object obj)
		{
			BlockList blockList = DataInstence.blockList;
			//Dictionary<long, char> dicBlock = new Dictionary<long, char>();

			ParameterBlockIds parameter = new ParameterBlockIds();
			if (blockList.next_cursor > 0)
				parameter.cursor = blockList.next_cursor.ToString();
			string json = WebInstence.SyncRequest(parameter);
			ClientBlockIds blockIds = JsonConvert.DeserializeObject<ClientBlockIds>(json);
			if (blockIds.ids != null)
				blockList.hashBlockUsers.UnionWith(blockIds.ids);

			blockList.next_cursor = blockIds.next_cursor;
			while (blockIds.next_cursor > 0)
			{
				parameter.cursor = blockIds.next_cursor;
				json = WebInstence.SyncRequest(parameter);
				if (string.IsNullOrEmpty(json))//리밋걸린거
				{
					//리밋에 걸리면 우선 저장한다
					FileInstence.UpdateBlockList(blockList);
					DataInstence.SetBlockList(blockList);
					Thread.Sleep(180000);
					continue;
				}
				blockIds = JsonConvert.DeserializeObject<ClientBlockIds>(json);
				blockList.next_cursor = blockIds.next_cursor;
				if (blockIds.ids != null)
					blockList.hashBlockUsers.UnionWith(blockIds.ids);
			}
			FileInstence.UpdateBlockList(blockList);//아이디가 많을 경우도 있어 저장해둔다
			DataInstence.SetBlockList(blockList);

			MainForm.DeleShowMessageBox dele = new DeleShowMessageBox(mainForm.ShowMessageBox);
			mainForm.BeginInvoke(dele, new object[] { "블락 리스트를 가져왔습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information });
		}

		public void LoadTweet(eSelectMenu eselect, string name)
		{
			if (isLoadingTweet) return;//트윗 로딩중이면 블락

			isLoadingTweet = true;
			BaseParameter parameter = new BaseParameter();
			switch (eselect)
			{
				case eSelectMenu.TIME_LINE:
					ParameterHomeTimeLine homeParameter = new ParameterHomeTimeLine();
					parameter = homeParameter;
					break;
				case eSelectMenu.MENTION:
					ParameterMentionTimeLine menParameter = new ParameterMentionTimeLine();
					parameter = menParameter;
					break;
				case eSelectMenu.DM:
					parameter = new TwitterClient.ParameterGetDM();
					break;
				case eSelectMenu.USER:
					if (name.Length != 0)
					{
						ParameterUserTimeLine userParameter = new ParameterUserTimeLine();
						userParameter.screen_name = name;
						parameter = userParameter;
					}
					break;
				case eSelectMenu.FAVORITE:
					ParameterFavoritesList favParameter = new ParameterFavoritesList();
					favParameter.user_id = DataInstence.userInfo.myUser.id;
					parameter = favParameter;
					break;
				case eSelectMenu.MY_TL:
					ParameterUserTimeLine myParameter = new ParameterUserTimeLine();
					myParameter.screen_name = DataInstence.userInfo.myUser.screen_name;
					myParameter.eresponse = eResponse.MY_TWEET;
					parameter = myParameter;
					break;
			}

			WebInstence.RequestTwitter(parameter);
		}

	}//class
}
