using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HACK
//OPTIMIZE
//WARNING :
// 今後重複するだろう機能が出てくるだろうが、
// 今はその対処はせずにとりあえず実装していく形で作っていく。
// 細かい設計はしてないので、普通ここで持たせるか？ってのがあると思いますが追々直します。


/// <summary>
/// イベントを一括で管理しておくところ。
/// イベントを呼び出したい場合は、当たり判定（OnTriggerEnter , OnTriggerExit , OnTriggerStay）の中で
/// このクラスの関数を呼び出して使ってください。
/// </summary>
public class EventManager : MonoBehaviour
{
    /*-----Components Definition-----*/
    [SerializeField] private AudioClip[]  clips;
    [SerializeField] private GameObject[] roomNumbers;

    /*-----Sound Setting-----*/
    [SerializeField] private float pitch      = 1.0f;
    [SerializeField] private float volume     = 0.7f;


    public bool[] isEventFinish;

    private void Start( )
    {
        isEventFinish = new bool[roomNumbers.Length];   
    }

    /// <summary>
    /// 部屋のイベント処理
    /// HACK : 効果音処理しか書かれてないため、モノが動くなどの事は出来ない。将来的に直す。
    /// </summary>
    /// <param name="roomIndex"></param>
    public void RoomEvent(int roomIndex)
    {
        if(roomNumbers != null && roomNumbers.Length > roomIndex)
        {
            //as_ : AudioSourceの頭文字
            AudioSource as_room = roomNumbers[roomIndex].AddComponent<AudioSource>();

            as_room.pitch = pitch;
            as_room.volume = volume;
            as_room.clip = clips[roomIndex];
            as_room.Play();
        }
    }

    /// <summary>
    /// イベントが再生されたと通知してるところ
    /// </summary>
    /// <param name="roomIndex"></param>
    public void SetEventFinishNotify(int roomIndex)
    {
        if(roomIndex >= 0 && roomIndex < isEventFinish.Length)
        {
            isEventFinish[roomIndex] = true;
        }
    }


    /// <summary>
    /// イベントを消すロジック
    /// </summary>
    public void DestroyEvent()
    {
        for(int i = 0; i < isEventFinish.Length; i++)
        {
            if (isEventFinish[i])
            {
                roomNumbers[i].DestroySafely();
            }
        }
    }

}
