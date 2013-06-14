using System;
using BulletSharp;
using UnityEngine;

public class UnityMotionState
{
}/*: MotionState
{
	UnityEngine.Transform myTransform;
	
	public UnityMotionState (UnityEngine.Transform transform) : base(convertUnityTransformToBullet())
	{
		myTransform = transform;
	}
	
	public override Matrix WorldTransform {
		get {
			return base.WorldTransform;
		}
		set {
			BulletSharp.Vector3 bulletPos = value.TranslationVector;
			BulletSharp.Quaternion bulletRot = BulletSharp.Quaternion.RotationMatrix (value);
			myTransform.position = new UnityEngine.Vector3 (bulletPos.X, bulletPos.Y, bulletPos.Z);
			myTransform.rotation = new UnityEngine.Quaternion (bulletRot.X, bulletRot.Y, bulletRot.Z, bulletRot.W);
			base.WorldTransform = value;
		}
	}
	
	override public void setWorldTransform (out Matrix transform){
		base.setWorldTransform(out transform);
		
	}
	
	BulletSharp.Matrix convertUnityTransformToBullet() {
		Matrix4x4 unityMatrix = Matrix4x4.TRS (myTransform.position, myTransform.rotation, UnityEngine.Vector3.one);
		bulletMatrix = new BulletSharp.Matrix (
			unityMatrix.m00, unityMatrix.m10, unityMatrix.m20, unityMatrix.m30, 
			unityMatrix.m01, unityMatrix.m11, unityMatrix.m21, unityMatrix.m31, 
			unityMatrix.m02, unityMatrix.m12, unityMatrix.m22, unityMatrix.m32, 
			unityMatrix.m03, unityMatrix.m13, unityMatrix.m23, unityMatrix.m33);
		return bulletMatrix;
	}
}*/
