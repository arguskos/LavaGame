using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using ResourcePool = UnityEngine.Pool.ObjectPool<Resource>;

public class ResourcePoolManager : MonoBehaviour {
    [SerializeField] ResourcePrefabs _prefabs;
    private Dictionary<ResourceType, ResourcePool> _typeToPool = new Dictionary<ResourceType, ResourcePool>();

    private void Start() {
        var values = Enum.GetValues(typeof(ResourceType));
        foreach (ResourceType type in values) {
            var prefab = _prefabs.GetPrefabByType(type);
            _typeToPool.Add(type, new ResourcePool(() => CreateFunc(prefab),
                OnGet, OnReturn));
        }
    }

    public void ReturnToPool(Resource resource) {
        _typeToPool[resource.ResourceType].Release(resource);
    }

    public Resource GetResourceByType(ResourceType type) {
        return _typeToPool[type].Get();
    }

    private Resource CreateFunc(Resource prefab) {
        var resource = Instantiate(prefab);
        return resource;
    }

    private void OnGet(Resource resource) {
        resource.gameObject.SetActive(true);
    }

    private void OnReturn(Resource resource) {
        resource.gameObject.SetActive(false);
        resource.SetPhysicsState(false);
    }
}
