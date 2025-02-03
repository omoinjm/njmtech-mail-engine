using AutoMapper;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Application.Response.General;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results;

namespace Mail.Engine.Service.Application.Mapper
{
    public class ConfigureMappingProfile : Profile
    {
        public ConfigureMappingProfile()
        {
            // Common
            CreateMap<UpdateResponse, UpdateRecordResult>().ReverseMap();
            CreateMap<CreateResponse, CreateRecordResult>().ReverseMap();
            CreateMap<DeleteResponse, DeleteRecordResult>().ReverseMap();

            // Mail
            CreateMap<MailboxEntity, MailboxResponse>().ReverseMap();
            CreateMap<MessageEntity, MessageResponse>().ReverseMap();

        }
    }
}