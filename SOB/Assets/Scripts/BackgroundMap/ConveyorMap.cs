using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ConveyorMap : MonoBehaviour
{
    public SpriteRenderer[] Layers;

    public float BetweenDistance;
    public float ConveyorLine;
    [Min(0.1f)]
    public float Speed = 1f;
    public bool isLeft = true;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Layers = GetComponentsInChildren<SpriteRenderer>();
        
        for(int i =0; i <Layers.Length;i++)
        {
            if(isLeft)
            {
                Layers[i].gameObject.transform.Translate(Vector2.right * BetweenDistance * i);
            }
            else
            {
                Layers[i].gameObject.transform.Translate(Vector2.left * BetweenDistance * i);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Layers == null)
            return;

        for (int i = 0; i < Layers.Length; i++)
        {
            if(isLeft)
            {
                Layers[i].gameObject.transform.Translate(Vector2.left * 0.1f * Speed);
                if (Layers[i].gameObject.transform.position.x < -ConveyorLine)
                {
                    Layers[i].gameObject.transform.Translate(Vector2.right * BetweenDistance * 2f);
                }
            }
            else
            {
                Layers[i].gameObject.transform.Translate(Vector2.right * 0.1f * Speed);
                if (Layers[i].gameObject.transform.position.x < -ConveyorLine)
                {
                    Layers[i].gameObject.transform.Translate(Vector2.left * BetweenDistance * 2f);
                }
            }
        }
    }
}

