using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float start, end, below, above;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // lay vi tri cua player
            var playerx = player.transform.position.x;
            var playery = player.transform.position.y;
            // lay vi tri cua camera
            var camx = transform.position.x;
            var camy = transform.position.y;
            var camz = transform.position.z;
            //chieu ngang
            if (playerx > start && playerx < end)
            {
                camx = playerx;
            }
            else
            {
                if (playerx < start)
                {
                    camx = start;
                }
                if (playerx > end)
                {
                    camx = end;
                }
            }
            //chieu doc
            if (playery > below && playery < above)
            {
                camy = playery;
            }
            else
            {
                if (playery < below)
                {
                    camy = below;
                }
                if (playery > above)
                {
                    camy = above;
                }
            }
            transform.position = new Vector3(camx, camy, camz);
        }
    }

}
