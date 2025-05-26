using UnityEngine;

public class GoblinData : MonoBehaviour
{
    [SerializeField] public int damage; //데미지

    [Header("최대 HP")]
    [SerializeField] int maxGoblinHP; //최대 고블린 체력
    public int currentHp { get; set; } //현재 고블린 체력
    [field: SerializeField] public float moveSpeed { get; set; } //움직이는 속도
    [SerializeField] public float seeRange; //플레이어 인지 거리
    GameObject Target; //공격 목표
    [SerializeField] GameObject Player; //플레이어 게임 오브젝트
    [SerializeField] GameObject WayPoint; //플레이어
    
    //고블린 상태 변수
    bool isDead = false; //죽었는가?
    bool insideCastle = false; //성에 들어갔는가?

    //스크립트 참조변수
    Vector2 moveDirection;
    Vector3 goblinPosition; //고블린 변수 담기

    void Awake()
    {
        ReferenceReset();
    }

    void ReferenceReset()
    {
    }

    void Start()
    {
        currentHp = maxGoblinHP; //기초 HP 설정
        Target = WayPoint; //기본 공격 목표 설정
    }

    void Update()
    {
        goblinPosition = transform.position; //고블린 위치 업데이트
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; //이미 죽었으면 데미지 X

        currentHp -= damageAmount; //현재 체력 감소

        CheckDead(); //체력 변화 후 죽었는지 확인
        
        // ### 피격 애니메이션, 효과음, 체력바 업데이트 등 여기에 구현 가능?
    }

    void CheckDead() //체력이 0 이하가 되었을 때 호출될 함수
    {
        if (currentHp > 0) //현재 HP가 0 초과이면
        {
            return;
        }
        //0이거나 그 아래일 경우
        goblinIsDead(); //고블린 삭제 메서드 실행
    }
    void goblinIsDead() //고블린 죽게 만드는 메서드
    {
        if (!isDead)
        {
            isDead = true;
        }

        if (isDead)
        {
            Destroy(gameObject, 1f); //1초 뒤 오브젝트 파괴
        }
    }

    // ### Waypoint에 고블린 도착한 것 인식하는 메서드 필요

    void OnTriggerEnter2D(Collider2D other) //WayPoint에 Trigger 콜라이더 있어야 작동, Waypoint에 고블린 도착한 것 인식하는 함수
    {
        if (other.CompareTag("WayPoint")) //충돌한 오브젝트가 WayPoint인지 확인
        {
            GotInsideCastle();
        }
    }

    void GotInsideCastle() //고블린이 성에 도착했을 때
    {
        if (!insideCastle)
        {
            insideCastle = true;

            // ### 성에 도착했을 때 성 체력 감소, UI 표시
            Destroy(gameObject); //성에 도착한 고블린 오브젝트 파괴
        }
    }
}
