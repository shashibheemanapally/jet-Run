using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JetMovement : MonoBehaviour
{
    public float forwardSpeedAmp = 10;
    public float strafSpeedAmp = 5;
    public float rotateAmp = 80;

    public Text score;

    private Transform shipModel;
    private float horizontalInput = 0;

    private void Start()
    {
        shipModel = getCurrentShipModel();
    }
    void Update()
    {
        forwardSpeedAmp =forwardSpeedAmp+ Time.deltaTime;
        strafSpeedAmp = forwardSpeedAmp / 2;

        //if (Input.touchCount > 0)
        //{
        //    var touch = Input.GetTouch(0);
        //    if (touch.position.x < Screen.width / 2)
        //    {
        //        horizontalInput = Mathf.Clamp(horizontalInput - Time.deltaTime * forwardSpeedAmp / 2, -1, horizontalInput);
        //    }
        //    if (touch.position.x > Screen.width / 2)
        //    {
        //        horizontalInput = Mathf.Clamp(horizontalInput + Time.deltaTime * forwardSpeedAmp / 2, horizontalInput, 1);
        //    }
        //}
        //else
        //{
        //    if (horizontalInput > 0)
        //    {
        //        horizontalInput = Mathf.Clamp(horizontalInput - Time.deltaTime * forwardSpeedAmp / 2, 0, 1);
        //    }
        //    if (horizontalInput < 0)
        //    {
        //        horizontalInput = Mathf.Clamp(horizontalInput + Time.deltaTime * forwardSpeedAmp / 2, -1, 0);
        //    }
        //}
        //transform.position += transform.forward * Time.deltaTime * forwardSpeedAmp;
        //transform.position += transform.right * Time.deltaTime * strafSpeedAmp * horizontalInput;
        //shipModel.transform.rotation = Quaternion.Euler(new Vector3(0, 180, horizontalInput * rotateAmp));
        //score.text = "score  " + ((int)transform.position.z).ToString();









        {

            transform.position += transform.forward * Time.deltaTime * forwardSpeedAmp;
            transform.position += transform.right * Time.deltaTime * strafSpeedAmp * Input.GetAxis("Horizontal");
            shipModel.transform.rotation = Quaternion.Euler(new Vector3(0, 180, Input.GetAxis("Horizontal") * rotateAmp));
            score.text = "score  " + ((int)transform.position.z).ToString();
        }



    }
    private Transform getCurrentShipModel()
    {
        foreach (Transform tr in transform)
        {
            if (tr.tag == "ShipModel")
            {
                return tr;
            }
        }
        return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You Died");
        if (PlayerPrefs.HasKey("high_score") && PlayerPrefs.GetInt("high_score")<(int)transform.position.z)
        {
            PlayerPrefs.SetInt("high_score", ((int)transform.position.z));
        }
        if (!PlayerPrefs.HasKey("high_score"))
        {
            PlayerPrefs.SetInt("high_score", ((int)transform.position.z));
        }
        
        SceneManager.LoadScene(0);
    }
}
