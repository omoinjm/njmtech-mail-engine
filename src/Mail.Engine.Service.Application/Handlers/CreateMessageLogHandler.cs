using Mail.Engine.Service.Application.Commands;
using Mail.Engine.Service.Application.Dto;
using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class CreateMessageLogHandler(IMailRepository repository) : IRequestHandler<CreateCommand<MessageLogDto, CreateResponse>, CreateResponse>
    {
        private readonly IMailRepository _repository = repository;

        public async Task<CreateResponse> Handle(CreateCommand<MessageLogDto, CreateResponse> request, CancellationToken cancellationToken)
        {
            var result = await _repository.CreateMessageLogRecord(
                new MessageLogEntity
                {
                    MessageLogTypeId = request.Item.MessageLogTypeId,
                    FromName = request.Item.FromName,
                    ToField = request.Item?.ToField,
                    Subject = request.Item!.Subject,
                    Body = request.Item!.Body,
                });

            if (!result.IsSuccess) throw new Exception(result.ResponseMessage);

            return LazyMapper.Mapper.Map<CreateResponse>(result);
        }
    }
}
