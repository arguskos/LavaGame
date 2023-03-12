using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {
    [SerializeField] Material material;
    [SerializeField] Renderer _renderer;
    private Material _original;

    public void Activate() {
        _original = _renderer.material;
        _renderer.material = material;
    }

    public void Deactivate() {
        _renderer.material = _original;
    }

    public void SetMix(float p) {
        var color = Color.Lerp(_original.color, material.color, p);
        _renderer.material.SetColor("_Main", color);
    }
}
