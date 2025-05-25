using UnityEngine;
using System.Collections.Generic;

public class PlayerMeleeHitbox : MonoBehaviour
{
    //스크립트 참조변수
    public PlayerAttackController playerAtkCtrlr;
    private List<Collider2D> colliderHitThisActivation; //현재 활성화된 동안 이미 충돌한 콜라이더 목록
    PlayerData playerData;
    int atkDamage;

    //참조변수

    void Awake()
    {
        atkDamage = playerData.meleeAtkDamage;
        ReferenceReset();
    }

    void ReferenceReset()
    {
        playerData = GetComponent<PlayerData>();
        playerAtkCtrlr = GetComponent<PlayerAttackController>();
        colliderHitThisActivation = new List<Collider2D>();
    }

    void OnEnable()
    {
        colliderHitThisActivation.Clear(); //히트박스가 활성화될 때마다 이전에 충돌한 콜라이더 목록 초기화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (colliderHitThisActivation.Contains(other)) return; //이미 처리된 충돌이므로 반환
    }
}
