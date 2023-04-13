using UnityEngine;

namespace Multiplayer.Cube.CubeObject
{
    [RequireComponent(typeof(CubeMovement), typeof(Rigidbody))]
    public class CubeIdentity : MonoBehaviour
    {
        [SerializeField] private CubeStats _stats;

        private CubeMovement _movement;

        private Rigidbody _cubeRigidbody;

        private int _id;

        public CubeStats Stats 
        {
            get 
            { 
                return _stats; 
            }
        }

        public CubeMovement Movement 
        {
            get
            { 
                return _movement;
            }
        }

        public Rigidbody CubeRigidbody 
        {
            get 
            {
                return _cubeRigidbody; 
            }
        }

        public int Id 
        {
            set
            { 
                _id = value; 
            }
            get 
            {
                return _id; 
            }
        }

        private void Awake()
        {
            if (_stats == null) { Debug.Log("Stats Require!"); }

            _movement = GetComponent<CubeMovement>();

            _cubeRigidbody = GetComponent<Rigidbody>();
        }
    }
}