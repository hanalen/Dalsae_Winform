using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static TwitterClient.FileManager;
using static TwitterClient.TwitterClientManager;
using static TwitterClient.TwitterWeb;
using static TwitterClient.DataManager;

namespace TwitterClient
{
	public enum eResponse
	{
		NONE,
		STREAM,
		TIME_LINE,
		USER_TIMELINE,
		MENTION,
		UPDATE,
		MY_INFO,
		MY_TWEET,
		IMAGE,
		RETWEET,
		UN_RETWEET,
		FAVORITE_LIST,
		FAVORITE_CREATE,
		FAVORITE_DESTROY,
		DELETE_TWEET,
		FOLLOWING,
		FOLLOWING_IDS,
		BLOCK_IDS,
		GET_DM,
		RETWEET_OFF_IDS,
		FOLLOWING_UPDATE,
		SINGLE_TWEET,
		BLOCK_CREAE,
		BLOCK_DESTROY,
	}

	//기초 패킷 클래스
	public class BaseParameter
	{
		public bool isMore { get; set; } = false;
		public string url { get; set; }
		public string multiMedia { get; set; }
		public eResponse eresponse { get; set; }
		public string method;
		public Dictionary<string, string> dicParams = new Dictionary<string, string>();

		public string MethodGetUrl()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(url);
			sb.Append("?");
			foreach (string item in dicParams.Keys)
			{
				sb.Append(item);
				sb.Append("=");
				sb.Append(Uri.EscapeDataString(dicParams[item]));
				sb.Append("&");
			}
			sb.Remove(sb.Length - 1, 1);

