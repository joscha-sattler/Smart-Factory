using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSCSendReceive : MonoBehaviour
{
	/// <summary>
	/// Source: https://github.com/heaversm/unity-osc-receiver/blob/master/Assets/Scripts/OSCTestSender.cs
	/// Modified Aug 2019: Leonard Paul - School of Video Game Audio - http://SoVGA.com
	/// </summary>   
	private Osc oscHandler;

    public string remoteIp = "127.0.0.1";	// point to localhost -> could also be a different machine
    public int sendToPort = 8001;
    public int listenerPort = 9001;
	public float m_size;	// used to set the y_scale of the tanks

	~OSCSendReceive()
    {
        if (oscHandler != null)
        {            
            oscHandler.Cancel();
        }

        // speed up finalization
        oscHandler = null;
        System.GC.Collect();
    }
		
	/// <summary>
	/// Send out OSC message to be played by Pure Data.
	/// </summary>
	public void PlaySoundOSC( string oscString )
	{		
		OscMessage oscM = Osc.StringToOscMessage ( oscString );
		oscHandler.Send (oscM);
	}
		
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(remoteIp, sendToPort, listenerPort);
		
		oscHandler = GetComponent<Osc>();
		oscHandler.init(udp);
		
		// set up a function to receive messages via OSC with starting with /sentFromPd
		oscHandler.SetAddressHandler("/sentFromPd", SentFromPd);
    }

	/// <summary>
	/// Shut down OSC connection nicely.
	/// </summary>
    void OnDisable()
    {
        // close OSC UDP socket
        // Debug.Log("closing OSC UDP socket in OnDisable");
        oscHandler.Cancel();
        oscHandler = null;
    }

	/// <summary>
	/// Call SentFromPd function when the /sentFromPd OSC message is received.
	/// </summary>
	public void SentFromPd(OscMessage m)
    {
		string s = (Osc.OscMessageToString (m)).Substring( ("/sentFromPd".Length+1) );

		// set the tank height variable to the float received from Pd
		m_size = System.Convert.ToSingle (s);
    }
}