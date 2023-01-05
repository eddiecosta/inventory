using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerActions
{
    public static Action OnEnemyKilled;
    public static Action OnOpenChest;
    public static Action OnPressE;
}



// Enemy is killed --> PlayerActions.OnenemyKille   d();

// Update Score --> OnEnable() PlayerActions.OnEnemyKilled += KilledEnemy;
//                             KilledEnemy(){ do something; }