using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*���j�̃��W�b�N*/
public class MinuteHand : ClockManager
{
    /// <summary>
    /// ���j�̓�����ݒ�
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Minute);
    }
}
