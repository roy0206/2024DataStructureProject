using UnityEngine;
using TMPro;  // TextMeshPro 사용을 위해 추가

public class Item : MonoBehaviour
{
    public GameObject pickupMessage;  // 텍스트 메시지를 연결할 변수
    private bool isPlayerNearby = false;  // 플레이어가 근처에 있는지 체크

    void Start()
    {
        // 시작 시 pickupMessage가 비활성화 상태인지 확인
        if (pickupMessage != null)
        {
            pickupMessage.SetActive(false);
        }
    }

    void Update()
    {
        // 플레이어가 근처에 있을 때 F 키를 눌러 아이템을 획득
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거 영역에 들어오면 메시지 표시
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pickupMessage != null)
            {
                pickupMessage.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 트리거 영역을 벗어나면 메시지 숨기기
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pickupMessage != null)
            {
                pickupMessage.SetActive(false);
            }
        }
    }

    void PickupItem()
    {
        // 아이템을 획득하는 로직
        Debug.Log("아이템을 획득했습니다!");
        Destroy(gameObject);  // 아이템 오브젝트 파괴
        if (pickupMessage != null)
        {
            pickupMessage.SetActive(false);  // 메시지 숨기기
        }
    }
}
