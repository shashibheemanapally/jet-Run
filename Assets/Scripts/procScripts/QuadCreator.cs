using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class QuadCreator : MonoBehaviour
{
    int xSize=100;
    int zSize=50;

    int resxSize;
    int reszSize;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    float[,] mat;

    private void Start()
    {

        generateMat();

        resxSize = 2 * xSize + 1;
        reszSize = 2 * zSize + 1;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        updateMesh();
        
    }

    void generateMat()
    {
        mat = new float[zSize+2, xSize+2];
        for(int x = 0; x < xSize + 2; x++)
        {
            mat[0, x] = 0;
            mat[zSize + 1, x] = 0;
        }
        for (int z = 0; z < zSize + 2; z++)
        {
            mat[z, 0] = 0;
            mat[z, xSize+1] = 0;
        }
        for (int z = 1; z <= zSize; z++)
        {
            for(int x = 1; x <=xSize; x++)
            {
                mat[z, x] = Random.Range(0f, 1f);
            }
        }
    }

   
    public void createShape()
    {


        vertices = new Vector3[(resxSize+1)*(reszSize+1)];
        triangles = new int[resxSize*reszSize * 6];
        uvs = new Vector2[vertices.Length];



        for (int i = 0, z = 0; z <= reszSize; z++)
        {
            for (int x = 0; x <= resxSize; x++)
            {
                float xValue;
                float zValue;
                float yValue;

                xValue = x / 2;
                zValue = z/ 2;

                yValue = mat[(z+1) / 2, (x+1) / 2];

                vertices[i] = new Vector3(xValue,yValue,zValue);

                uvs[i] = new Vector2((float)x / resxSize, (float)z / reszSize);
                i++;

            }


        }


        int vert = 0;
        int tris = 0;

        for (int z = 0; z < reszSize; z++)
        {
            for (int x = 0; x < resxSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + resxSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + resxSize + 1;
                triangles[tris + 5] = vert + resxSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        




    }
    public void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

    }
}
