using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    #region Variables
    Rigidbody colidedObjecRigidbody;
    Rigidbody thisObjecRigidbody;
    [SerializeField] GameObject mainSpaceToSpawnBalls;
    [SerializeField] BallsGenerator ballsGenerator;
    //[SerializeField] BallsGenerator ballsGenerator;

    [SerializeField] int ballsAmountToExplosion = 49;
    [SerializeField] float defaultScale = 1f;
    [SerializeField] float defaultMass = 0.5f;
    [Tooltip("Time after explosion!")][SerializeField] float timeWithoutCollision = 0.5f;
    #endregion

    #region CollisionMethods
    private void OnTriggerEnter(Collider other)
    {
        //Used to verify mass
        colidedObjecRigidbody = other.GetComponent<Rigidbody>();
        thisObjecRigidbody = GetComponent<Rigidbody>();

        //Check id to get which objects eats each other 
        //Used just if they have same mass
        int otherId = other.GetInstanceID();
        int thisId = this.GetInstanceID();


        if (ballsGenerator.numberToSpawn == ballsGenerator.maxNumberToSpawn)
        {
            return;
        }
        else 
        {
            if (other.CompareTag("BallGravity"))
            {
                //If balls have the same mass, pickup mother ball with bigger ID.
                if (colidedObjecRigidbody.mass == thisObjecRigidbody.mass)
                {
                    if (otherId < thisId)
                    {
                        AfterCollisionActions(other.gameObject);
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                    }
                }

                //Mother ball is with bigger mass
                else if (colidedObjecRigidbody.mass <= thisObjecRigidbody.mass)
                {
                    AfterCollisionActions(other.gameObject);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }

        }
    }   

    void AfterCollisionActions(GameObject eatenBall)
    {
        ChangeMassAndScaleOfBall(eatenBall.gameObject);
        CollectEatenBall(eatenBall.gameObject);
        ExplodeAfterTime(this.gameObject);
    }

    void ChangeMassAndScaleOfBall(GameObject eatenBall)
    {
        eatenBall.transform.localPosition = this.transform.position;
        transform.localScale += eatenBall.transform.localScale;
        thisObjecRigidbody.mass += colidedObjecRigidbody.mass;
    }

    void CollectEatenBall(GameObject eatenBall)
    {
        ChangeParentIfEatenBallEatsBefore(eatenBall);
        eatenBall.transform.SetParent(this.transform);
    }

    void ChangeParentIfEatenBallEatsBefore(GameObject thisBall)
    {
        Transform[] allChildrenBalls = thisBall.transform.GetComponentsInChildren<Transform>(true);

        for(int i = 0; i < allChildrenBalls.Length; i++)
        {
            allChildrenBalls[i].transform.SetParent(this.transform);
        }   
    }
    #endregion

    
    #region ExplosionMethods
    void ExplodeAfterTime(GameObject ball)
    {
        Transform[] allChildrenThisBall = ball.transform.GetComponentsInChildren<Transform>(true);
        Rigidbody arrayObjecRigidbody;

        if (ballsAmountToExplosion <= allChildrenThisBall.Length)
        {
            for (int i = 0; i < allChildrenThisBall.Length; i++)
            {
                //Transform reset
                allChildrenThisBall[i].transform.SetParent(mainSpaceToSpawnBalls.transform);
                allChildrenThisBall[i].gameObject.SetActive(true);
                allChildrenThisBall[i].gameObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

                //Rigidbody reset
                arrayObjecRigidbody = allChildrenThisBall[i].GetComponent<Rigidbody>();
                arrayObjecRigidbody.mass = defaultMass;
                arrayObjecRigidbody.detectCollisions = false;
            }
            this.gameObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
            thisObjecRigidbody.detectCollisions = false;

           TurnOnCollisionAfterTime(allChildrenThisBall);
        }
        else
        {
            return;
        }
    }
   
    IEnumerator TurnOnCollisionAfterTime(Transform[] allChildrenThisBall)
    {
        Rigidbody arrayObjecRigidbody;

        for (int i = 0; i < allChildrenThisBall.Length; i++)
        {
            //Turn Back Collision
            arrayObjecRigidbody = allChildrenThisBall[i].GetComponent<Rigidbody>();            
            arrayObjecRigidbody.detectCollisions = true;
        }
        thisObjecRigidbody.detectCollisions = true;

        yield return new WaitForSeconds((float)timeWithoutCollision);
    }
    #endregion


}
