using UnityEngine;
using System.Collections;

public class BtConeShape : BtShape
{	
	public float radius = 0.5f;
	public float height = 1f;
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		return new BulletSharp.ConeShape (radius, height);
	}
	#endregion
}
