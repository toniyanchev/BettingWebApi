using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using AppContext = WebApi.Helpers.AppContext;

namespace WebApi.Services
{
    public interface IEventService
    {
        dynamic GetDailyMatches();
        dynamic GetMatchById(int id);
        dynamic GetMatchesByType(int typeId);
        dynamic ProcessXmlData();
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

            var xml = DownloadString(@"https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7");
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
            File.WriteAllText("test.xml", xml);

            var path = Directory.GetCurrentDirectory() + @"\test.xml";
            var xmlInputData = File.ReadAllText(path);

            XmlSports xmlSports = ser.Deserialize<XmlSports>(xmlInputData);

            HandleSports(xmlSports.Sports);

            return null;
        }

        private void HandleSports(SportProxy[] sports)
        {
            foreach(var sport in sports)
            {
                var obj = new Sport(sport);
                if (!_context.Sports.Any(x => x.OldId == sport.Id))
                {
                    _context.Sports.Add(obj);
                    _context.SaveChanges();
                }
                else
                {
                    obj = _context.Sports.First(x => x.Id == sport.Id);
                    obj.Name = sport.Name;

                    _context.SaveChanges();
                }

                HandleEvents(sport.Events, obj);
            }
        }

        private void HandleEvents(EventProxy[] events, Sport parent)
        {
            foreach(var ev in events)
            {
                var obj = new Event(ev);
                obj.Sport = parent;
                if (!_context.Events.Any(x => x.OldId == ev.Id))
                {
                    _context.Events.Add(obj);
                    _context.SaveChanges();
                }
                else
                {
                    obj = _context.Events.First(x => x.OldId == ev.Id);
                    obj.Name = ev.Name;
                    obj.IsLive = ev.IsLive;

                    _context.SaveChanges();
                }

                HandleMatches(ev.Matches, obj);
            }
        }

        private void HandleMatches(MatchProxy[] matches, Event parent)
        {
            foreach (var match in matches)
            {
                var obj = new Match(match);
                obj.Event = parent;
                obj.Type = _context.MatchTypes.SingleOrDefault(x => x.Id == obj.GetTypeId(match.Type));

                if (!_context.Matches.Any(x => x.OldId == match.Id))
                {
                    _context.Matches.Add(obj);
                    _context.SaveChanges();
                }
                else
                {
                    obj = _context.Matches.First(x => x.Id == match.Id);
                    obj.Type = _context.MatchTypes.SingleOrDefault(x => x.Id == obj.GetTypeId(match.Type));
                    obj.Name = match.Name;

                    _context.SaveChanges();
                }

                HandleBets(match.Bets, obj);
            }
        }

        private void HandleBets(BetProxy[] bets, Match parent)
        {
            foreach (var bet in bets)
            {
                var obj = new Bet(bet);
                obj.Match = parent;

                if (!_context.Bets.Any(x => x.OldId == bet.Id))
                {
                    _context.Bets.Add(obj);
                    _context.SaveChanges();
                }
                else
                {
                    obj = _context.Bets.First(x => x.Id == bet.Id);
                    obj.Name = bet.Name;
                    obj.IsLive = bet.IsLive;

                    _context.SaveChanges();
                }

                HandleOdds(bet.Odds, obj);
            }
        }

        private void HandleOdds(OddProxy[] odds, Bet parent)
        {
            foreach (var odd in odds)
            {
                var obj = new Odd(odd);
                obj.Bet = parent;

                if (!_context.Odds.Any(x => x.OldId == odd.Id))
                {
                    _context.Odds.Add(obj);
                    _context.SaveChanges();
                }
                else
                {
                    obj = _context.Odds.First(x => x.Id == odd.Id);
                    obj.Name = odd.Name;
                    obj.Value = odd.Value;
                    obj.SpecialValue = odd.SpecialValue;

                    _context.SaveChanges();
                }
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