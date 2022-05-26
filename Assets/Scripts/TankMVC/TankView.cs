using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This Class is Attached to a Tank GameObject and is responsible for rendering and UI related work.
/// </summary>
public class TankView : MonoBehaviour,IDamagable
{
    [Header("Transform")]    
    public Transform BulletSpawner;
    private TankController tankController;

    public GameObject Turret;

    public Slider sliderHealth;
    public Slider aimSlider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    public Rigidbody shellPrefab;

    [Header("Bool")]
    public bool fired;
    internal bool tankDead;

    //public AudioSource TankMoveSound;


    private void Start()
    {
        tankController.SubscribeEvents();   

    }
   

    private void FixedUpdate()
    {
        tankController.HandleLeftJoyStickInput(GetComponent<Rigidbody>());
        tankController.HandleRightJoyStickInput(Turret.transform);
       
    }

    
    // Sets a reference to the corresponding TankController Script.
    public void SetTankControllerReference(TankController controller)
    {
        tankController = controller;
    }

    void IDamagable.TakeDamage(float damage)
    {
       // Debug.Log("Player Taking Damage" + damage);
        tankController.ApplyDamage(damage);
    }

    private void OnDisable()
    {
        tankController.UnsubscribeEvents();
    }

    



}