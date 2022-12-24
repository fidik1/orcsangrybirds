using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
    [SerializeField] BirdController birdController;
    [SerializeField] Text countBirds;
    [SerializeField] Text balance;
    [SerializeField] int[] price;
    int buyedBirds;

    void Start()
    {
        buyedBirds = 0;
    }

    void Update()
    {
        ShowCurrentBirds();
        ShowBalance();
    }

    public void BuyBird(int bird)
    {
        if (buyedBirds < 2 && Balance.balance >= price[bird])
        {
            GameObject birdPrefab = Instantiate(birds[bird], new Vector2(0, -15), Quaternion.identity);
            birdController.birdPrefabs.Add(birdPrefab.GetComponent<Rigidbody2D>());
            buyedBirds++;
            Balance.balance -= price[bird];
        }
    }

    public void AdBird(int bird)
    {
        // ad
    }

    void ShowCurrentBirds()
    {
        countBirds.text = "x" + birdController.birdPrefabs.Count.ToString();
    }

    void ShowBalance()
    {
        balance.text = "balance: " + Balance.balance.ToString();
    }
}
