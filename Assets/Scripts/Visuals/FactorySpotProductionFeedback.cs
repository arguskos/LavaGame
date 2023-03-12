using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySpotProductionFeedback : MonoBehaviour {
    [SerializeField] MaterialChanger _materialChanger;
    [SerializeField] Transform _shakingMesh;

    public float ShakeStrength = 2;

    public void OnProductionStarted(float time) {
        _materialChanger.Activate();
        _shakingMesh.DOShakeScale(time, ShakeStrength, 5, 0);
    }

    public void OnProuctionFinished() {
        _materialChanger.Deactivate();
        _shakingMesh.DOKill();
    }
}
