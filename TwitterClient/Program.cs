using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	static class Program
	{
		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			

			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			DataInstence.Init();
			FileInstence.Init();//init
			ClientInctence.Init();//init



			MainForm main = new TwitterClient.MainForm();
			ClientInctence.SetMainForm(main);
			Application.Run(main);

			//MainForm form;
			//using (var instance = new InstanceHelper("Dalsae"))
			//{
			//	if (instance.Check())
			//	{
			//		form = new MainForm();
			//		ClientInctence.SetMainForm(form);
			//		Application.Run(instance.MainWindow = form);
			//	}
			//}
		}
		public static class MINIDUMP_TYPE
		{
			public const int MiniDumpNormal = 0x00000000;
			public const int MiniDumpWithDataSegs = 0x00000001;
			public const int MiniDumpWithFullMemory = 0x00000002;
			public const int MiniDumpWithHandleData = 0x00000004;
			public const int MiniDumpFilterMemory = 0x00000008;
			public const int MiniDumpScanMemory = 0x00000010;
			public const int MiniDumpWithUnloadedModules = 0x00000020;
			public const int MiniDumpWithIndirectlyReferencedMemory = 0x00000040;
			public const int MiniDumpFilterModulePaths = 0x00000080;
			public const int MiniDumpWithProcessThreadData = 0x00000100;
			public const int MiniDumpWithPrivateReadWriteMemory = 0x00000200;
			public const int MiniDumpWithoutOptionalData = 0x00000400;
			public const int MiniDumpWithFullMemoryInfo = 0x00000800;
			public const int MiniDumpWithThreadInfo = 0x00001000;
			public const int MiniDumpWithCodeSegs = 0x00002000;
		}

		[DllImport("dbghelp.dll")]
		public static extern bool MiniDumpWriteDump(IntPtr hProcess,
													Int32 ProcessId,
													IntPtr hFile,
													int DumpType,
													IntPtr ExceptionParam,
													IntPtr UserStreamParam,
													IntPtr CallackParam);

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			CreateMiniDump();
		}

		private static void CreateMiniDump()
		{
			using (FileStream fs = new FileStream("UnhandledDump.dmp", FileMode.Create))
			{
				using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
				{
					MiniDumpWriteDump(process.Handle,
													 process.Id,
													 fs.SafeFileHandle.DangerousGetHandle(),
													 MINIDUMP_TYPE.MiniDumpNormal,
													 IntPtr.Zero,
													 IntPtr.Zero,
													 IntPtr.Zero);
				}
			}
		}
	}

	public sealed class InstanceHelper : IDisposable
	{
		private const uint CustomMsg = 0x7A8F;

		private readonly string m_uniqueName;
		public InstanceHelper(string uniqueName)
		{
			this.m_uniqueName = uniqueName;
		}
		public void Dispose()
		{
			this.DestroyWindow();

			if (this.m_mutex != null)
			{
				this.m_mutex.Dispose();
				this.m_mutex = null;
			}

			GC.SuppressFinalize(this);
		}

		private NativeMethods.WndProc m_proc;
		private IntPtr m_customHwnd;
		private Mutex m_mutex;

		public Form MainWindow { get; set; }

		public bool Check()
		{
			bool createdNew;
			this.m_mutex = new Mutex(true, this.m_uniqueName, out createdNew);

			if (createdNew && this.m_mutex.WaitOne(0))
			{
				this.CreateWindow();
				return true;
			}
			else
			{
				var hwnd = NativeMethods.FindWindow(this.m_uniqueName, null);
				if (hwnd != IntPtr.Zero)
					NativeMethods.SendMessage(hwnd, InstanceHelper.CustomMsg, IntPtr.Zero, IntPtr.Zero);

				this.m_mutex.Dispose();
				return false;
			}
		}

		private void CreateWindow()
		{
			this.m_proc = new NativeMethods.WndProc(this.CustomProc);

			var wndClass = new NativeMethods.WNDCLASS();
			wndClass.lpszClassName = this.m_uniqueName;
			wndClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(this.m_proc);

			var resRegister = NativeMethods.RegisterClass(ref wndClass);
			var resError = Marshal.GetLastWin32Error();

			if (resRegister == 0 && resError != NativeMethods.ERROR_CLASS_ALREADY_EXISTS)
				throw new Exception();

			this.m_customHwnd = NativeMethods.CreateWindowEx(0, this.m_uniqueName, null, 0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}
		private void DestroyWindow()
		{
			if (this.m_customHwnd != IntPtr.Zero)
			{
				NativeMethods.DestroyWindow(this.m_customHwnd);
				this.m_customHwnd = IntPtr.Zero;
			}
		}

		private IntPtr CustomProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			if (msg == CustomMsg && this.MainWindow != null)
			{
				try
				{
					this.MainWindow.Invoke(new Action(this.MainWindow.Activate));
				}
				catch
				{
				}
			}

			return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
		}

		private static class NativeMethods
		{
			public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

			[DllImport("user32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr FindWindow(
				string lpClassName,
				string lpWindowName);

			[DllImport("user32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr SendMessage(
				IntPtr hWnd,
				uint Msg,
				IntPtr wParam,
				IntPtr lParam);

			[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
			public static extern ushort RegisterClass(
				ref WNDCLASS pcWndClassEx);

			[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
			public static extern IntPtr CreateWindowEx(
				int dwExStyle,
				string lpClassName,
				string lpWindowName,
				int dwStyle,
				int x,
				int y,
				int nWidth,
				int nHeight,
				IntPtr hWndParent,
				IntPtr hMenu,
				IntPtr hInstance,
				IntPtr lpParam);

			[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
			public static extern IntPtr DefWindowProc(
				IntPtr hWnd,
				uint msg,
				IntPtr wParam,
				IntPtr lParam);

			[DllImport("user32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool DestroyWindow(
				IntPtr hWnd);

			//////////////////////////////////////////////////

			[StructLayout(LayoutKind.Sequential)]
			public struct WNDCLASS
			{
				public int style;
				public IntPtr lpfnWndProc;
				public int cbClsExtra;
				public int cbWndExtra;
				public IntPtr hInstance;
				public IntPtr hIcon;
				public IntPtr hCursor;
				public IntPtr hbrBackground;
				[MarshalAs(UnmanagedType.LPTStr)]
				public string lpszMenuName;
				[MarshalAs(UnmanagedType.LPTStr)]
				public string lpszClassName;
			}

			//////////////////////////////////////////////////

			public const int ERROR_CLASS_ALREADY_EXISTS = 1410;
		}
	}

}
