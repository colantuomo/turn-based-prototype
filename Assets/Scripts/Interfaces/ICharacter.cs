using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public Tween GetNextPositionToGo(Vector3 targetPosition);
    public Character GetCharacter();
}
