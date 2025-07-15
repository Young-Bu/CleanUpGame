using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem2D : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Camera mainCamera;

    public AudioClip successSound;          // ��� ���� ����
    private AudioSource audioSource;

    public string correctDropZoneTag;

    public string itemName;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // 2D�ϱ� z�� ����
            transform.position = mousePos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // ��� �� �ش� ��ġ�� Collider2D�� �ִ��� Ȯ��
        Vector2 dropPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(dropPoint);

        if (hit != null && hit.CompareTag(correctDropZoneTag))
        {
            // ���� ��ġ�� ����
            transform.position = hit.transform.position;

            if (successSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            // ���� �������� ī��Ʈ ����
            if (!alreadyPlaced)
            {
                GameManager.Instance.MarkItemAsCleaned(itemName);
                alreadyPlaced = true;
            }
        }
        else
        {
            // Ʋ���� ���� �ڸ���
            transform.position = originalPosition;
        }
    }
    private bool alreadyPlaced = false;
}
