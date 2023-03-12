using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;


public class ResourceHolder : MonoBehaviour {
    private Dictionary<ResourceType, int> _resourceToCount = new Dictionary<ResourceType, int>();
    public UnityEvent<ResourceType> HolderUpdatedEvent;

    public IReadOnlyDictionary<ResourceType, int> GetResources() {
        return _resourceToCount;
    }

    public int GetResourceCount(ResourceType resource) {
        int resourceCount = 0;
        _resourceToCount.TryGetValue(resource, out resourceCount);
        return resourceCount;
    }

    public void Consume(ResourceType resource, int count) {
        Debug.Assert(_resourceToCount.ContainsKey(resource), $"{resource} is not added");
        Debug.Assert(_resourceToCount[resource] >= count, $"There is not enough {resource}");

        _resourceToCount[resource] -= count;
        HolderUpdatedEvent.Invoke(resource);
    }

    public void Add(ResourceType resource) {
        Add(resource, 1);
    }

    public void Add(ResourceType resource, int count) {
        if (_resourceToCount.ContainsKey(resource)) {
            _resourceToCount[resource]++;
        }   else {
            _resourceToCount.Add(resource, count);
        }
        HolderUpdatedEvent.Invoke(resource);
    }
}
