using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactorySpotInteractor : MonoBehaviour {
    [SerializeField] ResourceHolder _resourceHolder;
    [SerializeField] GameData _gameData;
    [SerializeField] float NotMovingVeiocityThreshold;

    public UnityEvent<BaseSpot> InteractedEvent;
    private Transform _resourceTransform;
    private FactorySpot _currentFactory;
    private bool _canInteract;

    private void Start() {
        _resourceTransform = new GameObject($"{name} resources").transform;    
    }

    private void OnTriggerEnter(Collider other) {
        var interactionPlatform = other.GetComponent<InteractionPlatform>();
        if (interactionPlatform != null) {
            _currentFactory = interactionPlatform.FactorySpot;
            StartCoroutine(WaitUntilStop(_currentFactory));
        }
    }

    private void OnTriggerExit(Collider other) {
        var interactionPlatform = other.GetComponent<InteractionPlatform>();
        if (interactionPlatform != null && interactionPlatform.FactorySpot == _currentFactory) {
            _currentFactory = null;
            StopAllCoroutines();
        }
    }

    public void SetCanInteractSatate(bool canInteract) {
        _canInteract = canInteract;
    }

    private IEnumerator WaitUntilStop(FactorySpot factory) {
        yield return new WaitWhile(() => !_canInteract);
        var resource = _currentFactory.ResourceNeeded;
        ExtractResources(resource, factory);
    }

    private void ExtractResources(Resource resourcePrefab, FactorySpot factory) {
        InteractedEvent.Invoke(factory);
        var resourceType = resourcePrefab.ResourceType;
        var recourceCount = _resourceHolder.GetResourceCount(resourceType);
        if (recourceCount == 0) {
            return;
        }
        _resourceHolder.Consume(resourceType, recourceCount);
        for (int i = 0; i < recourceCount; i++) {
            var resource = Instantiate(resourcePrefab, _resourceTransform);
            resource.transform.position = transform.position;
            var physics = _gameData.PlayerSettings.FlyOutPhysics;
            resource.FlyOut(physics);
            resource.IsPickupable = false;
            resource.OnCollision.AddListener(
                (collision) => factory.ToConsume(resource)
            );
        }
    }
}
