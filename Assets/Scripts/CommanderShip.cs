using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class CommanderShip : Ship
{
    // POLYMORPHISM
    public override void GoTo(Ship target)
    {
        if (target.Team != _team)
        {
            Debug.LogError("Commander class cant interract with enemy ships!");
            return;
        }
        base.GoTo(target);
    }
    protected override void Action()
    {
        return;
    }
}
