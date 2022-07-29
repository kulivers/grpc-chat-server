using Chat;

namespace GrpcChatApp.Entities;

public interface IChatStorage
{
    IList<Message> Messages { get; set; }
    Message GetRandomMessage();
    void AddMessage(Message message);
    IOrderedEnumerable<Message> GetMessages();
}