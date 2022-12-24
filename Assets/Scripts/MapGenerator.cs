using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Transform parentPigs;
    [SerializeField] Transform parentBricks;
    [SerializeField] GameObject[] prefabsPigs;
    [SerializeField] GameObject[] prefabsBricks;
    [SerializeField] PigController pigController;

    [SerializeField] float[] bricksOffsetX;

    GameObject lastBrick;
    List<float> brickPosX = new List<float>();

    void Start()
    {
        lastBrick = new GameObject();
        lastBrick.transform.position = new Vector3(5.5f, 0);
        brickPosX.Add(0);
        brickPosX.Add(0);
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i <= Random.Range(2, 5); i++)
        {
            if (Random.Range(0, 3) != 0 || i == 1)
            {
                int randPig = Random.Range(0, prefabsPigs.Length);
                int randBrick = Random.Range(0, prefabsBricks.Length);
                GameObject pig;
                brickPosX[0] = lastBrick.transform.position.x + bricksOffsetX[0];
                brickPosX[1] = lastBrick.transform.position.x + bricksOffsetX[1];
                if (i == 1)
                {
                    pig = Instantiate(prefabsPigs[randPig], new Vector2(7.29f, -2.94f), Quaternion.identity, parentPigs);
                    pigController.pigs.Add(pig);
                }

                if (Random.Range(0, 4) != 3)
                {
                    if (randBrick == 0)
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -3.83f), Quaternion.identity, parentBricks);
                    else
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[1], -3.83f), Quaternion.identity, parentBricks);
                }
                else if (Random.Range(0, 4) == 3)
                {
                    if (randBrick == 0)
                    {
                        Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -3.83f), Quaternion.identity, parentBricks);
                        Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -2.73f), Quaternion.identity, parentBricks);
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -1.63f), Quaternion.identity, parentBricks);
                        
                        pig = Instantiate(prefabsPigs[randPig], new Vector2(brickPosX[0] + 0.45f, -2.73f), Quaternion.identity, parentPigs);
                        pigController.pigs.Add(pig);
                        pig = Instantiate(prefabsPigs[randPig], new Vector2(brickPosX[0] + 0.45f, -1.63f), Quaternion.identity, parentPigs);
                        pigController.pigs.Add(pig);
                    }
                    else
                    {
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[1], -3.83f), Quaternion.identity, parentBricks);

                        pig = Instantiate(prefabsPigs[randPig], new Vector2(brickPosX[1], -3.83f), Quaternion.identity, parentPigs);
                        pigController.pigs.Add(pig);
                    }
                }
                else
                {
                    if (randBrick == 0)
                    {
                        Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -3.83f), Quaternion.identity, parentBricks);
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[0], -2.73f), Quaternion.identity, parentBricks);

                        pig = Instantiate(prefabsPigs[randPig], new Vector2(brickPosX[0] + 0.45f, -2.73f), Quaternion.identity, parentPigs);
                        pigController.pigs.Add(pig);
                    }
                    else
                    {
                        lastBrick = Instantiate(prefabsBricks[randBrick], new Vector2(brickPosX[1], -3.83f), Quaternion.identity, parentBricks);

                        pig = Instantiate(prefabsPigs[randPig], new Vector2(brickPosX[1], -3.83f), Quaternion.identity, parentPigs);
                        pigController.pigs.Add(pig);
                    }
                }
                
            }
        }
    }
}