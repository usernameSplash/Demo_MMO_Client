using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor : MonoBehaviour
{

#if UNITY_EDITOR

    [MenuItem("Tools/GenerateMap %#g")]
    private static void HelloWorld()
    {
        if (EditorUtility.DisplayDialog("Hello World", "Create?", "Create", "Cancel"))
        {
            new GameObject("Hello World");
        }
    }


#endif

}
