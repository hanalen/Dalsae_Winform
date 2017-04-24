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

namespace TwitterClient
{
	public partial class TwitterClientManager
	{
		//----------------------------------------------------------------------------------------------------
		//---------------------------------------패킷 처리---------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		public void InputErrorPin()
		{
			ShowMessageBox("pin을 잘못 입력하였습니다. 다시 인증 해주세요", "오류", RestartOAuth);
			//StartGetOAuth();
		}

		public void ResponseJson(string json, eResponse eres, bool isMore)
		{
			MainForm.DeleLog dele = new MainForm.DeleLog(mainForm.Log);
			mainForm.BeginInvoke(dele, new object[] { json });


			switch (eres)//관심글 등록,취소는 유저스트리밍에서 처리하고있음
			{
				//더 불러오기를 사용하는 기능
				case eResponse.MY_TWEET:
					ResponseMyTweet(json, isMore);
					break;
				case eResponse.MENTION:
					ResponseMention(json, isMore);
					break;
				case eResponse.FAVORITE_LIST:
					ResponseFavoritesList(json, isMore);
					break;
				case eResponse.USER_TIMELINE:
					ResponseUserTimeLine(json, isMore);
					break;



				//더 불러오기를 사용하지 않는 기능
				case eResponse.TIME_LINE:
					ResponseHomeTimeLine(json);
					break;
				case eResponse.MY_INFO:
					ResponseMyInfo(json);
					break;
				case eResponse.RETWEET:
					ResponseRetweet(json, true);
					break;
				case eResponse.UN_RETWEET:
					ResponseRetweet(json, false);
					break;
				case eResponse.GET_DM:
					ResponseDirectMessage(json);
					break;
				case eResponse.RETWEET_OFF_IDS:
					ResponseRetweetOffIds(json);
					break;
				case eResponse.FOLLOWING_UPDATE:
					ResponseFollowingUpdate(json);
					break;
				case eResponse.SINGLE_TWEET:
					ResponseSingleTweet(json);
					break;
				case eResponse.FAVORITE_CREATE:
					ResponseFavorite(json, true);
					break;
				case eResponse.FAVORITE_DESTROY:
					ResponseFavorite(json, false);
					break;
			}
		}


		public void ResponseStreamJson(string json)
		{

			//File.AppendAllText("Data/Stream.txt", json);
			//File.AppendAllText("Data/Stream.txt", "\n\n");
			//MainForm.DeleLog log = new MainForm.DeleLog(mainForm.Log);
			//mainForm.BeginInvoke(log, new object[] { json });
			switch (json[2])
			{
				case 'd'://누군가의 트윗 삭제나 리트윗 취소, dm
					if (json[3] == 'e')//리트윗, 삭제 등
					{
						ClientStreamDelete delete = JsonConvert.DeserializeObject<ClientStreamDelete>(json);
						MainForm.DeleDeleteTweet deleDeleteTweet = new DeleDeleteTweet(mainForm.DeleteTweet);
						mainForm.BeginInvoke(deleDeleteTweet, new object[] { delete });
					}
					else//dm
					{
						StreamDirectMessage clientDM = JsonConvert.DeserializeObject<StreamDirectMessage>(json);
						MainForm.DeleAddDirectMessage deledm = new DeleAddDirectMessage(mainForm.AddDirectMessage);
						mainForm.BeginInvoke(deledm, new object[] { clientDM.direct_message });
						break;
					}
					break;
				case 'e'://이벤트들
					StreamEvent streamEvent = JsonConvert.DeserializeObject<StreamEvent>(json);
					ResponseStreamEvent(streamEvent);
					break;
				case 'c'://새 트윗
					ClientTweet tweet = JsonConvert.DeserializeObject<ClientTweet>(json);
					DeleAddTweet dele = new DeleAddTweet(mainForm.AddTweetStreaming);
					mainForm.BeginInvoke(dele, new object[] { tweet });
					break;
			}
		}

