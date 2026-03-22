using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    // Here's the list of commands we have:
        // TP to each area
    public static DebugCommand TP_A1;
    public static DebugCommand TP_A2;
    public static DebugCommand TP_A3;
    public static DebugCommand TP_A4;
    public static DebugCommand TP_Cent;
    public static DebugCommand TP_TutC;
        // Autocomplete puzzles
    public static DebugCommand Auto_Tut;
       // public static DebugCommand MORE AUTO PUZZLES!!
    public static DebugCommand set_player_speed;

    public List<object> commandList;

    private void Awake()
    {
        TP_A1 = new DebugCommand("TP_A1", "Teleport directly to Area 1", "TP_A1", () =>
        {
           // command things 
        });

        commandList = new List<object>
        {
            TP_A1, 
            TP_A2, 
            TP_A3, 
            TP_A4, 
            TP_Cent, 
            TP_TutC, 
            Auto_Tut, 
            set_player_speed
        };
    }
    

    // Check for when the magical keyboard combo is pressed and open/close console
    public void OnOpenDebugConsole(InputValue value)
    {
        showConsole = !showConsole;
        print("Debug console is now " + showConsole);
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void OnGUI()
    {
        if(!showConsole) { return; }
        float y = 2f;
        GUI.Box(new Rect(0,y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0,0,0,0);
        input = GUI.TextField(new Rect(10f,y + 5f, Screen.width-20f, 20f), input);
    }

    private void HandleInput()
    {
        for(int i = 0; i<commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId))
            {
                // Does the type of object fit the cast?
                if(commandList[i] as DebugCommand != null)
                {
                    // If so, roll it back and invoke it.
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
