using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject hero;

    void Start()
    {
        string temp =  gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallback(string btn)
    {
        if(btn.CompareTo("Left Skill Button") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("melee");
        }
        else if (btn.CompareTo("Middle Skill Button") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("range");
        }
        else 
        {
            hero.GetComponent<FighterAction>().SelectAttack("run");
        }
    }
}
