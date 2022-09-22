using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapElement;
    public int xOffset = 50;
    public int zOffset = 50;
    public GameObject[] obstacleTypes;
    public Material mapMaterial;

    
    private int colorPropetyId;

    //for obstacle management
    private List<Queue<GameObject>> obstaclePoolList;
    private int currentObstacleCallCount = 1;
    private int currentObstacleItem = 0;


    //for map grid management
    private GameObject firstMapElement;
    private GameObject secondMapElemant;
    private GameObject thirdMapElement;
    private GameObject fourthMapElement;

    private int curZ;
    private int prevZ;

    void Start()
    {
        
        colorPropetyId = Shader.PropertyToID("_BaseColor");

        obstaclePoolList = new List<Queue<GameObject>>();
        fillObstaclePoolList();

        spawnInitialMapElements();
        

        curZ = (int)(Math.Floor(transform.position.z / zOffset));
        prevZ = curZ;

        
    }
    

    private void fillObstaclePoolList()
    {
        foreach(GameObject obstacle in obstacleTypes)
        {
            Queue<GameObject> q = new Queue<GameObject>();
            for(int i = 0; i < obstacle.GetComponent<ObstacleSettings>().totalObstaclesAtInstance; i++)
            {
                q.Enqueue(Instantiate(obstacle) as GameObject);
            }
            obstaclePoolList.Add(q);
        }
    }

    void Update()
    {
        curZ = (int)(Math.Floor(transform.position.z / zOffset));

        if ( curZ != prevZ)
        {
            rearrangeMapElements();
            prevZ = curZ;
        }

        
        //mapMaterial.SetColor(colorPropetyId, new Color(mapMaterial.color.r, mapMaterial.color.g,(float)Math.Sin(transform.position.z*0.001)  , mapMaterial.color.a));
        
    }

    private void rearrangeMapElements()
    {
        
        GameObject temp = firstMapElement;
        firstMapElement = secondMapElemant;
        secondMapElemant = thirdMapElement;
        thirdMapElement = fourthMapElement;
        fourthMapElement = temp;
        fourthMapElement.transform.position = new Vector3(transform.position.x - xOffset, 0, (curZ + 2) * zOffset);
        spawnObstacles(transform.position.x - xOffset, (curZ + 2) * zOffset);

    }

    private void spawnInitialMapElements()
    {
        firstMapElement= Instantiate(mapElement, new Vector3(0, 0, -zOffset), Quaternion.identity);
        secondMapElemant = Instantiate(mapElement, new Vector3(0, 0, 0), Quaternion.identity);
        thirdMapElement= Instantiate(mapElement, new Vector3(0, 0, zOffset), Quaternion.identity);
        spawnObstacles(0,  zOffset);
        fourthMapElement = Instantiate(mapElement, new Vector3(0, 0, 2*zOffset), Quaternion.identity);
        spawnObstacles(0,  2 * zOffset);
    }

    private void spawnObstacles(float x,float z)
    {
        if (currentObstacleCallCount % 4 == 0)
        {
            currentObstacleItem = (currentObstacleItem + 1) % obstaclePoolList.Count;
        }
        
        Queue<GameObject> currentObstacleQueue = obstaclePoolList[currentObstacleItem];
        
        for (int i = 0; i < currentObstacleQueue.Peek().GetComponent<ObstacleSettings>().totalObstaclesAtInstance / 4; i++)
        {
            float xPlace = x+5*UnityEngine.Random.Range(0f, 50f);
            float zPlace = z+5*UnityEngine.Random.Range(0f, 25f);

            GameObject tempObstacle = currentObstacleQueue.Dequeue();
            tempObstacle.transform.position = new Vector3(xPlace, tempObstacle.transform.position.y, zPlace);
            currentObstacleQueue.Enqueue(tempObstacle);
        }
        currentObstacleCallCount++;
    }
}

