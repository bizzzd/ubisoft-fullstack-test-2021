namespace AcmeGames.Domain
{
    public class GameKey
    {
        public string Key { get; set; }

        public uint GameId { get; set; }

        public bool IsRedeemed { get; set; }

        public virtual Game Game { get; set; }
    }
}
