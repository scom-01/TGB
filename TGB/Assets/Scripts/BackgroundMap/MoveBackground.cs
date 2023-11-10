using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveBackground : MonoBehaviour
{
    /// <summary>
    /// 거리감
    /// </summary>
    [Tooltip("수가 낮을 수록 근접한 움직임 표현")]
    [SerializeField] private Vector2 Distance;

    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;
    private Transform camTransform;
    private Vector3 lastCamPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start()
    {
        camTransform = Camera.main.transform;
        if (GameManager.Inst.StageManager.Cam != null)
        {
            camTransform = GameManager.Inst.StageManager.Cam.transform;
        }
        lastCamPos = camTransform.position;
        if (this.GetComponent<SpriteRenderer>().drawMode != SpriteDrawMode.Tiled)
        {
            this.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
        }
        this.GetComponent<SpriteRenderer>().size = new Vector2(this.GetComponent<SpriteRenderer>().size.x * 3, this.GetComponent<SpriteRenderer>().size.y);
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = this.GetComponent<SpriteRenderer>().size.x * transform.localScale.x;
        textureUnitSizeY = this.GetComponent<SpriteRenderer>().size.y * transform.localScale.y;
        //textureUnitSizeX = texture.width * transform.localScale.x / sprite.pixelsPerUnit;
        //textureUnitSizeY = texture.height * transform.localScale.y / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        if (camTransform == null)
            return;

        Vector3 deltaMovement = camTransform.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * Distance.x, deltaMovement.y * Distance.y, 0);
        lastCamPos = camTransform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (infiniteHorizontal)
        {
            if (Mathf.Abs(camTransform.position.x - transform.position.x) > (textureUnitSizeX / 3))
            {
                float offsetPosX = (camTransform.position.x - transform.position.x) % (textureUnitSizeX / 3);
                transform.position = new Vector3(camTransform.position.x + offsetPosX, transform.position.y);
            }
        }
        if (infiniteVertical)
        {
            if (Mathf.Abs(camTransform.position.y - transform.position.y) > textureUnitSizeY)
            {
                float offsetPosY = (camTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, offsetPosY + camTransform.position.y);
            }
        }
    }
}
