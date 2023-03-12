using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseSpot : MonoBehaviour {
    [SerializeField] protected GameData _gameData;
    [SerializeField] Transform _resourceProduceTransform;

    private ResourcePoolManager _resourcePoolManager;
    

    protected virtual void Awake() {
        _resourcePoolManager = FindObjectOfType<ResourcePoolManager>();
    }

    protected void Spawn(Resource toSpawn, int count, ResourcePhysicsData physicsData) {
        Resource[] resources = new Resource[count];
        for (int i = 0; i < count; i++) {
            var resource = _resourcePoolManager.GetResourceByType(toSpawn.ResourceType);
            resource.transform.parent = _resourceProduceTransform;
            resource.transform.position = _resourceProduceTransform.position;
            resource.FlyOut(physicsData);
            resources[i] = resource;
        }
        StartCoroutine(MakePickupable(resources));
    }

    private IEnumerator MakePickupable(Resource[] resource) {
        yield return new WaitForSeconds(_gameData.ResourceSettings.UnpickupableTime);
        foreach (var r in resource) {
            r.IsPickupable = true;
        }
    }
}
