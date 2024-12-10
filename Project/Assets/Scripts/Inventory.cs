using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
public enum InventoryType { Stack, Queue}

public class Inventory : MonoBehaviour
{
    [SerializeField] internal int size;
    [SerializeField] internal List<ItemBase> items;
    [SerializeField] internal InventoryType type;
    internal int currentIndex = 0;
    InventoryTypeBase inventory;

    List<Image> slotItem;


    private void Awake()
    {
        items = new List<ItemBase>();
        for(int i = 0; i < size; i++)
        {
            items.Add(null);
        }
        switch (type)
        {
            case InventoryType.Stack: inventory = new InventoryStack(this); break;
            case InventoryType.Queue: inventory = new InventoryQueue(this); break;
        }
        slotItem = new List<Image>();

    }
    private void Start()
    {
        Transform frame = GameObject.FindWithTag("Bag").transform;
        for (int i = 0; i < 5; i++)
        {
            slotItem.Add(frame.Find((i + 1).ToString()).Find("Slot Item").GetComponent<Image>());
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) inventory.OnMoveInventoryLeft();
        else if(Input.GetKeyDown(KeyCode.RightArrow)) inventory.OnMoveInventoryRight();
        else if(Input.GetKeyDown(KeyCode.Space)) inventory.OnItemUsed();
        int i = -1;
        foreach(char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                i = int.Parse(c.ToString());
                break;
            }
        }
        if (i != -1) inventory.OnMoveInventoryToSpecificIndex(i);
        UpdateUi();
    }

    public bool AddItem(ItemBase item)
    {
        return inventory.OnItemAdded(item);

    }

    public void OnIndexChanged()
    {

    }

    void UpdateUi()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] != null) slotItem[i].sprite = items[i].itemImage;
            else slotItem[i].sprite = null;
            
            if(i == currentIndex) slotItem[i].transform.parent.GetComponent<Image>().color = new Color(1, 1, 0, 1);
            else slotItem[i].transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }


    }

}


public enum InventoryStartPoint { Front, Back }
public abstract class InventoryTypeBase
{
    protected Inventory inventory;
    public InventoryTypeBase(Inventory inventory, InventoryStartPoint point)
    {
        this.inventory = inventory;
        inventory.currentIndex = (point == InventoryStartPoint.Front) ? -1 : inventory.size;
        inventory.OnIndexChanged();
    }
    public abstract void OnMoveInventoryLeft();
    public abstract void OnMoveInventoryRight();
    public abstract void OnMoveInventoryToSpecificIndex(int index);
    public bool OnItemUsed()
    {
        var item = inventory.items.ElementAtOrDefault(inventory.currentIndex);
        if (item)
        {
            if (item.OnItemUsed())
            {
                inventory.items[inventory.items.IndexOf(item)] = null;
                inventory.currentIndex = GetCurrentIndex();
                return true;
            }
        }
        return false;
    }
    public bool OnItemAdded(ItemBase item)
    {
        int index = GetAquisitionIndex();
        if (index >= 0)
        {
            inventory.items[index] = item;
            inventory.currentIndex = GetCurrentIndex();
            return true;
        }
        return false;
    }
    public abstract int GetAquisitionIndex();
    public abstract int GetCurrentIndex();
}

public class InventoryStack : InventoryTypeBase
{
    public InventoryStack(Inventory inventory) : base(inventory, InventoryStartPoint.Front)
    {
        
    }

    public override void OnMoveInventoryLeft()
    {

    }

    public override void OnMoveInventoryRight()
    {

    }

    public override void OnMoveInventoryToSpecificIndex(int index)
    {

    }

    public override int GetAquisitionIndex()
    {
        for(int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i] == null) return i;
        }
        return -1;
    }

    public override int GetCurrentIndex()
    {

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i] == null) return i -1;
        }
        return inventory.items.Count -1;
    }
}

public class InventoryQueue : InventoryTypeBase
{
    public InventoryQueue(Inventory inventory) : base(inventory, InventoryStartPoint.Front)
    {

    }

    public override void OnMoveInventoryLeft()
    {

    }

    public override void OnMoveInventoryRight()
    {

    }

    public override void OnMoveInventoryToSpecificIndex(int index)
    {

    }

    public override int GetAquisitionIndex()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i] == null) return i;
        }
        return -1;
    }

    public override int GetCurrentIndex()
    {
        int count = 0;
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i] == null)
            {
                count++;
                inventory.items.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < count; i++)
        {
            inventory.items.Add(null);
        }
        return 0;
    }
}