using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public partial class ImageForm : Form
	{
		private List<ClientMedia> listMedia = new List<ClientMedia>();
		private int index = 0;
		private Bitmap []bitmapArray = new Bitmap[4];
		private bool isLoading = false;
		public delegate void DeleLoadImage();
		private bool isDM = false;
		public ImageForm()
		{
			InitializeComponent();

			Location = DataInstence.option.pointImageForm;
			Size = DataInstence.option.sizeImageForm;

			pictureBox1.BackgroundImageLayout = ImageLayout.Center;
			if (Generate.IsOnScreen(this) == false)
				Location = new Point(500, 100);
		}

		public void SetImageURL(List<ClientMedia> listMedia, bool isDm)
		{
			this.isDM = isDm;
			this.listMedia = listMedia;
		}

		public void LoadImage()
		{
			if (bitmapArray[index] != null)
			{
				SetImage();
			}
			else
			{
				try
				{
					isLoading = true;
					if (isDM)
					{
						ParameterImage parameter = new ParameterImage(listMedia[index].media_url_https);
						WebRequest req = (HttpWebRequest)WebRequest.Create(parameter.MethodGetUrl());
						req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));
						req.BeginGetResponse(new AsyncCallback(AsyncLoadBitmap), req);
					}
					else
					{
						WebRequest request = System.Net.WebRequest.Create(listMedia[index].media_url_https);
						request.BeginGetResponse(new AsyncCallback(AsyncLoadBitmap), request);
					}
				}
				catch { }
			}
		}

		private void AsyncLoadBitmap(IAsyncResult ar)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
				using (Stream stream = response.GetResponseStream())
				{
					Bitmap bitmap = new Bitmap(stream);
					bitmapArray[index] = bitmap;
				}
				DeleLoadImage dele = new DeleLoadImage(SetImage);
				this.BeginInvoke(dele);
			}
			catch(Exception e)
			{

			}
			finally
			{
				isLoading = false;
			}
		}

		private void SetImage()
		{
			pictureBox1.Image = bitmapArray[index];
			ChangeTitle();
			ResizeImage();
		}

		private void ChangeTitle()
		{
			Text = "이미지 보기 (" + (index + 1) + " / " + listMedia.Count + ")";
		}
		
		private void imageForm_KeyDown(object sender, KeyEventArgs e)//이미지 전환
		{
			if (isLoading) return;
			switch (e.KeyCode)
			{
				case Keys.Right:
					if (listMedia.Count == 1)
					{
						return;
					}
					else if (index + 1 < listMedia.Count)
					{
						index++;
						LoadImage();
					}
					else if (index + 1 >= listMedia.Count)
					{
						index = 0;
						LoadImage();
					}
					break;
				case Keys.Left:
					if (listMedia.Count == 1)
					{
						return;
					}
					else if (index - 1 < 0)
					{
						index = listMedia.Count - 1;
						LoadImage();
					}
					else
					{
						index--;
						LoadImage();
					}
					break;
				case Keys.Enter:
				case Keys.Escape:
					Close();
					break;
			}
		}

		private void ResizeImage()
		{
			if (bitmapArray[index] == null) return;

			int width = listMedia[index].sizes.large.w;
			int height = listMedia[index].sizes.large.h;
			//float width = listMedia[index].sizes.large.w;
			//float height = listMedia[index].sizes.large.h;
			//double scaleX = this.Width / (double)width;
			//double scaleY = this.Height / (double)height;
			//double ratio = Math.Min(scaleX, scaleY);
			//if (ratio > 1) ratio = 1;

			//int w = (int)Math.Ceiling(width * ratio);
			//int h = (int)Math.Ceiling(height * ratio);
			//pictureBox1.Size = new Size(w, h);

			if (width > Width)
				width = Width;
			if (height > Height)
				height = Height;
			pictureBox1.Width = width;
			pictureBox1.Height = height;

			int x = Width / 2 - width / 2;
			int y = Height / 2 - height / 2;
			pictureBox1.Location = new Point(x, y);
		}

		private void imageForm_Resize(object sender, EventArgs e)
		{
			ResizeImage();
		}

		private void imageForm_Closing(object sender, FormClosingEventArgs e)
		{
			if (WindowState != FormWindowState.Maximized && WindowState != FormWindowState.Minimized)
			{
				DataInstence.option.pointImageForm = Location;
				DataInstence.option.sizeImageForm = Size;
			}
			pictureBox1.Image = null;
			for(int i=0;i<bitmapArray.Length;i++)
			{
				if (bitmapArray[i] != null)
					bitmapArray[i].Dispose();
			}
		}

		private void 이미지저장ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FileInstence.SaveImage(listMedia[index].media_url_https, bitmapArray[index]);
			ClientInctence.ShowMessageBox("이미지 저장 완료", "알림", MessageBoxIcon.Information);
		}


		private void pictureBox_Click(object sender, MouseEventArgs e)
		{
			Control con = sender as Control;
			if (con == null) return;
			if(e.Button== MouseButtons.Right)
			{
				contextMenuStrip1.Show(con, e.Location);
			}
		}
	}
}