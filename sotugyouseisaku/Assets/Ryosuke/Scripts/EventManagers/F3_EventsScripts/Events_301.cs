using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Event_301 : MonoBehaviour
{

    [Header("Actor")]
    [SerializeField, Tooltip("Player���i�[���鏊")] private GameObject eventTarget;
    [SerializeField, Tooltip("�΍p�����i�[���鏊")] private List<GameObject> statueObjects;

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
    /// statue��Player�����鏈��
    /// �uOnTriggerStay�ŌĂяo���v
    /// </summary>
    private void LookToPlayer( )
    {
        if (eventTarget != null && statueObjects != null)
        {
            //�S�Ă̐΍p���̑��Έʒu�𑖍�
            foreach (GameObject statue in statueObjects)
            {
                Vector3 looktoPlayer = eventTarget.transform.position - statue.transform.position;
                looktoPlayer.y = 0.0f;
                statue.transform.rotation = Quaternion.LookRotation(looktoPlayer);
            }
        }
    }


    /// <summary>
    /// AudioSource���폜
    /// �uOnTriggerEnter�ŌĂяo���v
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
            Debug.Log("�����Ă��");
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
