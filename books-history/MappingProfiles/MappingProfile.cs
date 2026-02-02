using api.model.DTOs;
using AutoMapper;
using data.model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books_history.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<BookDTO, Book>().ReverseMap();
        }
    }
}
