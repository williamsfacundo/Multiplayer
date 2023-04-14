using UnityEngine;

namespace Multiplayer.Cube.CubeObject
{
    public class CubeMovement : CubeComponentBase
    {
        private float _horizontalInput;

        private float _verticalInput;

        private Vector3 _direction;
        
        void Start()
        {
            _direction = Vector3.zero;            
        }

        void Update()
        {
            CalculateMoveDirection();
        }

        void FixedUpdate()
        {            
            Move();       
        }

        private void Move() 
        {
            _direction.x = _horizontalInput;
            
            _direction.z = _verticalInput;

            Identity.CubeRigidbody.MovePosition(transform.position + _direction * Identity.Stats.Velocity * Time.deltaTime);                                                                                   
        }

        private void CalculateMoveDirection() 
        {
            _horizontalInput = Input.GetAxisRaw(Identity.Stats.HorizontalAxis);
            
            _verticalInput = Input.GetAxisRaw(Identity.Stats.VerticalAxis);
        }
    }
}