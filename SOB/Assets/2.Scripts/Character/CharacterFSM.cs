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
            //���ݰ���
        }

        if (CanJump())
        {
            //��������
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

        return false;
    }

    //���� ������ �� True
    bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && GlobalValue.Instance.GetJump())
        {
            rb.velocity = Vector2.up * 10f;
            Debug.Log("Jump");
            GlobalValue.Instance.SetJump(false);
            return true;
        }

        return false;
    }

    bool CanMove()
    {
        //�������� ������ ����

        return true;
    }

    bool CanJump()
    {
        //������ ������ ����
        if (Mathf.Abs(rb.velocity.y) <= 0.01f)
        {
            GlobalValue.Instance.SetJump(true);
            return true;
        }

        return false;
    }

    bool CanAttack()
    {
        //������ ������ ����
        return true;
    }

    bool Death()
    {
        //��� ���� �Ǵ�
        return false;
    }

    bool Pause()
    {
        //ȯ�漳�� �� ���� ���� ����
        return false;
    }
}
