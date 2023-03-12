using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlatform : MonoBehaviour {
    public FactorySpot FactorySpot => _factorySpot;
    private FactorySpot _factorySpot;

    private void Awake() {
        _factorySpot = GetComponentInParent<FactorySpot>();
    }
}
