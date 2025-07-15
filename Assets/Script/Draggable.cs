using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem2D : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Camera mainCamera;

    public AudioClip successSound;          // 드롭 성공 사운드
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
            mousePos.z = 0f; // 2D니까 z값 고정
            transform.position = mousePos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // 드롭 시 해당 위치에 Collider2D가 있는지 확인
        Vector2 dropPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(dropPoint);

        if (hit != null && hit.CompareTag(correctDropZoneTag))
        {
            // 정답 위치에 놓음
            transform.position = hit.transform.position;

            if (successSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            // 정리 성공으로 카운트 증가
            if (!alreadyPlaced)
            {
                GameManager.Instance.MarkItemAsCleaned(itemName);
                alreadyPlaced = true;
            }
        }
        else
        {
            // 틀리면 원래 자리로
            transform.position = originalPosition;
        }
    }
    private bool alreadyPlaced = false;
}
