using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcChatApp.Entities;

namespace GrpcChatApp.GrpcServices;

using Chat;

public class GrpcChatService : Chat.ChatBase
{
    private readonly IChatManagerService _chatManagerService;

    public GrpcChatService(IChatManagerService chatManagerService)
    {
        _chatManagerService = chatManagerService;
    }

    public override Task<MessagesList> GetMessages(Empty request, ServerCallContext context) =>
        Task.FromResult(new MessagesList() { Messages = { _chatManagerService.Storage.Messages } });

    public override Task<Empty> SendMessage(Message request, ServerCallContext context)
    {
        _chatManagerService.Storage.AddMessage(request);
        return Task.FromResult(new Empty());
    }

    public override Task StartAnswersStream(Empty request, IServerStreamWriter<Message> responseStream,
        ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested && _chatManagerService.IsStreaming)
        {
            var randMessage = _chatManagerService.Storage.GetRandomMessage();
            responseStream.WriteAsync(randMessage);
        }
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> StopStreaming(Empty request, ServerCallContext context)
    {
        _chatManagerService.IsStreaming = true;
        return Task.FromResult(new Empty());
    }
}