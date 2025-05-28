using AutoMapper;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Application.Response.Wati;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Results.Wati;

namespace Mail.Engine.Service.Application.Mapper
{
    public class ConfigureMappingProfile : Profile
    {
        public ConfigureMappingProfile()
        {
            // Common
            // CreateMap<UpdateResponse, UpdateRecordResult>().ReverseMap();
            CreateMap<CreateResponse, CreateRecordResult>().ReverseMap();
            // CreateMap<DeleteResponse, DeleteRecordResult>().ReverseMap();

            CreateMap<MailResult, MailResponse>().ReverseMap();
            CreateMap<ModelResult, ModelResponse>().ReverseMap();
            CreateMap<ContactResult, ContactResponse>().ReverseMap();
            CreateMap<ParameterResult, ParameterResponse>().ReverseMap();
            CreateMap<WatiApiResult, WatiApiResponse>().ReverseMap();
        }
    }
}
