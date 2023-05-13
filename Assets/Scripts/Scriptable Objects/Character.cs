using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    [Header("Character Stats")]
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public Sprite Photo;
    public float WalkSpeed;
}
