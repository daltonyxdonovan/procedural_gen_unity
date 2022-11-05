using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animate_sprite : MonoBehaviour
{
    public Texture first_frame;
    public Texture second_frame;
    public RawImage image;
    public int ticker = 0;
    // Start is called before the first frame update
    void Start()
    {
        ticker = Random.Range(0,100);
    }

    // Update is called once per frame
    void Update()
    {
        ticker++;
        //every 200 ticks, change the image to the other frame
        if (ticker == 100)
        {
            if (image.texture == first_frame)
            {
                image.texture = second_frame;
            }
            else
            {
                image.texture = first_frame;
            }
            ticker = 0;
        }

        //face the camera by rotating only the y axis
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

    }
}
