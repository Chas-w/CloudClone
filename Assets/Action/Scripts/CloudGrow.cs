using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGrow : MonoBehaviour
{
    public GameObject[] cloudSizes;

    public float timeToGrow;

    float timeToGrowCounter;

    int timesGrown = 0;

    private void Update()
    {
        if (timesGrown < cloudSizes.Length - 1)
        {
            timeToGrowCounter += Time.deltaTime;
            if (timeToGrowCounter > timeToGrow)
            {
                GrowCloud();
                timeToGrowCounter = 0;
            }
        }
    }

    void GrowCloud()
    {
        cloudSizes[timesGrown].SetActive(false);
        timesGrown++;
        cloudSizes[timesGrown].SetActive(true);
    }

}
