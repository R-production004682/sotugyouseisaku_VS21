using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*秒針のロジック*/
public class SecondHand : ClockManager
{
    /// <summary>
    /// 秒針の動きを設定
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Second);
    }
}
