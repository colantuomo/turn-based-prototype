using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChecker : MonoBehaviour
{
    private readonly float _searchRadius = 0.1f;
    private LayerMask _pathLayer;

    private void Start()
    {
        _pathLayer = LayerMask.GetMask("Path");
    }
    public bool IsAValidPath()
    {
        return Physics.CheckSphere(transform.position, _searchRadius, _pathLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}
