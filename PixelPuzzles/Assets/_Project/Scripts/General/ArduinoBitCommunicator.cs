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
using System.Text;
using System.Globalization;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class ArduinoBitCommunicator : MonoBehaviour
{
    public SerialController serialController;

    string messageIN = "non empty string";

    // Executed each frame
    void Update()
    {

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

    public BitArray getMessageIN()
    {
        //return messageIN.Select(x => x == '1').ToArray();
        //return new BitArray(Encoding.ASCII.GetBytes(messageIN));
        if (messageIN == null)
            return null; // or do something else, throw, ...

        BitArray ba = new BitArray(4 * messageIN.Length);
        for (int i = 0; i < messageIN.Length; i++)
        {
            byte b = byte.Parse(messageIN[i].ToString(), NumberStyles.HexNumber);
            for (int j = 0; j < 4; j++)
            {
                ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
            }
        }
        return ba;

    }
}
