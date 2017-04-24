using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	class TwitterWeb
	{
		private static TwitterWeb instence;
		private UserStream userStream = new UserStream();
		public static TwitterWeb WebInstence { get { return GetWeb(); } }
		private static TwitterWeb GetWeb()
		{
			if (instence == null)
				instence = new TwitterWeb();
			return instence;
		}

		public void Test(string json)
		{
			//c,d,e,f
			
		}
		public void DisconnectingUserStreaming()
		{
			userStream.Disconnecting();
			userStream = new TwitterClient.UserStream();
			ClientInctence.ConnectedStreaming(false);
		}
		public bool isConnectedUserStreaming() { return userStream.isConnectedStreaming; }

		public void ConnectUserStream(object obj)
		{
			if (obj == null) return;
			userStream.ConnectStreaming(obj as BaseParameter);
		}

		//각종 API요청용 함수
		public void RequestTwitter(BaseParameter parameter)
		{
			if (parameter == null) return;

			HttpWebRequest req;
			if (parameter.method == "POST")
				req = (HttpWebRequest)WebRequest.Create(parameter.url);
			else//GET일 경우
				req = (HttpWebRequest)WebRequest.Create(parameter.MethodGetUrl());

			req.ContentType= "application/x-www-form-urlencoded;encoding=utf-8";
			req.Method = parameter.method;
			req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));

			try
			{
				if (parameter.dicParams.Count > 0 && parameter.method=="POST")//POST일 때에만 Stream사용
				{
					TwitterRequest twitterRequest = new TwitterRequest(req, parameter);
					req.BeginGetRequestStream(new AsyncCallback(AsyncRequest), twitterRequest);
				}
				else
				{
					TwitterRequest twitterRequest = new TwitterRequest(req, parameter);
					req.BeginGetResponse(new AsyncCallback(AsyncResponse), twitterRequest);
				}
			}
			catch(WebException e)
			{
				using (Stream stream = e.Response.GetResponseStream())
				{
					StreamReader srReadData = new StreamReader(stream, Encoding.Default);
					string log = srReadData.ReadToEnd();
					ClientInctence.ResponseError(log);
				}
			}
		}

		private void AsyncRequest(IAsyncResult ar)
		{
			TwitterRequest req = (TwitterRequest)ar.AsyncState;

			Stream stream = req.request.EndGetRequestStream(ar);

			StringBuilder sb = new StringBuilder();

			foreach(string item in req.parameter.dicParams.Keys)
			{
				if (req.parameter.dicParams[item] != "")
				{
					sb.Append(item);
					sb.Append("=");
					OAuth.GetInstence().CalcParamUri(sb, req.parameter.dicParams[item]);
					sb.Append("&");
					//sb.Append(Uri.EscapeDataString(req.parameter.dicParams[item]));
				}
			}
			string sendData = sb.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(sendData);

			// Write to the request stream.
			stream.Write(bytes, 0, sendData.Length);
			stream.Close();
			
			req.request.BeginGetResponse(new AsyncCallback(AsyncResponse), req);
		}

		private void AsyncResponse(IAsyncResult ar)
		{
			TwitterRequest req = (TwitterRequest)ar.AsyncState;
			//HttpWebRequest request = (HttpWebRequest)ar.AsyncState;

			try
			{
				HttpWebResponse response = (HttpWebResponse)req.request.EndGetResponse(ar);
				Stream stream = response.GetResponseStream();
				StreamReader streamRead = new StreamReader(stream);
				string responseString = streamRead.ReadToEnd();

				stream.Close();
				streamRead.Close();
				response.Close();

				ClientInctence.ResponseJson(responseString, req.parameter.eresponse, req.parameter.isMore);
			}
			catch (WebException e)
			{
				using (Stream stream = e.Response?.GetResponseStream())
				{
					StreamReader srReadData = new StreamReader(stream, Encoding.Default);
					string log = srReadData.ReadToEnd();
					ClientInctence.ResponseError(log);
				}
			}
		}

		//이미지 업로드 시 요청하는 함수, 동기 전송
		//obj: 이미지 bytes를 담고있는 파라메터
		public string SendMultimedia(object obj)
		{
			if (obj == null) return null;

			string ret = string.Empty;
			BaseParameter parameter = obj as BaseParameter;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(parameter.url);
			TwitterRequest request = new TwitterRequest(req, parameter);
			
			req.ContentType = "multipart/form-data;encoding=utf-8;boundary=asdf";
			req.Method = parameter.method;
			req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));

			

			StringBuilder sb = new StringBuilder();

			sb.Append("--asdf\r\n");
			sb.Append("Content-Type: application/octet-stream\r\n");
			sb.Append("Content-Disposition: form-data; name=\"media_data\"; filename=\"img.png\"\r\n\r\n");
			sb.Append(parameter.multiMedia);
			sb.Append("\r\n\r\n");
			sb.Append("--asdf--");

			string sendData = sb.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(sendData);

			//-----------------------------------------------------------------------------------
			//------------------------------------Send------------------------------------------
			//-----------------------------------------------------------------------------------
			try//send!
			{
				using (Stream stream = req.GetRequestStream())
				{
					stream.Write(bytes, 0, sendData.Length);
					stream.Close();
					stream.Dispose();
				}
			}
			catch(WebException e)
			{
				using (Stream stream = e.Response.GetResponseStream())
				{
					StreamReader srReadData = new StreamReader(stream, Encoding.Default);
					string log = srReadData.ReadToEnd();
					ClientInctence.ResponseError(log);
				}
			}
			//-----------------------------------------------------------------------------------
			//-------------------------------Response------------------------------------------
			//-----------------------------------------------------------------------------------

			try//Response!!!
			{
				WebResponse response = req.GetResponse();
				using (Stream stream = response.GetResponseStream())
				{
					StreamReader streamRead = new StreamReader(stream);
					ret = streamRead.ReadToEnd();
					stream.Close();
					stream.Dispose();
				}
			}
			catch (WebException e)
			{
				using (Stream stream = e.Response.GetResponseStream())
				{
					StreamReader srReadData = new StreamReader(stream, Encoding.Default);
					string log = srReadData.ReadToEnd();
					ClientInctence.ResponseError(log);
				}
			}

			return ret;
		}
		
		public string SyncRequest(BaseParameter parameter)
		{
			string ret = string.Empty;
			HttpWebRequest req;
			if (parameter.method == "POST")
				req = (HttpWebRequest)WebRequest.Create(parameter.url);
			else//GET일경우
				req = (HttpWebRequest)WebRequest.Create(parameter.MethodGetUrl());
			TwitterRequest request = new TwitterRequest(req, parameter);

			req.ContentType = "application/x-www-form-urlencoded;encoding=utf-8";
			req.Method = parameter.method;
			req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));

			

			if (parameter.dicParams.Count > 0 && parameter.method == "POST")//POST일 때에만 Stream사용
			{
				//-----------------------------------------------------------------------------------
				//------------------------------------Send------------------------------------------
				//-----------------------------------------------------------------------------------
				Stream stream = req.GetRequestStream();
				StringBuilder sb = new StringBuilder();

				foreach (string item in parameter.dicParams.Keys)
				{
					if (parameter.dicParams[item] != "")
					{
						sb.Append(item);
						sb.Append("=");
						OAuth.GetInstence().CalcParamUri(sb, parameter.dicParams[item]);
						sb.Append("&");
					}
				}
				string sendData = sb.ToString();
				byte[] bytes = Encoding.UTF8.GetBytes(sendData);
				try//send!
				{
					stream.Write(bytes, 0, sendData.Length);
					stream.Close();
				}
				catch (WebException e)
				{
					using (Stream stream2 = e.Response.GetResponseStream())
					{
						StreamReader srReadData = new StreamReader(stream2, Encoding.Default);
						string log = srReadData.ReadToEnd();
						ClientInctence.ResponseError(log);
					}
				}
			}
			
			//-----------------------------------------------------------------------------------
			//-------------------------------Response------------------------------------------
			//-----------------------------------------------------------------------------------

			try//Response!!!
			{
				WebResponse response = req.GetResponse();
				Stream stream = response.GetResponseStream();
				StreamReader streamRead = new StreamReader(stream);
				ret = streamRead.ReadToEnd();
				stream.Close();
			}
			catch (WebException e)
			{
				//using (Stream stream = e.Response.GetResponseStream())
				//{
				//	StreamReader srReadData = new StreamReader(stream, Encoding.Default);
				//	string log = srReadData.ReadToEnd();
				//	ClientInctence.ResponseError(log);
				//}
			}

			return ret;
		}

		//OAuth, AccessToken 발급용 외부 호출 함수
		//parameter: BaseParameter를 상속받은 oauth, token용 parameter
		public void RequestOAuth(BaseParameter parameter)
		{
			if (parameter == null) return;

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(parameter.url);
			req.ContentType = "application/x-www-form-urlencoded;encoding=utf-8";
			req.Method = parameter.method;
			req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));

			req.BeginGetResponse(new AsyncCallback(AsyncResponseOAuth), req);
		}

		//비동기, OAuth, AccessToken발급용 함수
		//ar: HttpWebRequest
		private void AsyncResponseOAuth(IAsyncResult ar)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)ar.AsyncState;

				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
				Stream stream = response.GetResponseStream();
				StreamReader streamRead = new StreamReader(stream);
				string responseString = streamRead.ReadToEnd();

				string tokenStr = Regex.Match(responseString, @"oauth_token=([^&]+)").Groups[1].Value;//json으로 오는 게 아니라 이렇게 해야함
				string secretStr = Regex.Match(responseString, @"oauth_token_secret=([^&]+)").Groups[1].Value;
				bool isCallBack = false;
				bool.TryParse(Regex.Match(responseString, @"oauth_callback_confirmed=([^&]+)").Groups[1].Value, out isCallBack);
				//AccessToken 발급 시 user_id, screen_name, x_auth_expires(?) 옴. 현재는 사용x


				ClientInctence.UpdateToken(tokenStr, secretStr, !isCallBack);
				//TokenAndKey.SetUserToken(tokenStr);
				//TokenAndKey.SetUserTokenSecret(secretStr);

				if (isCallBack)//pin발급 시
					System.Diagnostics.Process.Start("https://api.twitter.com/oauth/authorize?oauth_token=" + tokenStr);

				stream.Close();
				streamRead.Close();
				response.Close();
			}
			catch(WebException e)
			{
				if (e.Message.IndexOf("401") > -1)
				{
					ClientInctence.InputErrorPin();
				}
			}
		}
	}

	class UserStream
	{
		private StreamReader streamRead;
		private Stream stream;
		public bool isConnectedStreaming { get; private set; } = false;
		private bool isDisconnecting { get; set; } = false;
		public void Disconnecting()
		{
			isDisconnecting = true;
			if (stream != null)
				stream.Close();
			if (streamRead != null)
				streamRead.Close();
		}
		public void ConnectStreaming(BaseParameter parameter)
		{
			if (parameter == null) return;
			if (isConnectedStreaming) return;
			
			//if (isDisconnecting) isDisconnecting = false;
			while (true)
			{
				HttpWebRequest req;
				if (parameter.method == "POST")
					req = (HttpWebRequest)WebRequest.Create(parameter.url);
				else//GET일 경우
					req = (HttpWebRequest)WebRequest.Create(parameter.MethodGetUrl());

				req.ContentType = "application/x-www-form-urlencoded;encoding=utf-8";
				//req.UserAgent = "Switter For Windows";
				req.Method = parameter.method;
				req.Headers.Add("Authorization", OAuth.GetInstence().GetHeader(parameter));
				try
				{
					WebResponse response = req.GetResponse();
					stream = response.GetResponseStream();
					streamRead = new StreamReader(stream);
					ClientInctence.ConnectedStreaming(true);
					isConnectedStreaming = true;
					string json;
					while ((json = streamRead.ReadLine()) != null)
					{
						if (isDisconnecting) break;
						if (string.IsNullOrWhiteSpace(json)) continue;
						ClientInctence.ResponseStreamJson(json);
						//TwitterClientManager.GetTwitterClientManager().ResponseJson(json, eResponse.STREAM);
					}
					stream.Close();
					streamRead.Close();
					if (isDisconnecting) break;
				}
				catch (WebException e)
				{
					if (isDisconnecting) break;
					
					Stream stReadData = e.Response?.GetResponseStream();
					if (stReadData != null)
					{
						StreamReader srReadData = new StreamReader(stReadData, Encoding.Default);
						string debug = srReadData.ReadToEnd();
						ClientInctence.ShowMessageBox(debug, "오류, 이 로그를 찍어 제보해주시면 고맙습니다.");
						break;
					}
					ClientInctence.ConnectedStreaming(false);
					//ClientInctence.ShowMessageBox("유저스트리밍 연결이 끊겼습니다. 자동으로 재접속 합니다.", "오류");
				}
				catch (Exception e)
				{
					if (isDisconnecting) break;
					Debug.Assert(false, e.ToString());
				}
			}
			isConnectedStreaming = false;
			//ClientInctence.ShowMessageBox("계정 전환 전 계정의 유저스트리밍이 끊어졌습니다.\r재연결을 시도해주세요", "확인");
		}

	}

	class TwitterRequest
	{
		public TwitterRequest(HttpWebRequest req, BaseParameter parameter)
		{
			this.parameter = parameter;
			this.request = req;
		}
		public BaseParameter parameter { get; private set; }
		public HttpWebRequest request { get; private set; }
	}
}
