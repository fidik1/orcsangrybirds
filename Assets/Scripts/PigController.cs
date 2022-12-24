using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigController : MonoBehaviour
{
    public List<GameObject> pigs = new List<GameObject>();
    public GameObject text;

    void Update()
    {
        if (pigs.Count <= 0)
            text.SetActive(true);
    }
}
