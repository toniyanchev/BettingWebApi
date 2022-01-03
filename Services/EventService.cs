using AppContext = WebApi.Helpers.AppContext;
using System.Linq;
using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IEventService
    {
        dynamic GetDailyMatches();
        dynamic GetMatchById(int id);
        dynamic GetMatchesByType(int typeId);
    }

    public class EventService : IEventService
    {
        private readonly AppContext _context;

        public EventService(AppContext context)
        {
            _context = context;
        }

        public dynamic GetDailyMatches()
        {
            return _context.Matches
                .Where(x => x.Date > DateTime.Now.AddDays(-1))
                .ToList();
        }

        public dynamic GetMatchById(int id)
        {
            return _context.Matches
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public dynamic GetMatchesByType(int typeId)
        {
            return _context.Matches
                .Where(x => x.Type.Id == typeId)
                .ToList();
        }

        public dynamic ProcessXmlData()
        {
            var ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;

            var xml = DownloadString(@"https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7");
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
            File.WriteAllText("test.xml", xml);

            path = Directory.GetCurrentDirectory() + @"\test.xml";
            xmlInputData = File.ReadAllText(path);

            XmlSports xmlSports = ser.Deserialize<XmlSports>(xmlInputData);

            //DB UPDATES

            return null;
        }

        private void HandleSports(SportProxy[] sports)
        {
            Sport obj = new Sport(sports.FirstOrDefault());
            foreach(var sport in sports)
            {
                
            }
        }

        private string DownloadString(string address)
        {
            string text;
            using (var client = new WebClient())
            {
                text = client.DownloadString(address);
            }
            return text;
        }
    }
}