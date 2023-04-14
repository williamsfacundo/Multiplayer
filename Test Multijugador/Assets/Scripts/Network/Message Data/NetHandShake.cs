using System;
using System.Collections.Generic;

using Multiplayer.Network.Enums;
using Multiplayer.Network.Interfaces;

public class NetHandShake : IMessage<(long, int)>
{
    private (long, int) _data;

    public long Adress 
    {
        set 
        {
            _data.Item1 = value;
        }
    }

    public long Port
    {
        set
        {
            _data.Item1 = value;
        }
    }

    public (long, int) Data 
    {        
        get 
        { 
            return _data;
        }
    }

    public NetHandShake()
    {
        _data.Item1 = 0;
        _data.Item2 = 0;
    }

    public NetHandShake((long, int) data) 
    {
        _data = data;
    }

    public byte[] Serialize()
    {
        List<byte> outData = new List<byte>();

        outData.AddRange(BitConverter.GetBytes((int)GetMessageType()));

        outData.AddRange(BitConverter.GetBytes(_data.Item1));
        outData.AddRange(BitConverter.GetBytes(_data.Item2));

        return outData.ToArray();
    }

    public (long, int) Deserialize(byte[] message)
    {
        (long, int) outData;

        outData.Item1 = BitConverter.ToInt64(message, 4);
        outData.Item2 = BitConverter.ToInt32(message, 12);

        return outData;
    }

    public MessageTypeEnum GetMessageType()
    {
        return MessageTypeEnum.HandShake;
    }    
}