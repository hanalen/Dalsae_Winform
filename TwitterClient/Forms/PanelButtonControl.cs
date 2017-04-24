using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient.Forms
{
	public partial class PanelButtonControl : UserControl
	{
		public PanelButtonControl()
		{
			InitializeComponent();
		}

		private void homePicture_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.TIME_LINE);	
		}

		private void notiPicture_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.MENTION);
		}

		private void dmPicture_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.DM);
		}

		private void favPicture_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.FAVORITE);
		}

		private void urlPicture_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.OPEN_URL);
		}

		private void homeLabel_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.TIME_LINE);
		}

		private void mentionLabel_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.MENTION);
		}

		private void dmLabel_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.DM);
		}

		private void favLabel_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.FAVORITE);
		}

		private void urlLabel_Click(object sender, EventArgs e)
		{
			ClientInctence.ShowPanel(MainForm.eSelectMenu.OPEN_URL);
		}
	}
}
