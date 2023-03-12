using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Miner : MonoBehaviour {
    public UnityEvent<BaseSpot> InteractedEvent;
    public UnityEvent<float> HitEvent;

    private bool _canInteract;
    private MiningSpot _miningSpot;

    private void OnTriggerEnter(Collider other) {
        var miningSpot = other.GetComponent<MiningSpot>();
        if (miningSpot != null) {
            _miningSpot = miningSpot;
        }
    }

    private void OnTriggerExit(Collider other) {
        var miningSpot = other.GetComponent<MiningSpot>();

        if (miningSpot != null && _miningSpot == miningSpot) {
            _miningSpot = null;
            StopAllCoroutines();
        }
    }

    public void SetCanInteract(bool canInteract) {
        _canInteract = canInteract;
        if (_canInteract && _miningSpot) {
            StartCoroutine(Mine(_miningSpot));
        }
    }

    private IEnumerator Mine(MiningSpot spot) {
        InteractedEvent.Invoke(spot);
        float timer = 1.0f / spot.CollectFrequency;
        while (true) { 
            if (!spot.IsExhausted) {
                yield return new WaitForSeconds(timer);
                spot.Hit(this);
                HitEvent?.Invoke(timer);
            }
            yield return null; 
        }
    }
}
