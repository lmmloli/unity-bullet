using UnityEngine;
using System.Collections;

public class BtBvhTriangleMeshShape : BtShape
{	
	public UnityEngine.Mesh mesh;
	public bool useQuantizedAabbCompression = true;
	
	#region implemented abstract members of BtShape
	override protected BulletSharp.CollisionShape createShape ()
	{
		if (mesh == null)
			mesh = GetComponent<MeshFilter> ().mesh;
		// TODO: share data using an own interface
		float[] vertices = new float[mesh.vertices.Length * 3];
		for (int i = 0; i < mesh.vertices.Length; i++) {
			vertices [i * 3] = mesh.vertices [i].x;
			vertices [i * 3 + 1] = mesh.vertices [i].y;
			vertices [i * 3 + 2] = mesh.vertices [i].z;
		}
		BulletSharp.TriangleIndexVertexArray bulletMesh = new BulletSharp.TriangleIndexVertexArray (mesh.triangles, vertices);
		return new BulletSharp.BvhTriangleMeshShape (bulletMesh, useQuantizedAabbCompression);
	}
	#endregion
}
