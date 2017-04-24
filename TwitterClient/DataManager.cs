using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	class DataManager
	{
		public const string ConsumerKey = 
		public const string ConsumerSecret = 
		public UserInfo userInfo { get; private set; }
		public Dictionary<long, UserSemi> dicFollwing { get; private set; } = new Dictionary<long, UserSemi>();
		public HashSet<long> hashRetweetOff { get; private set; } = new HashSet<long>();
		private static DataManager instence;
		public HotKeys hotKey { get; private set; }// = new HotKeys();
		public Option option { get; private set; }// = new Option();
		public BlockList blockList { get; private set; }
		public Skin skin { get; private set; }

		public bool isChangeOption { get; private set; } = false;
		public bool isChangeFollow { get; private set; } = false;
		public bool isChangeBlock { get; private set; } = false;
		public bool isChangeHotKey { get; private set; } = false;
		public bool isChangeUserInfo { get; private set; } = false;

		public static DataManager DataInstence { get { return GetInstence(); } }
		private static DataManager GetInstence()
		{
			if (instence == null)
			{
				instence = new DataManager();
				instence.userInfo = new UserInfo();
			}

			return instence;
		}

		public void Init()
		{
			instence.userInfo = new UserInfo();
		}

		public void SetOption(Option option) { this.option = option; }
		public void SetHotkey(HotKeys hotkey) { this.hotKey = hotkey; }
		public void SetUser(User myUser) { userInfo.UpdateUser(myUser); }
		public void SetBlockList(BlockList blockList) { this.blockList = blockList; }//key: id(long)
		public void SetFollowing(Dictionary<long, UserSemi> dicFollwing) { this.dicFollwing = dicFollwing; }//key: id_str
		public void SetRetweetOff(HashSet<long> dicRetweetOff) { this.hashRetweetOff = dicRetweetOff; }
		public void SetName(string name) { userInfo.UpdateScreenName(name); }
		public void OAuthEnd() { isChangeUserInfo = true; }
		public void UpdateSkin(string name) { isChangeOption = true; option.skinName = name; }

		public bool CheckIsMe(string screenName)
		{
			if (string.Equals(userInfo.myUser.screen_name, screenName, StringComparison.OrdinalIgnoreCase))
				return true;
			else
				return false;
		}

		public void UpdateMe(string screen_name)
		{
			SetName(screen_name);
		}
		
		public bool CheckIsMe(long id)
		{
			if (userInfo.myUser.id == id)
				return true;
			else
				return false;
		}

		public void UpdateBlockIds(long id, bool isAdd)
		{
			isChangeBlock = true;

			if (isAdd)
				blockList.hashBlockUsers.Add(id);
			else
				blockList.hashBlockUsers.Remove(id);
		}

		public bool UpdateRetweetOff(long id)
		{
			if (hashRetweetOff.Contains(id))
			{
				hashRetweetOff.Remove(id);
				return false;
			}
			else
			{
				hashRetweetOff.Add(id);
				return true;
			}
		}

		public bool isRetweetOffUser(long id)
		{
			bool ret = hashRetweetOff.Contains(id);

			return ret;
		}

		public bool isBlockUser(long id)
		{
			bool ret = blockList.hashBlockUsers.Contains(id);

			return ret;
		}

		public bool isBlockUser(List<ClientUserMentions> listId)
		{
			for (int i = 0; i < listId.Count; i++)
				if (blockList.hashBlockUsers.Contains(listId[i].id))
					return true;

			return false;
		}

		public void UpdateOption(Option option)
		{
			isChangeOption = true;
			this.option = option;
			this.option.boldFont = new System.Drawing.Font(option.font, System.Drawing.FontStyle.Bold);
			this.option.dateFont = new System.Drawing.Font(option.font.Name, 10);
		}

		public void UpdateFollow(User user, bool isAdd)
		{
			isChangeFollow = true;
			if (isAdd)
			{
				UserSemi usersemi = new UserSemi(user.name, user.screen_name, user.id);
				if (dicFollwing.ContainsKey(user.id) == false)
					dicFollwing.Add(user.id, usersemi);
			}
			else
			{
				if (dicFollwing.ContainsKey(user.id))
					dicFollwing.Remove(user.id);
			}
		}

		public void UpdateSkin(Skin skin)
		{
			if (skin != null)
				this.skin = skin;
		}

		public void UpdateHotkey(HotKeys hotkey)
		{
			isChangeHotKey = true;
			this.hotKey = hotkey;
		}

		public void SetUserTokenSecret(string secret)
		{
			userInfo.UpdateSecretToken(secret);
		}

		public void SetUserToken(string token)
		{
			userInfo.UpdateToken(token);
			OAuth.GetInstence().OAuthToken = token;
		}

		public void ClearToken()
		{
			userInfo.ClearToken();
			OAuth.GetInstence().Clear();
		}

		public void UpdateFollowingName(long id, string name)
		{
			if(dicFollwing.ContainsKey(id))
			{
				if (dicFollwing[id].name != name)
				{
					dicFollwing[id].UpdateName(name);
					isChangeFollow = true;
				}
			}
		}
	}

	public class BlockList
	{
		public long next_cursor { get; set; } = -1;
		public long previous_cursor { get; set; } = -1;
		public HashSet<long> hashBlockUsers { get; set; } = new HashSet<long>();

	}

	//클라이언트 사용자 관련 값들을 들고있음
	public class UserInfo
	{
		public string Token { get; set; }
		public string TokenSecret { get; set; }
		public User myUser { get; set; }
		public void ClearToken() { Token = string.Empty; TokenSecret = string.Empty; }
		public void UpdateSecretToken(string secret)	{		this.TokenSecret = secret;	}
		public void UpdateToken(string token)	{		this.Token = token;	}
		public void UpdateScreenName(string name)	{		myUser.screen_name = name;	}
		public void UpdateUser(User user)	{		this.myUser = user;	}
	}
}