		private void ResponseStreamEvent(StreamEvent streamEvent)
		{
			//retweeted_retweet - 남의 리트윗을 리트윗 한 경우
			//retweeted_retweet - 내 리트윗을 누가 리트윗한 경우
			//quoted_tweet - 인용당했을 때
			//favorited_retweet - 리트윗에 벽박을경우, 내 리트윗에 누가 별박으면

			//내 리트윗 취소도 delete
			//누군가의 트윗 삭제도 delete
			switch (streamEvent.Event)
			{
				//case "retweeted_retweet"://내 리트윗을 누가 리트윗했을 경우
				//	break;
				//case "favorited_retweet"://내 리트윗에 누가 별 박으면
				//	break;
				//case "favorite":
				//	MainForm.DeleUpdateTweet deleCreateFav = new DeleUpdateTweet(mainForm.UpdateTweetFavorite);
				//	mainForm.BeginInvoke(deleCreateFav, new object[] { streamEvent.target_object, true });
				//	break;
				//case "unfavorite":
				//	MainForm.DeleUpdateTweet deleDestroyFav = new DeleUpdateTweet(mainForm.UpdateTweetFavorite);
				//	mainForm.BeginInvoke(deleDestroyFav, new object[] { streamEvent.target_object, false });
				//	break;
				case "unblock":
					DataInstence.UpdateBlockIds(streamEvent.target.id, false);
					break;
				case "block":
					DataInstence.UpdateBlockIds(streamEvent.target.id, true);
					break;
				case "follow":
					DataInstence.UpdateFollow(streamEvent.target, true);
					break;
				case "unfollow":
					DataInstence.UpdateFollow(streamEvent.target, false);
					break;
				case "delete":

					break;
			}
		}

		public void ResponseError(string errorLog)
		{
			ClientAPIError error = JsonConvert.DeserializeObject<ClientAPIError>(errorLog);
			if (error.errors == null)
			{
				ShowMessageBox(errorLog, "오류");
				mainForm.ClearLoadTweet();
				if (isLoadingTweet)
					isLoadingTweet = false;
			}
			else
			{
				if (error.errors.Count > 0)
				{
					switch (error.errors[0].code)
					{
						case 144:
							ShowMessageBox("삭제된 트윗입니다.", "오류");
							mainForm.ClearLoadTweet();
							break;
						case 88:
							ShowMessageBox("불러오기 제한, 몇 분 뒤 시도해주세요.", "오류");
							break;
						case 187:
							ShowMessageBox("중복 트윗입니다. 같은 내용을 적지 말아주세요 :(", "오류");
							break;
						case 327:
							ShowMessageBox("이미 리트윗 한 트윗입니다.", "오류");
							break;
						case 324:
							ShowMessageBox("이미지 용량이 3mb를 넘어 업로드 할 수 없습니다.", "오류");
							break;
						case 34:
							ShowMessageBox("해당 유저는 없습니다.", "오류");
							mainForm.ClearLoadTweet();
							break;
						case 179:
							ShowMessageBox("대화 트윗을 쓴 유저가 잠금 계정입니다.", "오류");
							mainForm.ClearLoadTweet();
							break;
						case 139:
							ShowMessageBox("이미 관심글에 등록 된 트윗입니다.", "오류");
							break;
						default:
							ShowMessageBox(errorLog, "오류");
							mainForm.ClearLoadTweet();
							break;
					}
				}
				if (isLoadingTweet)
					isLoadingTweet = false;
			}
		}

		private void ResponseSingleTweet(string json)
		{
			ClientTweet tweet = JsonConvert.DeserializeObject<ClientTweet>(json);
			DeleAddTweet dele = new DeleAddTweet(mainForm.AddSingleTweet);
			mainForm.BeginInvoke(dele, new object[] { tweet });
		}

		private void ResponseFollowingUpdate(string json)
		{
			ClientFollowingUpdate friendsUpdate = JsonConvert.DeserializeObject<ClientFollowingUpdate>(json);

			if(DataInstence.UpdateRetweetOff(friendsUpdate.relationship.target.id))
				ShowMessageBox($"{friendsUpdate.relationship.target.screen_name}의 리트윗을 표시하지 않습니다.", "알림", MessageBoxIcon.Information);
			else
				ShowMessageBox($"{friendsUpdate.relationship.target.screen_name}의 리트윗을 표시합니다.", "알림", MessageBoxIcon.Information);
		}

