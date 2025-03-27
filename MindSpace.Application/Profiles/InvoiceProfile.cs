using AutoMapper;
using MindSpace.Application.DTOs.Invoices;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile() 
        {
            CreateMap<Invoice, InvoiceDTO>()
            .ForMember(d => d.AccountName, opt => opt.MapFrom(i => i.Account.FullName));
        }
    }
}
