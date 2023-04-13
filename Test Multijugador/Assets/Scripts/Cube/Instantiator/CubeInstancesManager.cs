using UnityEngine;
using System.Collections.Generic;

using Multiplayer.Utils;
using Multiplayer.Cube.CubeObject;

namespace Multiplayer.Cube.CubesManager
{
    public class CubeInstancesManager : MonoBehaviourSingleton<CubeInstancesManager>
    {
        [SerializeField] private GameObject _cubePrefab;

        private List<GameObject> _cubes;

        private static int CubeCount = 0;

        private bool _isPrefabACube;

        void Awake()
        {
            _cubes = new List<GameObject>();

            _isPrefabACube = false;

            if (_cubePrefab != null) 
            {
                if (_cubePrefab.GetComponent<CubeIdentity>() != null) 
                {
                    _isPrefabACube = true;
                }
                else 
                {
                    Debug.Log("Cube prefab is not a valid cube with cube identity component!");
                }
            }
            else 
            {
                Debug.Log("Cube prefab is not selected!");
            }
        }

        public void InstantiateNewCube() 
        {
            if (_isPrefabACube) 
            {
                GameObject _aux = Instantiate(_cubePrefab, _cubePrefab.GetComponent<CubeStats>().InitialPosition * CubeCount, Quaternion.identity);

                _aux.GetComponent<CubeIdentity>().Id = CubeCount;

                CubeCount += 1;

                _cubes.Add(_aux);
                
            }
        }

        public void DestroyExistingCube(int cubeId) 
        {
            if (_cubes.Count > 0) 
            {
                GameObject _auxCube = GetCubeWithID(cubeId);

                if (_auxCube != null) 
                {
                    _cubes.Remove(_auxCube);

                    Destroy(_auxCube);
                }
                else 
                {
                    Debug.Log("Invalid cube id!");
                }
            }
        }

        private GameObject GetCubeWithID(int cubeId) 
        {
            for (int i = 0; i < _cubes.Count; i++) 
            {
                if (_cubes[i].GetComponent<CubeIdentity>().Id == cubeId) 
                {
                    return _cubes[i];
                }
            }

            return null;
        }
    }
}