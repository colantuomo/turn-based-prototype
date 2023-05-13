using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField]
    Material _highlitedMaterial;
    Material _defaultMaterial;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }
    public void HighlightPath()
    {
        _meshRenderer.material = _highlitedMaterial;
    }

    public void BackPathToDefaultStyle()
    {
        _meshRenderer.material = _defaultMaterial;
    }
}
