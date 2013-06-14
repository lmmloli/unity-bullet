using UnityEngine;
using System.Collections;

public class BtCompoundSubShape : MonoBehaviour
{	
	public BtShape shape;
	
	void Awake ()
	{
		if (shape == null)
			shape = GetComponent<BtShape> ();
	}
	
	void Start ()
	{
		Matrix4x4 unityMatrix = Matrix4x4.TRS (transform.localPosition, transform.localRotation, UnityEngine.Vector3.one);
		BulletSharp.Matrix bulletMatrix = new BulletSharp.Matrix (
			unityMatrix.m00, unityMatrix.m10, unityMatrix.m20, unityMatrix.m30, 
			unityMatrix.m01, unityMatrix.m11, unityMatrix.m21, unityMatrix.m31, 
			unityMatrix.m02, unityMatrix.m12, unityMatrix.m22, unityMatrix.m32, 
			unityMatrix.m03, unityMatrix.m13, unityMatrix.m23, unityMatrix.m33);
		BtCompoundShape c = transform.parent.GetComponent<BtCompoundShape> ();
		c.AddChildShape (bulletMatrix, shape.Shape);
	}
}
