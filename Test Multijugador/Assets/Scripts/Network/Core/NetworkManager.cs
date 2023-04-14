using System;
using System.Net;
using UnityEngine;
using System.Collections.Generic;

using Multiplayer.UI;
using Multiplayer.Utils;
using Multiplayer.Network.Enums;
using Multiplayer.Network.Structs;
using Multiplayer.Cube.CubesManager;
using Multiplayer.Network.Connections;

namespace Multiplayer.Network.Core
{
    public class NetworkManager : MonoBehaviourSingleton<NetworkManager>, IReceiveData
    {
        public int TimeOut = 30;

        public Action<byte[], IPEndPoint> OnReceiveEvent;

        private UdpConnection connection;

        private readonly Dictionary<int, Client> clients = new Dictionary<int, Client>();

        private readonly Dictionary<IPEndPoint, int> ipToId = new Dictionary<IPEndPoint, int>();

        NetHandShake netHandShake;

        private MessageTypeEnum _messageType;

        int clientId = 0; // This id should be generated during first handshake        

        public IPAddress Address
        {
            get; private set;
        }

        public int Port
        {
            get; private set;
        }

        public bool IsServer
        {
            get; private set;
        }

        void Awake()
        {
            netHandShake = new NetHandShake();
        }

        private void Update()
        {
            // Flush the data in main thread
            if (connection != null)
            {
                connection.FlushReceiveData();
            }
        }

        public void StartServer(int port)
        {
            IsServer = true;

            this.Port = port;
            
            connection = new UdpConnection(port, this);

            ChatScreen.Instance.HideInputMessage();
        }

        public void StartClient(IPAddress ip, int port)
        {
            IsServer = false;

            this.Port = port;

            this.Address = ip;

            connection = new UdpConnection(ip, port, this);

            netHandShake.Adress = BitConverter.ToInt64(ip.GetAddressBytes(), 0);
            
            netHandShake.Port = port;

            SendToServer(netHandShake.Serialize());
        }      

        public void OnReceiveData(byte[] data, IPEndPoint ip)
        {
            _messageType = GetMessageType(data);

            switch (_messageType)
            {
                case MessageTypeEnum.HandShake:

                    (long, int) auxData = netHandShake.Deserialize(data);

                    IPEndPoint auxIp = new IPEndPoint(auxData.Item1, auxData.Item2);
                    
                    AddClient(auxIp);

                    OnReceiveEvent?.Invoke(data, ip);

                    break;
                case MessageTypeEnum.Console:

                    break;

                case MessageTypeEnum.Position:

                    OnReceiveEvent?.Invoke(data, ip);

                    break;
                default:
                    break;
            }            
        }

        public void SendToServer(byte[] data)
        {
            connection.Send(data);
        }

        public void Broadcast(byte[] data)
        {
            using (var iterator = clients.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    connection.Send(data, iterator.Current.Value.ipEndPoint);
                }
            }
        }

        private void AddClient(IPEndPoint ip)
        {
            if (!ipToId.ContainsKey(ip))
            {
                Debug.Log("Adding client: " + ip.Address);

                int id = clientId;

                ipToId[ip] = clientId;

                clients.Add(clientId, new Client(ip, id, Time.realtimeSinceStartup));

                clientId++;

                CubeInstancesManager.Instance.InstantiateNewCube(false);                
            }            
        }

        private void AddClient(IPEndPoint ip, int idOfNewClient)
        {
            if (!ipToId.ContainsKey(ip))
            {
                Debug.Log("Adding client: " + ip.Address);

                int id = clientId;

                ipToId[ip] = clientId;

                clients.Add(clientId, new Client(ip, id, Time.realtimeSinceStartup));

                clientId++;

                if (clientId == idOfNewClient)
                {
                    CubeInstancesManager.Instance.InstantiateNewCube(true);
                }
                else
                {
                    CubeInstancesManager.Instance.InstantiateNewCube(false);
                }
            }
        }

        private void RemoveClient(IPEndPoint ip)
        {
            if (ipToId.ContainsKey(ip))
            {
                Debug.Log("Removing client: " + ip.Address);

                clients.Remove(ipToId[ip]);
            }
        }

        private MessageTypeEnum GetMessageType(byte[] data) 
        {
            int type = BitConverter.ToInt32(data, 0);

            return (MessageTypeEnum)type;
        }
    }
}