		private void ResponseRetweetOffIds(string json)
		{
			List<long> listIds = JsonConvert.DeserializeObject<List<long>>(json);

			HashSet<long> hashRetweetOff = new HashSet<long>();
			hashRetweetOff.UnionWith(listIds);

			DataInstence.SetRetweetOff(hashRetweetOff);
		}

		private void ResponseDirectMessage(string json)
		{
			List<ClientDirectMessage> listdm = JsonConvert.DeserializeObject<List<ClientDirectMessage>>(json);
			MainForm.DeleAddDirectMessageList dele = new DeleAddDirectMessageList(mainForm.AddDirectMessage);
			mainForm.BeginInvoke(dele, new object[] { listdm });
		}

		private void ResponseFavoritesList(string json, bool isMore)
		{
			List<ClientTweet> listTweet = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
			if (isMore)
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddMoreTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.FAVORITE });
			}
			else
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.FAVORITE });
			}
		}

		private void ResponseMyTweet(string json, bool isMore)
		{
			List<ClientTweet> listTweet = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
			if (isMore)
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddMoreTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.MY_TL });
			}
			else
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.MY_TL });
			}
		}

		private void ResponseUserTimeLine(string json, bool isMore)
		{
			List<ClientTweet> listTweet = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
			string userName = string.Empty;
			if (listTweet.Count > 0)
				userName = listTweet[0].user.screen_name;
			if (isMore)
			{
				MainForm.DeleShowUserTimeLine dele = new MainForm.DeleShowUserTimeLine(mainForm.AddUserMoreTimeLine);
				mainForm.BeginInvoke(dele, new object[] { listTweet, userName });
			}
			else
			{
				MainForm.DeleShowUserTimeLine dele = new MainForm.DeleShowUserTimeLine(mainForm.AddUserTimeLine);
				mainForm.BeginInvoke(dele, new object[] { listTweet, userName });
			}
		}

		private void ResponseMention(string json, bool isMore)
		{
			List<ClientTweet> listTweet = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
			if (isMore)
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddMoreTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.MENTION });
			}
			else
			{
				MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddTweet);
				mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.MENTION });
			}
		}

		private void ResponseHomeTimeLine(string json)
		{
			//File.AppendAllText("Data/Stream.txt", json);
			//File.AppendAllText("Data/Stream.txt", "\n\n");
			List<ClientTweet> listTweet = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
			MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddTweet);
			mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.TIME_LINE });
		}

		//첫 실행 시 내 정보 받아오기
		private void ResponseMyInfo(string json)
		{
			User user = JsonConvert.DeserializeObject<User>(json);

			DataInstence.SetUser(user);
			FileInstence.UpdateToken(DataInstence.userInfo);
			MainForm.DeleUpdateProfilePicture dele = new MainForm.DeleUpdateProfilePicture(mainForm.UpdateProfilePicture);
			mainForm.BeginInvoke(dele, new object[] { user.profile_image_url });

			MainForm.DeleLoadAccount dele2 = new DeleLoadAccount(mainForm.LoadAccounts);
			mainForm.BeginInvoke(dele2);
			StartApiCalls();
		}

		//관심글 등록,해제
		private void ResponseFavorite(string json, bool isCreate)
		{
			ClientTweet tweet = JsonConvert.DeserializeObject<ClientTweet>(json);

			MainForm.DeleUpdateTweet deleCreateFav = new DeleUpdateTweet(mainForm.UpdateTweetFavorite);
			mainForm.BeginInvoke(deleCreateFav, new object[] { tweet, isCreate });
		}

		private void ResponseRetweet(string json, bool isRetweet)
		{
			if (WebInstence.isConnectedUserStreaming() && isRetweet)
				return;//스트리밍 연결 돼있고 리트윗일 경우 스트리밍 delete이벤트에서 처리

			ClientTweet tweet = JsonConvert.DeserializeObject<ClientTweet>(json);

			MainForm.DeleUpdateTweet deleCreateRetweet = new DeleUpdateTweet(mainForm.UpdateTweetRetweet);
			mainForm.BeginInvoke(deleCreateRetweet, new object[] { tweet, isRetweet });
		}
	}//class
}
