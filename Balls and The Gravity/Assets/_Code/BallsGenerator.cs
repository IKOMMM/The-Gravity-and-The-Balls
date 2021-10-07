using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsGenerator : MonoBehaviour
{
    float ballGenerationTime = 0.25f;
    public int maxNumberToSpawn = 250;
    public int numberToSpawn = 0;
    [SerializeField] BallsPooler ballsPooler;

    void Start()
    {
        ballsPooler = BallsPooler.Instance;
        StartCoroutine(GenerateBallsInTime());
    }
    IEnumerator GenerateBallsInTime()
    {
        while(maxNumberToSpawn > numberToSpawn)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            ballsPooler.SpawnBallFromPool("Ball", randomSpawnPosition, Quaternion.identity);
            numberToSpawn++;
            yield return new WaitForSeconds(ballGenerationTime);
        }
        yield return null;
    }     
}


