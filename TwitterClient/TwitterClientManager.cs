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
using static TwitterClient.MainForm;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public partial class TwitterClientManager
	{
		private static TwitterClientManager instence;
		private MainForm mainForm;
		//private string userTL_ID;

		private bool isEndLoadTweet = false;
		private bool isEndLoadMention = false;
		private SoundPlayer notiSound;
		private bool isLoadingTweet = false;

		public static TwitterClientManager ClientInctence { get { return GetInstence(); } }

		private static TwitterClientManager GetInstence()
		{
			if (instence == null)
			{
				instence = new TwitterClient.TwitterClientManager();
				instence.Init();
			}
			return instence;
		}

		public void Init()
		{
			ThreadPool.SetMaxThreads(6, 6);//thread pool 세팅
		}

		public void ClearClient(string screen_name)
		{
			ClearClient();
			FileInstence.ChangeAccount(screen_name);
			GetMyInfo();
		}

		public void ClearClient()
		{
			isLoadingTweet = false;
			isEndLoadTweet = false;
			isEndLoadMention = false;
			WebInstence.DisconnectingUserStreaming();
			InjangManager.GetInstence().Clear();
			DataInstence.ClearToken();

			GC.Collect();
		}

		public void AddAccount()
		{
			ClearClient();
			StartGetOAuth();
		}

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------외부 호출---------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		
		//pin잘못 입력했을 경우 재시도 하는 콜백
		public void RestartOAuth(DialogResult dr)
		{
			if (dr == DialogResult.OK)
			{
				DataInstence.ClearToken();
				StartGetOAuth();
			}
		}

		public void UpdateToken(string token, string secret, bool isOAuthEnd)
		{
			DataInstence.SetUserToken(token);
			DataInstence.SetUserTokenSecret(secret);

			if (isOAuthEnd)
			{
				//FileInstence.UpdateToken(DataInstence.userInfo);
				GetMyInfo();
				DataInstence.OAuthEnd();
				ThreadPool.QueueUserWorkItem(GetBlockIds, null);
				ThreadPool.QueueUserWorkItem(GetFollowList, null);
			}
		}

		//핀 입력 시 핀값을 PinForm에서 받아온 후 AccessToken을 발급받는다
		//pin: pin값
		public void UpdatePin(string pin)
		{
			GetAccessToken(pin);
		}


		public void SetMainForm(MainForm form)
		{
			this.mainForm = form;
		}

		public void EndLoadStartTweet()
		{
			isEndLoadTweet = true;
		}

		public void EndLoadStartMention()
		{
			isEndLoadMention = true;
		}

		public void EndLoadingTweet()
		{
			isLoadingTweet = false;
		}


		public void LoadedMainForm()
		{
			if (string.IsNullOrEmpty(DataInstence.userInfo.Token) ||
				string.IsNullOrEmpty(DataInstence.userInfo.TokenSecret))//키가 없을 경우
			{
				StartGetOAuth();
			}
			else
			{
				GetMyInfo();
			}
			ChangeSoundNoti(DataInstence.option.notiSound);
		}

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------내부 기능-----------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		private void StartApiCalls()
		{
			LoadTimeLine();
			GetRetweetOffIds();
			LoadMention();
			ThreadPool.QueueUserWorkItem(CheckLoadTweet, null);
		}

		private void CheckLoadTweet(object obj)
		{
			for (int i = 0; i < 200; i++)
				if (isEndLoadTweet && isEndLoadMention)
				{
					Thread.Sleep(1000);
					ConnectUserStreaming();
					break;
				}
				else
					Thread.Sleep(2000);
		}
		
		

		private void ShowInputPinForm()
		{
			PinForm form2 = new PinForm(true);
			form2.ShowDialog(mainForm);
		}


		

		//private bool CheckBetaUser(string name)
		//{
		//	HashSet<string> hashName = new HashSet<string>();
		//	hashName.Add("sakaki_shizuka");
		//	hashName.Add("LeonardWr");
		//	hashName.Add("juckmadosa");
		//	hashName.Add("sapphire_dev");
		//	hashName.Add("J_BaGi");
		//	hashName.Add("hanalen_");
		//	hashName.Add("PenGOrder_2");
		//	hashName.Add("open_ranka_");
		//	hashName.Add("Remilias_");
		//	hashName.Add("Zynyste");
		//	hashName.Add("eliemiz");
		//	hashName.Add("thesoulfate");
		//	hashName.Add("_RyuaRin");
		//	hashName.Add("Liansod");
		//	hashName.Add("Edgestorm");
		//	hashName.Add("spr0tsuki");
		//	hashName.Add("_Erio");
		//	hashName.Add("Norong_A");
		//	hashName.Add("QASTY_");
		//	hashName.Add("Kw_Saki");
		//	hashName.Add("sazanami_kai_2");
		//	hashName.Add("Iori_Jino");
		//	hashName.Add("cloverkuns");
		//	hashName.Add("harne_");
		//	hashName.Add("ruiness");
		//	hashName.Add("Hirno_");
		//	hashName.Add("Okirasama");
		//	hashName.Add("Shizuha_");
		//	hashName.Add("PasteCat");
		//	hashName.Add("loeldeno");
		//	hashName.Add("Neko_arc");
		//	hashName.Add("_DELETE");
		//	hashName.Add("TakaMinami_");
		//	hashName.Add("KOR_Eagle");
		//	hashName.Add("_20min");
		//	hashName.Add("EMPRESS_PLACE");
		//	hashName.Add("BinJip_");
		//	hashName.Add("ascoeur9");
		//	hashName.Add("TreePer0552");
		//	hashName.Add("Ssomebody_KR");
		//	hashName.Add("Luma_Debug");
		//	hashName.Add("Lusain_Kim");

		//	return hashName.Contains(name);
		//}

		

		

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------UI 기능-----------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		public void SelectControl(TweetControl con)
		{
			mainForm.SelectControl(con);
		}

		public void LoadSingleTweet(TweetControl con)
		{
			mainForm.LoadSingleTweet(con);
		}

		public void HideSingleTweet(TweetControl con)
		{
			mainForm.HideSingleTweet(con);
		}

		public void Debug(object text)
		{
			MainForm.DeleDebug dele = new MainForm.DeleDebug(mainForm.Debug);
			mainForm.BeginInvoke(dele, new object[] { text });
		}

		public void ChangeSmallUI(bool isSmall)
		{
			DeleChangeSmallUI dele = new DeleChangeSmallUI(mainForm.ChangeSmallUI);
			mainForm.BeginInvoke(dele, new object[] { isSmall });
		}

		public void ChangeSkin()
		{
			MainForm.DeleChangeSkin dele = new DeleChangeSkin(mainForm.ChangeSkin);
			mainForm.BeginInvoke(dele);
		}

		public void EnterInputTweet()
		{
			MainForm.DeleEnterInput dele = new DeleEnterInput(mainForm.EnterInput);
			mainForm.BeginInvoke(dele);
		}

		public void AddDMId(string name)
		{
			MainForm.DeleAddDMId dele = new DeleAddDMId(mainForm.AddIdDm);
			mainForm.BeginInvoke(dele, new object[] { name });
		}

		public void AddMentionIDS(bool isReplyAll, ClientTweet tweet)
		{
			MainForm.DeleAddMentionId dele = new DeleAddMentionId(mainForm.AddMentionIds);
			mainForm.BeginInvoke(dele, new object[] { isReplyAll, tweet });
		}

		public void ConnectedStreaming(bool isCon)
		{
			MainForm.DeleTaskBarUpdate dele = new DeleTaskBarUpdate(mainForm.UpdateTaskBar);
			if(isCon)
				mainForm.BeginInvoke(dele, new object[] {eTaskBar.STREAMING, "유저스트리밍(ON)" });
			else
				mainForm.BeginInvoke(dele, new object[] { eTaskBar.STREAMING, "유저스트리밍(OFF)" });

		}

		public void PlaySoundNoti()
		{
			if (notiSound != null && DataInstence.option.isPlayNoti)
				notiSound.Play();
		}

		public void AddOpenURL(List<ClientTweet> listTweet)
		{
			MainForm.DeleAddTweetList dele = new MainForm.DeleAddTweetList(mainForm.AddTweet);
			mainForm.BeginInvoke(dele, new object[] { listTweet, MainForm.eSelectMenu.OPEN_URL });
		}

		public void ChangeFont()
		{
			DeleChangeFont dele = new DeleChangeFont(mainForm.ChangeFont);
			mainForm.BeginInvoke(dele);
		}

		public void AddHashTag(string text)
		{
			DeleAddHashtag dele = new DeleAddHashtag(mainForm.AddHashtag);
			mainForm.BeginInvoke(dele, new object[] { text });
		}

		public void AddQTRetweet(ClientTweet tweet)
		{
			DeleQTRetweet dele = new DeleQTRetweet(mainForm.QTRetweet);
			mainForm.BeginInvoke(dele, new object[] {tweet });
		}

		public void ChangeSoundNoti(string path)
		{
			if (string.IsNullOrEmpty(path)) return;
			notiSound = new SoundPlayer(Path.GetDirectoryName(Application.ExecutablePath) + "\\" +
										FileManager.soundFolderPath + "\\" + path);
			notiSound.Load();
		}

		public void ShowMessageBox(string message, string title)
		{
			MainForm.DeleShowMessageBox dele = new DeleShowMessageBox(mainForm.ShowMessageBox);
			mainForm.BeginInvoke(dele, new object[] { message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning });
		}

		public void ShowMessageBox(string message, string title, MessageBoxIcon icon)
		{
			MainForm.DeleShowMessageBox dele = new DeleShowMessageBox(mainForm.ShowMessageBox);
			mainForm.BeginInvoke(dele, new object[] { message, title, MessageBoxButtons.OK, icon });
		}

		public void ShowMessageBox(string message, string title, Action<DialogResult> callback)
		{
			MainForm.DeleShowMessageBoxCallBack dele = new DeleShowMessageBoxCallBack(mainForm.ShowMessageBox);
			mainForm.BeginInvoke(dele, new object[] { message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning, callback });
		}

		public void LoadUserTweet(string name)
		{
			MainForm.DeleShowUserPanel dele = new DeleShowUserPanel(mainForm.ShowUserPanel);
			mainForm.BeginInvoke(dele, new object[] { name });
			LoadTweet(eSelectMenu.USER, name);
		}

		public void ShowPanel(eSelectMenu eselect)
		{
			MainForm.DeleShowPanel dele = new DeleShowPanel(mainForm.ShowPanelDele);
			mainForm.BeginInvoke(dele, new object[] { eselect });
		}

		public void ProgramClosing()
		{
			//마지막 위치 때문에 항상 변경해야함
			FileInstence.UpdateOption(DataInstence.option);

			if(DataInstence.isChangeUserInfo)
				FileInstence.UpdateToken(DataInstence.userInfo);
			if (DataInstence.isChangeHotKey)
				FileInstence.UpdateHotkey(DataInstence.hotKey);
			if (DataInstence.isChangeFollow)
				FileInstence.UpdateFollowList(DataInstence.dicFollwing);
			if (DataInstence.isChangeBlock)
				FileInstence.UpdateBlockList(DataInstence.blockList);
		}
	}//class end
}
