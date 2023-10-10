 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionGameManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public ActionPlayer startCount;
    
    public int maxClouds;
    public int minClouds;

    float timeToCloud;

    float timeToCloudCounter;

    BoxCollider2D worldBounds;

    Vector3 minBounds;
    Vector3 maxBounds;

    public List<GameObject> allClouds = new List<GameObject>();

    private void Start()
    {
        maxClouds = 15; minClouds = 15;
        worldBounds = GetComponent<BoxCollider2D>();
        minBounds = worldBounds.bounds.min;
        maxBounds = worldBounds.bounds.max;
    }

    private void Update()
    {
        Debug.Log(allClouds.Count);
        if (startCount.gameStartCountDown <= 0)
        {
            timeToCloud = 0; 
        } else { timeToCloud = 3; }

        timeToCloudCounter += Time.deltaTime;
        if (timeToCloudCounter > timeToCloud || allClouds.Count < minClouds)
        {
            if (allClouds.Count < maxClouds)
            {
                MakeACloud();
                timeToCloudCounter = 0;
            }
        }

        //allClouds = allClouds.Where(x => x != null).ToList();
        allClouds.RemoveAll(delegate (GameObject o) { return o == null; }); //destroys any NULL instance of an object within the allCLouds list


    }

    void MakeACloud()
    {
        GameObject newCloud = Instantiate(cloudPrefab);
        Vector3 newPos = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            0f  
            );
        newCloud.transform.localPosition = newPos;
        allClouds.Add(newCloud);
    }
}
