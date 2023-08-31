using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    // AutoMapperProfiles class defines mapping profiles for AutoMapper.

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Configure mapping from AppUser to MemberDto.
            CreateMap<AppUser, MemberDto>()
                // Configure the mapping for the 'PhotoUrl' property.
                // The 'ForMember' method is used to customize the mapping of 'PhotoUrl' property.
                .ForMember(dest => dest.PhotoUrl,
                    opt => opt.MapFrom(
                        src => src.Photos.FirstOrDefault(
                            x => x.IsMain
                        ).Url
                    ))

                // Configure the mapping for the 'Age' property.
                // The 'ForMember' method is used to customize the mapping of 'Age' property.
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(
                        src => src.DateOfBirth.CalculateAge()
                    ));

            // Configure mapping from Photo to PhotoDto.
            CreateMap<Photo, PhotoDto>();

            // Configure mapping from MemberUpdateDto to AppUser.
            CreateMap<MemberUpdateDto, AppUser>();

        }
    }

}