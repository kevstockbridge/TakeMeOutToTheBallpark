using Ballpark.Data;
using Ballpark.Models.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Services
{
    public class TeamService
    {
        public TeamService()
        {

        }

        public bool CreateTeam(TeamCreate model)        //creates instance of Team
        {
            var entity =
                new Team()
                {
                    TeamName = model.TeamName,
                    VenueID = model.VenueID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams() //see all Teams 
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Teams
                    .Select(
                        e =>
                        new TeamListItem
                        {
                            TeamID = e.TeamID,
                            TeamName = e.TeamName,
                            VenueID = e.VenueID,
                            VenueName = e.Venue.VenueName
                        }
                        );

                var teamList = query.ToArray();
                var orderedTeamList = teamList.OrderBy(e => e.TeamName);
                return orderedTeamList;
            }
        }

        public TeamDetail GetTeamByID (int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Teams
                    .Single(e => e.TeamID == id);
                return
                    new TeamDetail
                    {
                        TeamID = entity.TeamID,
                        TeamName = entity.TeamName,
                        VenueName = entity.Venue.VenueName
                    };
            }
        }

        public bool UpdateTeam(TeamEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Teams
                    .Single(e => e.TeamID == model.TeamID);

                entity.TeamName = model.TeamName;
                entity.VenueID = model.VenueID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int teamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Teams
                    .Single(e => e.TeamID == teamID);

                ctx.Teams.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public List<string> GetTeamNames()     //create List of Team Names
        {
            List<string> TeamNames = new List<string>();

            IEnumerable<TeamListItem> ListOfTeams = GetTeams();

            foreach (TeamListItem team in ListOfTeams)
            {
                TeamNames.Add(team.TeamName);
            }
            return TeamNames;
        }
    }
}
