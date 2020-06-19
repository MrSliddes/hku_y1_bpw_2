using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the room templates are stored in here
/// </summary>
public class RoomTemplates : MonoBehaviour
{    
    public GameObject[] bottomRooms; // as in opening at the bottom
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject bottomEnd;
    public GameObject topEnd;
    public GameObject leftEnd;
    public GameObject rightEnd;

    public GameObject closedRoom;
}
