using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsGenerator : MonoBehaviour
{
    [SerializeField] float ballGenerationTime = 0.25f;
    [SerializeField] public int maxNumberToSpawn = 250;
    public int numberToSpawn;
    BallsPooler ballsPooler;

    void Start()
    {
        ballsPooler = BallsPooler.Instance;
        StartCoroutine(GeneratingCoroutine());
    }

    IEnumerator GeneratingCoroutine()
    {
        while(maxNumberToSpawn > numberToSpawn)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            ballsPooler.SpawnBallFromPool("BallGravity", randomSpawnPosition, Quaternion.identity);
            Debug.Log("Spawn" + numberToSpawn);
            numberToSpawn++;
            yield return new WaitForSeconds((float)ballGenerationTime);
        }
        
    }

    /*
    IEnumerator GeneratingBallsCoroutine()
    {
        for(int numberToSpawn = 0; numberToSpawn >= maxNumberToSpawn; numberToSpawn++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            ballsPooler.SpawnBallFromPool("BallGravity", randomSpawnPosition, Quaternion.identity);
            numberToSpawn++;

            yield return new WaitForSeconds((float)ballGenerationTime);
        }
        
    }
    */
        

}


