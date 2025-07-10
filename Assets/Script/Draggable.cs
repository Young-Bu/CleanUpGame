using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem2D : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Camera mainCamera;

    [Header("올바른 드롭존 태그")]
    public string correctDropZoneTag;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
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
            // 추가로 사운드나 이펙트 가능
        }
        else
        {
            // 틀리면 원래 자리로
            transform.position = originalPosition;
        }
    }
}
