using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Collide, Interact}
public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] protected ItemType type;
    [SerializeField] protected TMP_Text pickupMessage;  // 텍스트 메시지를 연결할 변수
    [SerializeField] protected SpriteRenderer sprite;  // 텍스트 메시지를 연결할 변수
    protected PlayerController playerController;
    protected bool isPlayerNearby = false;

    protected virtual void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        if (pickupMessage != null)
        {
            pickupMessage.transform.parent.gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        // 플레이어가 근처에 있을 때 F 키를 눌러 아이템을 획득
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }
        sprite.transform.rotation *= Quaternion.Euler(0, 45 * Time.deltaTime, 0) ;
    }
    public abstract bool OnItemUsed();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(type == ItemType.Collide)
            {
                PickupItem();
            }
            else if(type == ItemType.Interact)
            {
                isPlayerNearby = true;
                if (pickupMessage != null)
                {
                    pickupMessage.transform.parent.gameObject.SetActive(true);
                }
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
                pickupMessage.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void PickupItem()
    {
        // 아이템을 획득하는 로직
        if(playerController.inventory.AddItem(this))
            gameObject.SetActive(false);
    }
}
