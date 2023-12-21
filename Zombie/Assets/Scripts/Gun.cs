using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour {
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 25; // 공격력
    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약


    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake() {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // 나중에 총을 쏠 때 렌더러의 1번 점은  총구 위치 , 2번 점은 탄알이 닿을 위치를 할당
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable() {
        // 총 상태 초기화
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    // 발사 시도
    public void Fire() {
        if (state != State.Ready || Time.time < lastFireTime + timeBetFire) { return; }
        lastFireTime = Time.time;
        Shot();


    }

    // 실제 발사 처리
    private void Shot() {
        RaycastHit hit; // 레이캐스트의 결과를 저장할 변수
        //탄알이 맞은 곳을 저장할 변수
        Vector3 hitPosition = Vector3.zero;
        // out hit : 3번째 파마리터 => 레이가 충돌한 경우 hitInfo에 자세한 충돌 정보가 채워짐
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //레이가 어떤 물체와 충돌한 경우
            IDamageable target = hit.collider.GetComponent<IDamageable>();


            if (target == null) return;

            target.OnDamage(damage, hitPosition, hit.normal);
            //레이가 충돌한 위치 저장
            hitPosition = hit.point;

        } else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        magAmmo--;

        if(magAmmo <=0)
        {
            state = State.Empty;
        }

        
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) {
        // 총구 화염 효과 재생
        muzzleFlashEffect.Play();
        // 탄피 배출 효과 재생
        shellEjectEffect.Play();
        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(shotClip);

        // 선의 시작점은 총구의 위치 
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // 선의 끝접은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);

        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload() {
        if(state  == State.Reloading  || ammoRemain == 0 || magAmmo == 0)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return false;
    }

    // 실제 재장전 처리를 진행=> 대기 시간이 필요하기 때문에 실질적인 재장전은 코루틴 메서드인 여기서 필요
    //코루틴~
    private IEnumerator ReloadRoutine() {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;

        gunAudioPlayer.PlayOneShot(reloadClip);

        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        int ammotToFill = magCapacity - magAmmo;

        //리팩토링 필요
        // 탄창에 채워야 할 탄알이 남은 타일보다 많다면
        // 채워야 할 탄알 수를 남은 탄알 수에 맞춰 줄임
        if (ammoRemain < ammotToFill)
        {
            ammotToFill =    ammoRemain;
        }
        //탄창을 채움
        magAmmo = ammotToFill;
        //남은 탄알에서 탄창을 채운만틈 탄알을 뺌
        ammoRemain -= ammotToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}