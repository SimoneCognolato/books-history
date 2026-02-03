using api.model.DTOs;
using api.model.Enums;
using AutoMapper;
using data.model.Entities;
using data.model.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books_history.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<BookCreationRequestDTO, Book>().ReverseMap();
            CreateMap<api.model.Enums.UpdatedFieldEnum, data.model.Enums.UpdatedFieldEnum>().ReverseMap();
            CreateMap<api.model.Enums.OrderingDirectionEnum, data.model.Enums.OrderingDirectionEnum>().ReverseMap();
        }
    }
}
