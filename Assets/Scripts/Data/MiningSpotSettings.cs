using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LavaGame/MiningSpotSettings")]
public class MiningSpotSettings : ScriptableObject {
    public Resource Resource;
    public float CollectFrequency;
    public int ResourcesPerHit;
    public float RecoveryTime;
    public int HitsTillExhaustion;
    public ResourcePhysicsData ResourcePhysicsData;
}
