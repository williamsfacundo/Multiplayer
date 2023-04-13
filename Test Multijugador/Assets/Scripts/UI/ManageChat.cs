using UnityEngine;

namespace Multiplayer.UI
{
    public class ManageChat : MonoBehaviour
    {
        [SerializeField] private KeyCode OpenCloseChatKey;

        private void Update()
        {
            if (Input.GetKeyDown(OpenCloseChatKey)) 
            {
                if (!NetworkScreen.Instance.gameObject.activeSelf) 
                {
                    ChatScreen.Instance.gameObject.SetActive(!ChatScreen.Instance.gameObject.activeSelf);
                }
            }            
        }
    }
}