using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HACK
//OPTIMIZE
//WARNING :
// ����d�����邾�낤�@�\���o�Ă��邾�낤���A
// ���͂��̑Ώ��͂����ɂƂ肠�����������Ă����`�ō���Ă����B
// �ׂ����݌v�͂��ĂȂ��̂ŁA���ʂ����Ŏ������邩�H���Ă̂�����Ǝv���܂����ǁX�����܂��B


/// <summary>
/// �C�x���g���ꊇ�ŊǗ����Ă����Ƃ���B
/// �C�x���g���Ăяo�������ꍇ�́A�����蔻��iOnTriggerEnter , OnTriggerExit , OnTriggerStay�j�̒���
/// ���̃N���X�̊֐����Ăяo���Ďg���Ă��������B
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
    /// �����̃C�x���g����
    /// HACK : ���ʉ���������������ĂȂ����߁A���m�������Ȃǂ̎��͏o���Ȃ��B�����I�ɒ����B
    /// </summary>
    /// <param name="roomIndex"></param>
    public void RoomEvent(int roomIndex)
    {
        if(roomNumbers != null && roomNumbers.Length > roomIndex)
        {
            //as_ : AudioSource�̓�����
            AudioSource as_room = roomNumbers[roomIndex].AddComponent<AudioSource>();

            as_room.pitch = pitch;
            as_room.volume = volume;
            as_room.clip = clips[roomIndex];
            as_room.Play();
        }
    }

    /// <summary>
    /// �C�x���g���Đ����ꂽ�ƒʒm���Ă�Ƃ���
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
    /// �C�x���g���������W�b�N
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
