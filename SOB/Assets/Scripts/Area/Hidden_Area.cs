using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hidden_Area : TouchObject
{
    private TilemapRenderer TR;

    private void Awake()
    {
        TR = this.GetComponent<TilemapRenderer>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TR.enabled = false;
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TR.enabled = true;
        }
    }
}
