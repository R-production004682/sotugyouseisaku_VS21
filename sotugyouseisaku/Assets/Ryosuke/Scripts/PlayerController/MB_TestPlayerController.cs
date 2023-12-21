using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_TestPlayerController : MonoBehaviour
{
    /*-----PlayerParameter Settings-----*/
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f;


    /*-----Component Settings------*/
    private CharacterController characterController;
    EventManager eventManager;

    void Start( )
    {
        characterController = GetComponent<CharacterController>();
         eventManager = GetComponent<EventManager>();
    }

    void Update( )
    {
        MovePlayer();
        RotationPlayer();
    }

    /// <summary>
    /// Player移動処理
    /// </summary>
    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = transform.TransformDirection(movement);
        movement *= speed;

        characterController.Move(movement * Time.deltaTime);
    }

    /// <summary>
    /// Playerの回転処理
    /// </summary>
    private void RotationPlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up , mouseX * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// イベント呼び出すトリガー
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter( Collider other )
    {
        string roomTag = other.gameObject.tag;

        switch (roomTag)
        {
            case "401":
                eventManager?.RoomEvent(0);
                eventManager.SetEventFinishNotify(0);
                break;

            case "402":
                eventManager?.RoomEvent(1);
                eventManager.SetEventFinishNotify(1);
                break;
            case "403":
                eventManager?.RoomEvent(2);
                eventManager.SetEventFinishNotify(2);
                break;
            case "404":
                eventManager?.RoomEvent(3);
                eventManager.SetEventFinishNotify(3);
                break;
            case "405":
                eventManager?.RoomEvent(4);
                eventManager.SetEventFinishNotify(4);
                break;
            case "406":
                eventManager?.RoomEvent(5);
                eventManager.SetEventFinishNotify(5);
                break;
        }
    }

    /// <summary>
    /// イベント消すトリガー
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit( Collider other )
    {
        //TODO : イベントを消す関数を入れる。
        eventManager.DestroyEvent();
    }

}
