using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class FactorySpot : BaseSpot {
    [SerializeField] FactorySpotSettings _settings;
    [SerializeField] Transform _resourceCollectionTransform;
    
    public UnityEvent<float> ProductionStarted;
    public UnityEvent ProductionFinished;

    public float IndividualFlyDelay = 0.1f;
    private int _toConsumeCount = 0;
    private int _storage;
    private bool _isProducing;

    public Resource ResourceNeeded => _settings.ResourceNeeded;

    public void ToConsume(Resource resource) {
        resource.SetPhysicsState(false);
        var rTransform = resource.transform;
        var flyTime = _gameData.ResourceSettings.FlyTime;
        var flyDelay = _gameData.ResourceSettings.FlyDelay;
        _toConsumeCount++;
        rTransform.DOMove(_resourceCollectionTransform.position, flyTime)
            .SetDelay(flyDelay + _toConsumeCount * IndividualFlyDelay)
            .OnComplete(() => OnConsumed());
    }

    private void OnConsumed() {
        _toConsumeCount--;
        _storage++;
        if (_storage >= _settings.ProduceCount && !_isProducing) {
            StartCoroutine(StartProduction());
        }
    }

    private IEnumerator StartProduction() {
        var productionTime = _settings.ProductionTime;
        ProductionStarted?.Invoke(productionTime);
        _isProducing = true;
        _storage -= _settings.ProduceCount;
        yield return new WaitForSeconds(productionTime);
        Produce();
        if (_storage >= _settings.ProduceCount) {
            StartCoroutine(StartProduction());
        }
    }

    private void Produce() {
        Spawn(_settings.ResourceProduce, _settings.ProduceCount, _settings.FlyOutPhysics);
        ProductionFinished?.Invoke();
        _isProducing = false;
    }

}
