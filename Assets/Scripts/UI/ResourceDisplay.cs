using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _countText;
    [SerializeField] Image _resoruceImage;
    [SerializeField] ResourcePrefabs _resourcePrefabs;

    public float PunchScale = 1.1f;
    public float ScaleDuration = 0.2f;
    public float ScaleIncrement = 0.2f;
    public float MaxScale = 1.5f;
    public float UnscaleSpeed = 0.5f;

    private int _prevCount;
    private Vector3 _maxScale;
    private int _targetScore;
    private int _currentScore;

    private void Start() {
        _maxScale = Vector3.one * PunchScale;
    }

    private void Update() {
        if (_targetScore > _currentScore) {
            Scale();
        }
        else if (_targetScore <= _currentScore) {
            Unscale();
        }
        var dif = _targetScore - _currentScore;
        if (_targetScore != _currentScore) {
            _currentScore += (int)Mathf.Sign(dif);
            _countText.text = _currentScore.ToString();
        }
    }

    private void Scale() {
        if (transform.localScale.x < MaxScale) {
            transform.localScale += Vector3.one * ScaleIncrement;
            if (transform.localScale.x > MaxScale) {
                transform.localScale = Vector3.one * MaxScale;
            }
        }
    }

    private void Unscale() {

        var nexsScale = transform.localScale -
            Vector3.one * Time.deltaTime * UnscaleSpeed;
        if (nexsScale.x < 1) {
            transform.localScale = Vector3.one;
        } else {
            transform.localScale = nexsScale;
        }
    }

    public void SetCount(int count) {
        _targetScore = count;
        //if (count > _prevCount) {
        //    transform.DOScale(_maxScale, ScaleDuration)
        //        .SetLoops(2, LoopType.Yoyo);
        //}
        //_countText.text = count.ToString();
        //_prevCount = count;
    }

    public void SetResource(ResourceType type) {
        var asset = _resourcePrefabs.GetAssetsByType(type);
        _resoruceImage.color = asset.Material.color;
    }
}
