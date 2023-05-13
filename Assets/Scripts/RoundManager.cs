using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Transform _charactersHolder;
    [SerializeField]
    private Transform _roundHeroesHolder;
    [SerializeField]
    private Transform _roundHeroButton;
    private List<Hero> _heroes = new();
    private List<Enemy> _enemies = new();
    private List<Round> _rounds = new();

    private void Awake()
    {
        RoundSetup();
        EventsManager.Instance.OnRoundDecisionTaken += OnRoundDecisionTaken;
        EventsManager.Instance.OnEndRound += OnEndRound;
    }

    private void OnEndRound()
    {
        RunRound();
    }

    private void OnRoundDecisionTaken(Round round)
    {
        _rounds.Add(round);
    }

    private void RoundSetup()
    {
        SetCharactersLists();
    }

    private void SetCharactersLists()
    {
        for (var i = 0; i < _charactersHolder.childCount; i++)
        {
            if (_charactersHolder.GetChild(i).TryGetComponent(out HeroBehavior heroBehavior))
            {
                _heroes.Add(heroBehavior.GetCurrentHero());
                var roundHeroButton = Instantiate(_roundHeroButton);
                if (roundHeroButton.TryGetComponent(out RoundHeroButton buttonHeroScript))
                {
                    buttonHeroScript.SetCharacter(heroBehavior.transform, heroBehavior.GetCurrentHero().Photo);
                    roundHeroButton.SetParent(_roundHeroesHolder);
                }
            }
            if (_charactersHolder.GetChild(i).TryGetComponent(out EnemyBehavior enemyBehavior))
            {
                _enemies.Add(enemyBehavior.GetCurrentEnemy());
            }
        }
    }

    private void RunRound()
    {
        Sequence mySequence = DOTween.Sequence();
        var rounds = _rounds.OrderBy(round => round.character.GetCharacter().WalkSpeed);
        foreach (var round in rounds)
        {
            var character = round.character.GetCharacter();
            print($"Character:{round.character.GetCharacter().name} Speed: {round.character.GetCharacter().WalkSpeed}");
            //if (character.WalkSpeed > currentHighestSpeed)
            //{
            //    currentHighestSpeed = character.WalkSpeed;
            //}
            mySequence.Append(round.character.GetNextPositionToGo(round.positionToMove));
        }
        mySequence.Play().OnComplete(() =>
        {
            EventsManager.Instance.RoundFinished();
        });
        _rounds.Clear();
    }
}
