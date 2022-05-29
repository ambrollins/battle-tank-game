using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Class is respponsible to Create, Destroy and Manage all the Tank MVCs in the Game.
/// </summary>
public class TankService : SingletonGeneric<TankService>
{
    public PlayerTankViewList playerTankViewList;
    public TankController tankController;
    public TankScriptableObjectList TankList;
    public BulletScriptableObjectList BulletList;
    public Joystick LeftJoyStick;
    public Joystick RightJoyStick;
    public Button FireButton;
    public CameraController cam;    
    public int TType;
    public AudioSource BGAudio;
    public AudioSource GameOverSound;

    private void Start()
    {
        StartGame();
        BGAudio.Play();
    }
   
    private void StartGame()
    {
        tankController = CreateNewPlayerTank();

    }     
   
    // This Function Creates a new Player Tank MVC & also set all the required references and returns the Tank Controller of the same.
    private TankController CreateNewPlayerTank()
    {
        TankModel tankModel = new TankModel(TankList.TankSOList[2]);
        TankController tankController = new TankController(tankModel, playerTankViewList.TankViewList[(int)TType]);
        tankController.SetJoyStickReferences(LeftJoyStick, RightJoyStick);
        cam.playerTank = tankController.GetTransform();
        tankController.TankView.SetTankControllerReference(tankController);
        return tankController;        
    }

    
    //This Function is used to communicate with Bullet Service Script when input to fire a bullet is recieved.
    public void Fire()
    {
        BulletService.Instance.FireBullet(tankController.TankView.BulletSpawner.transform, tankController.TankModel.BulletType);
        EventHandler.Instance.InvokeOnBulletFired();       
    }

    public void CallZoomOutCamera()
    {
        GameOverSound.Play();
        StartCoroutine(cam.ZoomOutCamera());//TankService.instance.cam.ZoomOutCamera();
        StartCoroutine(cam.DestroyEnvironment());
    }


}