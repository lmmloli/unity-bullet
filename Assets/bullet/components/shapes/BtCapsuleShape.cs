using UnityEngine;
using System.Collections;

public class BtCapsuleShape : BtShape
{	
	public float radius = 0.5f;
	public float halfHeight = 1f;
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		return new BulletSharp.CapsuleShape (radius, halfHeight);
	}
	#endregion
}
