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

    public List<type> GetScriptableObjs<type>(string folder) where type : ScriptableObject
    {
        //Gets the GUID for assets in the folder
        string[] assetGUID = AssetDatabase.FindAssets("", new string[] { folder });

        //Creates a new array of of the type
        List<type> objects = new List<type>();

        for (int i = 0; i < assetGUID.Length; i++)
        {
            //Loads the object using its path
            objects.Add(AssetDatabase.LoadAssetAtPath<type>(AssetDatabase.GUIDToAssetPath(assetGUID[i])));
        }
        
        return objects;
    }
}
