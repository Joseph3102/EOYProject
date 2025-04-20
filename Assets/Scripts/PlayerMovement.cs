using UnityEngine;
using System.IO.Ports;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{



    private Rigidbody2D body;
    private SerialPort serialPort;
    private bool isSerialConnected = false;


    IEnumerator TryConnectSerialPortCont() {
     while (!isSerialConnected){
     
            
            try{
                serialPort = new SerialPort("COM3", 9600);
                serialPort.Open();
                serialPort.ReadTimeout = 50;
                isSerialConnected = true;
                Debug.Log("Serial port connected");

            }
            catch
            {
                Debug.Log("Waiting for Arduino...");
            }

          yield return new WaitForSeconds(2f);
        
     }
    }

    IEnumerator readSerialInput()
    {
        while (true)
        {

            

            if (serialPort != null && isSerialConnected && serialPort.BytesToRead > 0)
            {
                try
                {
                    string data = serialPort.ReadLine().Trim();
                    if (data == "Left"){
                        moveLeft();
                    } else if (data == "Right")
                    {
                        moveRight();
                    } else if (data == "NoRight" || data == "NoLeft")
                {
                    noMovement();
                }



                }
                catch(System.Exception)
                {

                }
            }
            yield return new WaitForSeconds(0.1f);
            }


        }
    


    private void Awake(){

    body = GetComponent<Rigidbody2D>();
    StartCoroutine(readSerialInput());
    StartCoroutine(TryConnectSerialPortCont());
    }

    
    private void Update(){







       // body.linearVelocity = new Vector2(Input.GetAxis("Horizontal"),body.linearVelocity.y);

    }

    private void moveLeft()
    {
        body.linearVelocity = new Vector2(-2f, body.linearVelocity.y);
    }

    private void noMovement(){
         body.linearVelocity = new Vector2(0, body.linearVelocity.y);
    }

      private void moveRight()
    {
        body.linearVelocity = new Vector2(2f, body.linearVelocity.y);
    }


    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }


}
