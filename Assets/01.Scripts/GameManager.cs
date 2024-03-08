using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnBeePosition;
    [SerializeField]
    private GameObject beePrefab;
    [SerializeField]
    private GameObject player;
    public GameObject Player { get { return player; } }
    
    [SerializeField]
    private int beeSpawnIndex; //나중에 Scriptable 오브젝트로 스테이지의 정보를 가져와서 소환수를 바꿀것

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        for(int i = 0; i <= beeSpawnIndex; i++)
        {
            GameObject bee = Instantiate(beePrefab, spawnBeePosition);
            //bee.GetComponent<Bee>().Attack();
        }
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void GameOver()
    {
        UIManager.Instance.OnGameOverPanel();
    }
}
