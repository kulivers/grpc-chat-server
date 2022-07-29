using Chat;
using Google.Protobuf.WellKnownTypes;

namespace GrpcChatApp.Entities;

class ChatStorage : IChatStorage
{    private int _lastId = 0;

    public IList<Message> Messages { get; set; }
    private static readonly string[] RandomMessages = new string[5] { "hey", "im here", "what u gona do", "sex?", "ping pong?" };
    public Message GetRandomMessage()
    {
        var i = new Random().Next(0, 4);
        _lastId++;
        return new Message()
        {
            Id = _lastId,
            Text = RandomMessages[i],
            Time = Timestamp.FromDateTime(DateTime.Now)
        };
        
    }
    public void AddMessage(Message message)
    {
        message.Id = message.Id == 0 ? _lastId : message.Id;  
        message.Time = Timestamp.FromDateTime(DateTime.Now);
        Messages.Add(message);
        _lastId++;
    }

    public IOrderedEnumerable<Message> GetMessages() => Messages.OrderBy(m=>m.Time);
}