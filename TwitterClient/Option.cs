using System;
using System.Collections.Generic;
using System.Drawing;
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
	public class Option//옵션 추가 시 복사 생성자 유의!!!
	{
		public Option() { }
		public Option(Option option)
		{
			isMuteMention = option.isMuteMention;
			isShowRetweet = option.isShowRetweet;
			isMuteMention = option.isMuteMention;
			isShowTweetCount = option.isShowTweetCount;
			isYesnoTweet = option.isYesnoTweet;
			isRetweetProtectUser = option.isRetweetProtectUser;
			isSendEnter = option.isSendEnter;
			skinName = option.skinName;
			notiSound = option.notiSound;
			sizeImageForm = option.sizeImageForm;
			sizeMainForm = option.sizeMainForm;
			isSmallUI = option.isSmallUI;
			pointImageForm = option.pointImageForm;
			pointMainForm = option.pointMainForm;
			font = option.font;
			isPlayNoti = option.isPlayNoti;
			isNotiRetweet = option.isNotiRetweet;
			boldFont = new Font(font, FontStyle.Bold);
			dateFont = new Font(font.Name, 9);
			imageFolderPath = option.imageFolderPath;

			for (int i = 0; i < option.listHighlight.Count; i++)
				listHighlight.Add(option.listHighlight[i]);
			for (int i = 0; i < option.listMuteWord.Count; i++)
				listMuteWord.Add(option.listMuteWord[i]);
			for (int i = 0; i < option.listMuteClient.Count; i++)
				listMuteClient.Add(option.listMuteClient[i]);
			for (int i = 0; i < option.listMuteUser.Count; i++)
				listMuteUser.Add(option.listMuteUser[i]);
		}
		public bool isShowRetweet { get; set; } = true;//TL에 리트윗을 띄울지
		public bool isNotiRetweet { get; set; } = true;//멘션함에 리트윗을 띄울지
		//public bool isShowBlockUser { get; set; } = false;//블락 한 사람을 띄울지(기본 안 띄움)
		public bool isMuteMention { get; set; } = true;//멘션함도 뮤트(기본 해제)
		public bool isShowTweetCount { get; set; } = false; //새 트윗 생성 시 리트윗수, 관글수 표시할지(기본 해제)
		public bool isYesnoTweet { get; set; } = false;//트윗 올릴 때 물어보고 올릴지(기본 안 물어봄)
		public bool isRetweetProtectUser { get; set; } = true;//플텍 유저일 경우 수동RT해줄지(기본 함)
		public bool isSendEnter { get; set; } = false;//트윗 올릴 때 Enter / Ctrl+Enter고르기(기본 ctrl+enter)
		public string skinName { get; set; } = "pink";
		public bool isPlayNoti { get; set; } = false;
		public string notiSound { get; set; }//알림 소리(선택박스, sound폴더)
		public bool isSmallUI { get; set; } = false;
		public Point pointMainForm { get; set; } = new Point(500, 500);
		public Point pointImageForm { get; set; } = new Point(500, 500);
		public Size sizeMainForm { get; set; } = new Size(573, 783);
		public Size sizeImageForm { get; set; } = new Size(500, 500);
		public string imageFolderPath { get; set; } = "Image";

		public List<string> listHighlight = new List<string>();//단어 하이라이트(리스트 박스?)
		public List<string> listMuteWord = new List<string>();//단어 뮤트(리스트 박스)
		public List<string> listMuteClient = new List<string>();//클라이언트 뮤트(리스트 박스)
		public List<string> listMuteUser = new List<string>();//유저 뮤트(리스트 박스)
		[Newtonsoft.Json.JsonIgnore]
		public Font boldFont = new Font("맑은 고딕", 10, FontStyle.Bold);
		[Newtonsoft.Json.JsonIgnore]
		public Font dateFont = new Font("맑은 고딕", 9);
		public Font font = new Font("맑은 고딕", 10);//폰트/폰트크기(기본: 맑은 고딩 10)

		public bool MatchHighlight(string text)
		{
			bool ret = false;
			for (int i = 0; i < listHighlight.Count; i++)
				if (text.IndexOf(listHighlight[i]) > -1)
				{
					ret = true;
					break;
				}
			return ret;
		}

		public bool MatchMuteWord(string text)
		{
			bool ret = false;
			for (int i = 0; i < listMuteWord.Count; i++)
				if (text.IndexOf(listMuteWord[i], StringComparison.OrdinalIgnoreCase) > -1)
				{
					ret = true;
					break;
				}

			return ret;
		}

		public bool MatchMuteClient(string text)
		{
			bool ret = false;
			for (int i = 0; i < listMuteClient.Count; i++)
				if(string.Equals(listMuteClient[i], text, StringComparison.OrdinalIgnoreCase))
				{
					ret = true;
					break;
				}

			return ret;
		}

		public bool MatchMuteUser(string screenName)
		{
			bool ret = false;
			for (int i = 0; i < listMuteUser.Count; i++)
				if (string.Equals(listMuteUser[i], screenName, StringComparison.OrdinalIgnoreCase))
				{
					ret = true;
					break;
				}

			return ret;
		}

		public void AddMuteUser(string user)
		{
			listMuteUser.Add(user);
		}

		public void AddMuteClient(string client)
		{
			listMuteClient.Add(client);
		}
	}

	public class Skin
	{
		public Color blockOne { get; set; } = Color.FromArgb(255, 224, 224);
		public Color blockTwo { get; set; } = Color.FromArgb(255, 207, 207);
		public Color mentionOne { get; set; } = Color.FromArgb(0xe6, 0xff, 0xe6);
		public Color mentionTwo { get; set; } = Color.FromArgb(0xff, 0xff, 0xe0);
		public Color tweet { get; set; } = Color.Black;
		public Color retweet { get; set; } = Color.FromArgb(0x7e,0x7b,0xff);
		public Color mention { get; set; } = Color.FromArgb(0xff, 0x4b, 0x6a);
		public Color myretweet { get; set; }= Color.FromArgb(0x7e, 0x7b, 0xff);
		public Color defaultColor { get; set; } = Color.FromArgb(255, 224, 224);
		public Color leaveColor { get; set; } = Color.FromArgb(0xd9, 0xb0, 0xb0);
		public Color moreButton { get; set; } = Color.FromArgb(0xe5, 0x91, 0xd0);
		public Color topbar { get; set; }= Color.FromArgb(255, 207, 207);
		public Color bottomBar { get; set; } = Color.FromArgb(0xff, 0xbf, 0xbf);
		public Color menuColor { get; set; } = Color.FromArgb(0xff, 0x4b, 0x6a);
		public Color select { get; set; } = Color.FromArgb(0xcd, 0xc1, 0xd8);
		public Skin() { }
	}

	public class HotKeys
	{
		public HotKeys()
		{

		}

		public HotKeys(HotKeys key)
		{
			keyReplyAll = key.keyReplyAll;
			keyReply = key.keyReply;
			keyRetweet = key.keyRetweet;
			keyFavorite = key.keyFavorite;
			keyQRetweet = key.keyQRetweet;
			keyInput = key.keyInput;
			keyDM = key.keyDM;
			keyHashTag = key.keyHashTag;
			keyShowTL = key.keyShowTL;
			keyShowMention = key.keyShowMention;
			keyShowDM = key.keyShowDM;
			keyShowFavorite = key.keyShowFavorite;
			keyGoHome = key.keyGoHome;
			keyGoEnd = key.keyGoEnd;
			keyDeleteTweet = key.keyDeleteTweet;
			keyLoad = key.keyLoad;
			keyMenu = key.keyMenu;
			keyShowOpendURL = key.keyShowOpendURL;
		}

		public void SetDefaultKey()
		{
			keyReplyAll = Keys.A;
			keyReply = Keys.R;
			keyDeleteTweet = Keys.Delete;
			keyDM = Keys.D;
			keyFavorite = Keys.F;
			keyGoEnd = Keys.End;
			keyGoHome = Keys.Home;
			keyHashTag = Keys.H;
			keyInput = Keys.U;
			keyLoad = Keys.Space;
			keyQRetweet = Keys.W;
			keyRetweet = Keys.T;
			keyShowDM = Keys.D3;
			keyShowFavorite = Keys.D4;
			keyShowMention = Keys.D2;
			keyShowTL = Keys.D1;
			keyMenu = Keys.V;
			keyShowOpendURL = Keys.D5;
		}

		public enum eHotKeys
		{
			eKeyReplyAll,
			eKeyReply,
			eKeyRetweet,
			eKeyFavorite,
			eKeyQRetweet,
			eKeyInput,
			eKeySendDM,
			eKeyHashTag,
			eKeyShowTL,
			eKeyShowMention,
			eKeyShowDM,
			eKeyShowFavorite,
			eKeyGoHome,
			eKeyGoEnd,
			eKeyDeleteTweet,
			eKeyLoad,
			eKeyShowOpendUrl,
			eKeyMenu,
		}
		private Dictionary<eHotKeys, Keys> dicKeys = new Dictionary<eHotKeys, Keys>();
		public Keys keyReplyAll//전체 답변       A
		{
			get { return dicKeys[eHotKeys.eKeyReplyAll]; }
			set { dicKeys[eHotKeys.eKeyReplyAll] = value; }
		}
		public Keys keyReply//개인 답변 R
		{
			get { return dicKeys[eHotKeys.eKeyReply]; }
			set { dicKeys[eHotKeys.eKeyReply] = value; }
		}
		public Keys keyRetweet//리트윗 T
		{
			get { return dicKeys[eHotKeys.eKeyRetweet]; }
			set { dicKeys[eHotKeys.eKeyRetweet] = value; }
		}
		public Keys keyFavorite//관글 F
		{
			get { return dicKeys[eHotKeys.eKeyFavorite]; }
			set { dicKeys[eHotKeys.eKeyFavorite] = value; }
		}
		public Keys keyQRetweet//인용RT W
		{
			get { return dicKeys[eHotKeys.eKeyQRetweet]; }
			set { dicKeys[eHotKeys.eKeyQRetweet] = value; }
		}
		public Keys keyInput//트윗입력창 가기    U/Enter
		{
			get { return dicKeys[eHotKeys.eKeyInput]; }
			set { dicKeys[eHotKeys.eKeyInput] = value; }
		}
		public Keys keyDM//DM보내기   D
		{
			get { return dicKeys[eHotKeys.eKeySendDM]; }
			set { dicKeys[eHotKeys.eKeySendDM] = value; }
		}
		public Keys keyHashTag//트윗에 있는 해시 전부 추가 H
		{
			get { return dicKeys[eHotKeys.eKeyHashTag]; }
			set { dicKeys[eHotKeys.eKeyHashTag] = value; }
		}
		public Keys keyShowTL//TL보기		1
		{
			get { return dicKeys[eHotKeys.eKeyShowTL]; }
			set { dicKeys[eHotKeys.eKeyShowTL] = value; }
		}
		public Keys keyShowMention//멘션함 보기	2
		{
			get { return dicKeys[eHotKeys.eKeyShowMention]; }
			set { dicKeys[eHotKeys.eKeyShowMention] = value; }
		}
		public Keys keyShowDM//DM보기		3
		{
			get { return dicKeys[eHotKeys.eKeyShowDM]; }
			set { dicKeys[eHotKeys.eKeyShowDM] = value; }
		}
		public Keys keyShowFavorite//관글함 보기	4
		{
			get { return dicKeys[eHotKeys.eKeyShowFavorite]; }
			set { dicKeys[eHotKeys.eKeyShowFavorite] = value; }
		}

		public Keys keyShowOpendURL//열은 트윗 보기 5
		{
			get { return dicKeys[eHotKeys.eKeyShowOpendUrl]; }
			set { dicKeys[eHotKeys.eKeyShowOpendUrl] = value; }
		}
		public Keys keyGoHome//맨위로 홈
		{
			get { return dicKeys[eHotKeys.eKeyGoHome]; }
			set { dicKeys[eHotKeys.eKeyGoHome] = value; }
		}
		public Keys keyGoEnd//맨아래로 엔드
		{
			get { return dicKeys[eHotKeys.eKeyGoEnd]; }
			set { dicKeys[eHotKeys.eKeyGoEnd] = value; }
		}
		public Keys keyMenu//트윗 메뉴 띄우기		v
		{
			get { return dicKeys[eHotKeys.eKeyMenu]; }
			set { dicKeys[eHotKeys.eKeyMenu] = value; }
		}
		public Keys keyDeleteTweet//트윗 삭제	delete
		{
			get { return dicKeys[eHotKeys.eKeyDeleteTweet]; }
			set { dicKeys[eHotKeys.eKeyDeleteTweet] = value; }
		}
		public Keys keyLoad//현재 패널 글 불러오기     스페이스
		{
			get { return dicKeys[eHotKeys.eKeyLoad]; }
			set { dicKeys[eHotKeys.eKeyLoad] = value; }
		}

		//글 선택 이동 화살표(변경 불가)
		//새로고침 스페이스 길게(변경 불가)
		//-패널을 비우고 새로 API요청

		public void UpdateKey(eHotKeys ehotKey, Keys key)
		{
			//bool ret = dicKeys.ContainsValue(key);
			//if (ret == false)//중복 키가 없을 경우
			//{
			//	dicKeys[ehotKey] = key;
			//}
			dicKeys[ehotKey] = key;
			//return !ret;//return: 업데이트 성공여부
		}

	}
}
