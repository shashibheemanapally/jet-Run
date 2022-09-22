using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManyQuadCreator : MonoBehaviour
{

    public GameObject emptyGo;
    public Material material;

    int xSize = 50;
    int zSize = 25;


    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    float[,] mat;

    private void Start()
    {

        generateMat();

        for (int z = 1; z < zSize + 1; z++)
        {
            for (int x = 1; x < xSize + 1; x++)
            {
                generateCube(x, z);
            }
        }



    }

    void generateCube(int x, int z)
    {
        generateQuad(new Vector3[] { new Vector3(x - 1, mat[z, x], z - 1), new Vector3(x, mat[z, x], z - 1), new Vector3(x - 1, mat[z, x], z), new Vector3(x, mat[z, x], z) },
                                    new int[] { 0, 2, 1, 2, 3, 1 });
        if (mat[z,x] > mat[z-1, x])//bottom
        {
            generateQuad(new Vector3[] { new Vector3(x - 1, mat[z-1, x], z - 1), new Vector3(x, mat[z-1, x], z - 1), new Vector3(x - 1, mat[z, x], z-1), new Vector3(x, mat[z, x], z-1) },
                                    new int[] { 0, 2, 1, 2, 3, 1 });
        }
        if (mat[z, x] > mat[z + 1, x])//top
        {
            generateQuad(new Vector3[] { new Vector3(x - 1, mat[z , x], z ), new Vector3(x, mat[z , x], z ), new Vector3(x - 1, mat[z+1, x], z ), new Vector3(x, mat[z+1, x], z ) },
                                   new int[] { 0, 2, 1, 2, 3, 1 });
        }
        if (mat[z, x] > mat[z , x-1])//left
        {
            generateQuad(new Vector3[] { new Vector3(x - 1, mat[z , x-1], z ), new Vector3(x-1, mat[z , x-1], z - 1), new Vector3(x - 1, mat[z, x], z ), new Vector3(x-1, mat[z, x], z - 1) },
                                    new int[] { 0, 2, 1, 2, 3, 1 });
        }
        
        if (mat[z, x] > mat[z , x+1])//right
        {
            generateQuad(new Vector3[] { new Vector3(x , mat[z, x+1], z-1), new Vector3(x , mat[z, x + 1], z ), new Vector3(x , mat[z, x], z-1), new Vector3(x , mat[z, x], z ) },
                                   new int[] { 0, 2, 1, 2, 3, 1 });
        }

    }

    void generateQuad(Vector3[] vertices, int[] tris)
    {
        var go = Instantiate(emptyGo);
        go.transform.parent = gameObject.transform;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        

        go.GetComponent<MeshFilter>().mesh = mesh;
        go.GetComponent<MeshRenderer>().material = material;
    }

    void generateMat()
    {
        mat = new float[zSize + 2, xSize + 2];

        for (int x = 0; x < xSize + 2; x++)
        {
            mat[0, x] = 0;
            mat[zSize + 1, x] = 0;
        }
        for (int z = 0; z < zSize + 2; z++)
        {
            mat[z, 0] = 0;
            mat[z, xSize + 1] = 0;
        }
        for (int z = 1; z < zSize + 1; z++)
        {
            for (int x = 1; x < xSize + 1; x++)
            {
                mat[z, x] = Random.Range(0f, 0.5f);
                Debug.Log(mat[z, x]);
            }
        }
    }


    //public void createShape()
    //{


    //    vertices = new Vector3[(resxSize + 1) * (reszSize + 1)];
    //    triangles = new int[resxSize * reszSize * 6];
    //    uvs = new Vector2[vertices.Length];



    //    for (int i = 0, z = 0; z <= reszSize; z++)
    //    {
    //        for (int x = 0; x <= resxSize; x++)
    //        {
    //            float xValue;
    //            float zValue;
    //            float yValue;

    //            xValue = x / 2;
    //            zValue = z / 2;

    //            yValue = mat[z / 2, x / 2];

    //            vertices[i] = new Vector3(xValue, yValue, zValue);
    //            Debug.Log("added vert" + i);
    //            //uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
    //            i++;

    //        }


    //    }


    //    int vert = 0;
    //    int tris = 0;

    //    for (int z = 0; z < reszSize; z++)
    //    {
    //        for (int x = 0; x < resxSize; x++)
    //        {
    //            triangles[tris + 0] = vert + 0;
    //            triangles[tris + 1] = vert + resxSize + 1;
    //            triangles[tris + 2] = vert + 1;
    //            triangles[tris + 3] = vert + 1;
    //            triangles[tris + 4] = vert + resxSize + 1;
    //            triangles[tris + 5] = vert + resxSize + 2;

    //            vert++;
    //            tris += 6;
    //        }
    //        vert++;
    //    }





    //}
    //public void updateMesh()
    //{
    //    mesh.Clear();

    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;
    //    //mesh.uv = uvs;

    //    mesh.RecalculateNormals();

    //}
}
