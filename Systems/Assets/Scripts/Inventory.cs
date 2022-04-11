using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   // Storage triggers
    [Header ("Storage triggers")]
    public bool allowStorage = true;
    public bool allowEquipment = true;
    
    public ItemStorage[] storage = new ItemStorage[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
