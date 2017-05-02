using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service
{
    public class UserEqualityComparer: IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            return x.name.Equals(y.name);
        }
        public int GetHashCode(User obj)
        {
            return obj.name.GetHashCode();
        }
    }
    public class ApiManager
    {
        string baseAddress = "ws.audioscrobbler.com/2.0/?";
        //string initialUser = "col403";
        string apiKey = "c10556f6ca630990ddd3f988ba3404c3";
       
        public List<User> GetFriends(string initialUser)
        {
            string method = "user.getfriends";
            List<User> users = new List<User>();
            string uri = "http://" + baseAddress + "method=" + method + "&" + "user=" + initialUser + "&api_key=" + apiKey + "&format=json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                FriendsRootObject rootObj = new FriendsRootObject();
                rootObj = JsonConvert.DeserializeObject<FriendsRootObject>(json);
                users.AddRange(rootObj.friends.user);
            }
            return users;
        }

        public void GetTracks(string userName, int page, List<Track> tracks)
        {
            string method = "user.gettoptracks";
            int pages = 1;
            string uri = "http://" + baseAddress + "method=" + method + "&" + "user=" + userName + "&api_key=" + apiKey + "&format=json" + "&limit=1000" + "&page=" + page;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                TrackRootObject rootObj = new TrackRootObject();
                rootObj = JsonConvert.DeserializeObject<TrackRootObject>(json);
                pages = Int32.Parse(rootObj.toptracks.attr.totalPages);
                tracks.AddRange(rootObj.toptracks.track);
            }
            if (page != pages)
                this.GetTracks(userName, page + 1, tracks);
            //return tracks;
        }
        public List<string> GetTags(string artist)
        {
            string method = "artist.gettoptags";
            List<string> tags = new List<string>();
            //int pages = 1;
            string uri = "http://" + baseAddress + "method=" + method + "&" + "artist=" + artist.Replace(' ', '+') + "&api_key=" + apiKey + "&format=json" ;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                TagRootObject rootObj = new TagRootObject();
                rootObj = JsonConvert.DeserializeObject<TagRootObject>(json);
                tags.AddRange(rootObj.toptags.tag.Select(e => e.name));
            }
            return tags;
        }
    }

    public class User
    {
        public string name { get; set; }
        public int playcount { get; set; }
    }

    public class Attr
    {
        public string user { get; set; }
        public string page { get; set; }
        public string perPage { get; set; }
        public string totalPages { get; set; }
        public string total { get; set; }
    }

    public class Friends
    {
        public List<User> user { get; set; }
    }

    public class FriendsRootObject
    {
        public Friends friends { get; set; }
    }

    public class Artist
    {
        public string name { get; set; }
        //public string mbid { get; set; }
        //public string url { get; set; }
    }
    public class Track
    {
        public string name { get; set; }
        //public string duration { get; set; }
        public string playcount { get; set; }
        //public string mbid { get; set; }
        //public string url { get; set; }
        //public Streamable streamable { get; set; }
        public Artist artist { get; set; }
        //public List<Image> image { get; set; }
        //public Attr __invalid_name__@attr { get; set; }
    }
    public class Toptracks
    {
        public List<Track> track { get; set; }
        [JsonProperty("@attr")]
        public Attr attr { get; set; }
    }

    public class TrackRootObject
    {
        public Toptracks toptracks { get; set; }
    }

    public class Tag
    {
        //public int count { get; set; }
        public string name { get; set; }
        //public string url { get; set; }
    }

    //public class Attr
    //{
    //    public string artist { get; set; }
    //}

    public class Toptags
    {
        public List<Tag> tag { get; set; }
        //public Attr __invalid_name__@attr { get; set; }
    }

    public class TagRootObject
    {
        public Toptags toptags { get; set; }
    }
}
