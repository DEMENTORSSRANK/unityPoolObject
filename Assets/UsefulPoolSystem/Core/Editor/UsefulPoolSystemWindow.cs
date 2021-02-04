using UnityEditor;
using UnityEngine;
using UsefulPoolSystem.Core.Scripts;

namespace UsefulPoolSystem.Core.Editor
{
    public class UsefulPoolSystemWindow : EditorWindow
    {
        [MenuItem("UPS/Create pool system")]
        private static void ShowWindow()
        {
            if (FindObjectOfType<UsefulPoolObjectSystem>())
                return;

            var newItem = new GameObject {name = "Unity Pool System"};


            newItem.AddComponent<UsefulPoolObjectSystem>();
        }
    }
}