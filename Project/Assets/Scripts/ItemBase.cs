using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Collide, Interact}
public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] protected ItemType type;
    [SerializeField] protected TMP_Text pickupMessage;  // �ؽ�Ʈ �޽����� ������ ����
    [SerializeField] protected SpriteRenderer sprite;  // �ؽ�Ʈ �޽����� ������ ����
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
        // �÷��̾ ��ó�� ���� �� F Ű�� ���� �������� ȹ��
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
        // �÷��̾ Ʈ���� ������ ����� �޽��� �����
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
        // �������� ȹ���ϴ� ����
        if(playerController.inventory.AddItem(this))
            gameObject.SetActive(false);
    }
}
