using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;
//using DWORD = System.UInt32;

namespace TwitterClient
{
	class Generate
	{
		public static Bitmap RoundCorners(Bitmap StartImage, int CornerRadius, Color BackgroundColor)
		{
			if (StartImage == null)
			{
				ClientInctence.ShowMessageBox("이미지 에러", "오류");
				return null;
			}
			CornerRadius *= 2;
			try
			{
				Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
				using (Graphics g = Graphics.FromImage(RoundedImage))
				{
					g.Clear(BackgroundColor);
					g.SmoothingMode = SmoothingMode.AntiAlias;
					Brush brush = new TextureBrush(StartImage);
					GraphicsPath gp = new GraphicsPath();
					gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
					gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
					gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
					gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
					g.FillPath(brush, gp);
					return RoundedImage;
				}
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.Assert(false, e.ToString());
			}
			return null;
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

		private const int WM_SETREDRAW = 11;

		public static void SuspendDrawing(Control parent)
		{
			SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
		}

		public static void ResumeDrawing(Control parent)
		{
			SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
			parent.Refresh();
		}

		public static bool IsOnScreen(Form form)//모니터 밖인지 체크
		{
			Screen[] screens = Screen.AllScreens;
			foreach (Screen screen in screens)
			{
				Rectangle formRectangle = new Rectangle(form.Left, form.Top,
														 form.Width, form.Height);
				
				if (screen.WorkingArea.IntersectsWith(formRectangle))
				{
					return true;
				}
			}

			return false;
		}

		public static string ReplaceTextExpend(ClientTweet tweet)
		{
			string ret = tweet.text;
			if (tweet.isUrl)
				for (int i = 0; i < tweet.listUrl.Count; i++)
					ret = ret.Replace(tweet.listUrl[i].display_url, tweet.listUrl[i].expanded_url);

			if (tweet.isMedia)
				foreach (ClientMedia item in tweet.dicMedia.Values)
					ret = ret.Replace(item.display_url, item.expanded_url);

			return ret;
		}

		//public enum FlashMode
		//{
		//	/// 
		//	/// Stop flashing. The system restores the window to its original state.
		//	/// 
		//	FLASHW_STOP = 0,
		//	/// 
		//	/// Flash the window caption.
		//	/// 
		//	FLASHW_CAPTION = 1,
		//	/// 
		//	/// Flash the taskbar button.
		//	/// 
		//	FLASHW_TRAY = 2,
		//	/// 
		//	/// Flash both the window caption and taskbar button.
		//	/// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
		//	/// 
		//	FLASHW_ALL = 3,
		//	/// 
		//	/// Flash continuously, until the FLASHW_STOP flag is set.
		//	/// 
		//	FLASHW_TIMER = 4,
		//	/// 
		//	/// Flash continuously until the window comes to the foreground.
		//	/// 
		//	FLASHW_TIMERNOFG = 12
		//}

		//public static bool FlashWindowEx(IntPtr hWnd, FlashMode fm)
		//{
		//	FlasWinfo fInfo = new FlasWinfo();

		//	fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
		//	fInfo.hwnd = hWnd;
		//	fInfo.dwFlags = (uint)fm;
		//	fInfo.uCount = UInt32.MaxValue;
		//	fInfo.dwTimeout = 0;

		//	return FlashWindowEx(ref fInfo);
		//}

		//public static bool WINAPI FlashWindowEx(object obj)
		//{

		//}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

		//Flash both the window caption and taskbar button.
		//This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
		public const UInt32 FLASHW_ALL = 3;

		// Flash continuously until the window comes to the foreground. 
		public const UInt32 FLASHW_TIMERNOFG = 12;

		[StructLayout(LayoutKind.Sequential)]
		public struct FLASHWINFO
		{
			public UInt32 cbSize;
			public IntPtr hwnd;
			public UInt32 dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		// Do the flashing - this does not involve a raincoat.
		public static bool FlashWindowEx(Form form)
		{
			IntPtr hWnd = form.Handle;
			FLASHWINFO fInfo = new FLASHWINFO();

			fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
			fInfo.hwnd = hWnd;
			fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
			fInfo.uCount = UInt32.MaxValue;
			fInfo.dwTimeout = 0;

			return FlashWindowEx(ref fInfo);
		}
	}
}
