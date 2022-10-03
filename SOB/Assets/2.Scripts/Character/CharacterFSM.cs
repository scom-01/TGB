using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    private Rigidbody2D rb = null;

    private void Awake()
    {
        Init();
    }

    private bool Init()
    {
        rb = this.GetComponent<Rigidbody2D>();

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        if (CanJump())
        {
            //점프가능
            Jump();
        }

    }

    bool MoveChar()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 0.01f);
            return true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 0.01f);
            return true;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            CharacterValue.Instance.ShowDebug();
        }

        return false;
    }

    //점프 상태일 때 True
    bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && CharacterValue.Instance.GetJump())
        {
            rb.velocity = Vector2.up * 10f /*Jump Param*/;
            Debug.Log("Jump");
            CharacterValue.Instance.SetJump(false);
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
        if (Mathf.Abs(rb.velocity.y) <= 0.01f)
        {
            CharacterValue.Instance.SetJump(true);
            return true;
        }

        return false;
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
