using System;

using Multiplayer.Network.Enums;
using Multiplayer.Network.Interfaces;
using System.Collections.Generic;

namespace Multiplayer.MessageData 
{
    public class NetInt : IMessage<int>
    {
        int _data;

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();

            data.AddRange(BitConverter.GetBytes((int)GetMessageType()));

            data.AddRange(BitConverter.GetBytes(_data));

            return data.ToArray();
        }
        
        public int Deserialize(byte[] message)
        {
            int data;

            data = BitConverter.ToInt32(message, 4);

            return _data;
        }

        public MessageTypeEnum GetMessageType()
        {
            return MessageTypeEnum.Index;
        }
    }
}