using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*針の動きの処理をまとめとく所*/
public class ClockManager : MonoBehaviour
{
    protected Transform clockTransform;
    protected const int MAXTIME = 60;

    private void Awake()
    {
        clockTransform = GetComponent<Transform>();
    }


    /// <summary>
    /// 針の動きのロジック
    /// </summary>
    /// <param name="maxTime">時計の針が示す最大の時間単位</param>
    /// <param name="currentTime">現在の時刻を格納</param>
    /// <returns></returns>
    public int MovementOfTheNeedle(int maxTime  , int currentTime)
    {
        if(currentTime < 0)
        {
            Debug.Log("バグです : 時計の時間がマイナスになってる。");
            return 0;
        }
        return (int)((360.0f / maxTime) * currentTime);
    }

    /// <summary>
    /// 針を動かしてるところ。
    /// </summary>
    /// <param name="maxTime"></param>
    /// <param name="currentTime"></param>
    public void MoveNeedle(int maxTime, int currentTime)
    {
        int moveAngle = MovementOfTheNeedle(maxTime, currentTime);
        clockTransform.localEulerAngles = new Vector3(0.0f, moveAngle, 0.0f);
    }

}
