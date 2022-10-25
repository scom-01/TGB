using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rb = null;
    private BoxCollider2D boxcollider2d = null;

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
        boxcollider2d = this.GetComponent<BoxCollider2D>();
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            CharacterValue.Instance.ShowDebug();
        }

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
            //���ݰ���
        }

        
    }

    private void LateUpdate()
    {
        if (CanJump())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W");
                //��������
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

    //���� ������ �� True

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
            rb.velocity = Vector2.up * DefaultJumpPower * CharacterValue.Instance.GetJumpPower();

            return true;
        }
        return false;
    }

    bool CanMove()
    {
        //�������� ������ ����
        return CharacterValue.Instance.GetMove();
    }

    bool CanJump()
    {
        //������ ������ ����
        if(CharacterValue.Instance.GetJump())
        {
            CharacterValue.Instance.SetJump(true);
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            //������ Boxcast�� �����Ͽ����� ���� �ϴ� �� ���� 0.01���� �� ���� ���� CanJump�� �ν��Ͽ� ���� ������ �����ϴµ� ��ٷο� ������
            JumpCount = 2;
            CharacterValue.Instance.SetJump(true);
            Debug.Log("Coll layer 15");
        }
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
        //������ ������ ����        
        return CharacterValue.Instance.GetAttack();
    }

    bool Death()
    {
        //��� ���� �Ǵ�        
        return CharacterValue.Instance.GetDeath();
    }

    bool Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause");
            /*GlobalValue.Instance.SetCharacterDelta((GlobalValue.Instance.GetCharacterDelta() == 0.02f) ? 0.0f : 0.02f);
            GlobalValue.Instance.SetPause(GlobalValue.Instance.GetCharacterDelta() == 0.0f ? false : true);*/
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
        }

        return IsPause();
    }

    bool IsPause()
    {
        //ȯ�漳�� �� ���� ���� ����        
        return GlobalValue.Instance.GetPause();
    }

    bool CanTakeDamage()
    {        
        return CharacterValue.Instance.GetCanTakeDamage();
    }

    bool TakeDamage(int damage)
    {
        if(CanTakeDamage())
        {
            if (damage >= CharacterValue.Instance.GetHealth())
            {
                CharacterValue.Instance.SetHealth(0);
                CharacterValue.Instance.SetDeath(true);
                //��� �ִϸ��̼� ȣ��
            }
            else
            {
                CharacterValue.Instance.SetHealth(CharacterValue.Instance.GetHealth() + damage);

                //������ �ǰ� �ִϸ��̼� ȣ��
            }
            CharacterValue.Instance.SetCanTakeDamage(false);    //�ǰ� �Ŀ��� ���� �ð� �ǰݹ��� �ʵ���
            return true;
        }
        return false;
    }
}
