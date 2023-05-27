using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBackground : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float MoveSpeed;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null)
            return;

        transform.position = new Vector3(cam.transform.position.x* MoveSpeed, cam.transform.position.y* MoveSpeed, 0);
    }
}
