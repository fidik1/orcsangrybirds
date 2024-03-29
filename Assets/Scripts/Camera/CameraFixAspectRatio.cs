using UnityEngine;

public class CameraFixAspectRatio : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float aspect = Mathf.Round(camera.aspect * 100f) / 100f;

        //this is to be altered different Windows Phone 8 aspect ratios
        //there should be a better way of doing this
        if (aspect == 0.6f) //WXGA or WVGA
            camera.orthographicSize = 5;
        else if (aspect == 0.56f) //720p
        {
            camera.orthographicSize = 4.6f;
        }
    }
}