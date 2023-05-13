using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour, ICharacter
{
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]
    private Transform _gridContainer;
    private List<Transform> _grids = new();

    public Enemy GetCurrentEnemy() { return _enemy; }

    private void Start()
    {
        EventsManager.Instance.OnEndRound += OnEndRound;
        print($"_gridContainer.childCount: {_gridContainer.childCount}");
        for (var i = 0; i < _gridContainer.childCount; i++)
        {
            _grids.Add(_gridContainer.GetChild(i));
        }
        SetupPath();
    }

    private void OnEndRound()
    {
        SetupPath();
    }

    private void SetupPath()
    {
        var random = new System.Random();
        var randomGridPosition = random.Next(0, _grids.Count);
        var gridSelected = _grids[randomGridPosition].position;
        var positionToGo = new Vector3(gridSelected.x, transform.position.y, gridSelected.z);

        var round = new Round
        {
            character = this,
            positionToMove = positionToGo
        };
        EventsManager.Instance.RoundDecisionTaken(round);
    }

    public Tween GetNextPositionToGo(Vector3 positionToGo)
    {
        return transform.DOMove(positionToGo, _enemy.WalkSpeed).SetEase(Ease.OutQuad);
    }

    public Character GetCharacter() { return _enemy; }
}
