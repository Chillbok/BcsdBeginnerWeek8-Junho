using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    [Header("몬스터 Prefab")]
    [SerializeField] public GameObject originalGoblinTNT; //GoblinTNT 원본 Prefab
    [SerializeField] public GameObject originalGoblinTorch; //GoblinTorch 원본 Prefab

    public List<GameObject> tntGoblins = new List<GameObject>(); //tnt 던지는 고블린 보관하는 리스트
    public List<GameObject> torchGoblins = new List<GameObject>(); //근접공격하는 고블린 보관하는 리스트

    public static EnemyManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null) //instance가 비어있다면
        {
            Destroy(instance); //instance 지우기
            return;
        }

        instance = this; //instance에 배정
    }

    public void SpawnNewGoblinTNT(Vector2 pos) //pos 위치에 새로운 고블린 생성
    {
        GameObject newGoblin = Instantiate(originalGoblinTNT); //GoblinTNT 복제해서 newGoblin에 저장

        newGoblin.SetActive(true); //생성된 고블린 활성화
        newGoblin.transform.position = pos; //생성된 고블린 위치 매개변수로 설정

        tntGoblins.Add(newGoblin); //TNT 던지는 고블린 리스트에 추가
    }

    public void SpawnNewGoblinTorch(Vector2 pos)
    {
        GameObject newGoblin = Instantiate(originalGoblinTorch); //GoblinTorch 복제해서 newGoblin에 저장

        newGoblin.SetActive(true); //생성된 고블린 활성화
        newGoblin.transform.position = pos; //생성된 고블린 위치 매개변수로 설정

        torchGoblins.Add(newGoblin); //근접공격하는 고블린 리스트에 추가
    }

    public void RemoveTntGoblinFromList(GameObject goblin) //리스트에서 폭탄 고블린 제거
    {
        tntGoblins.Remove(goblin); //매개변수에 해당하는 폭탄 던지는 고블린 제거
    }

    public void RemoveTorchGoblinFromList(GameObject goblin) //리스트에서 근접공격 고블린 제거
    {
        torchGoblins.Remove(goblin); //매개변수에 해당하는 근접공격 고블린 제거
    }
}
