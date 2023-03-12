using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

[System.Serializable]
public class TutorialSequence {
    public List<BaseSpot> Spots = new List<BaseSpot>();
}


public class TutorialManager : MonoBehaviour {
    public List<TutorialSequence> _tutorialSequence = new List<TutorialSequence>();
    public bool IsActive = true;
    
    private int _currentSequence;
    private int _currentSpot;

    public void Start() {
            
    }

    public void Next() {
        var currenctSequence = _tutorialSequence[_currentSequence];
        if (_currentSpot < currenctSequence.Spots.Count - 1) {
            _currentSpot++;
        }   else {
            if (_currentSequence < _tutorialSequence.Count - 1) {
                _currentSequence++;
            } else {
                IsActive = false;
            }
        }
    }

    public void OnInetracted(BaseSpot spot) {
        if (spot.transform == GetCurrentTarget()) {
            Next();
        }
    }

    public Transform GetCurrentTarget() {
        if (!IsActive) {
            return null;
        }
        var spot = _tutorialSequence[_currentSequence].Spots[_currentSpot];
        return spot.transform;
    }
}
