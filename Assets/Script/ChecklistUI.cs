using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public Image itemIcon;       // 아이템 아이콘 이미지
    public Image checkMark;      // 체크마크 이미지

    public void Initialize(Sprite icon)
    {
        itemIcon.sprite = icon;
        checkMark.enabled = false;
        itemIcon.color = Color.white; // 선명하게 시작
    }

    public void MarkAsCleaned()
    {
        checkMark.enabled = true;
        itemIcon.color = new Color(1f, 1f, 1f, 0.4f); // 흐리게 표시
    }
}
