using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("目の中心アンカー")]
    private Transform _centerEyeAnchor = null;

    [SerializeField, Tooltip("移動速度")]
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
        /*現状キーボードだとEキー
         OculusだとたぶんAキーで呼び出せる
        */
        Debug.Log($"TestAction");
    }

    private void FixedUpdate()
    {
        /*キーボードはWASD
         Oculusは左スティック
        */
        _velocity = _centerEyeAnchor.rotation * new Vector3(PlayerInput.InputVector.x, 0, PlayerInput.InputVector.y);
        var pos = transform.position;
        pos += _velocity * _speed * Time.deltaTime;
        transform.position = pos;
    }
}
