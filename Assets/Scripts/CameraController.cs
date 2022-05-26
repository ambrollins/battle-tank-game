//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    public Transform playerTank;
//    //private TankType tankType;
   

//    private void LateUpdate()
//    {
//        transform.position = playerTank.position;
//    }
//}

using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTank;
    public Vector3 offset;
    public Camera camera;
    private float CameraZoomOutSpeed = 0.0001f;



    public void Start()
    {
        playerTank = GameObject.FindObjectOfType<TankView>().transform;
    }

    void Update()
    {
        CheckPlayer();
        transform.position = playerTank.transform.position + offset;
    }

    private void CheckPlayer()
    {
        if (playerTank == null)
        {
            playerTank = transform;
            return;
        }
    }

    private void LateUpdate()
    {

        offset = transform.position - playerTank.transform.position;

    }
    public IEnumerator ZoomOutCamera()  
    {
        //Debug.Log("zoom out hoja yaar");
        float lerp = 0.01f;
        //camera.transform.SetParent(null);
        while (camera.orthographicSize < 30f)
        {
            
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 30f, lerp);
            lerp += CameraZoomOutSpeed;
            yield return new WaitForSeconds(0.01f);
            //WaitForSeconds waitForSeconds = new WaitForSeconds(5f);
            Debug.Log("zoom out hoja yaar");
            //Debug.Log("zoom out hoja yaar");
        }
        

    }


}
