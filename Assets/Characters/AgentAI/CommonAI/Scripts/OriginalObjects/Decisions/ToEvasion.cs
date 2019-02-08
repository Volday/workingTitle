using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToEvasion")]
public class ToEvasion : Decision
{
    public override float Decide(StateController controller)
    {
        Vector3 vectorToTarget = controller.transform.position - controller.targetToAttack.targetToAtack.transform.position;
        float distanceToTarget = Mathf.Sqrt(vectorToTarget.x * vectorToTarget.x + vectorToTarget.z * vectorToTarget.z);
        if ((distanceToTarget / (controller.radiusOfView.radiusOfView)) * 200 - 100
            + (controller.GetComponent<AISkills>().agility - 50) > 0) {
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            UnitManager unitManager = gameManager.GetComponent<UnitManager>();
            for (int t = 0; t < unitManager.projectiles.Count; t++) {
                Projectile projectile = unitManager.projectiles[t].GetComponent<Projectile>();
                if (projectile.pernicious && unitManager.projectiles[t].GetComponent<UnitTeam>().name != controller.unitTeam.name) {
                    float distanceToRay = MyMath.sqrDistanceFromPointToRay(new Vector2(controller.transform.position.x, controller.transform.position.z),
                    new Vector2(unitManager.projectiles[t].transform.forward.x + unitManager.projectiles[t].transform.position.x,
                    unitManager.projectiles[t].transform.forward.z + unitManager.projectiles[t].transform.position.z),
                    new Vector2(unitManager.projectiles[t].transform.position.x, unitManager.projectiles[t].transform.position.z));

                    CapsuleCollider capsuleCollider = controller.GetComponent<CapsuleCollider>();
                    float myRadius = 0;
                    if (capsuleCollider != null) {
                        myRadius = capsuleCollider.radius;
                    }
                    float safeDistance = projectile.radius * projectile.radius + myRadius * myRadius;
                    if (distanceToRay < safeDistance) {
                        float sqrDistance = MyMath.sqrDistanceFromPointToPoint(controller.transform.position, unitManager.projectiles[t].transform.position);
                        float timeBeforeCollision = Mathf.Sqrt(sqrDistance) / unitManager.projectiles[t].GetComponent<MoveSpeed>().moveSpeed;
                        float timeToDodge = (Mathf.Sqrt(safeDistance) - Mathf.Sqrt(distanceToRay)) / controller.moveSpeed.moveSpeed;
                        if (timeBeforeCollision > timeToDodge * 1.5f && timeBeforeCollision < timeToDodge * 3) {
                            Vector2 pointEscape = (new Vector2(unitManager.projectiles[t].transform.forward.x + unitManager.projectiles[t].transform.position.x,
                                unitManager.projectiles[t].transform.forward.z + unitManager.projectiles[t].transform.position.z) -
                                new Vector2(unitManager.projectiles[t].transform.position.x, unitManager.projectiles[t].transform.position.z)).normalized;
                            float pointToRightOfLine = MyMath.PointToRightOfLine(new Vector2(controller.transform.position.x, controller.transform.position.z),
                                new Vector2(unitManager.projectiles[t].transform.forward.x + unitManager.projectiles[t].transform.position.x,
                                unitManager.projectiles[t].transform.forward.z + unitManager.projectiles[t].transform.position.z),
                                new Vector2(unitManager.projectiles[t].transform.position.x, unitManager.projectiles[t].transform.position.z));
                            if (pointToRightOfLine > 0)
                            {
                                pointEscape = MyMath.Rotate(pointEscape, -90) * 1.2f * timeToDodge * controller.moveSpeed.moveSpeed;
                            }
                            else {
                                pointEscape = MyMath.Rotate(pointEscape, 90) * 1.2f * timeToDodge * controller.moveSpeed.moveSpeed;
                            }
                            controller.gameObject.AddComponent<NowEvasion>().StartEvasion(timeToDodge * 1.2f);
                            controller.pointEscape = new Vector3(pointEscape.x + controller.transform.position.x,
                                controller.transform.position.y, pointEscape.y + controller.transform.position.z);
                            return float.MaxValue;
                        }
                    }
                }
            }
        }

        return float.MinValue;
    }
}
