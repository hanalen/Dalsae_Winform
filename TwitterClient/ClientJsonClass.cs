using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientAPIError
	{
		public List<ClientError> errors { get; set; }
	}

	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientError
	{
		public int code { get; set; }
	}

	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientSendTweet
	{
		public ParameterUpdate parameter { get; private set; }
		public Bitmap[] listBitmap { get; private set; }
		public string multiPath { get; private set; }

		public void SetTweet(ParameterUpdate parameter, Bitmap[] listBitmap)
		{
			this.parameter = parameter;
			this.listBitmap = listBitmap;
			if(listBitmap!=null)
				if (listBitmap.Length > 0)
					parameter.media_ids = string.Empty;//이거 안 하면 nullException뜸
		}

		public void SetTweet(ParameterUpdate parameter, string multiPath)
		{
			this.parameter = parameter;
			this.multiPath = multiPath;
			if(string.IsNullOrEmpty(multiPath)==false)
				parameter.media_ids = string.Empty;//이거 안 하면 nullException뜸
		}

		public void ResponseMedia(string mediaId)
		{
			parameter.media_ids += mediaId;
			parameter.media_ids += ",";
		}
	}
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class UserSemi
	{
		public UserSemi(string name, string screen_name, long id)
		{
			this.name = name;
			this.screen_name = screen_name;
			this.id = id;
			//this.id_str = id_str;
		}
		public string name { get; private set; }
		public string screen_name { get; private set; }
		public long id { get; private set; }
		public void UpdateName(string name)
		{
			this.name = name;
		}
		//public string id_str { get; private set; }
	}
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientBlockIds
	{
		public long next_cursor { get; set; }
		//public string next_cursor_str { get; set; }
		public long previous_cursor { get; set; }
		//public string previous_cursor_str { get; set; }
		public long[] ids;
	}
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class User
	{
		public string name { get; set; }// "OAuth Dancer",
		public string screen_name { get; set; }
		public string profile_image_url { get; set; }//"http://a0.twimg.com/profile_images/730275945/oauth-dancer_normal.jpg",
													 //public string id_str { get; set; }//r": "119476949",
		public long id { get; set; }
		public string profile_link_color { get; set; }//: "0084B4",
		public bool Protected { get; set; }
		public bool verified { get; set; }
		public int favourites_count { get; set; }
		public int friends_count { get; set; }
	}

	//팔로 리스트 땡길 때 사용
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientUsers
	{
		public long previous_cursor { get; set; }
		public string previous_cursor_str { get; set; }
		public long next_cursor { get; set; }
		public User[] users;
	}

	//dm용 클래스
	[ObfuscationAttribute(Exclude = false, ApplyToMembers = true)]
	public class ClientDirectMessage
	{
		private string _text;
		private DateTime dateTime;

		public void Init()//링크 변환 등
		{
			if (entities == null) return;

			if (entities.urls != null)
			{
				for (int i = 0; i < entities.urls.Count; i++)
					_text = _text.Replace(entities.urls[i].url, entities.urls[i].display_url);
			}

			if(entities.media!=null)
			{
				//for(int i=0;i<entities.media.Count;i++)
				if (entities.media.Count > 0)
					_text = _text.Replace(entities.media[0].url, entities.media[0].display_url);
			}
		}

		public object created_at { get { return dateTime; } set { SetDateTime(value); } }
        public ClientEntities entities { get; set; }
		public ClientSender sender { get; set; }
		public ClientRecipient recipient { get; set; }
		public long id { get; set; }
        public long recipient_id { get; set; }
        public string recipient_screen_name { get; set; }
        public long sender_id { get; set; }
        public string sender_screen_name { get; set; }
		public string text { get { return _text; } set { _text = HttpUtility.HtmlDecode(value); } }
		private void SetDateTime(object value)
		{
			dateTime = DateTime.ParseExact(value.ToString(), "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
		}
	}

	//dm용 클래스, 받는 사람 정보
	public class ClientRecipient
	{
		private DateTime dateTime;
		public object created_at { get { return dateTime; } set { SetDateTime(value); } }
		//public long id { get; set; }
		public string name { get; set; }
		public string screen_name { get; set; }
		public string profile_image_url { get; set; }

		private void SetDateTime(object value)
		{
			dateTime = DateTime.ParseExact(value.ToString(), "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
		}
	}

	//dm용 클래스, 보내는 사람 정보
	public class ClientSender
	{
		private DateTime dateTime;
		public object created_at { get { return dateTime; } set { SetDateTime(value); } }
		//public long id { get; set; }
		public string name { get; set; }
		public string screen_name { get; set; }
		public string profile_image_url { get; set; }
		private void SetDateTime(object value)
		{
			dateTime = DateTime.ParseExact(value.ToString(), "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
		}
	}

	public class ClientTweet
	{
		//--------------------------------------------------------------------------------
		//-------------------------외부 참조용 변수-------------------------------------
		//--------------------------------------------------------------------------------
		public bool isRetweet { get; set; } = false;
		public bool isMedia { get; private set; } = false;
		public bool isReply { get; private set; } = false;
		public bool isQTRetweet { get; private set; } = false;
		public bool isUrl { get; private set; } = false;
		public bool isMention { get; private set; } = false;
		public ClientEntities mediaEntities { get; private set; }//미디어 엔티티
		public ClientEntities lastEntities { get; private set; }
		[Newtonsoft.Json.JsonIgnore]
		public ClientTweet originalTweet { get; private set; }
		public Dictionary<string, ClientMedia> dicMedia = new Dictionary<string, ClientMedia>();//key: displayUrl
		//public List<ClientMedia> listMedia = new List<ClientMedia>();
		public List<ClientURL> listUrl = new List<ClientURL>();
		public HashSet<string> hashMention = new HashSet<string>();
		

		//private bool isExtendEntities = false;
		private bool isExtendTweet = false;
		private DateTime dateTime;
		private string _source;
		//public Dictionary<string, ClientURL> dicUrl = new Dictionary<string, ClientURL>();//메뉴에서 보여줄 ui정보
		public void Init()//t.co 문자 변환 등 변경이 필요한 값들을 변경해준다
		{
			if (originalTweet != null) return;
			SetOriginalTweet();
			SetBoolean();
			ReplaceText();
		}
		
		private void SetOriginalTweet()
		{
			if (retweeted_status != null)
				isRetweet = true;

			if (isRetweet)
				originalTweet = retweeted_status;
			else
				originalTweet = this;
		}

		private void SetBoolean()
		{
			if (string.IsNullOrEmpty(originalTweet.in_reply_to_status_id_str) == false)
				isReply = true;
			if (string.IsNullOrEmpty(originalTweet.quoted_status_id_str) == false)
				isQTRetweet = true;
			if (originalTweet.extended_tweet != null)
				isExtendTweet = true;

			//if (originalTweet.entities != null)//오리지널 트윗의 일반,확장 엔티티 설정
			//{
			//	if (extended_entities != null)
			//	{
			//		isExtendEntities = true;
			//		lastEntities = extended_entities;
			//	}
			//	else
			//	{
			//		lastEntities = entities;
			//	}
			//}
			if(isExtendTweet)//확장 트윗일 경우 확장 트윗의 일반,확장 엔티티 설정
			{
				if(originalTweet.extended_tweet.extended_entities!=null)
				{
					//isExtendEntities = true;
					mediaEntities = originalTweet.extended_tweet.extended_entities;
				}
				else
				{
					mediaEntities = originalTweet.entities;
				}
				lastEntities = originalTweet.extended_tweet.entities;
			}
			else
			{
				if (originalTweet.extended_entities != null)
				{
					//isExtendEntities = true;
					mediaEntities = originalTweet.extended_entities;
				}
				else
				{
					mediaEntities = originalTweet.entities;
				}
				lastEntities = originalTweet.entities;
			}

			//if (isExtendEntities)
			//{
				if (mediaEntities.media != null)
					if (mediaEntities.media.Count > 0)
						isMedia = true;
			//}
			//else
			//{
			//	if (originalTweet.entities.media != null)
			//		if (originalTweet.entities.media.Count > 0)
			//			isMedia = true;
			//}

			for (int i = 0; i < lastEntities.user_mentions.Count; i++)
			{
				if (DataInstence.CheckIsMe(lastEntities.user_mentions[i].id))
					isMention = true;

				hashMention.Add(lastEntities.user_mentions[i].screen_name);
			}

			if (lastEntities.urls != null)
				if (lastEntities.urls.Count > 0)
					isUrl = true;
		}


		private void ReplaceText()
		{
			if (originalTweet.truncated)//140자가 넘는 트윗이나 이미지 2장이상??일 경우 사용
			{
				if (isExtendTweet)//140자가 넘을 경우 
				{
					originalTweet._text = HttpUtility.HtmlDecode(originalTweet.extended_tweet.full_text);//140자 넘는 텍스트로 변경
					//ReplaceURL(lastEntities, originalTweet);
				}
				//if (isExtendEntities)
				ReplaceURL(originalTweet);
			}
			else
			{
				ReplaceURL(originalTweet);
			}
		}

		//private void ReplaceText()
		//{
		//	return;
		//	if (truncated)//140자가 넘는 트윗이나 이미지 2장이상??일 경우 사용
		//	{
		//		if (extended_tweet != null)//140자가 넘을 경우 
		//		{
		//			_text = extended_tweet.full_text;//140자 넘는 텍스트로 변경
		//			ReplaceURL(extended_tweet.entities, this);
		//		}
		//		if (extended_entities != null)
		//			ReplaceURL(extended_entities, this);
		//		//else
		//		//	ReplaceURL(entities, this);
		//	}
		//	else
		//	{
		//		ReplaceURL(entities, this);
		//	}
		//}

		private void ReplaceURL(/*ClientEntities entities, */ClientTweet tweet)
		{
			if (entities == null) return;

			if (isUrl)//url이 있을 경우 변경
				for (int i = 0; i < lastEntities.urls.Count; i++)
				{
					tweet._text = tweet._text.Replace(lastEntities.urls[i].url, lastEntities.urls[i].display_url);
					listUrl.Add(lastEntities.urls[i]);
					//if (tweet.dicUrl.ContainsKey(entities.urls[i].url) == false)
					//	tweet.dicUrl.Add(entities.urls[i].url, entities.urls[i]);
				}

			if (isMedia)//미디어가 있을 경우
				for (int i = 0; i < mediaEntities.media.Count; i++)
				{
					tweet._text = tweet._text.Replace(mediaEntities.media[i].url, mediaEntities.media[i].display_url);
					if (dicMedia.ContainsKey(mediaEntities.media[i].display_url) == false)
						dicMedia.Add(mediaEntities.media[i].display_url, mediaEntities.media[i]);
					//listMedia.Add(mediaEntities.media[i]);
					//if (tweet.dicUrl.ContainsKey(entities.media[i].url) == false)
					//	tweet.dicUrl.Add(entities.media[i].url, new ClientURL(entities.media[i]));
				}
		}

		private string _text;//트윗이요
		public string full_text { get { return _text; } set { _text = HttpUtility.HtmlDecode(value); } }//API땡기면 full_text가 옴
		public string text { get { return _text; } set { _text = HttpUtility.HtmlDecode(value); } }//스트리밍이면 text로 와서 이렇게 사용
		public User user { get; set; }//트윗 쓴 사람 정보, 리트윗일 경우 리트윗 정보에 원 트윗 user정보 있음

		public ClientTweet retweeted_status { get; set; }
		public ClientEntities entities { get; set; }
		public ClientEntities extended_entities { get; set; }//이미지가 여러장일 경우 사용됨
		public string in_reply_to_status_id_str { get; set; }
		public string quoted_status_id_str { get; set; }//인용 리트윗 트윗 id

		public object created_at { get { return dateTime; } set { SetDateTime(value); } }
		public long id { get; set; }//트윗 id
		public bool truncated { get; set; }//140자 넘는 경우 알려주는 거
		public ClientExtendedTweet extended_tweet { get; set; }
		public string source { get { return _source; } set { SetSource(value); } }
		//public long in_reply_to_status_id { get; set; }
		//public string in_reply_to_user_id { get; set; }
		
		//public string in_reply_to_screen_name { get; set; }
		//public bool is_quote_status { get; set; }
		public int retweet_count { get; set; }//리트윗 카운트
		public int favorite_count { get; set; }//별 카운트
		public bool favorited { get; set; }//별박았는지
		public bool retweeted { get; set; }//리트윗 했는지

		private void SetDateTime(object value)
		{
			dateTime = DateTime.ParseExact(value.ToString(), "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
		}

		private void SetSource(string value)
		{
			_source = System.Text.RegularExpressions.Regex.Replace(value, "<[^>]*>", string.Empty);
		}
	}

	public class ClientExtendedTweet
	{
		public string full_text { get; set; }//140자 넘는 트윗일 경우 사용
		public ClientEntities entities { get; set; }
		public ClientEntities extended_entities { get; set; }
	}

	public class ClientEntities
	{
		public List<ClientURL> urls = new List<ClientURL>();// { get; set; }
		public List<ClientHashtag> hashtags = new List<ClientHashtag>();// { get; set; }
		public List<ClientUserMentions> user_mentions = new List<ClientUserMentions>();
		public List<ClientMedia> media = new List<ClientMedia>();
		//public List<ClientSymbol> symbols = new List<ClientSymbol>();// { get; set; }
	}

	public class ClientHashtag
	{
		public string text { get; set; }
	}

	//public class ClientSymbol
	//{
	//	public string text { get; set; }
	//}

	public class ClientUserMentions//리트윗 한 글의 원 유저 정보. 답변 보낼 때 사용
	{
		public string screen_name { get; set; }
		public string name { get; set; }
		public long id { get; set; }
	}

	public class ClientURL
	{
		public ClientURL() { }
		public ClientURL(string url, string expandedUrl, string displayUrl)
		{
			this.url = url;
			this.expanded_url = expandedUrl;
			this.display_url = displayUrl;
		}
		public ClientURL(ClientMedia media)
		{
			this.url = media.url;
			this.expanded_url = media.expanded_url;
			this.display_url = media.display_url;
		}
		public string url { get; set; }
		public string expanded_url { get; set; }
		public string display_url { get; set; }
	}

	public class ClientMultimedia//전송 후 받는 id용
	{
		public string media_id_string { get; set; }
		public long media_id { get; set; }

	}

	public class ClientExtendedEntities
	{
		public ClientMedia[] media;
	}

	public class ClientMedia
	{
		public long id { get; set; }
		public string media_url_https { get; set; }                 //":"https://pbs.twimg.com/media/C06Y8onVEAA6Ktk.jpg",
		public string url { get; set; }                                 //":"https://t.co/gULwuVQFC6",
		public string display_url { get; set; }                         //":"pic.twitter.com/gULwuVQFC6",
		public string expanded_url { get; set; }                        //":"https://twitter.com/umasukesankana/status/814756998243680256/photo/1",
		public string type { get; set; }
		public ClientSize sizes = new ClientSize();
	}

	public class ClientSize
	{
		public ClientLarge large = new ClientLarge();
		public ClientTumb thumb = new ClientTumb();
		public ClientMedium medium = new ClientMedium();
		public ClientSmall small = new ClientSmall();

	}

	public class ClientLarge
	{
		public int w { get; set; }
		public int h { get; set; }
		public string resize { get; set; }
	}

	public class ClientTumb
	{
		public int w { get; set; }
		public int h { get; set; }
		public string resize { get; set; }
	}

	public class ClientMedium
	{
		public int w { get; set; }
		public int h { get; set; }
		public string resize { get; set; }
	}

	public class ClientSmall
	{
		public int w { get; set; }
		public int h { get; set; }
		public string resize { get; set; }
	}

	public class ClientFollowingUpdate
	{
		public ClientRelationship relationship { get; set; }
	}

	public class ClientRelationship
	{
		public ClientTraget target { get; set; }
		public ClientSource source { get; set; }
	}

	public class ClientTraget
	{
		public long id { get; set; }
		public string screen_name { get; set; }
	}

	public class ClientSource
	{
		public bool want_retweets { get; set; }
	}


	//-------------------------------------------------------------------------------------------------------
	//------------------------------------------유저스트리밍-----------------------------------------------
	//-------------------------------------------------------------------------------------------------------
	public class ClientStreamDelete
	{
		public StreamDelete delete { get; set; }
	}

	public class StreamDelete
	{
		public Status status { get; set; }
	}

	public class Status
	{
		public long id { get; set; }
	}

	public class StreamDirectMessage
	{
		public ClientDirectMessage direct_message { get; set; }
	}

	public class StreamEvent
	{
		public string Event { get; set; }
		public string created_at { get; set; }
		public StreamSource source { get; set; }
		public User target { get; set; }//유저관련 이벤트일 경우 유저 정보
		public ClientTweet target_object { get; set; }//트윗 관련 이벤트일 경우 트윗 정보
	}

	public class StreamSource//.....???
	{

	}
}
