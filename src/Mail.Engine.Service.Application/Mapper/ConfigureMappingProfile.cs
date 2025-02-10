using AutoMapper;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Results;

namespace Mail.Engine.Service.Application.Mapper
{
    public class ConfigureMappingProfile : Profile
    {
        public ConfigureMappingProfile()
        {
            CreateMap<MailResult, MailResponse>().ReverseMap();
        }
    }
}