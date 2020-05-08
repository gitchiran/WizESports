namespace DataAccessLayer.Entities
{
    public partial class TeamTournamentGroup
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public int? GroupId { get; set; }

        public TournamentGroup Group { get; set; }
        public Team Team { get; set; }
    }
}
