using System.Collections;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private PlayerAttackController playerAtkCtrlr;

    void Awake()
    {
        ReferenceReset();
    }

    void ReferenceReset()
    {
        //스크립트 참조변수
        playerAtkCtrlr = GetComponent<PlayerAttackController>();
    }

    void Update()
    {
    }
}