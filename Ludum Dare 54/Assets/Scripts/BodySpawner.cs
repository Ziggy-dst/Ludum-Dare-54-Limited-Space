using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodySpawner : MonoBehaviour
{
    public float mapSizeX;

    public float mapSizeY;

    //base area is where the player starts playing, and no body should be spawned in there
    public float baseSizeX;

    public float baseSizeY;

    public float bodyDistance;

    public int bodyQuantity;

    public List<GameObject> bodyPrefabList;

    public List<GameObject> bodyInstancePool;

    public Transform bodiesParent;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBodies(bodyQuantity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBodies(int quantity)
    {
        float xpos = Random.Range(baseSizeX, mapSizeX) * (Random.Range(0, 2) * 2 - 1);
        float ypos = Random.Range(-mapSizeY, mapSizeY);
        Vector2 initialSpawnPos = new Vector2(xpos, ypos);
        GameObject initialBody;

        initialBody = Instantiate(bodyPrefabList[Random.Range(0, bodyPrefabList.Count)], initialSpawnPos,
            Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
        initialBody.transform.parent = bodiesParent;
        bodyInstancePool.Add(initialBody);

        for (int i = 0; i < quantity - 1;) 
        {
            xpos = Random.Range(-mapSizeX, mapSizeX);
            ypos = Random.Range(-mapSizeY, mapSizeY);
            
            if (xpos >= baseSizeX || xpos <= -baseSizeX || ypos >= baseSizeY || ypos <= -baseSizeY) 
            {
                Vector3 spawnPos = new Vector3(xpos, ypos, 0);

                bool isAllDistant = false; 
                
                foreach (GameObject body in bodyInstancePool)
                {
                    if ((spawnPos - body.transform.position).magnitude > bodyDistance)
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
                    GameObject body;
                    body = Instantiate(bodyPrefabList[Random.Range(0, bodyPrefabList.Count)], spawnPos, Quaternion.Euler(0,0,0));
                    bodyInstancePool.Add(body);
                    body.transform.parent = bodiesParent;
                    body.transform.DORotate(new Vector3(0, 0, 360) * (Random.Range(0, 2) * 2 - 1),
                            Random.Range(30f, 90f), RotateMode.FastBeyond360)
                        .SetLoops(-1).SetEase(Ease.Linear);
                    i++;
                }
            }
        }
    }
    
}
