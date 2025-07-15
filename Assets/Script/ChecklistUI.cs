using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public Image itemIcon;       // ������ ������ �̹���
    public Image checkMark;      // üũ��ũ �̹���

    public void Initialize(Sprite icon)
    {
        itemIcon.sprite = icon;
        checkMark.enabled = false;
        itemIcon.color = Color.white; // �����ϰ� ����
    }

    public void MarkAsCleaned()
    {
        checkMark.enabled = true;
        itemIcon.color = new Color(1f, 1f, 1f, 0.4f); // �帮�� ǥ��
    }
}
