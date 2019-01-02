using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptableObjUtil {
    public void CreateNewScriptableObj(ScriptableObject obj, string objName, string path)
    {
        //Prevents file names being blank
        if (objName == "" | objName == null)
        {
            Debug.LogError("Invalid Object Name");
        }
        else
        {
            AssetDatabase.CreateAsset(obj, path + objName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
