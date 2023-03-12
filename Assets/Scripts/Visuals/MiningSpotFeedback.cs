using DG.Tweening;
using UnityEngine;

public class MiningSpotFeedback : MonoBehaviour {
    [SerializeField] Transform _scaleMesh;

    public float ScaleStrength = 1.2f;

    public void OnHit(float period) {
        _scaleMesh.DOScale(ScaleStrength, (period) / 2).SetLoops(2, LoopType.Yoyo);
    }
}