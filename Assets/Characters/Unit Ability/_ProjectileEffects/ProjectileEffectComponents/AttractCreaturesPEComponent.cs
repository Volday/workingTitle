using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCreaturesPEComponent : MonoBehaviour
{
    float force = 1.6f;
    float radius;
    void Start()
    {
        radius = GetComponent<Projectile>().radius;
        force *= GetComponent<Weight>().weight;
    }

    void FixedUpdate()
    {
        List<GameObject> targets = GetComponent<EnemiesAround>().FindTargetsCreatureAroundInRadius(radius);
        for (int t = 0; t < targets.Count; t++) {
            Rigidbody targetRigidbody = targets[t].GetComponent<Rigidbody>();
            Vector3 movement = (transform.position - targets[t].transform.position).normalized * Time.fixedDeltaTime * 
                (1 - MyMath.sqrDistanceFromPointToPoint(targets[t].transform.position, transform.position) / (radius * radius)) * force;
            if (targetRigidbody != null)
            {
                targetRigidbody.MovePosition(targetRigidbody.position + movement);
            }
            else {
                targets[t].transform.position += movement;
            }
        }
    }
}
