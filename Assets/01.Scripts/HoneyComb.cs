using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyComb : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    public void SpawnBee(int beeSpawnIndex, float force)
    {
        for (int i = 0; i <= beeSpawnIndex; i++)
        {
            GameObject bee = Instantiate(beePrefab, this.transform);
            bee.GetComponent<Bee>().obstaclePushForce = force;
        }
    }
}
