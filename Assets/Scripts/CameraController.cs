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
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTank;
    public Vector3 offset;
    public Camera camera;
    private float CameraZoomOutSpeed = 0.0001f;


    public void Start()
    {
        playerTank = FindObjectOfType<TankView>().transform;
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
        Debug.Log("zoom out hoja yaar");
        float lerp = 0.01f;
        //camera.transform.SetParent(null);
        while (camera.orthographicSize < 30)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 30, lerp);
            lerp = lerp + CameraZoomOutSpeed;
            yield return new WaitForSeconds(0.01f);
        }

    }


}
