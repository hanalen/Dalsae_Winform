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
		//---------------------------------------이벤트 처리------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		//메인폼이 리사이즈 끝나면 내부 패널 전부 리사이즈 시키는 함수
		private void MainForm_ResizeEnd(Object sender, EventArgs e)
		{
			//homePanel.Resize(true);
			//mentionPanel.Resize(true);
			//userPanel.Resize(true);
			//myPanel.Resize(true);
			//favPanel.Resize(true);
			//urlPanel.Resize(true);
			switch (eselectMenu)
			{
				case eSelectMenu.TIME_LINE: homePanel.Resize(true); break;
				case eSelectMenu.MENTION: mentionPanel.Resize(true); break;
				case eSelectMenu.USER: userPanel.Resize(true); break;
				case eSelectMenu.MY_TL: myPanel.Resize(true); break;
				case eSelectMenu.FAVORITE: favPanel.Resize(true); break;
				case eSelectMenu.OPEN_URL:		urlPanel.Resize(true); break;
			}
			
		}

		private void mainForm_Resize(object sender, EventArgs e)
		{
			if (prevWindowState != WindowState && WindowState != FormWindowState.Minimized)
			{
				//MainForm_ResizeEnd(sender, e);
				homePanel.Resize(true);
				mentionPanel.Resize(true);
				favPanel.Resize(true);
				urlPanel.Resize(true);

				if (myPanel != null)
					myPanel.Resize(true);
				if (userPanel != null)
					userPanel.Resize(true);
			}
			prevWindowState = WindowState;
		}

		private void mainForm_Actived(object sender, EventArgs e)
		{
			EnterInputTweet();
		}

		private void tweetButton_Click(object sender, EventArgs e)
		{
			if (listFiles.Count == 0 && inputTweet.Text.Length == 0) return;

			SendTweet();
		}

		private void account_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;

			if (item == null) return;
			if (FileInstence.isChangeAccount(item.Text) == false)
			{
				ClearMainform();
				ClientInctence.ClearClient(item.Text);
			}
		}

		private void 계정추가ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClearMainform();
			ClientInctence.AddAccount();
		}

		private void ClearMainform()
		{
			newMention.Hide();
			newDM.Hide();
			homePanel.Clear();
			mentionPanel.Clear();
			dmPanel.Clear();
			favPanel.Clear();
			urlPanel.Clear();
			foreach (FlowPanelManager item in dicUserPanel.Values)
			{
				item.Clear();
			}
			if (myPanel != null)
				myPanel.Clear();
			profilePictureBox.Image = null;
			profileBitmap.Dispose();
		}

		private void 기본ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPanel(eSelectMenu.TIME_LINE);
			ClientInctence.LoadTweet(eSelectMenu.TIME_LINE, string.Empty);
		}

		private void 이미지ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = true;
			ofd.Title = "열기";
			ofd.Filter = "이미지 파일(*.jpg, *jpeg, *png)|*.jpg; *.jpeg; *.png; | 모든 파일(*.*) | *.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				for (int i = 0; i < ofd.SafeFileNames.Length; i++)
				{
					string fileText = System.IO.Path.GetExtension(ofd.SafeFileNames[i]);
					if (fileText.Equals(".png", StringComparison.CurrentCultureIgnoreCase) == false &&
							fileText.Equals(".jpg", StringComparison.CurrentCultureIgnoreCase) == false &&
							fileText.Equals(".jpeg", StringComparison.CurrentCultureIgnoreCase) == false)
					{
						MessageBox.Show(this, "이미지 파일을 선택 해주세요!!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				if (ofd.SafeFileNames.Length > 4)
				{
					MessageBox.Show(this, "최대 4개까지 선택 가능", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (listFiles.Count + ofd.SafeFileNames.Length > 4)
				{
					MessageBox.Show(this, "최대 4개까지 선택 가능", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				//foreach (string item in ofd.FileNames)//중복파일 체크
				//{
				//	for (int i = 0; i < listPictureBox.Count; i++)
				//	{
				//		if (listPictureBox[i].ImageLocation == item)
				//		{
				//			MessageBox.Show(this, "중복 파일 선택", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//			return;
				//		}
				//	}
				//}
				List<string> listAddFile = new List<string>();
				foreach(string path in ofd.FileNames)
				{
					if (CheckFileSize(path))
						ShowMessageBox("크기가 3MB 이상인 이미지는 등록 불가능합니다.", "오류",
										MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else
						listAddFile.Add(path);
				}
			
				AddFile(listAddFile.ToArray());
			}
		}

		private bool CheckFileSize(string path)
		{
			bool isLarge = false;
			FileInfo fi = new FileInfo(path);
			if (fi.Length > 3145728)
				isLarge = true;

			return isLarge;
		}

		//첨부 이미지 클릭해서 삭제, 열기 기능
		private void imageBox_Click(object sender, EventArgs e)
		{
			PictureBox box = sender as PictureBox;
			MouseEventArgs me = (MouseEventArgs)e;

			if (box == null) return;
			if (box.Image == null) return;
			Picture picture = listFiles.FirstOrDefault(x => x.bitmap == box.Image);
			if (picture == null) return;

			if (me.Button == MouseButtons.Left && string.IsNullOrEmpty(picture.path) == false)//열기 기능
			{
				Process photoViewer = new Process();
				photoViewer.StartInfo.FileName = picture.path;
				photoViewer.StartInfo.Arguments = @"Your image file path";
				photoViewer.Start();
			}
			else if (me.Button == MouseButtons.Right)//지우기 기능
			{
				int index = listPictureBox.IndexOf(box);
				if (index == -1) return;

				box.Image = null;
				picture.bitmap.Dispose();
				listFiles.Remove(picture);

				for (int i = index; i < listPictureBox.Count; i++)
					listPictureBox[i].Image = null;
				for (int i = 0; i < listFiles.Count; i++)
					listPictureBox[i].Image = listFiles[i].bitmap;
			}
		}

		private void 멘션ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPanel(eSelectMenu.MENTION);
			//ParameterMentionTimeLine parameter = new ParameterMentionTimeLine();
			ClientInctence.LoadTweet(eSelectMenu.MENTION, string.Empty);
		}

		private void 연결ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClientInctence.ConnectUserStreaming();
		}

		private void UserTL_MenuStripClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item == null) return;

			ShowUserPanel(item.Text);

		}

		private void mainForm_DragEnter(object sender, DragEventArgs e)
		{
			//enter에서 drop으로 정보 전송하는 이벤트 호출 순서
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void mainForm_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				//e.Effect = DragDropEffects.All;
				List<string> listSend = new List<string>();
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				foreach (var file in files)
				{
					string fileText = System.IO.Path.GetExtension(file);
					if (fileText.Equals(".png", StringComparison.CurrentCultureIgnoreCase) ||
						fileText.Equals(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
						fileText.Equals(".jpeg", StringComparison.CurrentCultureIgnoreCase))
					{
						e.Effect = DragDropEffects.Copy;
						//Picture pic = listFiles.FirstOrDefault(x => x.path == file);//중복체크
						//if (pic == null)
						if(CheckFileSize(file))
							ShowMessageBox("크기가 3MB 이상인 이미지는 등록 불가능합니다.", "오류",
										MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
							listSend.Add(file);
					}
				}
				if (listSend.Count + listFiles.Count <= 4)
				{
					string[] fs = listSend.ToArray();
					AddFile(fs);
				}
			}
		}

		private void gIF추가GToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listFiles.Count > 0)
			{
				ShowMessageBox("이미지와 gif는 동시에 보낼 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			OpenFileDialog ofd = new OpenFileDialog();
			//ofd.Multiselect = true;
			ofd.Title = "열기";
			ofd.Filter = "gif 파일(*.gif)|*.gif;";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string fileText = System.IO.Path.GetExtension(ofd.SafeFileName);
				if (fileText.Equals(".gif", StringComparison.CurrentCultureIgnoreCase))
				{
					FileInfo fi = new FileInfo(ofd.FileName);
					if (fi.Length > 8388608)
					{
						ShowMessageBox("크기가 5MB 이상인 gif는 등록 불가능합니다.\r공식 홈페이지를 이용 바랍니다.", "오류",
										MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					isAddedGifAndVideo = true;
					pathGifAndVideo = ofd.FileName;
					listPictureBox[0].Image = Properties.Resources.gif;
				}
			}
		}

		private void 동영상추가DToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void 관심글ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//관심글은 처음 열 때 생성하게 함
			//eselectMenu = eSelectMenu.FAVORITE;
			//HidePanels();
			ShowPanel(eSelectMenu.FAVORITE);
			ClientInctence.LoadTweet(eSelectMenu.FAVORITE, string.Empty);
		}

		private void 내트윗ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			eselectMenu = eSelectMenu.MY_TL;
			if (myPanel == null)
				myPanel = new TwitterClient.FlowPanelManager(this, panel1, ePanel.MY_TL);

			//HidePanels();
			//myPanel.Show();
			ShowPanel(eSelectMenu.MY_TL);
			ClientInctence.LoadTweet(eSelectMenu.MY_TL, string.Empty);
		}

		private void 유저검색ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PinForm pin = new PinForm(false);
			pin.ShowDialog();
		}

		private void 프로그램설정OToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OptionForm of = new OptionForm();
			of.ShowDialog(this);
		}

		private void 단축키설정HToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HotKeyForm form = new TwitterClient.HotKeyForm();
			form.ShowDialog(this);
		}

		private void 아이디완성갱신NToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(ClientInctence.GetFollowList);
		}

		private void 블락리스트갱신BToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(ClientInctence.GetBlockIds);
		}

		private void 만든사람HToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MaidForm form = new TwitterClient.MaidForm();
			form.ShowDialog(this);
		}

		private void mainForm_Closing(object sender, FormClosingEventArgs e)
		{
			if (WindowState != FormWindowState.Maximized && WindowState != FormWindowState.Minimized)
			{
				DataInstence.option.pointMainForm = Location;
				DataInstence.option.sizeMainForm = Size;
			}
			ClientInctence.ProgramClosing();
		}


		private void 홈HToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPanel(eSelectMenu.TIME_LINE);
		}

		private void 멘션RToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPanel(eSelectMenu.MENTION);
		}

		private void 쪽지DToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowPanel(eSelectMenu.DM);
		}

		private void mainForm_Shown(object sender, EventArgs e)
		{
			ClientInctence.LoadedMainForm();
		}

		private void inputTweet_Enter(object sender, EventArgs e)
		{
			inputTweet.BackColor = Color.White;
			EnterInputTweet();
		}

		private void inputTweet_Leave(object sender, EventArgs e)
		{
			inputTweet.BackColor = Color.FromArgb(0xea, 0xea, 0xea);
		}

		private void doubleClick_mentionListBox(object sender, EventArgs e)
		{
			AddMentionID();
		}

		private void mainForm_Move(object sender, EventArgs e)
		{
			if (dmPanel != null)
				dmPanel.ResizePanel();
			if (homePanel != null)
				homePanel.ResizePanel();
			if (mentionPanel != null)
				mentionPanel.ResizePanel();
			if (urlPanel != null)
				urlPanel.ResizePanel();
			foreach (FlowPanelManager item in dicUserPanel.Values)
			{
				item.ResizePanel();
			}
		}
	}
}
