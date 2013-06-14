using System;
using UnityEngine;
using BulletSharp;

public class UnityDebugDrawer : BulletSharp.DebugDraw
{
	DebugDrawModes mode = DebugDrawModes.None;
	
	public UnityDebugDrawer ()
	{
		
	}

	#region implemented abstract members of BulletSharp.DebugDraw
	public override void DrawLine (ref BulletSharp.Vector3 from, ref BulletSharp.Vector3 to, ref BulletSharp.Vector3 color)
	{
		Debug.DrawLine (new UnityEngine.Vector3 (from.X, from.Y, from.Z),
						new UnityEngine.Vector3 (to.X, to.Y, to.Z),
						new Color (color.X, color.Y, color.Z));
	}

	public override void Draw3dText (ref BulletSharp.Vector3 location, string textString)
	{
		// TODO: implement (if it's ever called)
		throw new NotImplementedException ();
	}

	public override void ReportErrorWarning (string warningString)
	{
		Debug.LogWarning (warningString);
	}

	public override DebugDrawModes DebugMode {
		get {
			return mode;
		}
		set {
			mode = value;
		}
	}
	#endregion
}