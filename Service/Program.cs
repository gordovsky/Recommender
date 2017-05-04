using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Service started...");

            ApiManager manager = new ApiManager();

            //var tags = manager.GetTags("All Them Witches");
            //foreach (var u in users)
            //{
            //    List<Track> tracks = new List<Track>();
            //    manager.GetTracks(u.name, 1, tracks);
            //    Console.WriteLine(u.name + " tracks received");
            //}
            Console.WriteLine("Service end");
        }
    }
}
