using UnityEngine;
using UnityEngine.Rendering;

public class PlayerData : MonoBehaviour
{
    [Header("플레이어 최대 HP")]
    [SerializeField] int maxPlayerHP;
    public int currentPlayerHP { get; set; } //플레이어 체력

    [Header("플레이어 최대 스태미나")]
    [SerializeField] int maxPlayerSP;
    public int currentPlayerSP { get; set; } //플레이어 스태미나
    [Header("플레이어 데미지")]
    [field: SerializeField] public int meleeAtkDamage { get; set; } //플레이어 데미지
    [field: SerializeField] public float playerBasicMoveSpeed { get; set; } //플레이어 이동 속도

    public bool playerIsFlip { get; set; } = false; //플레이어가 좌우로 뒤집혔는가?
    public bool playerAbleToMove { get; set; } //플레이어가 이동할 수 있는가?
    public bool playerAbleToAttack { get; set; } //플레이어가 공격할 수 있는가?
    public Vector3 playerPosition { get; set; } //플레이어 transform 좌표

    //다른 변수
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        playerAbleToMove = true; //게임 시작할 때 플레이어 이동 활성화
        playerAbleToAttack = true; //게임 시작할 때 플레이어 공격 가능
        currentPlayerHP = maxPlayerHP; //현재 HP 최대 HP로
        currentPlayerSP = maxPlayerSP; //현재 SP 최대 SP로
        ResetReference();
    }

    void ResetReference()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerPosition = transform.position;
        if (playerIsFlip != spriteRenderer.flipX) //만약 데이터와 spriterenderer가 다르다면
        {
            //spriteRenderer.flipX = playerIsFlip;
            playerIsFlip = spriteRenderer.flipX;
        }
    }
}
