using UnityEngine;

namespace Multiplayer.Cube.CubeObject
{
    [RequireComponent(typeof(CubeIdentity))]
    public abstract class CubeComponentBase : MonoBehaviour
    {
        private CubeIdentity _identity;

        public CubeIdentity Identity 
        {
            get 
            { 
                return _identity; 
            }
        }

        void Awake()
        {
            _identity = GetComponent<CubeIdentity>();            
        }
    }
}