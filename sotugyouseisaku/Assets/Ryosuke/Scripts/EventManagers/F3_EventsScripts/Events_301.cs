using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Event_301 : MonoBehaviour
{

    [Header("Actor")]
    [SerializeField, Tooltip("Playerを格納する所")] private GameObject eventTarget;
    [SerializeField, Tooltip("石膏像を格納する所")] private List<GameObject> statueObjects;

    [Header("Component")]
    [SerializeField] private AudioClip clip;

    [Header("Parameter")]
    [SerializeField] private float seDeleteTimeLimit = 5.0f;

    private bool isCollision;

    private AudioSource audioSource;
    private BoxCollider trackingRange;

    private void Start( )
    {
        audioSource   = GetComponent<AudioSource>();
        trackingRange = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// statueがPlayerを見る処理
    /// 「OnTriggerStayで呼び出す」
    /// </summary>
    private void LookToPlayer( )
    {
        if (eventTarget != null && statueObjects != null)
        {
            //全ての石膏像の相対位置を走査
            foreach (GameObject statue in statueObjects)
            {
                Vector3 looktoPlayer = eventTarget.transform.position - statue.transform.position;
                looktoPlayer.y = 0.0f;
                statue.transform.rotation = Quaternion.LookRotation(looktoPlayer);
            }
        }
    }


    /// <summary>
    /// AudioSourceを削除
    /// 「OnTriggerEnterで呼び出す」
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeleteAudioSource( )
    {
        yield return new WaitForSeconds(seDeleteTimeLimit);
        Destroy(audioSource);
    }


    private void OnTriggerEnter( Collider other )
    {
        if(other.gameObject.CompareTag("Player") && !isCollision)
        {
            isCollision = true;
            audioSource.PlayOneShot(clip);
            StartCoroutine(DeleteAudioSource());        
        }
    }

    private void OnTriggerStay( Collider other )
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("向いてるよ");
            trackingRange.size = new Vector3Int(7 , 1 , 10);
            LookToPlayer();
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if(other.gameObject.CompareTag("Player"))
        {
            trackingRange.size = new Vector3Int(2, 1, 5);
        }
    }

}
