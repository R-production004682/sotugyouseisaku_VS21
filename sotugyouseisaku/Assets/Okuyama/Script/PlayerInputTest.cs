using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Proto
{
    public class PlayerInputTest : MonoBehaviour
    {
        [Header("PlayerPrefab�̃Z���^�[�A�C�A���J�[")]
        [SerializeField]
        private Transform _centerEyeAnchor = null;

        [Header("�X�s�[�h")]
        [SerializeField] 
        private float _speed = 2f;

        private float _nowPosY = 0;
        
        void Start()
        {
            //_nowPosY = transform.position.y;
        }

        void Update()
        {
            TestInput();
        }

        void TestInput()
        {
            Vector2 stickDirection = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 moveDirection = _centerEyeAnchor.rotation * new Vector3(stickDirection.x, 0, stickDirection.y);

            var pos = transform.position;
            pos += moveDirection * _speed * Time.deltaTime;
            pos.y = _nowPosY;
            transform.position = pos;
        }
    }
}

