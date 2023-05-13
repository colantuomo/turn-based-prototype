using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum Actions
{
    Attack,
    Defense,
}

public class ActionsManager : MonoBehaviour
{
    private ActionsType _currentAction;
    void Start()
    {
        EventsManager.Instance.OnClick += OnClick;
        EventsManager.Instance.OnActionSelected += OnActionSelected;
    }

    private void OnActionSelected(ActionsType action)
    {
        _currentAction = action;
        switch (_currentAction)
        {
            case ActionsType.Move:
                EventsManager.Instance.PerformingAction(_currentAction);
                break;
        }
    }

    private void OnClick()
    {
        if (IsClickingThroughUI()) return;
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (GameManager.Instance.IsPerformingAction())
            {
                if (_currentAction == ActionsType.Move)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Path"))
                    {
                        EventsManager.Instance.MoveCurrentHero(hit.transform.position);
                    }
                }
            }
        }
    }

    private bool IsClickingThroughUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
