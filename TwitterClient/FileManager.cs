using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;
using System.Drawing;
using System.Drawing.Imaging;

namespace TwitterClient
{
	[ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
	class FileManager
	{
		private static FileManager instence;

		private const string userFilePath = "Data/switter.ini";
		private const string optionFilePath = "Data/option.ini";
		private const string hotkeyFilePath = "Data/hotkey.ini";
		private const string followFilePath = "Data/follow.ini";
		private const string blockFilePath = "Data/block.ini";

		private const string dataFolderPath = "Data";
		private const string skinFolderPath = "Skin";
		private const string imageFolderPath = "Image";
		public const string soundFolderPath = "Sound";

		private Switter switter = new Switter();
		private FollowList followList = new FollowList();
		public static FileManager FileInstence { get { return GetInstence(); } }
		private static FileManager GetInstence()
		{
			if (instence == null)
			{
				instence = new TwitterClient.FileManager();
				instence.Init();
			}

			return instence;
		}

		public void Init()
		{
			CheckDataFolder();
			LoadToken();
			LoadOption();
			LoadHotKey();
			LoadFollowList();
			LoadBlockList();
			LoadSkin();
			//LoadSkin();
		}

		private void CheckDataFolder()
		{
			if (Directory.Exists(dataFolderPath) == false)
				System.IO.Directory.CreateDirectory(dataFolderPath);
			if (Directory.Exists(soundFolderPath) == false)
				System.IO.Directory.CreateDirectory(soundFolderPath);
			if (Directory.Exists(skinFolderPath) == false)
				System.IO.Directory.CreateDirectory(skinFolderPath);
			if (Directory.Exists(imageFolderPath) == false)
				System.IO.Directory.CreateDirectory(imageFolderPath);
		}

		private void LoadBlockList()
		{
			BlockList blockList = null;
			//Dictionary<long, char> dicBlock = null;
			if (File.Exists(blockFilePath))
			{
				string json = string.Empty;
				using (StreamReader sr = new StreamReader(blockFilePath))
				{
					json = sr.ReadToEnd();
				}
				blockList = JsonConvert.DeserializeObject<BlockList>(json);
				//dicBlock = JsonConvert.DeserializeObject<Dictionary<long, char>>(json);
			}
			else
			{
				blockList = new BlockList();
			}
			DataInstence.SetBlockList(blockList);
		}

		private void LoadFollowList()
		{
			//Dictionary<long, UserSemi> dicUsers = null;
			if (File.Exists(followFilePath))
			{
				string json = string.Empty;
				using (StreamReader sr = new StreamReader(followFilePath))
				{
					json = sr.ReadToEnd();
				}
				followList = JsonConvert.DeserializeObject<FollowList>(json);
				if (switter.selectAccount != null)
					DataInstence.SetFollowing(followList.dicFollow[switter.selectAccount.id]);
			}
			else
			{
				followList = new FollowList();
			}
		}

		//private void LoadFollowList()
		//{
		//	//FollowList followList = new FollowList();
		//	Dictionary<long, UserSemi> dicUsers = null;
		//	if (File.Exists(followFilePath))
		//	{
		//		string json = string.Empty;
		//		using (StreamReader sr = new StreamReader(followFilePath))
		//		{
		//			json = sr.ReadToEnd();
		//		}
		//		dicUsers = JsonConvert.DeserializeObject<Dictionary<long, UserSemi>>(json);
		//	}
		//	else
		//	{
		//		dicUsers = new Dictionary<long, UserSemi>();
		//	}
		//	DataInstence.SetFollowing(dicUsers);
		//}

		private void LoadSkin()
		{
			Skin skin = null;
			string path = skinFolderPath + "/" + DataInstence.option.skinName + ".txt";
			if (File.Exists(path))//스킨 파일 체크
			{
				string json = string.Empty;

				using (StreamReader sr = new StreamReader(path))
				{
					json = sr.ReadToEnd();
				}

				try
				{
					skin = JsonConvert.DeserializeObject<Skin>(json);
				}
				catch
				{
					skin = new Skin();
				}
			}
			else
			{
				skin = new Skin();
			}
			DataInstence.UpdateSkin(skin);
		}

		private void LoadOption()
		{
			Option option;
			if (System.IO.File.Exists(optionFilePath))
			{
				string json = string.Empty;

				using (StreamReader sr = new StreamReader(optionFilePath))
				{
					json = sr.ReadToEnd();
				}

				option = JsonConvert.DeserializeObject<Option>(json);
			}
			else
			{
				option = new TwitterClient.Option();
				UpdateOption(option);
			}
			DataInstence.SetOption(option);
		}

		private void LoadHotKey()
		{
			HotKeys hotkey;
			if (System.IO.File.Exists(hotkeyFilePath))
			{
				string json = string.Empty;

				using (StreamReader sr = new StreamReader(hotkeyFilePath))
				{
					json = sr.ReadToEnd();
				}

				hotkey = JsonConvert.DeserializeObject<HotKeys>(json);
			}
			else
			{
				hotkey = new TwitterClient.HotKeys();
				hotkey.SetDefaultKey();
				UpdateHotkey(hotkey);
			}
			DataInstence.SetHotkey(hotkey);
		}

		private void LoadToken()
		{
			if (System.IO.File.Exists(userFilePath))
			{
				string json = string.Empty;

				using (StreamReader sr = new StreamReader(userFilePath))
				{
					json = sr.ReadToEnd();
				}

				switter = JsonConvert.DeserializeObject<Switter>(json);

				ChangeAccount(switter.selectAccount);
			}
		}

		//private void LoadToken()//TODO 얘 혼자 함수 작동 방식이 다름
		//{
		//	if (System.IO.File.Exists(userFilePath))
		//	{
		//		string json = string.Empty;

		//		using (StreamReader sr = new StreamReader(userFilePath))
		//		{
		//			json = sr.ReadToEnd();
		//		}

		//		UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json);

		//		if (!string.IsNullOrEmpty(userInfo.Token) || !string.IsNullOrEmpty(userInfo.TokenSecret))//비어있지 않을 경우만 갱신
		//		{
		//			//GlobalOptionAndKey.GetInstence().SetPin(userInfo.Pin);
		//			DataInstence.SetUserToken(userInfo.Token);
		//			DataInstence.SetUserTokenSecret(userInfo.TokenSecret);
		//		}
		//	}
		//}

		public string[] GetSoundList()
		{
			if (Directory.Exists(soundFolderPath))
			{
				DirectoryInfo di = new DirectoryInfo(soundFolderPath);
				FileInfo[] fi = di.GetFiles();
				List<string> listSound = new List<string>();
				for (int i = 0; i < fi.Length; i++)
				{
					if (fi[i].Extension == ".wav" || fi[i].Extension == ".ogg")//wav, ogg일 경우에만 추가
						listSound.Add(fi[i].Name);
				}

				return listSound.ToArray();
			}
			else
			{
				return null;
			}
		}

		public string[] GetSkinList()
		{
			if (Directory.Exists(skinFolderPath))
			{
				DirectoryInfo di = new DirectoryInfo(skinFolderPath);
				FileInfo[] fi = di.GetFiles();
				List<string> listSkin = new List<string>();
				for (int i = 0; i < fi.Length; i++)
				{
					if (fi[i].Extension == ".txt")//txt일경우에만 추가
						listSkin.Add(fi[i].Name.Replace(".txt", ""));
				}

				return listSkin.ToArray();
			}
			else
			{
				return null;
			}
		}

		public void UpdateSkin()
		{
			LoadSkin();
		}

		public void TestStream(string json)
		{
			File.AppendAllText("Data/Stream.txt", json);
			File.AppendAllText("Data/Stream.txt", "\n\n");
		}

		public bool isChangeAccount(string screen_name)
		{
			return switter.selectAccount.screen_name == screen_name;
		}

		public string DeleteSelectAccount()
		{
			string ret = string.Empty;
			if (switter.dicUserKey.ContainsKey(switter.selectAccount.id) == false) return ret;

			ret = switter.dicUserKey[switter.selectAccount.id].screen_name;
			switter.dicUserKey.Remove(switter.selectAccount.id);
			switter.selectAccount = null;

			string json = JsonConvert.SerializeObject(switter);
			File.WriteAllText(userFilePath, json);
			return ret;
		}

		private void ChangeAccount(UserKey userKey)
		{
			UserKey token = null;
			if (userKey != null)
			{
				if(switter.dicUserKey.ContainsKey(userKey.id))
					token = switter.dicUserKey[userKey.id];
			}
			else
			{
				if (switter.dicUserKey.Count > 0)
				{
					foreach (UserKey item in switter.dicUserKey.Values)
					{
						token = item;
						break;
					}
				}
			}
			foreach (UserKey item in switter.dicUserKey.Values)
			{
				if (item.id == token.id)
				{
					switter.selectAccount = item;
					break;
				}
			}
			if (token != null)
			{
				DataInstence.SetUserToken(token.Token);
				DataInstence.SetUserTokenSecret(token.TokenSecret);
			}
		}

		public List<string> GetAccountArray()
		{
			List<string> listScreenName = new List<string>();
			foreach (UserKey item in switter.dicUserKey.Values)
			{
				listScreenName.Add(item.screen_name);
			}
			return listScreenName;
		}


		public void ChangeAccount(string screen_name)
		{
			foreach (UserKey item in switter.dicUserKey.Values)
			{
				if (item.screen_name == screen_name)
				{
					DataInstence.SetUserToken(item.Token);
					DataInstence.SetUserTokenSecret(item.TokenSecret);
					switter.selectAccount = item;
					DataInstence.SetFollowing(followList.dicFollow[switter.selectAccount.id]);
					break;
				}
			}
		}

		public void UpdateToken(UserInfo userinfo)
		{
			User user = userinfo.myUser;
			if (switter.dicUserKey.ContainsKey(user.id) == false)
			{
				UserKey key = new UserKey();
				key.id = user.id;
				key.Token = userinfo.Token;
				key.TokenSecret = userinfo.TokenSecret;
				key.screen_name = user.screen_name;
				switter.dicUserKey.Add(user.id, key);
				switter.selectAccount = key;
			}
			else
			{
				switter.dicUserKey[user.id].screen_name = user.screen_name;
			}

			string json = JsonConvert.SerializeObject(switter);
			File.WriteAllText(userFilePath, json);
		}

		//public void UpdateUserInfo(User user)
		//{
		//	if(switter.dicUserKey.ContainsKey(user.id))
		//	{
		//		switter.dicUserKey[user.id].id = user.id;
		//		switter.dicUserKey[user.id].screen_name = user.screen_name;
		//	}
		//}

		public void UpdateFollowList(Dictionary<long, UserSemi> dicUser)
		{
			if (switter.selectAccount == null) return;

			if (followList.dicFollow.ContainsKey(switter.selectAccount.id))
				followList.dicFollow[switter.selectAccount.id] = dicUser;
			else
				followList.dicFollow.Add(switter.selectAccount.id, dicUser);

			string json = JsonConvert.SerializeObject(followList);
			File.WriteAllText(followFilePath, json);
		}

		public void UpdateBlockList(BlockList blockList)
		{
			string json = JsonConvert.SerializeObject(blockList);
			File.WriteAllText(blockFilePath, json);
		}

		public void UpdateHotkey(HotKeys hotkey)
		{
			string json = JsonConvert.SerializeObject(hotkey);
			File.WriteAllText(hotkeyFilePath, json);
		}

		public void UpdateOption(Option option)
		{
			string json = JsonConvert.SerializeObject(option);
			File.WriteAllText(optionFilePath, json);
		}

		public void SaveImage(string url, Bitmap bitmap)
		{
			string path = DataInstence.option.imageFolderPath;
			if (Directory.Exists(path) == false)
			{
				if (Directory.Exists(imageFolderPath) == false)
					System.IO.Directory.CreateDirectory(imageFolderPath);
				path = imageFolderPath;
				DataInstence.option.imageFolderPath = path;
			}
			string http = url;
			string fileName = http.Replace("https://pbs.twimg.com/media/", "");
			string ext = fileName.Substring(fileName.IndexOf('.') + 1, 3);
			if (ext == "jpg")
				bitmap.Save($"{path}/{fileName}", ImageFormat.Jpeg);
			else if (ext == "png")
				bitmap.Save($"{path}/{fileName}", ImageFormat.Png);
		}

		private class Switter
		{
			public UserKey selectAccount { get; set; }
			//public TokenInfo selectUserInfo { get; set; }
			public Dictionary<long, UserKey> dicUserKey = new Dictionary<long, UserKey>();
		}

		private class FollowList
		{
			public Dictionary<long, Dictionary<long, UserSemi>> dicFollow = new Dictionary<long, Dictionary<long, UserSemi>>();//1차키: 계정 id, 2차키: usersemi의 id
		}

		private class UserKey
		{
			public long id { get; set; }
			public string screen_name { get; set; }
			public string Token { get; set; }
			public string TokenSecret { get; set; }
		}

	}
}
