using UnityEngine;
using TMPro;  // TextMeshPro ����� ���� �߰�

public class Item : MonoBehaviour
{
    public GameObject pickupMessage;  // �ؽ�Ʈ �޽����� ������ ����
    private bool isPlayerNearby = false;  // �÷��̾ ��ó�� �ִ��� üũ

    void Start()
    {
        // ���� �� pickupMessage�� ��Ȱ��ȭ �������� Ȯ��
        if (pickupMessage != null)
        {
            pickupMessage.SetActive(false);
        }
    }

    void Update()
    {
        // �÷��̾ ��ó�� ���� �� F Ű�� ���� �������� ȹ��
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ Ʈ���� ������ ������ �޽��� ǥ��
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
        // �÷��̾ Ʈ���� ������ ����� �޽��� �����
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
        // �������� ȹ���ϴ� ����
        Debug.Log("�������� ȹ���߽��ϴ�!");
        Destroy(gameObject);  // ������ ������Ʈ �ı�
        if (pickupMessage != null)
        {
            pickupMessage.SetActive(false);  // �޽��� �����
        }
    }
}
