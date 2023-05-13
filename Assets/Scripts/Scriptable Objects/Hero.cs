using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Heros/New Hero", order = 1)]
public class Hero : Character
{
    private string _name;
    [Header("Hero")]
    [SerializeField]
    private int DistanceInBlocks;
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
    public HeroType Type;
    public List<Skill> skills = new();
    public int GetDistanceInBlocks()
    {
        return DistanceInBlocks;
    }
}
