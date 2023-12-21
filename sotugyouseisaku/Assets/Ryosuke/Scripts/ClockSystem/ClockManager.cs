using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*�j�̓����̏������܂Ƃ߂Ƃ���*/
public class ClockManager : MonoBehaviour
{
    protected Transform clockTransform;
    protected const int MAXTIME = 60;

    private void Awake()
    {
        clockTransform = GetComponent<Transform>();
    }


    /// <summary>
    /// �j�̓����̃��W�b�N
    /// </summary>
    /// <param name="maxTime">���v�̐j�������ő�̎��ԒP��</param>
    /// <param name="currentTime">���݂̎������i�[</param>
    /// <returns></returns>
    public int MovementOfTheNeedle(int maxTime  , int currentTime)
    {
        if(currentTime < 0)
        {
            Debug.Log("�o�O�ł� : ���v�̎��Ԃ��}�C�i�X�ɂȂ��Ă�B");
            return 0;
        }
        return (int)((360.0f / maxTime) * currentTime);
    }

    /// <summary>
    /// �j�𓮂����Ă�Ƃ���B
    /// </summary>
    /// <param name="maxTime"></param>
    /// <param name="currentTime"></param>
    public void MoveNeedle(int maxTime, int currentTime)
    {
        int moveAngle = MovementOfTheNeedle(maxTime, currentTime);
        clockTransform.localEulerAngles = new Vector3(0.0f, moveAngle, 0.0f);
    }

}
