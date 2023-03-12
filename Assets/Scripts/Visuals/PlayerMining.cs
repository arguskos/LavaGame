using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour {
    [SerializeField] Transform _arm;

    public float RotateAngle = 35;

    public void OnHit(float hitTime) {
        if (DOTween.IsTweening(_arm)) {
            return;
        }
        _arm.DOLocalRotate(new Vector3(RotateAngle, 0, 0), hitTime / 2)
            .SetLoops(2, LoopType.Yoyo); 
    }
}
