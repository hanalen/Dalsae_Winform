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
	public partial class OptionForm : Form
	{
		private Option option;
		private Font prevFont;
		private string prevSkinName;
		private string prevSound;
		private bool prevSmallUI;
		public OptionForm()
		{
			InitializeComponent();
			this.option = new Option(DataInstence.option);
			prevFont = new Font(option.font.Name, option.font.Size);
			prevSkinName = option.skinName;
			prevSound = option.notiSound;
			prevSmallUI = option.isSmallUI;
			fontLabel.Text = "글꼴: "+option.font.Name + " / " +"크기: "+ (int)Math.Round(option.font.Size); ;

			SetSkinComboBox();
			SetSoundComboBox();
			SetMuteList();
			SetCheckBox();
			labelImagePath.Text = DataInstence.option.imageFolderPath;
		}

		private void SetCheckBox()
		{
			checkMuteMention.Checked = option.isMuteMention;
			checkRtProtecd.Checked = option.isRetweetProtectUser;
			checkSendEnter.Checked = option.isSendEnter;
			checkShowCount.Checked = option.isShowTweetCount;
			checkYesNo.Checked = option.isYesnoTweet;
			checkNotiPlay.Checked = option.isPlayNoti;
			checkSmallUI.Checked = option.isSmallUI;
			checkShowRetweet.Checked = option.isShowRetweet;
			checkRetweetNoti.Checked = option.isNotiRetweet;
			if (checkNotiPlay.Checked == false)
			{
				label6.Hide();
				soundComboBox.Hide();
			}
			if (checkShowRetweet.Checked == false)
			{
				checkRetweetNoti.Checked = false;
				option.isNotiRetweet = false;
				checkRetweetNoti.Hide();
			}
		}

		private void SetMuteList()
		{
			highLightList.Items.AddRange(option.listHighlight.ToArray());
			muteClientList.Items.AddRange(option.listMuteClient.ToArray());
			muteUserList.Items.AddRange(option.listMuteUser.ToArray());
			muteWordList.Items.AddRange(option.listMuteWord.ToArray());
		}

		private void SetSoundComboBox()
		{
			string[] soundArray = FileInstence.GetSoundList();
			if (soundArray == null) return;

			if (soundArray.Length > 0)//스킨 폴더에 있는 리스트 가져와 추가, 선택
			{
				soundComboBox.Items.AddRange(soundArray);
				if (option.notiSound != null)
				{
					int soundIndex = soundComboBox.Items.IndexOf(option.notiSound);
					if (soundIndex != -1)
						soundComboBox.SelectedIndex = soundIndex;
					else
						soundComboBox.SelectedIndex = 0;
				}
			}
		}

		private void SetSkinComboBox()
		{
			string[] skinArray = FileInstence.GetSkinList();
			if (skinArray == null) return;

			if (skinArray.Length > 0)//스킨 폴더에 있는 리스트 가져와 추가, 선택
			{
				skinComboBox.Items.AddRange(skinArray);

				int skinIndex = skinComboBox.Items.IndexOf(option.skinName);
				if (skinIndex != -1)
					skinComboBox.SelectedIndex = skinIndex;
				else
					skinComboBox.SelectedIndex = 0;
			}
		}

		private void SaveOption()
		{
			if (prevSound != option.notiSound)
				ClientInctence.ChangeSoundNoti(option.notiSound);
			DataInstence.UpdateOption(option);
			FileInstence.UpdateOption(option);

			if (prevFont.Name != option.font.Name || prevFont.Size != option.font.Size)//폰트 설정 변경 시
				ClientInctence.ChangeFont();

			if (prevSkinName != option.skinName)
			{
				DataInstence.option.skinName = option.skinName;
				FileInstence.UpdateSkin();
				ClientInctence.ChangeSkin();
			}

			if (prevSmallUI != option.isSmallUI)
			{
				ClientInctence.ChangeSmallUI(option.isSmallUI);
			}
		}

		private void fontButton_Click(object sender, EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.Font = new Font(option.font.Name, (int)Math.Round(option.font.Size));
			DialogResult result = fd.ShowDialog();
			if (result == DialogResult.OK)
			{
				option.font =new Font(fd.Font.Name, fd.Font.Size);
				fontLabel.Text = "글꼴: " + option.font.Name + " / " + "크기: " + (int)Math.Round(option.font.Size);
			}
		}

		private void okButton_Click(object sender, EventArgs e)//저장버튼
		{
			SaveOption();
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void highOkButton_Click(object sender, EventArgs e)
		{
			option.listHighlight.Add(highLightTextBox.Text);
			highLightList.Items.Add(highLightTextBox.Text);
			highLightTextBox.Text = string.Empty;
		}

		private void highRemButton_Click(object sender, EventArgs e)
		{
			option.listHighlight.Remove(highLightTextBox.Text);
			highLightList.Items.Remove(highLightTextBox.Text);
			highLightTextBox.Text = string.Empty;
		}

		private void muteUserOKBut_Click(object sender, EventArgs e)
		{
			string mute = muteUserText.Text.Replace("@", string.Empty);
			mute = mute.Replace(" ", string.Empty);
			option.listMuteUser.Add(mute);
			muteUserList.Items.Add(mute);
			muteUserText.Text = string.Empty;
		}

		private void muteUserRemBut_Click(object sender, EventArgs e)
		{
			option.listMuteUser.Remove(muteUserText.Text);
			muteUserList.Items.Remove(muteUserText.Text);
			muteUserText.Text = string.Empty;
		}

		private void muteWordOKBut_Click(object sender, EventArgs e)
		{
			option.listMuteWord.Add(muteWordText.Text);
			muteWordList.Items.Add(muteWordText.Text);
			muteWordText.Text = string.Empty;
		}

		private void muteWordRemBut_Click(object sender, EventArgs e)
		{
			option.listMuteWord.Remove(muteWordText.Text);
			muteWordList.Items.Remove(muteWordText.Text);
			muteWordText.Text = string.Empty;
		}

		private void muteClientOKBut_Click(object sender, EventArgs e)
		{
			option.listMuteClient.Add(muteClientText.Text);
			muteClientList.Items.Add(muteClientText.Text);
			muteClientText.Text = string.Empty;
		}

		private void muteClientRemBut_Click(object sender, EventArgs e)
		{
			option.listMuteClient.Remove(muteClientText.Text);
			muteClientList.Items.Remove(muteClientText.Text);
			muteClientText.Text = string.Empty;
		}

		private void listbox_SelectedChange(object sender, EventArgs e)
		{
			ListBox box = sender as ListBox;
			if (box == null) return;

			if (box.Equals(muteClientList))
			{
				if (muteClientList.SelectedItem != null)
					muteClientText.Text=muteClientList.SelectedItem.ToString();
			}
			else if(box.Equals(muteUserList))
			{
				if (muteUserList.SelectedItem != null)
					muteUserText.Text = muteUserList.SelectedItem.ToString();
			}
			else if (box.Equals(muteWordList))
			{
				if (muteWordList.SelectedItem != null)
					muteWordText.Text = muteWordList.SelectedItem.ToString();
			}
			else if (box.Equals(highLightList))
			{
				if(highLightList.SelectedItem!=null)
					highLightTextBox.Text = highLightList.SelectedItem.ToString();
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox box = sender as ComboBox;
			if (box == null) return;

			if (box.Equals(skinComboBox))
				option.skinName = skinComboBox.SelectedItem.ToString();
			else if (box.Equals(soundComboBox))
				option.notiSound = soundComboBox.SelectedItem.ToString();
		}

		private void checkBox_Changed(object sender, EventArgs e)
		{
			CheckBox box = sender as CheckBox;
			if (box == null) return;

			if (box.Equals(checkMuteMention))
				option.isMuteMention = box.Checked;
			else if (box.Equals(checkRtProtecd))
				option.isRetweetProtectUser = box.Checked;
			else if (box.Equals(checkSendEnter))
				option.isSendEnter = box.Checked;
			else if (box.Equals(checkShowCount))
				option.isShowTweetCount = box.Checked;
			else if (box.Equals(checkRetweetNoti))
				option.isShowRetweet = box.Checked;
			else if (box.Equals(checkYesNo))
				option.isYesnoTweet = box.Checked;
		}

		private void optionForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void notiCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if(checkNotiPlay.Checked)
			{
				label6.Show();
				soundComboBox.Show();
			}
			else
			{
				label6.Hide();
				soundComboBox.Hide();
			}
			option.isPlayNoti = checkNotiPlay.Checked;
		}

		private void checkSmallUI_CheckedChanged(object sender, EventArgs e)
		{
			option.isSmallUI = checkSmallUI.Checked;
		}

		private void checkShowRetweet_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox menu = sender as CheckBox;
			if (menu == null) return;

			if (menu.Checked)
				checkRetweetNoti.Show();
			else
				checkRetweetNoti.Hide();
			option.isShowRetweet = menu.Checked;
		}

		private void checkRetweetNoti_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox menu = sender as CheckBox;
			if (menu == null) return;

			option.isNotiRetweet = menu.Checked;
		}

		private void imageButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.SelectedPath = Environment.CurrentDirectory;

			DialogResult dr = fbd.ShowDialog(this);

			if (dr == DialogResult.OK)
			{
				option.imageFolderPath = fbd.SelectedPath;
				labelImagePath.Text = fbd.SelectedPath;
			}


		}
	}
}
