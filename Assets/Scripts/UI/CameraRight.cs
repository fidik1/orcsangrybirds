using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class CameraRight : MonoBehaviour
{
    new Transform camera;
    public static bool isDown;
    [SerializeField] BirdController controller;

    void Start()
    {
        camera = Camera.main.transform;
    }

    public void OnPointerDown()
    {   
        isDown = true;
    }
    
    public void OnPointerUp()
    {
        isDown = false;
    }

    void Update()
    {
        if (isDown && camera.position.x < 4.75f)
            camera.position = new Vector3(camera.position.x + 0.25f, camera.position.y, camera.position.z);
        else if (!isDown && camera.position.x > 0 && controller.slingshotState != SlingshotState.BirdFlying)
            camera.position = new Vector3(camera.position.x - 0.5f, camera.position.y, camera.position.z);
    }
}
