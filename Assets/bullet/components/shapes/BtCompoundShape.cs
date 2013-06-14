using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BtCompoundShape : BtShape
{	
	public bool enableDynamicAABBTree = true;
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		BulletSharp.CompoundShape compound = new BulletSharp.CompoundShape (enableDynamicAABBTree);
		return compound;
	}
	#endregion
	
	public void AddChildShape (BulletSharp.Matrix bulletMatrix, BulletSharp.CollisionShape subShape)
	{
		((BulletSharp.CompoundShape)Shape).AddChildShape (bulletMatrix, subShape);
		if (observers != null)
			observers ();
	}
}
