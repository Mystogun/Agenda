using System.Collections.Generic;
using AutoMapper;
using LawAgendaApi.Data.Dtos;
using LawAgendaApi.Data.Dtos.Responses;
using LawAgendaApi.Data.Entities;

namespace LawAgendaApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User Maps

            CreateMap<RegisterUserDto, User>();

            CreateMap<User, UserToReturnDto>();

            CreateMap<A1UserType, UserTypeToReturnDto>();

            #endregion


            CreateMap<File, FileToReturnDto>();
            CreateMap<A1FileType, FileTypeToReturnDto>();


            CreateMap<Case, CaseToReturnDto>();
            CreateMap<CaseToCreateRequestDto, Case>();
            CreateMap<CaseTimeline, CaseTimelineToReturnDto>();
            CreateMap<CaseTimelineToCreateRequestDto, CaseTimeline>();


            CreateMap<A1CaseType, CaseTypeToReturnDto>();

            CreateMap<Customer, CustomerToReturnDto>();
            CreateMap<CustomerToCreateRequestDto, Customer>();
            CreateMap<CustomerToUpdateRequestDto, Customer>();
            

        }
    }
}