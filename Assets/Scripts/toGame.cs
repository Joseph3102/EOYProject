using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class SerialSceneLoader : MonoBehaviour
{
    public string portName = "COM5";     // Change this to match your system (e.g., COM4, /dev/ttyUSB0 on Mac/Linux)
    public int baudRate = 9600;          // Make sure it matches your Arduino
    public string targetScene = "scene#1"; // Replace with the name of the scene you want to load

    private SerialPort serialPort;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
            Debug.Log("Serial port opened: " + portName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string input = serialPort.ReadLine().Trim();
                Debug.Log("Serial input: " + input);

                if (input == "Jump")
                {
                    SceneManager.LoadScene(targetScene);
                }
            }
            catch (System.TimeoutException) { }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed.");
        }
    }
}
