using AcmeGames.Application.Common.Mappings;
using AutoMapper;
using System;

namespace AcmeGames.Application.Queries.Ownership
{
    public class MyGameDto : IMapFrom<Domain.Ownership>
    {
        public uint GameId { get; set; }

        public uint OwnershipId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public uint? AgeRestriction { get; set; }

        public DateTime RegisteredDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Ownership, MyGameDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(o => o.Game.Name))
                .ForMember(d => d.AgeRestriction, opt => opt.MapFrom(o => o.Game.AgeRestriction))
                .ForMember(d => d.Thumbnail, opt => opt.MapFrom(o => o.Game.Thumbnail));
        }
    }
}
