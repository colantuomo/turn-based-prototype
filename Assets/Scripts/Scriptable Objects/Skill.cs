using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Heros/New Skill", order = 1)]
public class Skill : ScriptableObject
{
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
    public HeroType HeroType;
    public SkillType SkillType;
    [Min(0)]
    public int MaxArea;
}