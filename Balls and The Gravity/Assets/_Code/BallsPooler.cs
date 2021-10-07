using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPooler : MonoBehaviour
{
    [System.Serializable]
    public class BallsPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Variables
    [SerializeField] List<BallsPool> ballPools;
    [SerializeField] Dictionary<string, Queue<GameObject>> ballsDictionary;
    [SerializeField] GameObject mainSpaceToSpawnBalls;
    #endregion

    #region Singleton Pattern
    public static BallsPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Functions and Methods
    void Start()
    {
        FillTheBallsPool();
    }

    void FillTheBallsPool()
    {
        ballsDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (BallsPool pool in ballPools)
        {
            Queue<GameObject> ballPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; ++i)
            {
                GameObject ball = Instantiate(pool.prefab);
                ball.transform.parent = mainSpaceToSpawnBalls.transform;
                ball.SetActive(false);
                ballPool.Enqueue(ball);
            }

            ballsDictionary.Add(pool.tag, ballPool);
        }
    }

    public GameObject SpawnBallFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!ballsDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("There is no pool with tag" + tag);
            return null;
        }

        GameObject ballToSpawn = ballsDictionary[tag].Dequeue();

        ballToSpawn.SetActive(true);
        ballToSpawn.transform.position = position;
        ballToSpawn.transform.rotation = rotation;

        ballsDictionary[tag].Enqueue(ballToSpawn);
        return ballToSpawn;
    }
    #endregion
}
