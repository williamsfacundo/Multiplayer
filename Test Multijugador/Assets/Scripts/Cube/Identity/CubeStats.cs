using UnityEngine;

namespace Multiplayer.Cube.CubeObject
{
    [CreateAssetMenu(fileName = "Cube", menuName = "ScriptableObjects/CubeStats", order = 1)]
    public class CubeStats : ScriptableObject
    {
        public Vector3 InitialPosition;

        public float Velocity;

        public string HorizontalAxis;

        public string VerticalAxis;
    }
}