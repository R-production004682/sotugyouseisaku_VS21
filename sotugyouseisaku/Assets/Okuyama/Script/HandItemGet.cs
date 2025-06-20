using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物の取得
/// </summary>
public class HandItemGet : MonoBehaviour
{
    [SerializeField,Header("鍵取得までの時間"), Tooltip("鍵取得までの時間")]
    private float _keyGetTime = 2.0f;

    [SerializeField, Header("鍵効果音"), Tooltip("鍵効果音")]
    private AudioSource _keySE;

    public　void KeyGet(GameObject key)
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
