using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormalController : MonoBehaviour
{
    [SerializeField] private float timeForDestroy = 5f;
    
    private void OnEnable()
    {
        Debug.Log("me dispararon, bala normal");
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeForDestroy);
        Destroy(gameObject);
    }
}
