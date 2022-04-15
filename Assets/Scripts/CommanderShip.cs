using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderShip : Ship
{
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
