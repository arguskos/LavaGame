using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MiningSpot : BaseSpot {
    [SerializeField] MiningSpotSettings _settings;
    [SerializeField] Transform _resourceSpawnPoint;

    public UnityEvent<float> HitEvent;
    public UnityEvent ExhaustedEvent;
    public UnityEvent RecoveredEvent;

    private int _hitsTillExhausted;
    
    public bool IsExhausted => _hitsTillExhausted == 0;
    public float CollectFrequency => _settings.CollectFrequency;

    private void Start() {
        _hitsTillExhausted = _settings.HitsTillExhaustion;   
    }

    public void Hit(Miner miner) {
        if (IsExhausted) {
            return;
        }
        ProduceResources(miner);
        var period = 1 / _settings.CollectFrequency;
        HitEvent?.Invoke(period);
        _hitsTillExhausted--;
        if (_hitsTillExhausted <= 0) {
            ExhaustedEvent?.Invoke();
            StartCoroutine(Recover());
        }
    }

    private IEnumerator Recover() {
        yield return new WaitForSeconds(_settings.RecoveryTime);
        RecoveredEvent?.Invoke();
        _hitsTillExhausted = _settings.HitsTillExhaustion;
    }

    private void ProduceResources(Miner miner) {
        Spawn(_settings.Resource, _settings.ResourcesPerHit, _settings.ResourcePhysicsData);
    }
}
