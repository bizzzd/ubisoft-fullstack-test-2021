using AcmeGames.Application.Common.Mappings;
using AutoMapper;

namespace AcmeGames.Application.Queries.User
{
    public class UserPersonalInfoDto : IMapFrom<Domain.User>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.User, UserPersonalInfoDto>()
                .ForMember(d => d.Email, opt => opt.MapFrom(u => u.EmailAddress));
        }
    }
}
