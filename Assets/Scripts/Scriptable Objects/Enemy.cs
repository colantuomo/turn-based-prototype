using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/New Enemy", order = 2)]
public class Enemy : Character
{
    [Header("Enemy")]
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        private set
        {
            _name = GetType().Name;
        }
    }
}
