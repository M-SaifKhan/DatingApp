using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
namespace API;

public class AutoMapperProfiles : Profile
{
public AutoMapperProfiles()
{
    //CreateMap<from, to>();
    CreateMap<AppUser, MemberDto>()
          .ForMember(x=>x.Age, o=>o.MapFrom(s=>s.DateOfBirth.CalculateAge()))
          .ForMember(d=>d.PhotoUrl, o => o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain)!.Url));
    CreateMap<Photo, PhotoDto>();
    CreateMap<MemberUpdateDto, AppUser>();
    // CreateMap<RegisterDto, AppUser>();
    // CreateMap<string, DateOnly>().ConvertUsing(s =>DateOnly.Parse(s));

    // General rule to map RegisterDto to AppUser
    CreateMap<RegisterDto, AppUser>();
    // Global rule for string to DateOnly conversion
    CreateMap<string, DateOnly>().ConvertUsing(src =>
            DateOnly.FromDateTime(DateTime.Parse(src))
        );

}
}