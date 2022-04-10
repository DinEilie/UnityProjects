using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Item", menuName = "Create new Item")]
public class Item : ScriptableObject
{
    // Item Properties
   [Header ("Item Properties")]
   [SerializeField] [Tooltip("The item ID located in the database.")] private float id;
   [SerializeField] [Tooltip("Item in-game name.")] private string title;
   public enum itemType {Weapon, Armor, Material, Quest, None}
   public enum weaponType {Rifle, Canon, Thrower, Melee, None}
   [SerializeField] [Tooltip("Item type.")] private itemType type;
   [SerializeField] [Tooltip("Weapon type (if not a weapon then mark ''None'').")] private weaponType wType;
   [SerializeField] [TextArea(minLines: 10, maxLines: 10)] private string info;
   [SerializeField] [Tooltip("Item weight.")] private float weight;
   [SerializeField] [Tooltip("Item quantity.")] private int quantity;
   [SerializeField] [Tooltip("Item max quantity.")] private int maxQuantity;
   [SerializeField] [Tooltip("Mark if the item is sellable on shops/tradeable.")] private bool sellable;
   [SerializeField] [Tooltip("Mark if the item is stackable.")] private bool stackable;
   [SerializeField] [Tooltip("Item price.")] private float price;
   [SerializeField] [Tooltip("Weapon demage.")] private float damage;
   [SerializeField] [Tooltip("Weapon battery drain while using it.")] private float batteryDrain;

    public Item(){
        this.title = "";
        this.type = itemType.Material;
        this.wType = weaponType.None;
        this.id = 0f;
        this.info = "";
        this.weight = 0f;
        this.quantity = 1;
        this.maxQuantity = 50;
        this.sellable = false;
        this.stackable = true;
        this.price = 0f;
        this.damage = 0f;
        this.batteryDrain = 0f;
    }

    public Item(string otherTitle, itemType otherType,weaponType otherWeaponType ,float otherID, string otherInfo, float otherWeight, int otherQuantity, int otherMaxQuantity, bool otherSellable, bool otherStackable, float otherPrice, float otherDamage, float otherBatteryDrain){
        this.title = otherTitle;
        this.type = otherType;
        this.wType = otherWeaponType;
        this.id = otherID;
        this.info = otherInfo;
        this.weight = otherWeight;
        this.quantity = otherQuantity;
        this.maxQuantity = otherMaxQuantity;
        this.sellable = otherSellable;
        this.stackable = otherStackable;
        this.price = otherPrice;
        this.damage = otherDamage;
        this.batteryDrain = otherBatteryDrain;
    }   

   public Item(Item other){
       Item newItem = new Item(other.title, other.type, other.wType, other.id, other.info, other.weight, other.quantity, other.maxQuantity, other.sellable, other.stackable, other.price, other.damage, other.batteryDrain);
       this.title = newItem.title;
       this.type = newItem.type;
       this.id = newItem.id;
       this.info = newItem.info;
       this.weight = newItem.weight;
       this.quantity = newItem.quantity;
       this.maxQuantity = newItem.maxQuantity;
       this.sellable = newItem.sellable;
       this.stackable = newItem.stackable;
       this.price = newItem.price;
       this.damage = newItem.damage;
       this.batteryDrain = newItem.batteryDrain;
   }

   public int GetQuantity(){
       Item newItem = new Item(this);
       return newItem.quantity;
   }

   public int GetMaxQuantity(){
       Item newItem = new Item(this);
       return newItem.maxQuantity;
   }

   public float GetWeight(){
       Item newItem = new Item(this);
       return newItem.weight;
   }

   public float GetTotalWeight(){
       Item newItem = new Item(this);
       return newItem.weight * newItem.quantity;
   }

   public void AddQuantity(int num){
       this.quantity += num;
   }

   public void AddWeight(int num){
       this.weight += num;
   }

   public string GetType(){
       return this.type + "";
   }

   public bool IsNone(){
       if(GetType() == "None" && id == 0f)
            return true;
       return false;
   }

   public bool IsEquals(Item other){
       if(this.id == other.id && this.title == other.title)
            return true;
       return false;
   }

   public bool IsFull(){
       if(this.quantity == this.maxQuantity)
            return true;
        return false;
   }

   // Return true if the item is stackable
   public bool IsStackable(){
       if (stackable)
            return true;
       return false;
   }

}
