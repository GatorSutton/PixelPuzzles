/**
 * SerialCommUnity (Serial Communication for Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Linq;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class ArduinoCommunicator : MonoBehaviour
{
    public SerialController serialController;
    public float timeBetweenSend;

    string messageIN = "non empty string";
    string messageOUT;
    float timer;
    


    // Executed each frame
    void Update()
    {
        timer += Time.deltaTime;
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------
        if (timer > timeBetweenSend)
        {
            serialController.SendSerialMessage(messageOUT);
            timer = 0;

        }
        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------
        
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(messageIN, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(messageIN, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + messageIN);
        messageIN = message;
        

    }

    public void setMessageOUT(string data)
    {
        messageOUT = data;
    }

    public bool[] getMessageIN()
    {
        return messageIN.Select(x => x == '1').ToArray();
    }
}
