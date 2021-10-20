using AutoMapper;

namespace jdobson_pairs.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Game, Models.Game>();
            CreateMap<Entities.Card, Models.Card>();

            CreateMap<Entities.GameCard, Models.GameCard>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Card.Name))
                .ForMember(
                    dest => dest.ImagePath,
                    opt => opt.MapFrom(src => src.Card.ImagePath));

            CreateMap<Entities.Game, Models.HistoryItem>()
                .ForMember(
                    dest => dest.NumCards,
                    opt => opt.MapFrom(src => src.Cards.Count));
        }
    }
}
