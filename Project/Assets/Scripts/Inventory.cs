using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] internal int size;
    internal List<ItemBase> items;
    internal int currentIndex = 0;
    InventoryTypeBase inventory;


    private void Awake()
    {
        inventory = new InventoryStack(this);
        items = new List<ItemBase>();
        for(int i = 0; i < size; i++)
        {
            items.Add(null);
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
            inventory.currentIndex = index;
            inventory.items[index] = item;
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
        return -1;
    }
}