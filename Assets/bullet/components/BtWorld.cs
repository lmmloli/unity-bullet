using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;

public class BtWorld : MonoBehaviour
{
	public static BtWorld main;
	public UnityEngine.Vector3 gravity = new UnityEngine.Vector3 (0, -10, 0);
	public int maxSubSteps = 5;
	public DebugDrawModes[] debugDrawModes = new DebugDrawModes[]{};
	BulletSharp.DiscreteDynamicsWorld world;
	BulletSharp.BroadphaseInterface broadphase;
	BulletSharp.DefaultCollisionConfiguration collisionConfiguration;
	BulletSharp.Dispatcher dispatcher;
	BulletSharp.ConstraintSolver solver;
		
	public BulletSharp.DiscreteDynamicsWorld World {
		get { return world; } 
	}

	void Awake ()
	{
		if (BtWorld.main == null || gameObject.tag.Equals ("main"))
			BtWorld.main = this;
		
		// TODO: expose in editor?
		broadphase = new BulletSharp.DbvtBroadphase ();
		collisionConfiguration = new BulletSharp.DefaultCollisionConfiguration ();
		dispatcher = new BulletSharp.CollisionDispatcher (collisionConfiguration);
		solver = new BulletSharp.SequentialImpulseConstraintSolver ();
		world = new BulletSharp.DiscreteDynamicsWorld (dispatcher, broadphase, solver, collisionConfiguration);
		
		world.Gravity = new BulletSharp.Vector3 (gravity.x, gravity.y, gravity.z);
		world.DebugDrawer = new UnityDebugDrawer ();
		world.DebugDrawer.DebugMode = BulletSharp.DebugDrawModes.None;
		foreach (BulletSharp.DebugDrawModes drawMode in debugDrawModes) {
			world.DebugDrawer.DebugMode |= drawMode;
		}
	}
	
	void Update ()
	{
		world.DebugDrawer.DebugMode = BulletSharp.DebugDrawModes.None;
		foreach (BulletSharp.DebugDrawModes drawMode in debugDrawModes) {
			world.DebugDrawer.DebugMode |= drawMode;
		}
		world.DebugDrawWorld ();	
	}
	
	void LateUpdate ()
	{
		world.StepSimulation (Time.deltaTime, maxSubSteps, Time.fixedDeltaTime);
	}
	
	void OnDestroy ()
	{
		Debug.Log ("cleanup world");
		
		for (int i = World.NumConstraints - 1; i >= 0; i--) {
			BulletSharp.TypedConstraint constraint = World.GetConstraint (i);
			World.RemoveConstraint (constraint);
			constraint.Dispose ();
		}

		World.Dispose ();
		broadphase.Dispose ();
		solver.Dispose ();
		dispatcher.Dispose ();
		collisionConfiguration.Dispose ();
	}
}
