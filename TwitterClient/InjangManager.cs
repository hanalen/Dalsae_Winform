using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public class InjangManager
	{
		private static readonly object lockObject = new object();
		public static InjangManager instence;
		public static InjangManager GetInstence()
		{
			if (instence == null)
				instence = new TwitterClient.InjangManager();

			return instence;
		}
		Dictionary<string, Injang> dicInjang = new Dictionary<string, Injang>();//key: url

		public void Clear()
		{
			lock(lockObject)
			{
				foreach(Injang item in dicInjang.Values)
					item.bitmap.Dispose();

				dicInjang.Clear();
			}
		}

		public Bitmap GetInjang(string url, int roundSize)
		{
			lock (lockObject)
			{
				if (dicInjang.ContainsKey(url))
				{
					Bitmap ret = Generate.RoundCorners(dicInjang[url].bitmap, roundSize, Color.Transparent);
					return ret;
				}
				else
				{
					Injang injang = new Injang();
					try
					{
						WebRequest request = System.Net.WebRequest.Create(url);
						WebResponse response = request.GetResponse();
						using (Stream stream = response.GetResponseStream())
						{
							Bitmap bitmap = new Bitmap(stream);
							injang.SetBitmap(bitmap);
							dicInjang.Add(url, injang);
						}
						Bitmap ret = Generate.RoundCorners(dicInjang[url].bitmap, roundSize, Color.Transparent);
						return ret;
					}
					catch(Exception e)
					{

					}
					return null;
				}
			}
		}
		

		public class Injang
		{
			public Bitmap bitmap { get; private set; }
			public int count { get; private set; } = 0;
			public void SetBitmap (Bitmap bitmap) { this.bitmap = bitmap; }
		}
	}
}
