using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*•bj‚ÌƒƒWƒbƒN*/
public class SecondHand : ClockManager
{
    /// <summary>
    /// •bj‚Ì“®‚«‚ğİ’è
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Second);
    }
}
