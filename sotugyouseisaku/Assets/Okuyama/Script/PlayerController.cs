using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�ڂ̒��S�A���J�[")]
    private Transform _centerEyeAnchor = null;

    [SerializeField, Tooltip("�ړ����x")]
    private float _speed = 2;

    private Vector3 _velocity;
    private float _nowPosY = 1;
    private void OnEnable()
    {
        PlayerInput.SetEnterInput(InputType.Action1, Action);
    }
    private void OnDisable()
    {
        PlayerInput.LiftEnterInput(InputType.Action1, Action);
    }

    private void Action()
    {
        /*����L�[�{�[�h����E�L�[
         Oculus���Ƃ��Ԃ�A�L�[�ŌĂяo����
        */
        Debug.Log($"TestAction");
    }

    private void FixedUpdate()
    {
        /*�L�[�{�[�h��WASD
         Oculus�͉E�X�e�B�b�N
        */
        _velocity = _centerEyeAnchor.rotation * new Vector3(PlayerInput.InputVector.x, 0, PlayerInput.InputVector.y);
        var pos = transform.position;
        pos += _velocity * _speed * Time.deltaTime;
        pos.y = _nowPosY;
        transform.position = pos;
    }
}
