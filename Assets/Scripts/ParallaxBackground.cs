using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    private float lenghth;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");

        lenghth = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;      
    }

    private void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(xPosition + distanceToMove, cam.transform.position.y);

        if (distanceMoved > xPosition * lenghth)
            xPosition = xPosition + lenghth;
        else if (distanceMoved < xPosition - lenghth)
            xPosition = xPosition - lenghth;
    }
}
