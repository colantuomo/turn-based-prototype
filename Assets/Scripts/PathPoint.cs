using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockPathDirection
{
    Up,
    Down,
    Left,
    Right,
    UpperRight,
    UpperLeft,
    BottomRight,
    BottomLeft
}

public class PathPoint : MonoBehaviour
{
    [SerializeField]
    private LayerMask _pathLayer;
    private readonly float _searchRadius = 0.1f;
    private Hero _currentHero;
    [SerializeField]
    private Transform _movePointPrefab;
    private readonly float HEIGHT_OFFSET = 0.5f;
    private List<Transform> _paths = new();
    private void Start()
    {
        var heroBehavior = GetComponentInParent<HeroBehavior>();
        _currentHero = heroBehavior.GetCurrentHero();
        if (_currentHero == null)
        {
            throw new Exception("You need to add a Hero");
        }
        var distanceInBlocks = _currentHero.GetDistanceInBlocks();
        var lastPathPosition = heroBehavior.transform.position;
        var distance = Vector3.Distance(transform.position, heroBehavior.transform.position);
        var fixedDirection = transform.position - heroBehavior.transform.position;
        for (int i = 1; i <= distanceInBlocks; i++)
        {
            var distanceRoundedToInt = Mathf.RoundToInt(distance);
            var positionToPlacePathPoint = lastPathPosition + fixedDirection;
            positionToPlacePathPoint.y = HEIGHT_OFFSET;
            var currentPath = positionToPlacePathPoint * distanceRoundedToInt;
            var prefab = Instantiate(_movePointPrefab, currentPath, Quaternion.identity);
            lastPathPosition = prefab.position;
            prefab.SetParent(transform);
            _paths.Add(prefab);
        }
    }
    public bool IsAvailable()
    {
        return _paths.Count > 0;
    }

    public List<Transform> GetAllPaths()
    {
        return _paths;
    }

    public void Highlight()
    {
        //Collider[] paths = Physics.OverlapSphere(transform.position, _searchRadius, _pathLayer);
        print($"Paths: {_paths.Count}");
        foreach (Transform path in _paths)
        {
            if (path.transform.TryGetComponent(out PathManager pathManager))
            {
                pathManager.HighlightPath();
            }
        }
    }

    public void BackToNormalStyle()
    {
        Collider[] paths = Physics.OverlapSphere(transform.position, _searchRadius, _pathLayer);
        foreach (Collider path in paths)
        {
            if (path.transform.TryGetComponent(out PathManager pathManager))
            {
                pathManager.BackPathToDefaultStyle();
            }
        }
    }
}
