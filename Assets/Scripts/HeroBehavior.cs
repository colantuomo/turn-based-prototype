using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
//using System.Threading.Tasks;

public class HeroBehavior : MonoBehaviour, ICharacter
{
    private List<PathChecker> _paths = new();
    [SerializeField]
    private Hero _hero;
    [SerializeField]
    private Ease _moveEase;
    [SerializeField]
    private Transform _pathHighligh;
    private List<Transform> _pathsHighlighted = new();
    void Start()
    {
        EventsManager.Instance.OnCancel += OnCancel;
        AddAvailablePaths();
    }

    public Tween GetNextPositionToGo(Vector3 targetPosition)
    {
        var positionToGo = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        return transform.DOMove(positionToGo, _hero.WalkSpeed).SetEase(_moveEase);
        //EventsManager.Instance.GameStateChanged(GameStates.Playing);
        //EventsManager.Instance.Cancel();
        //BackAllAvailablePathsToDefaultStyle();
        //return Task.CompletedTask;
    }

    private void OnCancel()
    {
        //BackAllAvailablePathsToDefaultStyle();
    }

    private void AddAvailablePaths()
    {
        transform.GetComponentsInChildren<PathPoint>().All(children =>
        {
            children.GetComponentsInChildren<PathChecker>().All(path => { _paths.Add(path); return true; });
            return true;
        });
    }
    public List<PathChecker> GetAllAvailablePaths()
    {
        return _paths;
    }
    public void HighlightAllAvailablePaths()
    {
        print($"HighlightAllAvailablePaths: {_paths.Count}");
        if (_paths.Count == 0 || _pathsHighlighted.Count > 0) return;
        _paths.ForEach(path =>
        {
            print($"path.IsAValidPath(): {path.IsAValidPath()}");
            if (path.IsAValidPath())
            {
                //TODO: move that to the game manager or actions manager;
                var pos = new Vector3(path.transform.position.x, path.transform.position.y, path.transform.position.z);
                var pathHighlight = Instantiate(_pathHighligh, pos, Quaternion.identity);
                _pathsHighlighted.Add(pathHighlight);
            }
        });
    }

    public void BackAllAvailablePathsToDefaultStyle()
    {
        print($"BackAllAvailablePathsToDefaultStyle {_pathsHighlighted.Count}");
        _pathsHighlighted.ForEach(path =>
        {
            Destroy(path.gameObject);
        });
        _pathsHighlighted.Clear();
    }

    public List<Skill> GetAllSkills()
    {
        return _hero.skills;
    }

    public Hero GetCurrentHero()
    {
        return _hero;
    }

    public Character GetCharacter() { return _hero; }
}
