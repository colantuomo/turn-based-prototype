using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _mainCam, _actionSelectionCam;

    private void Start()
    {
        EventsManager.Instance.OnHeroSelected += OnHeroSelected;
        EventsManager.Instance.OnCancel += OnCancel;
        EventsManager.Instance.OnPerformingAction += OnPerformingAction;
    }

    private void OnPerformingAction(ActionsType action)
    {
        //DeactivateActionSelectionCam();
    }

    private void OnCancel()
    {
        DeactivateActionSelectionCam();
    }

    private void OnHeroSelected(Transform hero)
    {
        ActivateActionSelectionCam(hero);
    }

    private void ActivateActionSelectionCam(Transform target)
    {
        _mainCam.gameObject.SetActive(false);
        _actionSelectionCam.gameObject.SetActive(true);
        _actionSelectionCam.m_Follow = target;
    }

    private void DeactivateActionSelectionCam()
    {
        _mainCam.gameObject.SetActive(true);
        _actionSelectionCam.gameObject.SetActive(false);
        _mainCam.m_Follow = null;
    }
}
