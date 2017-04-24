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
	public partial class LoadControl : UserControl
	{
		private FlowPanelManager flowManager;

		public LoadControl(FlowPanelManager flowManager, int width)
		{
			InitializeComponent();
			this.flowManager = flowManager;
			this.Width = width;
			Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			button1.Size = new Size(Width - 14, Height);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			flowManager.MoreLoadTweet();
		}
	}
}
