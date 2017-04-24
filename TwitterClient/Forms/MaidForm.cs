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
	public partial class MaidForm : Form
	{
		private List<Keys> listKey = new List<Keys>();
		private int index = 0;
		private bool isStart = false;

		public MaidForm()
		{
			InitializeComponent();
			BackColor = DataInstence.skin.defaultColor;
			listKey.Add(Keys.Left);
			listKey.Add(Keys.Up);
			listKey.Add(Keys.Right);
			listKey.Add(Keys.Down);
			listKey.Add(Keys.Left);
			listKey.Add(Keys.Up);
			listKey.Add(Keys.Right);
			listKey.Add(Keys.Down);
			listKey.Add(Keys.Left);
			listKey.Add(Keys.Up);
			listKey.Add(Keys.Right);
			listKey.Add(Keys.Down);
			listKey.Add(Keys.Left);
			listKey.Add(Keys.Up);
			listKey.Add(Keys.Right);
			listKey.Add(Keys.Down);
		}

		private void madiForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();

			if (isStart == false)
			{
				if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
				{
					isStart = true;
					index = listKey.IndexOf(e.KeyCode);
					index++;
				}
			}
			else
			{
				if (listKey[index] == e.KeyCode)
				{
					index++;
					if (index >= listKey.Count)
					{
						System.Diagnostics.Process.Start("http://kr.battle.net/heroes/ko/");
						isStart = false;
						index = 0;
					}
				}
				else
				{
					isStart = false;
					index = 0;
				}
			}

		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://twitter.com/Dalsae_info");
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://twitter.com/hanalen_");
		}
	}
}
