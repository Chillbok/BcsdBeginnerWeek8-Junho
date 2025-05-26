using UnityEngine;

public class TorchGoblinAttackController : MonoBehaviour
{
    //Layer 설정
    public LayerMask playerLayer; //공격할 대상 레이어 1: 플레이어
    public LayerMask castleLayer; //공격할 대상 레이어 2: 성

    //Awake에서 초기화해야 할 변수들(GetComponent)
    private GoblinData goblinData;
    private PlayerData playerData;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    Vector3 goblinPosition; //이 고블린 위치
    Vector3 playerPosition; //플레이어 위치

    private bool ableToAttack = true;
    private float angle;

    void Awake()
    {
        ResetReference();
    }

    void ResetReference()
    {
        goblinData = GetComponent<GoblinData>();
        playerData = GetComponent<PlayerData>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SetPlayerPosition(); //플레이어 위치 구하기
        SetGoblinPosition(); //고블린 각도 구하기
        CalculatePlayerPosAngle(); //각도 계산
        AttackAnimationParameter(); //각도 계산 후 공격 방향 계산
    }

    void SetPlayerPosition() //플레이어 위치 기록하기 위한 메서드
    {
        if (playerData != null)
        {
            playerPosition = playerData.playerPosition; //플레이어 위치 저장
        }
        else
        {
            Debug.LogWarning("플레이어 데이터 스크립트 지정되지 않음");
        }
    }

    void SetGoblinPosition() //몬스터 위치 기록하기 위한 메서드
    {
        if (goblinData != null)
        {
            goblinPosition = goblinData.transform.position;
            goblinPosition.y += 1f; //캐릭터 위치 맞추기 위해서 y로 1f만큼 올리기
        }
        else
        {
            Debug.LogWarning("고블린 위치 기록 완료");
        }
    }

    void CalculatePlayerPosAngle() //플레이어 위치 검색
    {
        Vector2 directionToGoblin = playerPosition - goblinPosition; //고블린에서 플레이어까지의 벡터
        Vector2 referenceDirection = Vector2.right; //오른쪽 향하는 벡터
        float signedAngle = Vector3.SignedAngle(referenceDirection, directionToGoblin, Vector3.forward); //벡터 각도 계산

        //0 ~ 360 각도로 변환
        float angle360 = (signedAngle < 0) ? (signedAngle + 360) : signedAngle; //만약 3, 4사분면이면 360 더해서 양수로 만듬
        angle = angle360;
    }

    void AttackAnimationParameter() //공격 애니메이션 출력 방향 패러미터 설정
    {
        if (45f <= angle && angle < 135f) //위쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 0f);
        }
        else if (135f <= angle && angle < 225f) //왼쪽
        {
            spriteRenderer.flipX = true;
            animator.SetFloat("AttackWay", 1f);
        }
        else if (225f <= angle && angle < 315f) //아래쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 2f);
        }
        else //오른쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 3f);
        }
    }

    /*
    # 공격 절차
    0. 성을 향해 뛰어감
    1. 주변 사물 감지
    2. 성을 향해 Run
    3. 플레이어가 인식 범위 안에 들어옴
    4. 플레이어 쫓아감
    5. 공격 범위 안에 대상이 들어오면 공격
        5.1. 플레이어와 성 모두 있는 경우, 플레이어 우선으로 공격
    */

    void DefineAttack()
    {
        bool AbleToSee = SeeObject();
        if (AbleToSee)
        {
            ableToAttack = false;

        }
    }
    void PerformAttack() //공격
    {
    }

    bool SeeObject() //플레이어 또는 성 찾고 true 또는 false 반환
    //true: 플레이어 또는 성 감지
    //false: 플레이어 또는 성 감지 못함
    {
        bool result = false; //플레이어, 성 감지하면 true 반환
        return result;
    }

    bool AbleToAttackObject(GameObject gameObject) //대상 공격 범위 내에 들어옴
    //true: 공격 가능
    //false: 공격 불가능
    {
        bool result = false;
        return result; //결과 반환
    }

    void SearchObjectBeforeHit() //플레이어인지 성인지 판단하고 때리기
    {
    }
}
