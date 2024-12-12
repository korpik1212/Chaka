using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeCard : Card
{
    public StrikeCard(CardData cardData) : base(cardData)
    {
    }

    public override void Cast(AbilityTarget abilityTarget)
    {
        Debug.Log("casting strike card");

        abilityTarget.enemy.enemyHPManager.TakeDamageServerRPC(50);
        base.Cast(abilityTarget);
    }
}
