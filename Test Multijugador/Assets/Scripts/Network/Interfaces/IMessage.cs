using Multiplayer.Network.Enums;

namespace Multiplayer.Network.Interfaces
{
    public interface IMessage<T> 
    {
        public byte[] Serialize();
    
        public T Deserialize(byte[] message);
    
        public MessageTypeEnum GetMessageType();
    }
}