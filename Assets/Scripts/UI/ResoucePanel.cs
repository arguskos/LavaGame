using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoucePanel : MonoBehaviour {
    [SerializeField] ResourceHolder _holder;
    [SerializeField] ResourceDisplay _resourceDisplayPrefab;

    private Dictionary<ResourceType, ResourceDisplay> _resourceToDisplay = new Dictionary<ResourceType, ResourceDisplay>();

    private void Start() {
        var rType = Enum.GetValues(typeof(ResourceType));
        foreach (ResourceType value in rType) {
            _resourceToDisplay[value] = SpawnNewDisplay(value);
        }
        _holder.HolderUpdatedEvent.AddListener(OnHolderUpdate);
    }

    private void OnHolderUpdate(ResourceType resource) {
        var r = _holder.GetResources();
        var newValue = r[resource];
        ResourceDisplay resourceDisplay = _resourceToDisplay[resource];
        if (newValue == 0) {
            resourceDisplay.gameObject.SetActive(false);
        }
        else {
            resourceDisplay.gameObject.SetActive(true);
            resourceDisplay.SetCount(newValue);
        }
    }

    private ResourceDisplay SpawnNewDisplay(ResourceType resource) {
        var rDsiplay = Instantiate(_resourceDisplayPrefab, transform);
        rDsiplay.gameObject.SetActive(false);
        rDsiplay.SetResource(resource);
        return rDsiplay;
    }
 
    
}
