using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Net;
using System.Drawing.Imaging;

namespace TwitterClient.Forms
{
	public partial class FavoriteForm : Form
	{
		private static readonly object lockObject = new object();
		private TweetControl tweetControl;
		private List<PictureBox> listPictureBox = new List<PictureBox>();
		private List<Bitmap> listBitmap = new List<Bitmap>();
		private List<ClientTweet> listTweet = new List<ClientTweet>();
		private int countFav = 0;
		private bool isLoading = false;
		private int tweetIndex = 0;
		Bitmap[] bitmapArray = new Bitmap[4];

		private delegate void DeleLoadingEnd();
		private delegate void DeleSetImage(int index);

		private TweetControl.DeleChangeTweet deleChangeTweet;
		private DeleSetImage deleSetImage;

		private const string imagePath = "Image/";
		private int loadCount = 0;

		//----------------------------------------------------------------------------------------------------
		//---------------------------------------로딩 기능---------------------------------------------------
		//----------------------------------------------------------------------------------------------------
		public FavoriteForm()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			listPictureBox.Add(pictureBox1);
			listPictureBox.Add(pictureBox2);
			listPictureBox.Add(pictureBox3);
			listPictureBox.Add(pictureBox4);
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if (isLoading) return;
			label1.Hide();
			startButton.Hide();
			ThreadPool.QueueUserWorkItem(Loading);
		}

		private void Loading(object obj)
		{
			isLoading = true;
			LoadMyInfo();
			SetLabel();
			LoadingFavorite();
			DeleLoadingEnd dele = new DeleLoadingEnd(LoadingEnd);
			BeginInvoke(dele);
		}

		private void LoadMyInfo()
		{
			statusLabel.Text = "정보 불러오는 중";

			ParameterVerifyCredentials parameter = new ParameterVerifyCredentials();
			string json = WebInstence.SyncRequest(parameter);
			User user = JsonConvert.DeserializeObject<User>(json);
			if (user == null)
			{
				statusLabel.Text = "정보 불러오기 실패";
				return;
			}

			statusLabel.Text = "정보 불러오기 완료";
			countFav = user.favourites_count;
		}

		private void SetLabel()
		{
			countLabel.Text = $"관심글 수: {countFav}";
		}

		private void LoadingFavorite()
		{
			ParameterFavoritesList parameter = new ParameterFavoritesList();
			parameter.count = 200.ToString();
			int loadFavCount = 0;
			for (int j = 0; j < 15; j++)
			{
				string json = WebInstence.SyncRequest(parameter);
				if (json.Length == 0)
				{
					MessageBox.Show("불러오기 제한! 몇 분 뒤 다시 시도해주세요(최대 15분", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					break;
				}
				List<ClientTweet> listTemp = JsonConvert.DeserializeObject<List<ClientTweet>>(json);
				loadFavCount += listTemp.Count;
				statusLabel.Text = $"불러오는중: {loadFavCount}/{countFav}";
				for (int i = 0; i < listTemp.Count; i++)
				{
					listTemp[i].Init();
					if (listTemp[i].isMedia)
					{
						if (listTemp[i].dicMedia.Count == 1)//media가 있는데 photo일 경우에만 저장
							foreach (ClientMedia item in listTemp[i].dicMedia.Values)
							{
								if (item.type == "photo")
									listTweet.Add(listTemp[i]);
								break;
							}
						else
							listTweet.Add(listTemp[i]);
					}
				}
				if (loadFavCount >= countFav) break;
				parameter.max_id = listTemp[listTemp.Count - 1].id;
			}
			statusLabel.Text = "불러오기 완료";
		}

		private void LoadingEnd()
		{
			if (File.Exists(imagePath) == false)
				System.IO.Directory.CreateDirectory(imagePath);
			if (listTweet.Count == 0) return;

			imageLabel.Text = $"이미지 관심글: {tweetIndex + 1} / {listTweet.Count}";
			favButton.Show();
			nextButton.Show();
			prevButton.Show();
			unfavButton.Show();
			unfavSaveButton.Show();
			saveButton.Show();
			for (int i = 0; i < listPictureBox.Count; i++)
				listPictureBox[i].Show();
			if (listTweet.Count > 0)
			{
				tweetControl = new TwitterClient.TweetControl(listTweet[0], Width, DataInstence.skin.blockOne, eTweet.NONE);
				tweetControl.Parent = this;
				tweetControl.Location = new Point(0, 30);
				tweetControl.Show();
				deleChangeTweet = new TweetControl.DeleChangeTweet(tweetControl.ChangeTweet);
				//tweetControl.ControlResize(Width);
			}
			deleSetImage = new DeleSetImage(SetImage);
			LoadImage();
			isLoading = false;
		}


		//----------------------------------------------------------------------------------------------------
		//---------------------------------------ui기능 -----------------------------------------------------
		//----------------------------------------------------------------------------------------------------

		private void ShowTweet()
		{
			//isLoading = true;
			tweetControl.BeginInvoke(deleChangeTweet, new object[] { listTweet[tweetIndex] });
			imageLabel.Text = $"이미지 관심글: {tweetIndex + 1} / {listTweet.Count}";
			LoadImage();
		}

		private void ClearImage()
		{
			loadCount = 0;
			listPictureBox[0].Image = null;
			listPictureBox[1].Image = null;
			listPictureBox[2].Image = null;
			listPictureBox[3].Image = null;

			if (bitmapArray[0] != null)
				bitmapArray[0].Dispose();
			if (bitmapArray[1] != null)
				bitmapArray[1].Dispose();
			if (bitmapArray[2] != null)
				bitmapArray[2].Dispose();
			if (bitmapArray[3] != null)
				bitmapArray[3].Dispose();
		}

		private void LoadImage()
		{
			ClearImage();

			int index = 0;
			//loadCount = listTweet[tweetIndex].mediaEntities.media.Count;
			//if (loadCount > 4) loadCount = 4;
			foreach(ClientMedia item in listTweet[tweetIndex].mediaEntities.media)
			{
				if (item.type != "photo") continue;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{item.media_url_https}:orig");
				AsyncLoadImage async = new AsyncLoadImage(index, request);
				request.BeginGetResponse(new AsyncCallback(AsyncLoadBitmap), async);
				index++;
				loadCount++;
			}
			if (loadCount > 4) loadCount = 4;
		}

		private void AsyncLoadBitmap(IAsyncResult ar)
		{
			AsyncLoadImage request = (AsyncLoadImage)ar.AsyncState;
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.req.EndGetResponse(ar);
				using (Stream stream = response.GetResponseStream())
				{
					Bitmap bitmap = new Bitmap(stream);
					bitmapArray[request.index] = bitmap;
				}
				this.BeginInvoke(deleSetImage, new object[] { request.index });
				//DeleLoadImage dele = new DeleLoadImage(SetImage);
				//this.BeginInvoke(dele);
			}
			catch (Exception e)
			{

			}
			finally
			{
				lock (lockObject) { loadCount--; }
			}
		}

