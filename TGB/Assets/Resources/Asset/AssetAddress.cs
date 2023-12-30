using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class AssetAddress : MonoBehaviour
{
#if UNITY_EDITOR

    [ContextMenu("CheckAsset")]
    public void CheckAsset()
    {
        string Assetpath = "Asset/";
        var allAssets = Resources.LoadAll(Assetpath);

        List<string> filePaths = new List<string>();

        foreach (Object asset in allAssets)
        {
            string path = AssetDatabase.GetAssetPath(asset);
            filePaths.Add(path);
        }


        foreach (var address in filePaths)
        {
            Debug.Log($"address = {address}");
            var resourcepath = System.IO.Path.GetFileName(address);
            var oldname = Path.GetFileNameWithoutExtension(address);
            var extension = Path.GetExtension(address);
            Debug.Log($"resourcepath = {resourcepath}");
            Debug.Log($"extension = {Path.GetExtension(address)}");
            CheckAddress(address, oldname, extension);
        }
    }

    public void CheckAddress(string address,string _oldName, string extension)
    {
        if (address.Contains("[") && address.Contains("]"))
        {
            var idx = _oldName.IndexOf("[");
            var newname = "";
            if ((idx + 1 >= _oldName.Length))
            {
                var splitName = _oldName.Split("[");
                newname = splitName[0] + "0" + extension;
            }
            else
            {
                var splitName = _oldName.Split("[");
                newname = splitName[0] + _oldName[idx + 1] + extension;
            }            
            Debug.Log($"newname = {newname}");
            var rename = AssetDatabase.RenameAsset(address, newname);
        }
    }
#endif
}
