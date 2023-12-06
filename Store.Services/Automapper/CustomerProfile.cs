using AutoMapper;
using MLGStore.Entities;
using MLGStore.Services.DTOs;

namespace MLGStore.Services.Automapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDTO, Customer>()
                .ForMember(e => e.Password, opt => opt.Ignore());

            CreateMap<UpdateCustomerDTO, Customer>();

            CreateMap<Customer, CustomerDTO>();
        }
    }
}