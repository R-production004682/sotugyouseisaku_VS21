using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���̎擾
/// </summary>
public class HandItemGet : MonoBehaviour
{
    [SerializeField,Header("���擾�܂ł̎���"), Tooltip("���擾�܂ł̎���")]
    private float _keyGetTime = 2.0f;

    [SerializeField, Header("�����ʉ�"), Tooltip("�����ʉ�")]
    private AudioSource _keySE;

    public�@void KeyGet(GameObject key)
    {
        StartCoroutine(Kye(key)); 
    }
    IEnumerator Kye(GameObject key)
    {
        yield return new WaitForSeconds(_keyGetTime);
        _keySE.Pause();
        key.SetActive(false);
    }
}
