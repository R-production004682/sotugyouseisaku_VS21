using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*�b�j�̃��W�b�N*/
public class SecondHand : ClockManager
{
    /// <summary>
    /// �b�j�̓�����ݒ�
    /// </summary>
    private void Update()
    {
        MoveNeedle(MAXTIME , DateTime.Now.Second);
    }
}
