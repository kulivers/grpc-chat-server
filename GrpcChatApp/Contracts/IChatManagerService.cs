namespace GrpcChatApp.Entities;

public interface IChatManagerService
{
    bool IsStreaming { get; set; }
    IChatStorage Storage { get; }
}