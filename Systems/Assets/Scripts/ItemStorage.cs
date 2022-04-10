using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    private Item[,] itemStorage = new Item[6,7];
    public int[,] FindItem(int r, int c, Item item){
        for(int j = c; j < itemStorage.GetLength(1); j++){
            if (itemStorage[r,j].IsEquals(item))
                return {r,j};
        }

        for(int i = r+1; i < itemStorage.GetLength(0); i++){
            for(int j = 0; j < itemStorage.GetLength(1); j++){
                if (itemStorage[i,j].IsEquals(item))
                    return {i,j};
            }
        }
    }
    
    public void Deposit(int r, int c, Item otherItem){
        if (itemStorage[r,c].IsNone()){
            Item newItem = new Item(otherItem);
            itemStorage[r,c] = newItem;
        } else if (itemStorage[r,c].IsEquals(otherItem)){
            int a = itemStorage[r,c].GetQuantity();
            int b = otherItem.GetQuantity();
            int c = itemStorage[r,c].GetMaxQuantity();
            if (itemStorage[r,c].IsStackable() && !itemStorage[r,c].IsFull()){
                if(a + b <= c)
                    itemStorage[r,c].AddQuantity(b);
                    otherItem.AddQuantity(-1 * b);
                else {
                    int diff = Math.Abs(a - b);
                    itemStorage[r,c].AddQuantity(diff);
                    otherItem.AddQuantity(-1 * diff);
                }
            }
        }
    }

    public Item Withdraw(int r, int c){
        Item newItem = new Item(otherItem);
        itemStorage[r,c] = newItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
