using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    // Start is called before the first frame update
    public float start, end, below, above;
    public GameObject player;
    private SpriteRenderer sRenderer;
    void Start()
    {
        sRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // tao hieu ung di chuyen cho background
        sRenderer.material.mainTextureOffset = new Vector2(0f, Time.deltaTime * 0.5f);

        if(player != null)
        {
            // lay vi tri cua player
            var playerx = player.transform.position.x;
            var playery = player.transform.position.y;
            // lay vi tri cua camera
            var bgX = transform.position.x;
            var bgY = transform.position.y;
            var bgZ = transform.position.z;
            //chieu ngang
            if (playerx > start && playerx < end)
            {
                bgX = playerx;
            }
            else
            {
                if (playerx < start)
                {
                    bgX = start;
                }
                if (playerx > end)
                {
                    bgX = end;
                }
            }
            //chieu doc
            if (playery > below && playery < above)
            {
                bgY = playery;
            }
            else
            {
                if (playery < below)
                {
                    bgY = below;
                }
                if (playery > above)
                {
                    bgY = above;
                }
            }
            transform.position = new Vector3(bgX, bgY, bgZ);
        }
    }
}
