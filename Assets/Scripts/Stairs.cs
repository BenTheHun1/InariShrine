using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stairs : MonoBehaviour
{
	public Mesh mesh;
	public Vector3[] vertices;
	public int[] triangles;
	public Vector3[] easyTri;
	public Vector3[] normals;
	public Vector2[] uvs;

	public GameObject gate;

	public bool hasGenNextStep;
	void Start()
    {
		mesh = new Mesh();
		
		
		//triangles = new int[12];

		/*vertices[0] = new Vector3(-1f, 0f, 1f);
		vertices[1] = new Vector3(-1f, 0f, 0f);
		vertices[2] = new Vector3(1f, 0f, 1f);
		vertices[3] = new Vector3(1f, 0f, 0f);
		vertices[4] = new Vector3(1f, -1f, 0f);
		vertices[5] = new Vector3(-1f, -1f, 0f);*/

		vertices = new Vector3[14];
		vertices[0] = new Vector3(-2f, 0f, 1f);
		vertices[1] = new Vector3(2f, 0f, 1f);
		vertices[2] = new Vector3(-2f, 0f, 0f);
		vertices[3] = new Vector3(2f, 0f, 0f);
		vertices[4] = new Vector3(-2f, -1f, 0f);
		vertices[5] = new Vector3(2f, -1f, 0f);
		vertices[6] = new Vector3(-10f, 0f, 1f);
		vertices[7] = new Vector3(-10f, -1f, 0f);
		vertices[8] = new Vector3(10f, 0f, 1f);
		vertices[9] = new Vector3(10f, -1f, 0f);
		vertices[10] = new Vector3(-20f, 0f, 10f);
		vertices[11] = new Vector3(-20f, 0f, 9f);
		vertices[12] = new Vector3(20f, 0f, 10f);
		vertices[13] = new Vector3(20f, 0f, 9f);

		mesh.vertices = vertices;

		normals = new Vector3[vertices.Length];
		normals[0] = new Vector3(0f, 1f, 0f);
		normals[1] = new Vector3(0f, 1f, 0f);
		normals[2] = new Vector3(0f, 1f, 0f);
		normals[3] = new Vector3(0f, 1f, 0f);
		normals[4] = new Vector3(0f, 1f, 0f);
		normals[5] = new Vector3(0f, 1f, 0f);
		normals[6] = new Vector3(0f, 1f, 0f);
		normals[7] = new Vector3(0f, 1f, 0f);
		normals[8] = new Vector3(0f, 1f, 0f);
		normals[9] = new Vector3(0f, 1f, 0f);
		normals[10] = new Vector3(0f, 1f, 0f);
		normals[11] = new Vector3(0f, 1f, 0f);
		normals[12] = new Vector3(0f, 1f, 0f);
		normals[13] = new Vector3(0f, 1f, 0f);

		mesh.normals = normals;

		uvs = new Vector2[vertices.Length];
		uvs[0] = new Vector2(1f / 3f, 1f);
		uvs[1] = new Vector2(2f / 3f, 1f);
		uvs[2] = new Vector2(1f / 3f, 0.5f);
		uvs[3] = new Vector2(2f / 3f, 0.5f);
		uvs[4] = new Vector2(1f / 3f, 0f);
		uvs[5] = new Vector2(2f / 3f, 0f);
		uvs[6] = new Vector2(1f / 6f, 1f);
		uvs[7] = new Vector2(1f / 6f, 0f);
		uvs[8] = new Vector2(5f / 6f, 1f);
		uvs[9] = new Vector2(5f / 6f, 0f);
		uvs[10] = new Vector2(0f, 1f);
		uvs[11] = new Vector2(0f, 0f);
		uvs[12] = new Vector2(1f, 1f);
		uvs[13] = new Vector2(1f, 0f);

		mesh.uv = uvs;

		easyTri = new Vector3[14];
		easyTri[0] = new Vector3(0, 1, 2);
		easyTri[1] = new Vector3(3, 2, 1);
		easyTri[2] = new Vector3(2, 3, 4);
		easyTri[3] = new Vector3(5, 4, 3);
		easyTri[4] = new Vector3(2, 4, 0);
		easyTri[5] = new Vector3(1, 5, 3);
		easyTri[6] = new Vector3(0, 4, 6);
		easyTri[7] = new Vector3(7, 6, 4);
		easyTri[8] = new Vector3(1, 8, 5);
		easyTri[9] = new Vector3(9, 5, 8);
		easyTri[10] = new Vector3(6, 7, 10);
		easyTri[11] = new Vector3(11, 10, 7);
		easyTri[12] = new Vector3(8, 12, 9);
		easyTri[13] = new Vector3(13, 9, 12);


		triangles = new int[easyTri.Length * 3];
		for (int i = 0; i < easyTri.Length; i++)
		{
			triangles[i * 3] = (int)easyTri[i].x;
			triangles[i * 3 + 1] = (int)easyTri[i].y;
			triangles[i * 3 + 2] = (int)easyTri[i].z;
		}

		/*triangles[0] = 2;
		triangles[1] = 1;
		triangles[2] = 0;
		triangles[3] = 2;
		triangles[4] = 3;
		triangles[5] = 1;
		triangles[6] = 1;
		triangles[7] = 4;
		triangles[8] = 5;
		triangles[9] = 1;
		triangles[10] = 3;
		triangles[11] = 4;*/

		mesh.triangles = triangles;

		//mesh.RecalculateNormals();
		//Debug.Log(mesh.normals.Length);

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
		FindObjectOfType<StairsController>().AdjustStep(this);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			Rebuild();
		}
		Rebuild();
    }

	public void Rebuild()
	{
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}
}
