using Ballpark.Data;
using Ballpark.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Services
{
    public class EventService
    {
        private readonly Guid _userId;
        private ApplicationDbContext _database = new ApplicationDbContext();

        public EventService(Guid userID)
        {
            _userId = userID;
        }

        public bool CreateEvent(EventCreate model)     //creates the instance of Event
        {
            var entity =
                new Event()
                {
                    OwnerID = _userId,
                    DateOfGame = model.DateOfGame,
                    ProfileID = model.ProfileID,
                    HomeID = model.HomeTeamID,
                    AwayID = model.AwayTeamID,
                    Result = model.Result,
                    Comments = model.Comments
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Events.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EventListItem> GetEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Events
                    .Where( e => e.OwnerID == _userId)
                    .Select(
                        e =>
                        new EventListItem
                        {
                            DateOfGame = e.DateOfGame,
                            EventID = e.EventID,
                            VenueName = e.HomeTeam.Venue.VenueName,
                            HomeTeam = e.HomeTeam.TeamName,
                            AwayTeam = e.AwayTeam.TeamName,
                            Result = e.Result
                        }
                        );

                var eventList = query.ToArray();
                var orderedEventList = eventList.OrderBy(e => e.DateOfGame);
                
                return orderedEventList;
            }
        }

        public HashSet<string> GetVenueHashSet()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Events
                    .Where(e => e.OwnerID == _userId)
                    .Select(
                        e =>
                        new EventListItem
                        {
                            DateOfGame = e.DateOfGame,
                            EventID = e.EventID,
                            VenueName = e.HomeTeam.Venue.VenueName,
                            HomeTeam = e.HomeTeam.TeamName,
                            AwayTeam = e.AwayTeam.TeamName,
                            Result = e.Result
                        }
                        );

                var eventList = query.ToList();
                List<string> ListOfVenueNames = new List<string>();
                foreach (var venueName in eventList)
                {
                    ListOfVenueNames.Add(venueName.VenueName);
                }
                HashSet<string> HashSetOfVenueNames = new HashSet<string>(ListOfVenueNames);
                return HashSetOfVenueNames;
            }

        }

        public EventDetail GetEventByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Events
                    .Single(e => e.EventID == id && e.OwnerID == _userId);
                return
                    new EventDetail
                    {
                        EventID = entity.EventID,
                        DateOfGame = entity.DateOfGame,
                        VenueName = entity.HomeTeam.Venue.VenueName,
                        HomeTeam = entity.HomeTeam.TeamName,
                        AwayTeam = entity.AwayTeam.TeamName,
                        Result = entity.Result,
                        Comments = entity.Comments
                    };
            }
        }

        public IEnumerable<EventDetail> GetEventByVenueName(string venueName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Events
                    .Where(e => e.HomeTeam.Venue.VenueName == venueName)
                    .Select(
                        e =>
                    new EventDetail
                    {
                        EventID = e.EventID,
                        DateOfGame = e.DateOfGame,
                        VenueName = e.HomeTeam.Venue.VenueName,
                        HomeTeam = e.HomeTeam.TeamName,
                        AwayTeam = e.AwayTeam.TeamName,
                        Result = e.Result,
                        Comments = e.Comments
                    }
                    );
                var eventList = query.ToArray();
                var orderedEventList = eventList.OrderBy(e => e.DateOfGame);
                return orderedEventList;
            }
        }

        public bool UpdateEvent(EventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Events
                    .Single(e => e.EventID == model.EventID && e.OwnerID == _userId);

                entity.DateOfGame = model.DateOfGame;
                entity.HomeID = model.HomeTeamID;
                entity.AwayID = model.AwayTeamID;
                entity.Result = model.Result;
                entity.Comments = model.Comments;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEvent(int eventID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Events
                    .Single(e => e.EventID == eventID);

                ctx.Events.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
