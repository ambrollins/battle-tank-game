using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This Class implements all the logic which is required for a Tank Entity in the game to Operate as required.
/// </summary>
public class TankController
{
    private Joystick LeftJoyStick;
    private Joystick RightJoyStick;
    private float SpeedMultipier = 0.001f;
    private float RotationSpeedMultiplier = 0.01f;
    public TankController(TankModel tankModel, TankView tankPrefab)
    {
        TankModel = tankModel;
        TankView = GameObject.Instantiate<TankView>(tankPrefab);
        OnEnable();
    }

    public TankModel TankModel { get; }
    public TankView TankView { get; }

    public TankService TankService;
    public CameraController CameraController { get; }  
    // Sets the reference to left & right Joysticks on the Canvas.
    public void SetJoyStickReferences(Joystick leftJoyStick, Joystick rightJoyStick)
    {
        LeftJoyStick = leftJoyStick;
        RightJoyStick = rightJoyStick;
    }
    
    public void HandleLeftJoyStickInput(Rigidbody tankRigidBody)
    {
        if (LeftJoyStick.Vertical != 0)
        {
            Vector3 ZAxisMovement = tankRigidBody.transform.position + (tankRigidBody.transform.forward * LeftJoyStick.Vertical * TankModel.Speed * SpeedMultipier);
            tankRigidBody.MovePosition(ZAxisMovement);            
        }

        if (LeftJoyStick.Horizontal != 0)
        {
            Quaternion newRotation = tankRigidBody.transform.rotation * Quaternion.Euler(Vector3.up * LeftJoyStick.Horizontal * TankModel.RotationSpeed * RotationSpeedMultiplier);
            tankRigidBody.MoveRotation(newRotation);
        }
    }

   
    

    // This Function Handles the Input recieved from the Right Joystick.
    public void HandleRightJoyStickInput(Transform turretTransform)
    {
        Vector3 desiredRotation = Vector3.up * RightJoyStick.Horizontal * TankModel.TurretRotationSpeed * RotationSpeedMultiplier;
        turretTransform.Rotate(desiredRotation, Space.Self);
    }
    private void OnEnable()
    {
        TankModel.currentHealth = TankModel.TankHealth;
        SetHealthUI();
    }

    public void ApplyDamage(float amount)
    {
        TankModel.currentHealth -= amount;
       // Debug.Log("tankhealth"+TankModel.currentHealth);

        if (!TankView.tankDead && TankModel.currentHealth <= 0f)
        {
            TankModel.currentHealth = 0;
            SetHealthUI();
            TankService.instance.CallZoomOutCamera();
            TankDestroy();            // CameraController camera_Controller = new CameraController();
            TankService.instance.BGAudio.Stop();
            return;
        }
        SetHealthUI();
    }
    
    public Transform GetTransform()
    {
        return TankView.transform;
    }
    private void SetHealthUI()
    {
        TankView.sliderHealth.value = TankModel.currentHealth / TankModel.TankHealth;
        TankView.fillImage.color = Color.Lerp(TankView.zeroHealthColor, TankView.fullHealthColor, TankModel.currentHealth / TankModel.TankHealth);
    }
    

   
    public void TankDestroy()
    {
        TankView.tankDead = true;
        TankView.gameObject.SetActive(false);  
        TankView.TankExlposionParticle.Play();  
        GameObject.Destroy(TankView.gameObject);     
        GameManager.Instance.EnableGameOverPanel();
    }

    
    public void SubscribeEvents()
    {
        EventHandler.Instance.OnBulletFired += FiredBullet;
    }

    public void UnsubscribeEvents()
    {
        EventHandler.Instance.OnBulletFired -= FiredBullet;
    }
    

    public void FiredBullet()
    {
        TankModel.BulletsFired++;
        //Debug.Log("achievemntbF");
        AchievementSystem.Instance.BulletsFiredCountCheck(TankModel.BulletsFired);
    }
}