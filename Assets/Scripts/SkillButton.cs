using UnityEngine;
using TMPro;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name;
    private Skill _currentSkill;
    public void OnSkillSelected()
    {
        print($"Skill Selected {_currentSkill.Name}");
        EventsManager.Instance.SkillSelected(_currentSkill);
    }

    public void SetCurrentSkill(Skill skill)
    {
        _currentSkill = skill;
        _name.text = _currentSkill.name;
    }

}
