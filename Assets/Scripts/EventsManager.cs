using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }
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

    public event Action OnClick;
    public void Click()
    {
        OnClick?.Invoke();
    }

    public event Action OnCancel;
    public void Cancel()
    {
        OnCancel?.Invoke();
    }

    public event Action<Transform> OnHeroSelected;
    public void HeroSelected(Transform hero)
    {
        OnHeroSelected?.Invoke(hero);
    }

    public event Action<ActionsType> OnActionSelected;
    public void ActionSelected(ActionsType action)
    {
        OnActionSelected?.Invoke(action);
    }

    public event Action<Skill> OnSkillSelected;
    public void SkillSelected(Skill skill)
    {
        OnSkillSelected?.Invoke(skill);
    }

    public event Action<ActionsType> OnPerformingAction;
    public void PerformingAction(ActionsType action)
    {
        OnPerformingAction?.Invoke(action);
    }

    public event Action<GameStates> OnGameStateChanged;
    public void GameStateChanged(GameStates gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
    }

    public event Action<Vector3> OnMoveCurrentHero;
    public void MoveCurrentHero(Vector3 position)
    {
        OnMoveCurrentHero?.Invoke(position);
    }

    public event Action<Round> OnRoundDecisionTaken;
    public void RoundDecisionTaken(Round round)
    {
        OnRoundDecisionTaken?.Invoke(round);
    }

    public event Action OnEndRound;
    public void EndRound()
    {
        OnEndRound?.Invoke();
    }

    public event Action OnRoundFinished;
    public void RoundFinished()
    {
        OnRoundFinished?.Invoke();
    }
}
