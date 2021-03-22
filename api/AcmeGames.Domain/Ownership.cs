using System;

namespace AcmeGames.Domain
{
    public class Ownership
    {
        public uint OwnershipId { get; set; }

        public uint GameId { get; set; }

        public DateTime RegisteredDate { get; set; }

        public OwnershipState State { get; set; }

        public string UserAccountId { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
