using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*分針のロジック*/
public class MinuteHand : ClockManager
{
    /// <summary>
    /// 分針の動きを設定
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Minute);
    }
}
