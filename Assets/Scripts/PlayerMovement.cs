using UnityEngine;
using System.IO.Ports;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SerialPort serialPort;
    private bool isSerialConnected = false;
    private float horizontalInputFromArduino = 0f;

    [SerializeField] private float speed = 4;
    [SerializeField] private float jump = 5;

    IEnumerator TryConnectSerialPortCont()
    {
        while (!isSerialConnected)
        {
            try
            {
                serialPort = new SerialPort("COM5", 9600);
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
                    if (data == "Left")
                    {
                        moveLeft();
                    }
                    else if (data == "Right")
                    {
                        moveRight();
                    }
                    else if (data == "NoRight" || data == "NoLeft")
                    {
                        noMovement();
                    }

                    if (data == "Jump")
                    {
                        if (Mathf.Abs(body.linearVelocity.y) < 0.01f)
                        {
                            body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
                        }
                    }
                }
                catch (System.Exception)
                {
                    // Ignore read errors
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(readSerialInput());
        StartCoroutine(TryConnectSerialPortCont());
    }

    private void Update()
    {
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float horizontalInput = horizontalInputFromArduino;

        // Keyboard overrides Arduino input if pressed
        if (keyboardInput != 0)
        {
            horizontalInput = keyboardInput;
        }

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Jump with Space key
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        }

        // Flip sprite based on movement direction
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void moveLeft()
    {
        horizontalInputFromArduino = -1f;
    }

    private void moveRight()
    {
        horizontalInputFromArduino = 1f;
    }

    private void noMovement()
    {
        horizontalInputFromArduino = 0f;
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}