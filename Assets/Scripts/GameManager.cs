using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStates _currentState;
    private Transform _currentSelectedHero;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        _currentState = GameStates.Playing;
        EventsManager.Instance.OnHeroSelected += OnHeroSelected;
        EventsManager.Instance.OnSkillSelected += OnSkillSelected;
        EventsManager.Instance.OnPerformingAction += OnPerformingAction;
        EventsManager.Instance.OnCancel += OnCancel;
        EventsManager.Instance.OnMoveCurrentHero += OnMoveCurrentHero;
        EventsManager.Instance.GameStateChanged(_currentState);
    }

    private void OnMoveCurrentHero(Vector3 position)
    {
        var heroBehavior = _currentSelectedHero.GetComponent<HeroBehavior>();
        var round = new Round
        {
            character = heroBehavior,
            positionToMove = position
        };
        EventsManager.Instance.RoundDecisionTaken(round);
        heroBehavior.BackAllAvailablePathsToDefaultStyle();
        EventsManager.Instance.Cancel();
    }

    private void OnCancel()
    {
        SetIsPlayingState();
    }

    private void OnPerformingAction(ActionsType action)
    {
        var hero = _currentSelectedHero.GetComponent<HeroBehavior>();
        print($"Perform Action {action} - Hero: {hero.name}");
        SetPerformingActionState();
        switch (action)
        {
            case ActionsType.Move:
                hero.HighlightAllAvailablePaths();
                break;

        }
    }

    private void OnSkillSelected(Skill skill)
    {
        print($"Skill selected: {skill.Name}");
    }

    private void OnHeroSelected(Transform hero)
    {
        _currentSelectedHero = hero;
        SetIsSelectionActionState();
    }
    private void SetIsPlayingState()
    {
        _currentState = GameStates.Playing;
        EventsManager.Instance.GameStateChanged(_currentState);
    }

    private void SetIsSelectionActionState()
    {
        _currentState = GameStates.SelectingAction;
        EventsManager.Instance.GameStateChanged(_currentState);
    }

    private void SetPerformingActionState()
    {
        _currentState = GameStates.PerformingAction;
        EventsManager.Instance.GameStateChanged(_currentState);
    }

    public bool IsPlaying()
    {
        return _currentState == GameStates.Playing;
    }

    public bool IsWaiting()
    {
        return _currentState == GameStates.Waiting;
    }

    public bool IsSelectingAction()
    {
        return _currentState == GameStates.SelectingAction;
    }

    public bool IsPerformingAction()
    {
        return _currentState == GameStates.PerformingAction;
    }

    public GameStates State()
    {
        return _currentState;
    }

    public void ActionSelected(ActionsType action)
    {
        EventsManager.Instance.ActionSelected(action);
    }

}
