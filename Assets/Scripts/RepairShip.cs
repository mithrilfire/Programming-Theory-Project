using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShip : Ship
{
    protected override void Action()
    {
        if (_target.Team == _team)
        {
            //repair
        }
        else
        {
            //attack
        }
    }
}
