using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ResourceAssets {
    public Material Material;
    public Resource Prefab;
}

[CreateAssetMenu(menuName = "LavaGame/ResourcePrefabs")]
public class ResourcePrefabs : ScriptableObject {
    public List<ResourceAssets> Assets = new List<ResourceAssets>();

    public ResourceAssets GetAssetsByType(ResourceType type) {
       var assets = Assets.Find((other) => other.Prefab.ResourceType == type);
        Debug.Assert(assets != null, $"No resource asset of type {type}");
        return assets;
    }

    public Resource GetPrefabByType(ResourceType type) {
        var assets = GetAssetsByType(type);
        return assets.Prefab;
    }
}