		private void SetImage(int index)
		{
			listPictureBox[index].Image = bitmapArray[index];
		}

		private void nextButton_Click(object sender, EventArgs e)
		{
			if (loadCount > 0) return;
			tweetIndex++;
			if (tweetIndex < listTweet.Count)
				ShowTweet();
			else
				tweetIndex--;
		}

		private void prevButton_Click(object sender, EventArgs e)
		{
			if (loadCount>0) return;
			tweetIndex--;
			if (tweetIndex != 0)
				ShowTweet();
			else
				tweetIndex = 0;
		}

		private void unfavSaveButton_Click(object sender, EventArgs e)
		{
			ParameterFavorites_Destroy parameter = new ParameterFavorites_Destroy();
			parameter.id = listTweet[tweetIndex].id;
			string json = WebInstence.SyncRequest(parameter);
			if (json.Length > 50)
			{
				SaveFile();
				listTweet[tweetIndex].favorited = false;
				MessageBox.Show("저장, 관심글 해제 성공", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void SaveFile()
		{
			if (loadCount > 0) return;
			int fileIndex = 0;
			foreach (ClientMedia item in listTweet[tweetIndex].mediaEntities.media)
			{
				if (item.type != "photo") continue;
				string http = item.media_url_https;
				string fileName = http.Replace("https://pbs.twimg.com/media/", "");
				string ext = fileName.Substring(fileName.IndexOf('.') + 1, 3);
				if (ext=="jpg")
					bitmapArray[fileIndex].Save($"{DataInstence.option.imageFolderPath}/{fileName}", ImageFormat.Jpeg);
				else if(ext=="png")
					bitmapArray[fileIndex].Save($"{DataInstence.option.imageFolderPath}/{fileName}", ImageFormat.Png);
				fileIndex++;
			}
		}

		private class AsyncLoadImage
		{
			public int index;
			public HttpWebRequest req;
			public AsyncLoadImage(int index, HttpWebRequest req)
			{
				this.index= index;
				this.req = req;
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			SaveFile();
			MessageBox.Show("저장 성공", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void unfavButton_Click(object sender, EventArgs e)
		{
			ParameterFavorites_Destroy parameter = new ParameterFavorites_Destroy();
			parameter.id = listTweet[tweetIndex].id;
			string json = WebInstence.SyncRequest(parameter);
			if (json.Length > 50)
			{
				MessageBox.Show("관심글 해제 완료", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
				listTweet[tweetIndex].favorited = false;
			}
		}

		private void favButton_Click(object sender, EventArgs e)
		{
			ParameterFavorites_Create parameter = new ParameterFavorites_Create();
			parameter.id = listTweet[tweetIndex].id;
			string json = WebInstence.SyncRequest(parameter);
			if (json.Length > 50)
			{
				MessageBox.Show("관심글 등록 완료", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
				listTweet[tweetIndex].favorited = false;
			}
		}
	}
}
