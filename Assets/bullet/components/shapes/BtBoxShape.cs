using UnityEngine;
using System.Collections;

public class BtBoxShape : BtShape
{	
	public Vector3 halfExtents = new Vector3 (0.5f, 0.5f, 0.5f);
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		return new BulletSharp.BoxShape (halfExtents.x, halfExtents.y, halfExtents.z);
	}
	#endregion
}
