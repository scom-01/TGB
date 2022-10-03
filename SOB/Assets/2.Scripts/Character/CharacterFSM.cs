using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rb = null;
    private BoxCollider2D boxcollider2d = null;

    private int JumpCount = 1;
    private float DefaultMoveSpeed = 7.0f;
    private float DefaultJumpPower = 20.0f;

    private void Awake()
    {
        Init();
    }

    private bool Init()
    {
        rb = this.GetComponent<Rigidbody2D>();
        boxcollider2d = this.GetComponent<BoxCollider2D>();
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CharacterValue.Instance.ShowDebug();
        }

        if (Pause())
        {
            return;
        }

        if (Death())
        {
            return;
        }

        if (CanMove())
        {
            if (MoveChar())
            {
                Debug.Log("Move");
            }
        }

        if (CanAttack())
        {
            //공격가능
        }

        
    }

    private void LateUpdate()
    {
        if (CanJump())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W");
                //점프가능
                //Jump();
                JumpCal();
            }
            Debug.Log("CanJump true");
        }

    }

    bool MoveChar()
    {
        float dirX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(dirX * DefaultMoveSpeed *  CharacterValue.Instance.GetMoveSpeed(), rb.velocity.y);
        if (rb.velocity.magnitude > 0.0f)
        {
            return true;
        }
        return false;
    }

    //점프 상태일 때 True

    void JumpCal()
    {
        if (CharacterValue.Instance.GetJump())
        {
            if (JumpCount > 0) 
            { 
                Jump();
                if(JumpCount == 2)
                {
                    Debug.Log("Jump");
                }
                else if(JumpCount == 1)
                {
                    Debug.Log("Double Jump");
                }
                JumpCount--;
            }
            else
            {
                CharacterValue.Instance.SetJump(false);
            }
        }
    }
    bool Jump()
    {
        if (CharacterValue.Instance.GetJump())
        {
            rb.velocity = Vector2.up * DefaultJumpPower* CharacterValue.Instance.GetJumpPower();
            //rb.AddForce(transform.up * DefaultJumpPower * CharacterValue.Instance.GetJumpPower());

            return true;
        }
        return false;
    }

    bool CanMove()
    {
        //움직임이 가능한 상태
        if (CharacterValue.Instance.GetMove())
        {
            return true;
        }
        return false;
    }

    bool CanJump()
    {
        //점프가 가능한 상태
        if(CharacterValue.Instance.GetJump())
        {
            //JumpCount = 2;
            CharacterValue.Instance.SetJump(true);
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            JumpCount = 2;
            CharacterValue.Instance.SetJump(true);
            Debug.Log("Coll layer 15");
        }
    }

    private void OnDrawGizmos()
    {
        if(boxcollider2d != null)
        {
            Gizmos.DrawCube(boxcollider2d.bounds.center + Vector3.down *0.01f, boxcollider2d.bounds.size);
        }
    }

    bool CanAttack()
    {
        //공격이 가능한 상태
        if (CharacterValue.Instance.GetAttack())
        {
            return true;
        }
        return false;
    }

    bool Death()
    {
        //사망 상태 판단
        if (CharacterValue.Instance.GetDeath())
        {
            return true;
        }
        return false;
    }

    bool Pause()
    {
        //환경설정 및 게임 정지 상태
        if(GlobalValue.Instance.GetPause())
        {
            return true;
        }
        return false;
    }
}
