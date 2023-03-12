using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ResourcePickuper : MonoBehaviour {
    [SerializeField] GameData _gameData;
    [SerializeField] SphereCollider _collider;
    
    public UnityEvent<ResourceType> PickedUpEvent;
    public float InstantAbsorbDistance = 0.4f;
    public float ChaiseSpeed = 2;

    private ResourcePoolManager _poolManager;
    private List<Resource> _toPickup = new List<Resource>();
    private float _speed;
    private Vector3 _prevPosition;


    private void Start() {
        _poolManager = FindObjectOfType<ResourcePoolManager>();
        _collider.radius = _gameData.PlayerSettings.CollectRadius;
    }

    private void Update() {
        _speed = CalculateSpeed();
        for (int i = _toPickup.Count - 1; i >= 0; i--) {
            Resource resource = _toPickup[i];
            if (resource == null || resource.IsPickupable) {
                var isAbsorbed = Absorb(resource);
                if (isAbsorbed) {
                    _toPickup.RemoveAt(i);
                }
            }
        }
    }

    private float CalculateSpeed() {
        var distance = transform.position - _prevPosition;
        var speed = distance.magnitude / Time.deltaTime;
        _prevPosition = transform.position;
        return speed;
    }
  
    private void OnTriggerEnter(Collider other) {
        var resource = other.GetComponent<Resource>();
        if (resource != null) {
            _toPickup.Add(resource);
        }
    }

    private void OnTriggerExit(Collider other) {
        var resource = other.GetComponent<Resource>();
        if (resource != null) {
            _toPickup.Remove(resource);
        }
    }

    private bool Absorb(Resource resource) {
        Vector3 dir = (transform.position - resource.transform.position);
        var distance = dir.magnitude;
        if (distance < InstantAbsorbDistance) {
            OnPickedUp(resource);
            return true;
        }

        var minSpeed = _gameData.PlayerSettings.MinPickupSpeed;
        var speed = minSpeed > _speed ? minSpeed : _speed + ChaiseSpeed;
        var velocity = dir.normalized * speed * Time.deltaTime;
        resource.transform.position += velocity;
        return false;
        
    }

    private void OnPickedUp(Resource resource) {
        PickedUpEvent?.Invoke(resource.ResourceType);
        _poolManager.ReturnToPool(resource);
    }
}