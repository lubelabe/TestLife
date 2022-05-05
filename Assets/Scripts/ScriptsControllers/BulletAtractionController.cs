using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BulletAtractionController : MonoBehaviour
{
    [SerializeField] private float effectRange;
    [SerializeField] private float forceAtraction = 50;
    [SerializeField] private float gravityValue = 6.7f;
    [SerializeField] private float timeForDestroy = 5f;
    [SerializeField] private LayerMask layerObjAtraction;

    private Collider[] objsNearby;
    GameObject objTemp = null;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        objsNearby = Physics.OverlapSphere(transform.position, effectRange, layerObjAtraction);
        
        if (objsNearby.Length > 0)
        {
            foreach (var obj in objsNearby)
            {
                objTemp = obj.transform.gameObject;
                
                Vector3 difference = transform.GetChild(0).position - objTemp.transform.position;
                float dist = difference.magnitude;
                Vector3 gravityDirection = difference.normalized;
                float gravity = gravityValue * (transform.GetChild(0).localScale.x * objTemp.transform.localScale.x * forceAtraction) / (dist * dist);
                Vector3 gravityVector = (gravityDirection * gravity);
                
                objTemp.transform.GetComponent<Rigidbody>().AddForce(gravityVector * forceAtraction, ForceMode.Acceleration);
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeForDestroy);
        Destroy(gameObject);
    }
}
