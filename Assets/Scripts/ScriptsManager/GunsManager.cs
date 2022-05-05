using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsManager : MonoBehaviour
{
    public SOSettingsWeapons settingsGun;
    public Transform spawnToBullet;
    [NonSerialized] public Vector3 posInitial;
    [NonSerialized] public Vector3 directionShoot;

    private void Awake()
    {
        posInitial = transform.position;
    }

    public void ShootNow()
    {
        GameObject bullet = CreateBullet(spawnToBullet);
        Vector3 angleBullet = directionShoot * settingsGun.powerForAngle;
        bullet.GetComponent<Rigidbody>().AddForce((transform.forward * settingsGun.powerOfShoot) + angleBullet);
    }

    private GameObject CreateBullet(Transform spawnLocal)
    {
        GameObject bulletTemp = Instantiate(settingsGun.prefabBullet, spawnLocal.position, Quaternion.identity);
        return bulletTemp;
    }
}