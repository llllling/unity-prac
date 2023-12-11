using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

    private const int maxJumpCount = 2;
   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

   private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            return;
        }

        // 마우스 왼쪽 버튼 누르고 최대 점프 횟수에 도달하지 않았으면
        bool isJumpOn = Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount;
        //마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
        /*
         playerRigidbody.velocity.y > 0 : 최고점에서 속도는 제로에 가깝다. 
            이 시점에서 점프 속도가 아닌 낙하 속도를 절반으로 줄이는 문제가 발생할 수 있다.
            마우스 왼쪽 버튼을 너무 오래 누르고 있다가 캐릭터가 최고 높이에 도달한 후 낙하하기 시작한 시점에 손을 떼었다고 가정.
            y 방향 속도 값이 0 이하일 때 속도를 절반으로 줄이면 상승 속도가 아니라 낙하 속도가 절반 줄어듬 그래서 해당 조건 추가
         */
        bool isJumpOff = Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0;
        if (isJumpOn)
        {
            jumpCount++;
            // 점프 직전 속도를 제로로 변경 => 직전까지의 힘(속도)가 상쇄되거나 합쳐져서 점프 높이가 비일관적으로 되는 현상을 막기위해
            /*
               1. 점프 사이에 충분한 시간 간격을 두고 이단 점프 실행(마우스 왼쪽 버튼을 여유 있게 두번 클릭)
               2. 매우 짧은 간격으로 이단 점프 실행(마우스 왼쪽 버튼을 빠르게 두번 클릭)
               2번의 경우 첫 번째 점프의 힘과 속력이 두 번째 점프의 힘과 속력에 그대로 합쳐진다. 따라서 2의 경우 두 번째 점프에 의한 상승 속도와 높이가 1의 경우에 비해 비약적으로 증가함
             */
            playerRigidbody.velocity = Vector2.zero;
            //리비드바디의 위쪽으로 힘 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        } else if (isJumpOff) {
            //속도 절반으로 변경
            playerRigidbody.velocity -= playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

   private void Die() {
        // 사망 처리
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
   }

   private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지

        if (other.tag != "Dead" || isDead) return;

        Die();
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        //collision.contacts[0] : collision.contacts라는 배열 변수의 길이는 충돌 지점의 개수와 일치, 그 중에 첫번째 충돌 지점 정보
        // normal 변수 : 충돌 표면의 방향(노멀 벡터)을 알려주는 변수
        if (collision.contacts[0].normal.y <= 0.7f) return; 

        isGrounded = true;
        jumpCount = 0;
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
   }
}