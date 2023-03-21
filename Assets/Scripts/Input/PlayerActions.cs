using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerActions
{

    /// <summary>
    /// Collection of possible player interactions events
    ///     
    /// </summary>

    public static Action OnPressE;
    public static Action OnJump;

    public static Action OnReward;
}



// Enemy is killed --> PlayerActions.OnEnemyKilled();

// Update Score --> OnEnable() PlayerActions.OnEnemyKilled += KilledEnemy;
//                             KilledEnemy(){ do something; }