using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHP; //최대 HP
    [SerializeField] int maxSP; //최대 SP
    [SerializeField] float moveSpeed;

    private Rigidbody2D varRigidBody; //플레이어의 rigidbody2d 컴포넌트 참조
    private Vector2 movementInput; //플레이어의 입력 값을 저장할 변수
    public int currentHP; //현재 HP
    public int currentSP; //현재 SP
    float leftRightInput; //좌우 이동값
    float upDownInput; //위아래 이동값

    void Awake()
    {
        //게임 시작 시 Rigidbody2d 컴포넌트 가져오기
        varRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //프레임마다 사용자 입력 받기
        GetPlayerInput();
    }

    void FixedUpdate()
    {
        //물리 관련 계산 및 이동 처리
        PlayerMove();
    }

    void GetPlayerInput() //사용자 입력 받는 메서드
    {
        leftRightInput = Input.GetAxisRaw("Horizontal");
        upDownInput = Input.GetAxisRaw("Vertical");

        //이동 방향 벡터 저장
        //.normalized를 사용해 대각선 이동 시 속도가 빨라지는 것 방지
        movementInput = new Vector2(leftRightInput, upDownInput).normalized;
    }
    void PlayerMove() //플레이어 이동을 구현한 메서드
    {
        //이동 방향 * 이동 속도
        Vector2 targetVelocity = movementInput * moveSpeed;
        varRigidBody.linearVelocity = targetVelocity;
    }
}
