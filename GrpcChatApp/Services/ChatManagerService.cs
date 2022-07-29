namespace GrpcChatApp.Entities;

public class ChatManagerService : IChatManagerService
{
    public bool IsStreaming { get; set; }
    public IChatStorage Storage { get; }
    public ChatManagerService()
    {
        Storage = new ChatStorage();
        IsStreaming = false;
    }
    

}