using System.Collections.Generic;
using UnityEngine;

public class AttractSimulationHandler : MonoBehaviour
{
    #region Variables
    //F=(m1*m2)/d^2*G
    const float G = 6.67f;      
    [SerializeField] Rigidbody rb;
    [SerializeField] bool isPositiveAttraction = true;
    BallsGenerator ballsGenerator;

    int pullValue;

    static List<AttractSimulationHandler> attractors;
    #endregion

    #region AttracionMethods

    private void Start()
    {
        ballsGenerator = FindObjectOfType<BallsGenerator>(); 
    }

    void FixedUpdate()
    {
        foreach (AttractSimulationHandler attractor in attractors)
        {
            if(attractor != this) Attract(attractor);
        }

        GravityState();
    }

    void OnEnable()
    {
        if (attractors == null) attractors = new List<AttractSimulationHandler>();

        attractors.Add(this);
    }

    void OnDisable()
    {
        attractors.Remove(this);
    }

    void Attract(AttractSimulationHandler objectToAttract)
    {
        Rigidbody rbToAttract = objectToAttract.rb;        

        Vector3 direction = rb.position - rbToAttract.position;

        float distance = direction.magnitude;

        if (distance == 0f) return;

        if (isPositiveAttraction)
        {
            pullValue = 1;
        }
        else
        {
            pullValue = -1;
        }     
               
        float forceMagnitude = (rb.mass * rbToAttract.mass) / Mathf.Pow(distance,2) * G;

        Vector3 force = direction.normalized * forceMagnitude * pullValue;
        
        rbToAttract.AddForce(force);
    }    
    
    void GravityState()
    {
        if (ballsGenerator.numberToSpawn == ballsGenerator.maxNumberToSpawn)
        {
            isPositiveAttraction = false;
        }
        else
        {
            isPositiveAttraction = true;
        }
    }
    #endregion
}
