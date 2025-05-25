using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    //스크립트 참조변수
    PlayerData playerData;

    [SerializeField] LayerMask targetLayer; //히트박스가 감지할 대상의 Layer

    List<GameObject> damagedTargets = new List<GameObject>(); //이미 데미지를 입힌 대상 목록

    void Awake()
    {
        GameObject playerRoot = transform.root.gameObject; //PlayerData 스크립트가 붙어 있는 게임 오브젝트 찾기
        playerData = playerRoot.GetComponent<PlayerData>(); // PlayerData.cs 참조
    }

    public void OnEnable() //히트박스 활성화될 때마다 호출
    {
        damagedTargets.Clear(); //히트박스 활성화 되기 이전에 데미지 받았던 대상 목록 초기화
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerData != null && ((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            if (!damagedTargets.Contains(other.gameObject)) //이미 데미지를 입힌 대상인지 확인
            {
                //대상 오브젝트에서 데미지 입히는 함수 호출
                GoblinData targetGoblin = other.GetComponent<GoblinData>();

                //GoblinData 컴포넌트가 존재한다면
                if (targetGoblin != null)
                {
                    targetGoblin.TakeDamage(playerData.meleeAtkDamage); //고블린에게 플레이어 공격력만큼의 데미지 주기
                    damagedTargets.Add(other.gameObject); // 데미지 입힌 대상 목록에 추가해 한 번의 공격에 여러번 데미지 주는 것을 방지
                }
            }
        }
    }
}