			return sb.ToString();
		}
	}

	//Pin받기위해 띄움
	class ParameterGetOAuth : BaseParameter
	{
		public ParameterGetOAuth()
		{
			this.url = "https://api.twitter.com/oauth/request_token";
			this.method = "POST";
			oauth_callback = "oob";
		}
		public string oauth_callback { get { return dicParams["oauth_callback"]; } set { dicParams["oauth_callback"] = value; } }
	}

	//핀 받고 엑세스 토큰받을 때 사용
	class ParameterGetAccessToken : BaseParameter
	{
		public ParameterGetAccessToken()
		{
			this.url = "https://api.twitter.com/oauth/access_token";
			this.method = "POST";
		}
		public string oauth_verifier { get { return dicParams["oauth_verifier"]; } set { dicParams["oauth_verifier"] = value; } }
	}

	//다중계정 아이디 변경 확인 용
	class ParameterLookUp : BaseParameter
	{
		public ParameterLookUp(long user_id)
		{
			this.url = "https://api.twitter.com/1.1/users/lookup.json";
			this.method = "GET";
		}
		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
	}

	class ParameterFavoritesList : BaseParameter
	{
		public ParameterFavoritesList()
		{
			url = "https://api.twitter.com/1.1/favorites/list.json";
			method = "GET";
			eresponse = eResponse.FAVORITE_LIST;
			count = 40.ToString();
			tweet_mode = "extended";
		}
		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
		public string screen_name { get { return dicParams["screen_name"]; } set { dicParams["screen_name"] = value; } }
		public string count { get { return dicParams["count"]; } set { dicParams["count"] = value; } }
		public object max_id { get { return dicParams["max_id"]; } set { dicParams["max_id"] = value.ToString(); isMore = true; } }
		public string tweet_mode { get { return dicParams["tweet_mode"]; } set { dicParams["tweet_mode"] = value; } }
	}

	//유저 트윗 긁을 떄 사용
	class ParameterUserTimeLine : BaseParameter
	{
		public ParameterUserTimeLine()
		{
			url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
			method = "GET";
			eresponse = eResponse.USER_TIMELINE;
			count = 40.ToString();
			tweet_mode = "extended";
			//exclude_replies = true;
			//trim_user = true;
		}
		//public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
		public string screen_name { get { return dicParams["screen_name"]; } set { dicParams["screen_name"] = value; } }
		public object max_id { get { return dicParams["max_id"]; } set { dicParams["max_id"] = value.ToString(); isMore = true; } }
		public string count { get { return dicParams["count"]; } set { dicParams["count"] = value; } }
		public string tweet_mode { get { return dicParams["tweet_mode"]; } set { dicParams["tweet_mode"] = value; } }
		//public object trim_user { get { return dicParams["trim_user"]; } set { dicParams["trim_user"] = value.ToString(); } }
		//public object exclude_replies { get { return dicParams["exclude_replies"]; } set { dicParams["exclude_replies"] = value.ToString(); } }
		//public object contributor_details { get { return dicParams["contributor_details"]; } set { dicParams["contributor_details"] = value.ToString(); } }
		//public object include_rts { get { return dicParams["include_rts"]; } set { dicParams["include_rts"] = value.ToString(); } }
	}

	//자기 정보 긁을 때 사용
	class ParameterVerifyCredentials : BaseParameter
	{
		public ParameterVerifyCredentials()
		{
			url = "https://api.twitter.com/1.1/account/verify_credentials.json";
			method = "GET";
			eresponse = eResponse.MY_INFO;
		}
	}

	//트윗 업로드에 사용
	public class ParameterUpdate : BaseParameter
	{
		public ParameterUpdate()
		{
			this.url = "https://api.twitter.com/1.1/statuses/update.json";
			this.method = "POST";
			eresponse = eResponse.UPDATE;
		}

		public string status { get { return dicParams["status"]; } set { dicParams["status"] = value.ToString(); } }
		public string in_reply_to_status_id { get { return dicParams["in_reply_to_status_id"]; } set { dicParams["in_reply_to_status_id"] = value.ToString(); } }
		public string media_ids { get { return dicParams["media_ids"]; } set { dicParams["media_ids"] = value.ToString(); } }
		public string possibly_sensitive { set { dicParams["status"] = value.ToString(); } }
		public string lat { set { dicParams["status"] = value.ToString(); } }
		public string Long { set { dicParams["status"] = value.ToString(); } }
		public string place_id { set { dicParams["status"] = value.ToString(); } }
		public string display_coordinates { set { dicParams["status"] = value.ToString(); } }
		public string trim_user { set { dicParams["status"] = value.ToString(); } }
		//public string media_ids { set { dicParams["status"] = value.ToString(); } }
	}

	//홈 타임라인 땡길때
	class ParameterHomeTimeLine : BaseParameter
	{
		public ParameterHomeTimeLine()
		{
			url = "https://api.twitter.com/1.1/statuses/home_timeline.json";
			method = "GET";
			eresponse = eResponse.TIME_LINE;
			count = 40.ToString();
			tweet_mode = "extended";
		}
		public string count { get { return dicParams["count"]; } set { dicParams["count"] = value; } }
		public string tweet_mode { get { return dicParams["tweet_mode"]; } set { dicParams["tweet_mode"] = value; } }
		//public string MaxId { set { dicParams["max_id"] = value; } }
		//public string TrimUser { set { dicParams["trim_user"] = value; } }
		//public string ExcludeReplies { set { dicParams["exclude_replies"] = value; } }
	}

	//이미지 올릴 떄
	class ParameterMediaUpload : BaseParameter
	{
		public ParameterMediaUpload()
		{
			this.url = "https://upload.twitter.com/1.1/media/upload.json";
			this.method = "POST";
		}

		//public string media { get { return dicParams["media"]; } set { dicParams["media"] = value.ToString(); } }
		public string media_data { get { return multiMedia; } set { multiMedia = value.ToString(); } }
		public string additional_owners { get { return dicParams["additional_owners"]; } set { dicParams["additional_owners"] = value.ToString(); } }
	}

	//관글 on
	class ParameterFavorites_Create : BaseParameter
	{
		public ParameterFavorites_Create()
		{
			url = "https://api.twitter.com/1.1/favorites/create.json";
			method = "POST";
			eresponse = eResponse.FAVORITE_CREATE;
		}

		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }
		public string include_entities { get { return dicParams["include_entities"]; } set { dicParams["include_entities"] = value.ToString(); } }
	}

	//관글off
	class ParameterFavorites_Destroy : BaseParameter
	{
		public ParameterFavorites_Destroy()
		{
			url = "https://api.twitter.com/1.1/favorites/destroy.json";
			method = "POST";
			eresponse = eResponse.FAVORITE_DESTROY;
		}

		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }
		public string include_entities { get { return dicParams["include_entities"]; } set { dicParams["include_entities"] = value.ToString(); } }
	}

	//retweet
	class ParameterRetweet : BaseParameter
	{
		public ParameterRetweet(long id)
		{
			url = "https://api.twitter.com/1.1/statuses/retweet/" + id + ".json";
			method = "POST";
			eresponse = eResponse.RETWEET;
			this.id = id;
		}
		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }
	}

	//리트윗 취소
	class ParameterUnRetweet : BaseParameter
	{
		public ParameterUnRetweet(long id)
		{
			url = "https://api.twitter.com/1.1/statuses/unretweet/" + id + ".json";
			method = "POST";
			eresponse = eResponse.UN_RETWEET;
			this.id = id;
		}

		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }
	}

	class ParameterMentionTimeLine : BaseParameter
	{
		public ParameterMentionTimeLine()
		{
			url = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
			method = "GET";
			eresponse = eResponse.MENTION;
			count = 40.ToString();
			tweet_mode = "extended";
		}

		public string count { get { return dicParams["count"]; } set { dicParams["count"] = value.ToString(); } }
		public object max_id { get { return dicParams["max_id"]; } set { dicParams["max_id"] = value.ToString(); isMore = true; } }
		public string tweet_mode { get { return dicParams["tweet_mode"]; } set { dicParams["tweet_mode"] = value; } }
		//public string max_id { get { return dicParams["max_id"]; } set { dicParams["max_id"] = value.ToString(); } }
		//public string trim_user { get { return dicParams["trim_user"]; } set { dicParams["trim_user"] = value.ToString(); } }
		//public string include_entities { get { return dicParams["include_entities"]; } set { dicParams["include_entities"] = value.ToString(); } }
	}

	class ParameterUserStream : BaseParameter
	{
		public ParameterUserStream()
		{
			url = "https://userstream.twitter.com/1.1/user.json";
			method = "GET";
			eresponse = eResponse.NONE;
		}
		public string delimited { get { return dicParams["delimited"]; } set { dicParams["delimited"] = value.ToString(); } }
		public string stall_warnings { get { return dicParams["stall_warnings"]; } set { dicParams["stall_warnings"] = value.ToString(); } }
		public string with { get { return dicParams["with"]; } set { dicParams["with"] = value.ToString(); } }
		public string replies { get { return dicParams["replies"]; } set { dicParams["replies"] = value.ToString(); } }
		public string track { get { return dicParams["track"]; } set { dicParams["track"] = value.ToString(); } }
		public string locations { get { return dicParams["locations"]; } set { dicParams["locations"] = value.ToString(); } }
		public string stringify_friend_ids { get { return dicParams["stringify_friend_ids"]; } set { dicParams["stringify_friend_ids"] = value.ToString(); } }
	}

	class ParameterTweetDelete : BaseParameter
	{
		public ParameterTweetDelete(long id)
		{
			url = "https://api.twitter.com/1.1/statuses/destroy/" + id + ".json";
			method = "POST";
			this.id = id.ToString();
			eresponse = eResponse.DELETE_TWEET;
		}
		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }
		public string trim_user { get { return dicParams["trim_user"]; } set { dicParams["trim_user"] = value.ToString(); } }
	}

	class ParameterFollowingIds : BaseParameter//팔로잉 아이디만 가져옴, max 5000
	{
		public ParameterFollowingIds()
		{
			url = "https://api.twitter.com/1.1/friends/ids.json";
			method = "GET";
			eresponse = eResponse.FOLLOWING_IDS;
		}
	}

	class ParameterBlockCreate : BaseParameter
	{
		public ParameterBlockCreate(long id)
		{
			url = "https://api.twitter.com/1.1/blocks/create.json";
			method = "POST";
			eresponse = eResponse.BLOCK_CREAE;
			this.user_id = id;
		}
		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
	}

	class ParameterBlockDestroy:BaseParameter
	{
		public ParameterBlockDestroy(long id)
		{
			url = "https://api.twitter.com/1.1/blocks/create.json";
			method = "POST";
			eresponse = eResponse.BLOCK_DESTROY;
			this.user_id = id;
		}
		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
	}

	class ParameterFollowing : BaseParameter//팔로잉 리스트, max 200
	{
		public ParameterFollowing()
		{
			url = "https://api.twitter.com/1.1/friends/list.json";
			method = "GET";
			eresponse = eResponse.FOLLOWING;
		}
		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
		public string screen_name { get { return dicParams["screen_name"]; } set { dicParams["screen_name"] = value.ToString(); } }
		public string cursor { get { return dicParams["cursor"]; } set { dicParams["cursor"] = value.ToString(); } }
		public string count { get { return dicParams["count"]; } set { dicParams["count"] = value.ToString(); } }
		public string skip_status { get { return dicParams["skip_status"]; } set { dicParams["skip_status"] = value.ToString(); } }
		public string include_user_entities { get { return dicParams["include_user_entities"]; } set { dicParams["include_user_entities"] = value.ToString(); } }
	}

	class ParameterBlockIds : BaseParameter//블락리스트, max 5000
	{
		public ParameterBlockIds()
		{
			url = "https://api.twitter.com/1.1/blocks/ids.json";
			method = "GET";
			eresponse = eResponse.BLOCK_IDS;

		}
		public string stringify_ids { get { return dicParams["stringify_ids"]; } set { dicParams["stringify_ids"] = value.ToString(); } }
		public object cursor { get { return dicParams["cursor"]; } set { dicParams["cursor"] = value.ToString(); } }
	}

	class ParameterGetDM:BaseParameter
	{
		public ParameterGetDM()
		{
			url = "https://api.twitter.com/1.1/direct_messages.json";
			method = "GET";
			eresponse = eResponse.GET_DM;
			count = 20;
		}
		public object count { get { return dicParams["count"]; } set { dicParams["count"] = value.ToString(); } }
	}

	class ParameterGetRetweetOffIds : BaseParameter
	{
		public ParameterGetRetweetOffIds()
		{
			url = "https://api.twitter.com/1.1/friendships/no_retweets/ids.json";
			method = "GET";
			eresponse = eResponse.RETWEET_OFF_IDS;
		}
	}

	class ParameterUpdateFollowingData:BaseParameter
	{
		public ParameterUpdateFollowingData()
		{
			url = "https://api.twitter.com/1.1/friendships/update.json";
			method = "POST";
			eresponse = eResponse.FOLLOWING_UPDATE;
		}

		public object user_id { get { return dicParams["user_id"]; } set { dicParams["user_id"] = value.ToString(); } }
		public object retweets { get { return dicParams["retweets"]; } set { dicParams["retweets"] = value.ToString(); } }
	}

	class ParameterImage : BaseParameter
	{
		public ParameterImage(string url)
		{
			this.url = url;
			method = "GET";
		}
	}

	class ParameterSingleTweet : BaseParameter
	{
		public ParameterSingleTweet(string id)
		{
			url = "https://api.twitter.com/1.1/statuses/show.json";
			method = "GET";
			eresponse = eResponse.SINGLE_TWEET;
			this.id = id;
		}

		public object id { get { return dicParams["id"]; } set { dicParams["id"] = value.ToString(); } }

	}
}