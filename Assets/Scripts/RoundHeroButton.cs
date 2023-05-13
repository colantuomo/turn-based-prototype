using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundHeroButton : MonoBehaviour
{
    private Transform _currentCharacter;
    [SerializeField]
    private Image _image;
    public void OnClick()
    {
        print($"Character: {_currentCharacter.name}");
        EventsManager.Instance.HeroSelected(_currentCharacter);
    }

    public void SetCharacter(Transform character, Sprite photo)
    {
        _currentCharacter = character;
        _image.sprite = photo;
    }
}
