using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Inst.MoveTitle();
    }
}
