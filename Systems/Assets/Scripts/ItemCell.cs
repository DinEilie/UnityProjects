using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCell : MonoBehaviour
{
    private Item item;
    private int quantity;
    private float totalWeight;
    
    public ItemCell(){
        this.quantity = 0;
        this.totalWeight = 0f;
    }

    public ItemCell(Item otherItem, int otherQuantity){
        this.item = otherItem;
        this.quantity = otherQuantity;
        this.totalWeight = otherQuantity * otherItem.getWeight();
    }

    public ItemCell(ItemCell otherCell){
        ItemCell newCell = new ItemCell(otherCell.item, otherCell.quantity);
        this.item = newCell.item;
        this.quantity = newCell.quantity;
        this.totalWeight = newCell.totalWeight;
    }

    public void clear(){
        this.item = null;
        this.quantity = 0;
        this.totalWeight = 0f;
    }

    public void add(Item otherItem){
        if (this.quantity == 0){
            this.item = otherItem;
            this.quantity++;
            this.totalWeight = otherItem.getWeight();
        } else if (this.quantity < 50 && this.item == otherItem){
            this.quantity++;
            this.totalWeight = otherItem.getWeight() * this.quantity;
        }
    }

    public void remove(int num){
        if (num >= this.quantity)
            clear();
        else
            this.quantity -= num;
    }

    public void add(ItemCell cell){
        if (this.quantity == 0){
            this.item = cell.item;
            this.quantity = cell.quantity;
            this.totalWeight = cell.totalWeight;
            cell.clear();
        } else if (quantity < 50 && cell.item == this.item && this.item.isStackable()){
            int sum = cell.quantity + this.quantity;
            if (sum <= 50){
                this.quantity = sum;
                this.totalWeight = this.item.getWeight() * this.quantity;
                cell.clear();
            }
            else {
                sum -= 50;
                cell.quantity = sum;
                cell.totalWeight = cell.item.getWeight() * cell.quantity;
                this.quantity = 50;
                this.totalWeight = this.item.getWeight() * 50f;
            }
        }
    }
}
