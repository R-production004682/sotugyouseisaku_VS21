using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// •¨‚Ìæ“¾
/// </summary>
public class HandItemGet : MonoBehaviour
{
    [SerializeField,Header("Œ®æ“¾‚Ü‚Å‚ÌŠÔ"), Tooltip("Œ®æ“¾‚Ü‚Å‚ÌŠÔ")]
    private float _keyGetTime = 2.0f;

    [SerializeField, Header("Œ®Œø‰Ê‰¹"), Tooltip("Œ®Œø‰Ê‰¹")]
    private AudioSource _keySE;

    public@void KeyGet(GameObject key)
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
