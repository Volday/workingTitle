using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {

    public LayerMask collisionMask;
    public float speed;
    public float lifeTime = 3;
    float currentLifeTime = 0;
    public float damage = 10;

	void Update () {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > lifeTime)
        {
            Destroy(gameObject);
        }
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance * 2);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.gameObject.GetComponent<HealthPoints>() != null)
            {
                hit.collider.gameObject.GetComponent<HealthPoints>().currentHealthPoints -= damage;
            }
            Destroy(gameObject);
        }
    }
}
