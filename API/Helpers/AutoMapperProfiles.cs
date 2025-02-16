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
    CreateMap<Message, MessageDto>()
         .ForMember(d=>d.SenderPhotoUrl, 
             o=>o.MapFrom(s=>s.Sender.Photos.FirstOrDefault(x=>x.IsMain)!.Url))
         .ForMember(d=>d.RecipientPhotoUrl, 
             o=>o.MapFrom(s=>s.Recipient.Photos.FirstOrDefault(x=>x.IsMain)!.Url));    
    CreateMap<DateTime, DateTime>().ConvertUsing(d=>DateTime.SpecifyKind(d, DateTimeKind.Utc)); //Convertion for required DateTime        
    CreateMap<DateTime?, DateTime?>().ConvertUsing(d=>d.HasValue? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc):null); //Convertion for nullable DateTime        

}
}