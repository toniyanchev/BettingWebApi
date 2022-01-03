using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

            _ = HandleSportsAsync(xmlSports.Sports);

            return null;
        }

        private async Task HandleSportsAsync(SportProxy[] sports)
        {
            foreach(var sport in sports)
            {
                var obj = new Sport(sport);
                var doesExist = await _context.Sports.AnyAsync(x => x.OldId == sport.Id);
                if (!doesExist)
                {
                    _context.Sports.Add(obj);
                }
                else
                {
                    obj = await _context.Sports.FirstAsync(x => x.OldId == sport.Id);
                    obj.Name = sport.Name;

                    _context.SaveChanges();
                }

                if (sport.Events != null)
                {
                    _ = HandleEventsAsync(sport.Events, obj);
                }
            }
            _context.SaveChanges();
        }

        private async Task HandleEventsAsync(EventProxy[] events, Sport parent)
        {
            foreach(var ev in events)
            {
                var obj = new Event(ev);
                obj.Sport = parent;
                var doesExist = await _context.Events.AnyAsync(x => x.OldId == ev.Id);
                if (!doesExist)
                {
                    _context.Events.Add(obj);
                }
                else
                {
                    obj = _context.Events.First(x => x.OldId == ev.Id);
                    obj.Name = ev.Name;
                    obj.IsLive = ev.IsLive;

                    _context.SaveChanges();
                }

                if (ev.Matches != null)
                {
                    _ = HandleMatchesAsync(ev.Matches, obj);
                }
            }
            _context.SaveChanges();
        }

        private async Task HandleMatchesAsync(MatchProxy[] matches, Event parent)
        {
            foreach (var match in matches)
            {
                var obj = new Match(match);
                obj.Event = parent;
                obj.Type = await _context.MatchTypes.SingleOrDefaultAsync(x => x.Id == obj.GetTypeId(match.Type));
                var doesExist = await _context.Matches.AnyAsync(x => x.OldId == match.Id);
                if (!doesExist)
                {
                    _context.Matches.Add(obj);
                }
                else
                {
                    obj = _context.Matches.First(x => x.OldId == match.Id);
                    obj.Type = _context.MatchTypes.SingleOrDefault(x => x.Id == obj.GetTypeId(match.Type));
                    obj.Name = match.Name;

                    _context.SaveChanges();
                }

                if (match.Bets != null)
                {
                    _ = HandleBetsAsync(match.Bets, obj);
                }
            }
            _context.SaveChanges();
        }

        private async Task HandleBetsAsync(BetProxy[] bets, Match parent)
        {
            foreach (var bet in bets)
            {
                var obj = new Bet(bet);
                obj.Match = parent;
                var doesExist = await _context.Bets.AnyAsync(x => x.OldId == bet.Id);
                if (!doesExist)
                {
                    _context.Bets.Add(obj);
                }
                else
                {
                    obj = await _context.Bets.FirstAsync(x => x.OldId == bet.Id);
                    obj.Name = bet.Name;
                    obj.IsLive = bet.IsLive;

                    _context.SaveChanges();
                }

                if (bet.Odds != null)
                {
                    _ = HandleOddsAsync(bet.Odds, obj);
                }
            }
            _context.SaveChanges();
        }

        private async Task HandleOddsAsync(OddProxy[] odds, Bet parent)
        {
            foreach (var odd in odds)
            {
                var obj = new Odd(odd);
                obj.Bet = parent;
                var doesExist = await _context.Odds.AnyAsync(x => x.OldId == odd.Id);
                if (!doesExist)
                {
                    _context.Odds.Add(obj);
                }
                else
                {
                    obj = await _context.Odds.FirstAsync(x => x.OldId == odd.Id);
                    obj.Name = odd.Name;
                    obj.Value = odd.Value;
                    obj.SpecialValue = odd.SpecialValue;

                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();
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