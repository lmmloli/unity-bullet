using UnityEngine;
using System.Collections;

public class BtConvexHullShape : BtShape
{	
	public UnityEngine.Vector3[] points = new UnityEngine.Vector3[]{};
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		BulletSharp.Vector3[] bulletPoints = new BulletSharp.Vector3[points.Length];
		for (int i = points.Length - 1; i >= 0; i--) {
			bulletPoints [i] = new BulletSharp.Vector3 (points [i].x, points [i].y, points [i].z);
		}
		return new BulletSharp.ConvexHullShape (bulletPoints);
	}
	#endregion
}
