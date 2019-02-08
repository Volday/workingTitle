using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayChecker : MonoBehaviour
{
    void Update()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        UnitManager unitManager = gameManager.GetComponent<UnitManager>();
        for (int t = 0; t < unitManager.projectiles.Count; t++)
        {
            Projectile projectile = unitManager.projectiles[t].GetComponent<Projectile>();
            float distanceToRay = MyMath.sqrDistanceFromPointToRay(new Vector2(transform.position.x, transform.position.z),
                new Vector2(unitManager.projectiles[t].transform.forward.x + unitManager.projectiles[t].transform.position.x,
                unitManager.projectiles[t].transform.forward.z + unitManager.projectiles[t].transform.position.z),
                new Vector2(unitManager.projectiles[t].transform.position.x, unitManager.projectiles[t].transform.position.z));
            Debug.Log(distanceToRay);
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            float myRadius = 0;
            if (capsuleCollider != null)
            {
                myRadius = capsuleCollider.radius;
            }
            if (distanceToRay < projectile.radius * projectile.radius + myRadius * myRadius)
            {
                
            }
        }
    }
}
