using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceTrashSpawner : MonoBehaviour
{
    public float maxDistanceX;
    
    public float maxDistanceY;
    
    //base area is where the player starts playing, and no body should be spawned in there
    public float viewSizeX;

    public float viewSizeY;

    public float spawnInterval;

    public List<GameObject> trashPrefabList;

    public Transform trashParent;

    private Transform playerTransform;

    private Vector2 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("SpawnBodies",5, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerTransform.position;
        if (GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            CancelInvoke();
        }
    }

    public void SpawnBodies()
    {
        for (int i = 0; i < 1;)
        {
            float xpos = Random.Range(-maxDistanceX, maxDistanceX);
            float ypos = Random.Range(-maxDistanceY, maxDistanceY);
            // print(new Vector2(xpos,ypos));
            
            if (xpos >= viewSizeX || xpos <= -viewSizeX || ypos >= viewSizeY || ypos <= -viewSizeY)
            {
                Vector3 spawnPos = new Vector3(xpos + playerPosition.x, ypos + playerPosition.y, 0);
                
                GameObject trash;
                trash = Instantiate(trashPrefabList[Random.Range(0, trashPrefabList.Count)], spawnPos,
                    Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
                // print("Trash Spawned");
                trash.transform.parent = trashParent;

                i++;
            }
        }
    }
}
