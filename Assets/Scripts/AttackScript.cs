using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float manaCost;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;

    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if(attackerStats.mana >= manaCost)
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

            damage = multiplier * attackerStats.melee;
            if (magicAttack)
            {
                damage = multiplier * attackerStats.range;
            }

            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0,damage - (defenseMultiplier * targetStats.defense));
            owner.GetComponent<Animator>().Play(animationName);
            targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
            attackerStats.updateManaFill(manaCost);
        }
        else
        {
            Invoke("SkipTurnContinueGame",2);
        }
    }

    void SkipTurnContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
}


