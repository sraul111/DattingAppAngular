using System.Linq;
using AutoMapper;
using DattingApp.API.DTOs;
using DattingApp.API.Models;

namespace DattingApp.API.Helpers
{
    public class AutoMapperProfie:Profile
    {
        public AutoMapperProfie()
        {
           CreateMap<User, UserForListDto>()
           .ForMember(dest => dest.PhotoUrl, opt => 
                opt.MapFrom(src => src.Photos.FirstOrDefault( p => p.IsMain).Url)) 
           .ForMember(dest => dest.Age, opt => opt.MapFrom(src => 
                src.DateOfBirth.CalculateAge()));

           CreateMap<User,UserForDetailedDto>()
           .ForMember(dest => dest.PhotoUrl, opt =>
               opt.MapFrom(src => src.Photos.FirstOrDefault( p => p.IsMain).Url))
           .ForMember(dest => dest.Age, opt => opt.MapFrom(src => 
               src.DateOfBirth.CalculateAge()));

           CreateMap<Photo,PhotosForDetailedDto>();
        }
    }
}