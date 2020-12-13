using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPoolObject.Core;

namespace UnityPoolObject.Example.Scripts
{
    public class ExampleUseUnityPoolObject : MonoBehaviour
    {
        [SerializeField] private ExampleUnityPoolObject testPrefab;
        
        [Space(5)] [SerializeField] private ExampleUnityPoolObject toRemoveObject;

        [ContextMenu("Print all objects")]
        private void DebugPrintAll()
        {
            
        }
        
        [ContextMenu("Remove selected")]
        private void RemoveSelected()
        {
            toRemoveObject.Push();
        }
        
        [ContextMenu("Create")]
        private void CreateNew()
        {
            UnityPoolManager.Instance.PopOrCreate(testPrefab);
        }
        
        private void Start()
        {
            CreateNew();
        }
    }
}
