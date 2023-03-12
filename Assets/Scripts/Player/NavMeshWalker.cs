using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshWalker : MonoBehaviour
{
    public float ConsiderStopped;
    public UnityEvent<bool> MovingEvent;
    
    private NavMeshAgent _agent;
    private bool _isMoving;

    public Vector3 Velocity {
        get => _agent.velocity;
        set => _agent.velocity = value;
    }
    
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();    
    }

    private void Update() {
        bool wasMoving = _isMoving;
        _isMoving = Velocity.sqrMagnitude > ConsiderStopped * ConsiderStopped;
        if (_isMoving != wasMoving) {
            MovingEvent?.Invoke(_isMoving);
        }
    }
}
