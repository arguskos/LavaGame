using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTargetDrawer : MonoBehaviour {
    [SerializeField] Image _arrow;
    [SerializeField] Camera _camera;
    [SerializeField] GameData _gameData;
    TutorialManager _tutorial;
    Player _player;

    public float Border = 50;
    public float Offest = 100;
    public float AddededRotation = 90;

    private void Start() {
        _tutorial = FindObjectOfType<TutorialManager>();
        if (!_tutorial) {
            gameObject.SetActive(false);
        }
        _player = FindObjectOfType<Player>();
    }

    private void Update() {
        var target = _tutorial.GetCurrentTarget();
        if (target) {
            var distance = Vector3.Distance(target.position, _player.transform.position);
            _arrow.enabled = distance > _gameData.TutorialSettings.TurnOffDistance;
            PlaceArrow(_player.transform, target);
        } else {
            _arrow.enabled = false;
            enabled = false;
        }
    }

    private void PlaceArrow(Transform player, Transform target) {
        var dir = player.position - target.position;
        var screenPlayer = _camera.WorldToScreenPoint(player.position);
        var screen = _camera.WorldToScreenPoint(target.position + dir * Offest);
        if (screen.x < Border) {
            screen.x = Border;
        }
        if (screen.y < Border) {
            screen.y = Border;
        }
        if (screen.x > Screen.width - Border) {
            screen.x = Screen.width - Border;
        }
        if (screen.y > Screen.height - Border) {
            screen.y = Screen.height - Border;
        }
        _arrow.transform.position = screen;
        var vector2d = screen - screenPlayer;
        var angle = Mathf.Rad2Deg * Mathf.Atan2(vector2d.y, vector2d.x);
        angle += AddededRotation;
        _arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
