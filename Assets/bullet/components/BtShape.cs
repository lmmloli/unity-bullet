using UnityEngine;
using System.Collections;

public abstract class BtShape : MonoBehaviour
{	
	public delegate void changed ();

	protected changed observers;
	BulletSharp.CollisionShape shape;
	Transform myTransform;
	Vector3 previousScale;
	
	public BulletSharp.CollisionShape Shape {
		get { return shape; }	
	}
	
	void Awake ()
	{
		shape = createShape ();
		myTransform = transform;
		previousScale = myTransform.localScale;
		shape.LocalScaling = new BulletSharp.Vector3 (previousScale.x, previousScale.y, previousScale.z);
	}
	
	void Update ()
	{
		Vector3 currentScale = myTransform.localScale;
		if (currentScale != previousScale) {
			shape.LocalScaling = new BulletSharp.Vector3 (currentScale.x, currentScale.y, currentScale.z);
			previousScale = currentScale;
			observers ();
		}
	}

	void OnDestroy ()
	{
		Debug.Log ("cleanup shape");
		shape.Dispose ();
		observers = null;
	}
	
	protected abstract BulletSharp.CollisionShape createShape ();
	
	public void AddObserver (changed observer)
	{
		observers += observer;
	}
	
	public void RemoveObserver (changed observer)
	{
		observers -= observer;
	}
}
