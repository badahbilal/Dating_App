using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
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
                .ForMember(dest => dest.PhotoUrl,
                    opt => opt.MapFrom(
                        src => src.Photos.FirstOrDefault(
                            x => x.IsMain
                        ).Url
                    ));

            // Configure mapping from Photo to PhotoDto.
            CreateMap<Photo, PhotoDto>();
        }
    }

}