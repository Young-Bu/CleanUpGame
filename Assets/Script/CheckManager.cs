using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckManager : MonoBehaviour
{
    public static CheckManager Instance;

    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite icon;
    }

    public List<ItemData> itemList;                    // 전체 아이템 이름 + 아이콘 정보
    public GameObject checklistItemPrefab;             // 프리팹
    public Transform checklistContainer;               // 슬롯들이 배치될 부모 오브젝트

    private Dictionary<string, ChecklistUI> itemUIs = new();

    void Awake()
    {
        Instance = this;

        // 아이템 리스트 생성
        foreach (var item in itemList)
        {
            GameObject go = Instantiate(checklistItemPrefab, checklistContainer);
            ChecklistUI ui = go.GetComponent<ChecklistUI>();
            ui.Initialize(item.icon);
            itemUIs[item.itemName] = ui;
        }
    }

    public void UpdateChecklist(string cleanedItem)
    {
        if (itemUIs.ContainsKey(cleanedItem))
        {
            itemUIs[cleanedItem].MarkAsCleaned();
        }
    }

    public void ResetChecklistUI()
    {
        foreach (var ui in itemUIs.Values)
        {
            ui.Initialize(ui.itemIcon.sprite); // 아이콘 원래대로 초기화
        }
    }
}
