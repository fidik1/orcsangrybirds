using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTheFirstBirds : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
    [SerializeField] BirdController birdController;
    int pickedBirds;
    public static bool isPicked;

    public void BuyBird(int bird)
    {
        if (pickedBirds < 3)
        {
            GameObject birdPrefab = Instantiate(birds[bird], new Vector2(0, -15), Quaternion.identity);
            birdController.birdPrefabs.Add(birdPrefab.GetComponent<Rigidbody2D>());
            pickedBirds++;
        }
        if (pickedBirds >= 3)
        {
            isPicked = true;
            gameObject.SetActive(false);
        }
    }
}
