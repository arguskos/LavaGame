using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Joystick _joystick;
    [SerializeField] Player _player;
    [SerializeField] GameData _gameData;
    private Saver _saver;

    private void Start() {
        _saver = FindObjectOfType<Saver>();
        _saver.LoadGame();
        StartCoroutine(AutoSave());
    }

    private void Update() {
        Vector3 direction = _joystick.Direction;
        direction.z = direction.y;
        direction.y = 0;
        _player.SetDirection(direction);
    }

    private void OnApplicationQuit() {
        _saver.SaveGame();
    }

    private IEnumerator AutoSave() {
        var autoSave = _gameData.AutoSaveTime;
        yield return new WaitForSeconds(autoSave);
        _saver.SaveGame();
    }
}
