using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceSettings {
    public float ScatterRadius;
    public float FlyDelay;
    public float FlyTime;
    public float FlyHeight;
    public float UnpickupableTime;
    public float FlyOutSpotForce;
    public float FlyOutCharacterForce;
}

[System.Serializable]
public class PlayerSettings {
    public float WalkingSpeed;
    public float CollectRadius;
    public ResourcePhysicsData FlyOutPhysics;
    public float MinPickupSpeed = 0.5f;
}

[System.Serializable]
public class TutorialSettings {
    public float TurnOffDistance = 5;
} 

[CreateAssetMenu(menuName = "LavaGame/GameData")]
public class GameData : ScriptableObject
{
    public ResourceSettings ResourceSettings;
    public PlayerSettings PlayerSettings;
    public TutorialSettings TutorialSettings;
    public float AutoSaveTime = 120;
}
