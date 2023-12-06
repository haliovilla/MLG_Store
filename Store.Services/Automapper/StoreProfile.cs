using AutoMapper;
using MLGStore.Entities;
using MLGStore.Services.DTOs;

namespace MLGStore.Services.Automapper
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<CreateStoreDTO, Store>();
            CreateMap<Store, StoreDTO>();
        }
    }
}
