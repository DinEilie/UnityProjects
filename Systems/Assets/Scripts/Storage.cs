using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    // Storage triggers
    [Header ("Storage triggers")]
    public bool allowStorage = true;
    public bool allowEquipment = true;
    
    public ItemCell[,] inventory = new ItemCell[5,5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
