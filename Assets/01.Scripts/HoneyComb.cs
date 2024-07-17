using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyComb : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    public void SpawnBee(int beeSpawnIndex, float force)
    {
        //¹úÀÇ ½ºÆù ¼ö¸¸Å­ ¹ú ¼ÒÈ¯
        for (int i = 0; i <= beeSpawnIndex; i++)
        {
            //¹ú ¼ÒÈ¯
            GameObject bee = Instantiate(beePrefab, this.transform);
            //¹úÀÇ ¹Ì´Â Èû Àû¿ë
            bee.GetComponent<Bee>().obstaclePushForce = force;
        }
    }
}
