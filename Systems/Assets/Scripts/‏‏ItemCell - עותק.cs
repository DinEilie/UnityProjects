using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private float battery = 1500f;
    private float shield = 1000f;
    private float armor = 1000f;
    private float ai = 0f;
    private float level = 1f;
    private float nextAi = 50f;

    public float GetBattery(){
        return this.battery;
    }

    public float GetShield(){
        return this.shield;
    }
    
    public float GetArmor(){
        return this.armor;
    }

    public float GetAi(){
        return this.ai;
    }

    public float GetLevel(){
        return this.level;
    }

    public void AddBattery(float num){
        this.battery += num;
    }

    public void AddShield(float num){
        this.shield += num;
    }

    public void AddArmor(float num){
        this.armor += num;
    }

    public void AddAi(float num){
        if (num + this.ai >= this.nextAi){
            this.level += 1f;
            float extraAi = (num + this.ai) - this.nextAi;
            this.ai = 0;
        }

    }
}
