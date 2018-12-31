using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptableObjUtil {
    public void CreateNewScriptableObj(ScriptableObject obj, string objName, string path)
    {
        AssetDatabase.CreateAsset(obj, path + objName + ".asset");
        AssetDatabase.SaveAssets();
    }
}
