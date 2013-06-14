using UnityEngine;
using System.Collections;

public class BtSphereShape : BtShape
{	
	public float radius = 0.5f;
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		return new BulletSharp.SphereShape (radius);
	}
	#endregion
}
