using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameData _gameData;
    //You could decouple it but I don't see need in this project
    [SerializeField] Miner _miner;
    [SerializeField] FactorySpotInteractor _factoryInteractor;
    private NavMeshWalker _walker;

    private void Awake() {
        _walker = GetComponent<NavMeshWalker>();
    }

    public void SetDirection(Vector3 direction) {
        var velocity = direction * _gameData.PlayerSettings.WalkingSpeed;
        _walker.Velocity = velocity;
    }

    public void OnIsWalking(bool state) {
        _miner.SetCanInteract(!state);
        _factoryInteractor.SetCanInteractSatate(!state);
    }
}
