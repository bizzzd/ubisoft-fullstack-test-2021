namespace AcmeGames.Domain
{
    public class Game
    {
        public uint GameId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public uint? AgeRestriction { get; set; }
    }
}
