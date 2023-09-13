using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*���j�̃��W�b�N*/
public class HourHand : ClockManager
{

    /// <summary>
    /// ���j�̓�����ݒ�
    /// </summary>
    private void Update()
    {
        int currentTime_Hour   = DateTime.Now.Hour;
        int currentTime_Minute = DateTime.Now.Minute;

        float HourHandAngle = ((360.0f / 12.0f * currentTime_Hour) +
                               30.0f / 60.0f * currentTime_Minute);

        clockTransform.localEulerAngles = new Vector3(0.0f , (int)HourHandAngle , 0.0f);
    }
}
