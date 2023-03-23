using System.Net;

namespace Multiplayer.Network.Structs 
{
    public struct Client
    {
        public float timeStamp;
        public int id;
        public IPEndPoint ipEndPoint;

        public Client(IPEndPoint ipEndPoint, int id, float timeStamp)
        {
            this.timeStamp = timeStamp;
            this.id = id;
            this.ipEndPoint = ipEndPoint;
        }
    }
}