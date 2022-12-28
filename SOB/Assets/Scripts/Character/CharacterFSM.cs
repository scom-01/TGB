using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rb = null;
    private BoxCollider2D boxcollider2d = null;
    private RaycastHit2D raycastbox;
    private CharacterValue CV = null;

    public Animator Anim { get; private set; }



    private int     JumpCount = 1;
    private float   DefaultMoveSpeed = 7.0f;
    private float   DefaultJumpPower = 20.0f;

    private float   PauseVelocityX = 0.0f;
    private float   PauseVelocityY = 0.0f;

    private void Awake()
    {
        Init();
    }

    private bool Init()
    {
        rb = this.GetComponent<Rigidbody2D>();
        CV = this.GetComponent<CharacterValue>(); 
        boxcollider2d = this.GetComponent<BoxCollider2D>();
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Pause())
        {
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            CV.ShowDebug();
        }*/

        if(GlobalValue.Instance.GetPause())
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
        /*if (CanJump())
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

        if(Input.GetKeyDown(KeyCode.B))
        {
            transform.position = Vector3.zero;
        }*/

    }

    bool MoveChar()
    {
        //float dirX = Input.GetAxis("Horizontal");

        //rb.velocity = new Vector2(dirX * DefaultMoveSpeed * CV.GetMoveSpeed(), rb.velocity.y);
        if (rb.velocity.magnitude > 0.0f)
        {
            return true;
        }
        return false;
    }

    //점프 상태일 때 True

    void JumpCal()
    {
        if (CV.GetJump())
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

            if (JumpCount == 0)
            {
                CV.SetJump(false);
            }
        }
    }
    bool Jump()
    {
        if (CV.GetJump())
        {
            rb.velocity = Vector2.up * DefaultJumpPower * CV.GetJumpPower();

            return true;
        }
        return false;
    }

    bool CanMove()
    {
        //움직임이 가능한 상태
        return CV.GetMove();
    }

    bool CanJump()
    {
        //점프가 가능한 상태
        RaycastHit2D hit = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, .1f,LayerMask.GetMask("Ground"));
        
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 0.1f, new Color(0, 1, 0), LayerMask.GetMask("Ground"));
            Debug.Log("RayCastHit");
            JumpCount = 2;
            CV.SetJump(true);
            return true;
        }

        if(CV.GetJump())
        {
            CV.SetJump(true);
            return true;
        }

        return false;
    }

    

    private void OnDrawGizmos()
    {
        if(boxcollider2d != null)
        {
            Gizmos.DrawCube(boxcollider2d.bounds.center + Vector3.down * 0.01f, boxcollider2d.bounds.size);
        }
    }

    bool CanAttack()
    {
        //공격이 가능한 상태        
        return CV.GetAttack();
    }

    bool Death()
    {
        //사망 상태 판단        
        return CV.GetDeath();
    }

    bool Pause()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause");
            *//*GlobalValue.Instance.SetCharacterDelta((GlobalValue.Instance.GetCharacterDelta() == 0.02f) ? 0.0f : 0.02f);
            GlobalValue.Instance.SetPause(GlobalValue.Instance.GetCharacterDelta() == 0.0f ? false : true);*//*
            if(rb != null)
            {
                if(GlobalValue.Instance.GetCharacterDelta() == 0.02f)
                {
                    GlobalValue.Instance.SetCharacterDelta(0.0f);
                    GlobalValue.Instance.SetPause(true);
                    PauseVelocityX = rb.velocity.x;
                    PauseVelocityY = rb.velocity.y; 
                    rb.bodyType = RigidbodyType2D.Static;
                } 
                else
                {
                    GlobalValue.Instance.SetCharacterDelta(0.02f);
                    GlobalValue.Instance.SetPause(false);
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.velocity = new Vector3(PauseVelocityX, PauseVelocityY, 0f);
                }
            }
        }*/

        return IsPause();
    }

    bool IsPause()
    {
        //환경설정 및 게임 정지 상태        
        return GlobalValue.Instance.GetPause();
    }

    bool CanTakeDamage()
    {        
        return CV.GetCanTakeDamage();
    }

    bool TakeDamage(int damage)
    {
        if(CanTakeDamage())
        {
            if (damage >= CV.GetHealth())
            {
                CV.SetHealth(0);
                CV.SetDeath(true);
                //사망 애니메이션 호출
            }
            else
            {
                CV.SetHealth(CV.GetHealth() + damage);

                //데미지 피격 애니메이션 호출
            }
            CV.SetCanTakeDamage(false);    //피격 후에는 일정 시간 피격받지 않도록
            return true;
        }
        return false;
    }

    //Physics~    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        /*if (coll.gameObject.layer == 15)
        {
            //점프를 Boxcast로 구현하였더니 점프 하는 그 순간 0.01정도 떠 있을 때도 CanJump로 인식하여 더블 점프를 구현하는데 까다로워 변경함
            JumpCount = 2;
            CV.SetJump(true);
            Debug.Log("Coll layer 15");
        }*/
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 16)
        {
            Debug.Log("SlowGround");
        }
    }
    //~Physics
}
