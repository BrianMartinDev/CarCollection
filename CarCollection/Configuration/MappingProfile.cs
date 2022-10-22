using AutoMapper;
using BugTracker.Data;
using CarCollection.Data;
using CarCollection.ViewModels;

namespace CarCollection.Configuration
    {
    public class MappingProfile : Profile
        {
        public MappingProfile()
            {
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            }
        }
    }
