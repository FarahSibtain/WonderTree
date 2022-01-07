using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioForcer : MonoBehaviour
{
    // set the desired aspect ratio (the values in this example are
    // hard-coded for 16:9, but you could make them into public
    // variables instead so you can set them at design time)
    float targetaspect = 1024.0f / 768.0f;

    // determine the game window's current aspect ratio

    float windowaspect;

    // current viewport height should be scaled by this amount

    float scaleheight = 0;

    // obtain camera component so we can modify its viewport

    Camera camera = null;

    // Use this for initialization
    void Start()
    {
        scaleheight = windowaspect / targetaspect;

        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float calScaleheight = (float)Screen.width / (float)Screen.height;

        if (scaleheight != calScaleheight)
        {
            scaleheight = calScaleheight;
            UpdateCameraRect(scaleheight);        
        }
    }
    
    void UpdateCameraRect(float scaleheight)
    {
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;

            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;

        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;

            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
