using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _actionsPanel;
    [SerializeField]
    private RectTransform _actionSkillsPanel;
    [SerializeField]
    private TMP_Text _gameStateLabel;
    [SerializeField]
    private TMP_Text _gameRoundLabel;
    [SerializeField]
    private RectTransform _skillButton;
    private List<Skill> _currentSkillList;

    // TODO: Move this logic to round manager;
    private int _roundCount = 1;

    private void Awake()
    {
        EventsManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void Start()
    {
        EventsManager.Instance.OnHeroSelected += OnHeroSelected;
        EventsManager.Instance.OnCancel += OnCancel;
        EventsManager.Instance.OnActionSelected += OnActionSelected;
        EventsManager.Instance.OnRoundFinished += OnRoundFinished;
        _gameRoundLabel.text = _roundCount.ToString();
    }

    private void OnRoundFinished()
    {
        _roundCount++;
        _gameRoundLabel.text = _roundCount.ToString();
    }

    private void OnGameStateChanged(GameStates gameState)
    {
        print($"Game State Changed {gameState}");
        _gameStateLabel.text = gameState.ToString();
    }

    private void OnActionSelected(ActionsType action)
    {
        //if (action != ActionsType.Move)
        //{
        //    UseActionsPanel(false);
        //    UseActionSkillsPanel(true);
        //    AddSkillButtonsToScreen(action);
        //}
        //else
        //{
        //    UseActionsPanel(false);
        //    EventsManager.Instance.PerformingAction(action);
        //}
    }

    private void OnCancel()
    {
        UseActionsPanel(false);
    }

    private void OnHeroSelected(Transform hero)
    {
        //if (hero.TryGetComponent(out HeroBehavior heroBehavior))
        //{
        //    _currentSkillList = heroBehavior.GetAllSkills();
        //}
        //UseActionsPanel(true);
    }

    private void UseActionsPanel(bool inUse)
    {
        _actionsPanel.gameObject.SetActive(inUse);
    }

    private void UseActionSkillsPanel(bool inUse)
    {
        print($"_currentSkillList {_currentSkillList.Count}");
        _actionSkillsPanel.gameObject.SetActive(inUse);
    }

    private void AddSkillButtonsToScreen(ActionsType action)
    {
        var list = _currentSkillList.FindAll(skill => skill.SkillType == (SkillType)action);
        list.ForEach(skill =>
        {
            var skillButton = Instantiate(_skillButton, _actionSkillsPanel);
            skillButton.GetComponent<SkillButton>().SetCurrentSkill(skill);
            skillButton.SetParent(_actionSkillsPanel.transform);
        });
    }

    public void BackToActionsMenu()
    {
        RemoveAllSkillButton();
        UseActionsPanel(true);
        UseActionSkillsPanel(false);
    }

    private void RemoveAllSkillButton()
    {
        for (var i = 0; i < _actionSkillsPanel.childCount; i++)
        {
            var button = _actionSkillsPanel.GetChild(i);
            if (i != 0)
            {
                button.DetachChildren();
                Destroy(button.gameObject);
            }
        }
    }

    public void ChooseATK()
    {
        EventsManager.Instance.ActionSelected(ActionsType.Attack);
    }

    public void ChooseDEF()
    {
        EventsManager.Instance.ActionSelected(ActionsType.Defense);
    }

    public void ChooseMOVE()
    {
        EventsManager.Instance.ActionSelected(ActionsType.Move);
    }

    public void EndRound()
    {
        EventsManager.Instance.EndRound();
    }
}
