using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*•ªj‚ÌƒƒWƒbƒN*/
public class MinuteHand : ClockManager
{
    /// <summary>
    /// •ªj‚Ì“®‚«‚ğİ’è
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Minute);
    }
}
