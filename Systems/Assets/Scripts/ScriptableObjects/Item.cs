using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Item", menuName = "Create new Item")]
public class Item : ScriptableObject
{
   public string title;
   public enum itemType {Weapon, Armor, Material, Misc}
   public itemType type;
   public float id;
   public string info;
   public float weight;
   public bool sellable;
   public float price;

    public Item(){
        this.title = "";
        this.type = itemType.Weapon;
        this.id = 0f;
        this.info = "";
        this.weight = 0f;
        this.sellable = false;
        this.price = 0f;
    }

    public Item(string otherTitle, itemType otherType, float otherID, string otherInfo, float otherWeight, bool otherSellable, float otherPrice){
        this.title = otherTitle;
        this.type = otherType;
        this.id = otherID;
        this.info = otherInfo;
        this.weight = otherWeight;
        this.sellable = otherSellable;
        this.price = otherPrice;
    }   

   public Item(Item other){
       Item newItem = new Item(other.title, other.type, other.id, other.info, other.weight, other.sellable, other.price);
       this.title = newItem.title;
       this.type = newItem.type;
       this.id = newItem.id;
       this.info = newItem.info;
       this.weight = newItem.weight;
       this.sellable = newItem.sellable;
       this.price = newItem.price;
   }

   public bool isStackable(){
       if (type != itemType.Weapon && type != itemType.Armor)
            return true;
       return false;
   }

}
