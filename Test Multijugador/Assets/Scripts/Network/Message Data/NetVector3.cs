using System;
using UnityEngine;
using System.Collections.Generic;

using Multiplayer.Network.Interfaces;
using Multiplayer.Network.Enums;

namespace Multiplayer.MessageData 
{
    public class NetVector3 : IMessage<Vector3>
    {
        Vector3 _data;

        public Vector3 Data 
        {
            set 
            {
                _data = value;
            }
        }

        public NetVector3(Vector3 data)
        {
            _data = data;
        }

        public byte[] Serialize() 
        {
            List<byte> data = new List<byte>();

            data.AddRange(BitConverter.GetBytes((int)GetMessageType()));

            data.AddRange(BitConverter.GetBytes(_data.x));
            data.AddRange(BitConverter.GetBytes(_data.y));
            data.AddRange(BitConverter.GetBytes(_data.z));

            return data.ToArray();
        }

        public Vector3 Deserialize(byte[] message)
        {
            Vector3 data = new Vector3();
            
            data.x = BitConverter.ToSingle(message, 4);
            data.y = BitConverter.ToSingle(message, 8);
            data.z = BitConverter.ToSingle(message, 12);

            return data;
        }

        public MessageTypeEnum GetMessageType() 
        {
            return MessageTypeEnum.Vec3;
        }
    }
}