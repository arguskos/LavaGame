using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "LavaGame/FactorySpotSettings")]
public class FactorySpotSettings : ScriptableObject {
    public Resource ResourceNeeded;
    public Resource ResourceProduce;
    public float ProductionTime;
    public int NeededCount;
    public int ProduceCount;
    public ResourcePhysicsData FlyOutPhysics;
}
