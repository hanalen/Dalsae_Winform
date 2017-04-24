using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
	public partial class HotKeyForm : Form
	{
		private HotKeys hotkey;
		HashSet<string> hashKeys = new HashSet<string>();
		public HotKeyForm()
		{
			InitializeComponent();
			hotkey =new HotKeys(DataInstence.hotKey);
			SetHashs();
			SetComboBox();
			SetKeys();
		}
		
		private void SetHashs()
		{
			hashKeys.Add("0");
			hashKeys.Add("1");
			hashKeys.Add("2");
			hashKeys.Add("3");
			hashKeys.Add("4");
			hashKeys.Add("5");
			hashKeys.Add("6");
			hashKeys.Add("7");
			hashKeys.Add("8");
			hashKeys.Add("9");
			for (char i = 'A'; i <= 'Z'; i++)
			{
				hashKeys.Add(i.ToString());
			}
			hashKeys.Add("F1");
			hashKeys.Add("F2");
			hashKeys.Add("F3");
			hashKeys.Add("F4");
			hashKeys.Add("F5");
			hashKeys.Add("F6");
			hashKeys.Add("F7");
			hashKeys.Add("F8");
			hashKeys.Add("F9");
			hashKeys.Add("F10");
			hashKeys.Add("F11");
			hashKeys.Add("F12");
			//hashKeys.Add("`");
			//hashKeys.Add(".");
			//hashKeys.Add(",");
			//hashKeys.Add("/");
			//hashKeys.Add("\\");
			hashKeys.Add("Space");
			hashKeys.Add("Delete");
			hashKeys.Add("Home");
			hashKeys.Add("End");
			hashKeys.Add("Insert");
		}

		private void SetComboBox()
		{
			string[] arrayItems = hashKeys.ToArray();
			textReplyAll.Items.AddRange(arrayItems);
			textReply.Items.AddRange(arrayItems);
			textRetweet.Items.AddRange(arrayItems);
			textFavorite.Items.AddRange(arrayItems);
			textQRtweet.Items.AddRange(arrayItems);
			textInput.Items.AddRange(arrayItems);
			textSendDM.Items.AddRange(arrayItems);
			textDelete.Items.AddRange(arrayItems);
			textHashTag.Items.AddRange(arrayItems);
			textTL.Items.AddRange(arrayItems);
			textMention.Items.AddRange(arrayItems);
			textShowDM.Items.AddRange(arrayItems);
			textShowFavorite.Items.AddRange(arrayItems);
			textGoEnd.Items.AddRange(arrayItems);
			textGoHome.Items.AddRange(arrayItems);
			textLoad.Items.AddRange(arrayItems);
			textMenu.Items.AddRange(arrayItems);
			textUrl.Items.AddRange(arrayItems);
		}

		private void SetKeys()
		{
			textReplyAll.Text = GetStringFromKey(hotkey.keyReplyAll);
			textReply.Text = GetStringFromKey(hotkey.keyReply);
			textRetweet.Text = GetStringFromKey(hotkey.keyRetweet);
			textFavorite.Text = GetStringFromKey(hotkey.keyFavorite);
			textQRtweet.Text = GetStringFromKey(hotkey.keyQRetweet);
			textInput.Text = GetStringFromKey(hotkey.keyInput);
			textSendDM.Text = GetStringFromKey(hotkey.keyDM);
			textDelete.Text = GetStringFromKey(hotkey.keyDeleteTweet);
			textHashTag.Text = GetStringFromKey(hotkey.keyHashTag);
			textTL.Text = GetStringFromKey(hotkey.keyShowTL);
			textMention.Text = GetStringFromKey(hotkey.keyShowMention);
			textShowDM.Text = GetStringFromKey(hotkey.keyShowDM);
			textShowFavorite.Text = GetStringFromKey(hotkey.keyShowFavorite);
			textGoEnd.Text = GetStringFromKey(hotkey.keyGoEnd);
			textGoHome.Text = GetStringFromKey(hotkey.keyGoHome);
			textLoad.Text = GetStringFromKey(hotkey.keyLoad);
			textMenu.Text = GetStringFromKey(hotkey.keyMenu);
			textUrl.Text = GetStringFromKey(hotkey.keyShowOpendURL);
		}

		private string GetStringFromKey(Keys key)
		{
			string ret = string.Empty;
			if (Keys.D0 <= key && key <= Keys.D9)
				ret = key.ToString()[1].ToString();
			else
				ret = key.ToString();

			return ret;
		}

		private Keys GetKeyFromString(string key)
		{
			Keys ret = Keys.None;
			if (key == "1")
				ret = Keys.D1;
			else if (key == "2")
				ret = Keys.D2;
			else if (key == "3")
				ret = Keys.D3;
			else if (key == "4")
				ret = Keys.D4;
			else if (key == "5")
				ret = Keys.D5;
			else if (key == "6")
				ret = Keys.D6;
			else if (key == "7")
				ret = Keys.D7;
			else if (key == "8")
				ret = Keys.D8;
			else if (key == "9")
				ret = Keys.D9;
			else if (key == "0")
				ret = Keys.D0;
			else
				ret = (Keys)Enum.Parse(typeof(Keys), key, false);

			return ret;
		}

		private void SaveHotkey()
		{
			DataInstence.UpdateHotkey(hotkey);
			//FileManager.GetFileManager().UpdateHotkey(hotkey);
		}

		//private void textBox_KeyDown(object sender, KeyEventArgs e)
		//{
		//	TextBox textBox = sender as TextBox;
		//	if (textBox == null) return;
		//	e.SuppressKeyPress = true;

		//	if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
		//		e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)//기본키로 사용하는 키들 막기
		//	{
		//		MessageBox.Show(this, "선택 불가능한 단축키입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//		//return;
		//	}
		//	else if (UpdateKey(textBox, e.KeyCode))
		//	{
		//		if (Keys.D0 <= e.KeyCode && e.KeyCode <= Keys.D9)
		//			textBox.Text = e.KeyCode.ToString()[1].ToString();
		//		else
		//			textBox.Text = e.KeyCode.ToString();
		//	}
		//	else
		//	{
		//		MessageBox.Show(this, "단축키가 중복됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//	}
		//}

		private void UpdateKey()
		{
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyReplyAll, GetKeyFromString(textReplyAll.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyReply, GetKeyFromString(textReply.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyRetweet, GetKeyFromString(textRetweet.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyFavorite, GetKeyFromString(textFavorite.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyQRetweet, GetKeyFromString(textQRtweet.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyInput, GetKeyFromString(textInput.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeySendDM, GetKeyFromString(textSendDM.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyDeleteTweet, GetKeyFromString(textDelete.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyHashTag, GetKeyFromString(textHashTag.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowTL, GetKeyFromString(textTL.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowMention, GetKeyFromString(textMention.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowDM, GetKeyFromString(textShowDM.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowFavorite, GetKeyFromString(textShowFavorite.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyGoEnd, GetKeyFromString(textGoEnd.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyGoHome, GetKeyFromString(textGoHome.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyLoad, GetKeyFromString(textLoad.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowOpendUrl, GetKeyFromString(textUrl.Text));
			hotkey.UpdateKey(HotKeys.eHotKeys.eKeyMenu, GetKeyFromString(textMenu.Text));
		}

		private bool CheckKeys()
		{
			bool ret = true;

			HashSet<Keys> keys = new HashSet<Keys>();
			keys.Add(hotkey.keyReplyAll);
			keys.Add(hotkey.keyReply);
			keys.Add(hotkey.keyRetweet);
			keys.Add(hotkey.keyFavorite);
			keys.Add(hotkey.keyQRetweet);
			keys.Add(hotkey.keyInput);
			keys.Add(hotkey.keyDM);
			keys.Add(hotkey.keyDeleteTweet);
			keys.Add(hotkey.keyHashTag);
			keys.Add(hotkey.keyShowTL);
			keys.Add(hotkey.keyShowMention);
			keys.Add(hotkey.keyShowDM);
			keys.Add(hotkey.keyShowFavorite);
			keys.Add(hotkey.keyGoEnd);
			keys.Add(hotkey.keyMenu);
			keys.Add(hotkey.keyGoHome);
			keys.Add(hotkey.keyShowOpendURL);
			keys.Add(hotkey.keyLoad);

			if (keys.Count != 18)
				ret = false;
			return ret;
		}

		//private bool UpdateKey(TextBox textBox, Keys key)
		//{
		//	bool ret = true;

		//	if(textBox.Equals(textReplyAll))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyReplyAll, key);
		//	}
		//	else if(textBox.Equals(textReply))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyReply, key);
		//	}
		//	else if (textBox.Equals(textRetweet))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyRetweet, key);
		//	}
		//	else if (textBox.Equals(textFavorite))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyFavorite, key);
		//	}
		//	else if (textBox.Equals(textQRtweet))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyQRetweet, key);
		//	}
		//	else if (textBox.Equals(textInput))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyInput, key);
		//	}
		//	else if (textBox.Equals(textSendDM))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeySendDM, key);
		//	}
		//	else if (textBox.Equals(textDelete))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyDeleteTweet, key);
		//	}
		//	else if (textBox.Equals(textHashTag))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyHashTag, key);
		//	}
		//	else if (textBox.Equals(textTL))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowTL, key);
		//	}
		//	else if (textBox.Equals(textMention))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowMention, key);
		//	}
		//	else if (textBox.Equals(textShowDM))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowDM, key);
		//	}
		//	else if (textBox.Equals(textShowFavorite))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyShowFavorite, key);
		//	}
		//	else if (textBox.Equals(textGoEnd))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyGoEnd, key);
		//	}
		//	else if (textBox.Equals(textGoHome))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyGoHome, key);
		//	}
		//	else if (textBox.Equals(textLoad))
		//	{
		//		ret = hotkey.UpdateKey(HotKeys.eHotKeys.eKeyLoad, key);
		//	}

		//	return ret;
		//}

		private void okButton_Click(object sender, EventArgs e)
		{
			UpdateKey();
			if (CheckKeys())
			{
				SaveHotkey();
				this.Close();
			}
			else
			{
				ClientInctence.ShowMessageBox("단축키가 중복됩니다.", "오류");
			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
