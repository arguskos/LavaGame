using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

using Random = UnityEngine.Random;
public class Resource : MonoBehaviour {
    [SerializeField] GameData _gameData;
    [SerializeField] Renderer _renderer;
    
    public ResourceType ResourceType;
    public UnityEvent<Collision> OnCollision;

    private Collider _collider;
    private Rigidbody _rb;
    public bool IsPickupable { get; set; }

    private void Awake() {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        OnCollision?.Invoke(collision);
        //Meh but I'm lazy
        if (collision.transform.CompareTag("Ground")) {
            SetPhysicsState(false);
        }
    }

    public void FlyOut(ResourcePhysicsData data) {
        SetPhysicsState(true);
        Vector3 vector = Random.insideUnitCircle;
        vector.z = vector.y;
        vector.y = 0;
        vector *= data.MaxForceForward;
        if (vector.magnitude < data.MinForceForward) {
            vector = vector.normalized * data.MinForceForward;
        }
        vector.y = data.ForceUp;
        _rb.AddForce(vector, ForceMode.Impulse);
    }

    public void SetPhysicsState(bool state) {
        _rb.isKinematic = !state;
    }
}
