using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRepresentation
{
    /*
    public float LifeTime { get; set; }

    protected override void Start()
    {
        
        IPlaceable representedObject = RepresentedObject as IPlaceable;

        gameObject.transform.position = representedObject.Position;
        LifeTime = 0.5f;
    }

    protected override void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public static TrajectoryRepresentation MakeFrom(HitTrajectory hitTrajectory)
    {
       
        GameObject representationGameObject = (GameObject)GameObject.Instantiate(
            representationManager.getPrefab("hitTrajectory"));

        TrajectoryRepresentation representation = representationGameObject.AddComponent<TrajectoryRepresentation>();
        representation.LifeTime = 0.2f;

        LineRenderer line = representationGameObject.GetComponent<LineRenderer>();
        line.SetPosition(0, new Vector3(hitTrajectory.FirePoint.x, hitTrajectory.FirePoint.y , hitTrajectory.FirePoint.z));
        line.SetPosition(1, new Vector3(hitTrajectory.Target.x, hitTrajectory.Target.y, hitTrajectory.Target.z));
        line.useWorldSpace = true;
        line.alignment = LineAlignment.View;

        representation.RepresentedObject = hitTrajectory;
        return representation;
    }
*/
}
