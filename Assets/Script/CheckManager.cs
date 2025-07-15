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

    public List<ItemData> itemList;                    // ��ü ������ �̸� + ������ ����
    public GameObject checklistItemPrefab;             // ������
    public Transform checklistContainer;               // ���Ե��� ��ġ�� �θ� ������Ʈ

    private Dictionary<string, ChecklistUI> itemUIs = new();

    void Awake()
    {
        Instance = this;

        // ������ ����Ʈ ����
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
            ui.Initialize(ui.itemIcon.sprite); // ������ ������� �ʱ�ȭ
        }
    }
}
