using AutoMapper;
using BugTracker.Data;
using CarCollection.Data;
using CarCollection.ViewModels.AppUser;
using CarCollection.ViewModels.Comment;
using CarCollection.ViewModels.Vehicle;

namespace CarCollection.Configuration
    {
    public class MappingProfile : Profile
        {
        public MappingProfile()
            {

            // Vehicle Model Mapping
            CreateMap<Vehicle, BaseVehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, GetVehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, UpdateVehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, CreateVehicleViewModel>().ReverseMap();

            // Comment Model Mapping
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Comment, GetCommentViewModel>().ReverseMap();
            CreateMap<Comment, BaseCommentViewModel>().ReverseMap();
            CreateMap<Comment, CreateCommentViewModel>().ReverseMap();
            CreateMap<Comment, UpdateCommentViewModel>().ReverseMap();

            // AppUser Model Mapping
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            }
        }
    }
