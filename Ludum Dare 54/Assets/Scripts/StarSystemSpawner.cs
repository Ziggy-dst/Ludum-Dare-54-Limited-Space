using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarSystemSpawner : MonoBehaviour
{
    public float mapSizeX;

    public float mapSizeY;

    //base area is where the player starts playing, and no body should be spawned in there
    public float baseSizeX;

    public float baseSizeY;

    public float starSystemDistance;

    public int starSystemQuantity;

    public List<GameObject> starSystemPrefabList;

    public List<GameObject> starSystemInstancePool;

    public Transform starSystemsParent;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnStarSystems(starSystemQuantity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnStarSystems(int quantity)
    {
        float xpos = Random.Range(baseSizeX, mapSizeX) * (Random.Range(0, 2) * 2 - 1);
        float ypos = Random.Range(-mapSizeY, mapSizeY);
        Vector2 initialSpawnPos = new Vector2(xpos, ypos);
        GameObject initialBody;

        initialBody = Instantiate(starSystemPrefabList[Random.Range(0, starSystemPrefabList.Count)], initialSpawnPos,
            Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
        initialBody.transform.parent = starSystemsParent;
        starSystemInstancePool.Add(initialBody);

        for (int i = 0; i < quantity - 1;) 
        {
            xpos = Random.Range(-mapSizeX, mapSizeX);
            ypos = Random.Range(-mapSizeY, mapSizeY);
            
            if (xpos >= baseSizeX || xpos <= -baseSizeX || ypos >= baseSizeY || ypos <= -baseSizeY) 
            {
                Vector3 spawnPos = new Vector3(xpos, ypos, 0);

                bool isAllDistant = false; 
                
                foreach (GameObject body in starSystemInstancePool)
                {
                    if ((spawnPos - body.transform.position).magnitude > starSystemDistance)
                    {
                        isAllDistant = true;
                    }
                    else
                    {
                        isAllDistant = false;
                        break;
                    }
                }

                if (isAllDistant)
                {
                    GameObject starSystem;
                    starSystem = Instantiate(starSystemPrefabList[Random.Range(0, starSystemPrefabList.Count)], spawnPos, Quaternion.Euler(0,0,0));
                    starSystemInstancePool.Add(starSystem);
                    starSystem.transform.parent = starSystemsParent;
                    i++;
                }
            }
        }
    }
    
}
