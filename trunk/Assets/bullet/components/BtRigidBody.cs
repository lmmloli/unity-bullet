using UnityEngine;
using System.Collections;
using BulletSharp;

public class BtRigidBody : MonoBehaviour
{
	public float mass = 1f;
	public bool isKinematic = false;
	public bool isTrigger = false;
	public BtWorld world;
	public BtShape shape;
	BulletSharp.RigidBody myRigidBody;
	Transform myTransform;
	
	void changedShape ()
	{
		if (!Mathf.Approximately (mass, 0f)) {
			Debug.Log ("updating inertia");
			myRigidBody.SetMassProps (mass, shape.Shape.CalculateLocalInertia (mass));
			myRigidBody.UpdateInertiaTensor ();
		}
	}
	
	void Start ()
	{
		myTransform = transform;
		if (shape == null)
			shape = GetComponent<BtShape> ();
		shape.AddObserver (changedShape);
		myRigidBody = createRigidBody (shape.Shape, myTransform, mass);
		
		if (isTrigger)
			myRigidBody.CollisionFlags |= BulletSharp.CollisionFlags.NoContactResponse;
		
		if (isKinematic)
			myRigidBody.CollisionFlags |= BulletSharp.CollisionFlags.KinematicObject;
		else if (Mathf.Approximately (mass, 0f))
			myRigidBody.CollisionFlags |= BulletSharp.CollisionFlags.StaticObject;
		
		if (world == null)
			world = BtWorld.main;
		world.World.AddRigidBody (myRigidBody);
	}
	
	void Update ()
	{
		BulletSharp.Vector3 bulletPos = myRigidBody.WorldTransform.TranslationVector;
		BulletSharp.Quaternion bulletRot = BulletSharp.Quaternion.RotationMatrix (myRigidBody.WorldTransform);
		myTransform.position = new UnityEngine.Vector3 (bulletPos.X, bulletPos.Y, bulletPos.Z);
		myTransform.rotation = new UnityEngine.Quaternion (bulletRot.X, bulletRot.Y, bulletRot.Z, bulletRot.W);
	}
	
	void OnDestroy ()
	{
		Debug.Log ("cleanup rigidbody");
		if (world != null)
			world.World.RemoveCollisionObject (myRigidBody);
		if (myRigidBody.MotionState != null)
			myRigidBody.MotionState.Dispose ();
		myRigidBody.Dispose ();
		if (shape != null)
			shape.RemoveObserver (changedShape);
	}
	
	BulletSharp.RigidBody createRigidBody (BulletSharp.CollisionShape shape, UnityEngine.Transform transform, float mass)
	{
		Matrix4x4 unityMatrix = Matrix4x4.TRS (transform.position, transform.rotation, UnityEngine.Vector3.one);
		BulletSharp.Matrix bulletMatrix = new BulletSharp.Matrix (
			unityMatrix.m00, unityMatrix.m10, unityMatrix.m20, unityMatrix.m30, 
			unityMatrix.m01, unityMatrix.m11, unityMatrix.m21, unityMatrix.m31, 
			unityMatrix.m02, unityMatrix.m12, unityMatrix.m22, unityMatrix.m32, 
			unityMatrix.m03, unityMatrix.m13, unityMatrix.m23, unityMatrix.m33);
		
		BulletSharp.MotionState motionState = new BulletSharp.DefaultMotionState (bulletMatrix);
		BulletSharp.Vector3 inertia = new BulletSharp.Vector3 (0f, 0f, 0f);
		if (!Mathf.Approximately (mass, 0f)) {
			shape.CalculateLocalInertia (mass, out inertia);
		}
		BulletSharp.RigidBodyConstructionInfo myRigidBodyCI = new BulletSharp.RigidBodyConstructionInfo (mass, motionState, shape, inertia);
		BulletSharp.RigidBody myRigidBody = new BulletSharp.RigidBody (myRigidBodyCI);
		myRigidBodyCI.Dispose ();
		return myRigidBody;
	}
}
