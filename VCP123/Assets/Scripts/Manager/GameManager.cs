using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int _lives = 10;

    public int lives
    {
        get
        {
            return _lives;
        }
        set
        { 
            
        }
}
