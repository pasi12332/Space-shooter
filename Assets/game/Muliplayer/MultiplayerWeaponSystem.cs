using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerWeaponSystem : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public Bullet BulltePrefab;
    public float FireRate = 1f;
    Vector3 pos;

    private float _fireRateCounter;

    private void Update()
    {
        _fireRateCounter += Time.deltaTime;
    }

    public void Fire()
    {
        if (_fireRateCounter > FireRate)
        {
            _fireRateCounter = 0;

            foreach (var spawnPoint in SpawnPoints)
            {
                pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
                PhotonNetwork.Instantiate("" + BulltePrefab, pos, Quaternion.identity);
            }
        }
    }
}
