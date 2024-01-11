using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�ڂ̒��S�A���J�[")]
    private Transform _centerEyeAnchor = null;

    [SerializeField, Tooltip("�ړ����x")]
    private float _speed = 1;

    private Vector3 _velocity;
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
         Oculus�͍��X�e�B�b�N
        */
        _velocity = _centerEyeAnchor.rotation * new Vector3(PlayerInput.InputVector.x, 0, PlayerInput.InputVector.y);
        var pos = transform.position;
        pos += _velocity * _speed * Time.deltaTime;
        transform.position = pos;
    }
}
