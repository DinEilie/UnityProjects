using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    // Stats properties
    [SerializeField] [Tooltip("The player main source of energy.")] private float power = 1000f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float shield = 100f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float jet = 300f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float jetCooldown = 0;
    [SerializeField] [Tooltip("The player main source of energy.")] private float jetHeight = 0;
    [SerializeField] [Tooltip("The player main source of energy.")] private float maxPower = 1000f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float maxShield = 100f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float maxJetCooldown = 3f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float maxJetHeight = 1.5f;
    [SerializeField] [Tooltip("The player main source of energy.")] private float maxJet = 300f;
    [SerializeField] [Tooltip("The player main source of energy.")] private bool isJetOverheating = false;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Slider sliderPower;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Slider sliderFuel;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Slider sliderJet;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private Image sliderImg;
    [SerializeField] [Tooltip("Refrence to the animator controller.")] private GameObject sliderObj;

    void Start()
    {
        sliderPower.maxValue = GetMaxPower();
        sliderPower.value = GetPower();
        sliderFuel.maxValue = GetMaxJet();
        sliderFuel.value = GetJet();
        sliderJet.maxValue = GetMaxJetCooldown();
        sliderJet.value = GetJetCooldown();
    }

    // Return the current power
    public float GetPower(){
        return this.power;
    }

    // Return the current shield
    public float GetShield(){
        return this.shield;
    }

    // Return the current jet cooldown
    public float GetJetCooldown(){
        return this.jetCooldown;
    }

    // Return the current jet height
    public float GetJetHeight(){
        return this.jetHeight;
    }

    // Return the current jet power
    public float GetJet(){
        return this.jet;
    }

    // Return the current max power
    public float GetMaxPower(){
        return this.maxPower;
    }

    // Return the current max shield
    public float GetMaxShield(){
        return this.maxShield;
    }

    // Return the current max jet power
    public float GetMaxJetCooldown(){
        return this.maxJetCooldown;
    }

    // Return the current max jet power
    public float GetMaxJetHeight(){
        return this.maxJetHeight;
    }

    // Return the current max jet power
    public float GetMaxJet(){
        return this.maxJet;
    }

    // Add to the current power
    public void AddPower(float var){
        if (this.power + var <= 0)
            this.power = 0;
        else if (this.power + var >= this.maxPower)
            this.power = this.maxPower;
        else
            this.power += var;
        sliderPower.value += var;
    }

    // Add to the current shield
    public void AddShield(float var){
        if (this.shield + var <= 0)
            this.shield = 0;
        else if (this.shield + var >= this.maxShield)
            this.shield = this.maxShield;
        else
            this.shield += var;
    }

    // Add to the current jet cooldown
    public void AddJetCooldown(float var){
        if (this.jetCooldown + var <= 0)
        {
            this.jetCooldown = 0;
            this.isJetOverheating = false;
            sliderObj.SetActive(false);
            sliderImg.color = new Color(0.1373f, 1f, 0f, 1f);
        }
        else if (this.jetCooldown + var >= this.maxJetCooldown)
        {
            this.jetCooldown = this.maxJetCooldown;
            this.isJetOverheating = true;
            sliderImg.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            this.jetCooldown += var;
            sliderObj.SetActive(true);
        }
        sliderJet.value += var;
    }

    // Add to the current jet
    public void AddJetHeight(float var){
        if (this.jetHeight + var <= 0)
            this.jetHeight = 0;
        else if (this.jetHeight + var >= this.maxJetHeight)
            this.jetHeight = this.maxJetHeight;
        else
            this.jetHeight += var;
    }

    // Add to the current jet
    public void AddJet(float var){
        if (this.jet + var <= 0)
            this.jet = 0;
        else if (this.jet + var >= this.maxJet)
            this.jet = this.maxJet;
        else
            this.jet += var;
        sliderFuel.value += var;
    }

    // Set the current max power
    public void SetMaxPower(float var){
        this.maxPower = var;
    }

    // Set the current max shield
    public void SetMaxShield(float var){
        this.maxShield = var;
    }

    // Set the current max jet cooldown
    public void SetMaxJetCooldown(float var){
        this.maxJetCooldown = var;
    }

    // Set the current max jet height
    public void SetMaxJetHeight(float var){
        this.maxJetHeight = var;
    }

    // Set the current max jet
    public void SetMaxJet(float var){
        this.maxJet = var;
    }

    // Set the current max jet
    public bool IsJetOverheat(){
        return this.isJetOverheating;
    }

    void Update()
    {
        if (this.isJetOverheating)
            AddJetCooldown(-0.15f * Time.deltaTime);
    }
}
