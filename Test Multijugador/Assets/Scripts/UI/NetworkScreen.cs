﻿using System.Net;
using UnityEngine.UI;

using Multiplayer.Utils;
using Multiplayer.Network.Core;

namespace Multiplayer.UI
{
    public class NetworkScreen : MonoBehaviourSingleton<NetworkScreen>
    {
        public Button connectBtn;

        public Button startServerBtn;

        public InputField portInputField;

        public InputField addressInputField;

        protected override void Initialize()
        {
            connectBtn.onClick.AddListener(OnConnectBtnClick);

            startServerBtn.onClick.AddListener(OnStartServerBtnClick);
        }

        private void OnConnectBtnClick()
        {
            IPAddress ipAddress = IPAddress.Parse(addressInputField.text);

            int port = System.Convert.ToInt32(portInputField.text);

            NetworkManager.Instance.StartClient(ipAddress, port);
        
            SwitchToChatScreen();
        }

        private void OnStartServerBtnClick()
        {
            int port = System.Convert.ToInt32(portInputField.text);

            NetworkManager.Instance.StartServer(port);

            SwitchToChatScreen();
        }

        private void SwitchToChatScreen()
        {
            ChatScreen.Instance.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}