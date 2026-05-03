using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    public static DebugController Instance;
    bool showConsole;
    bool showHelp;
    string input;

    // Here's the list of commands we have:
        // TP to each area
    public static DebugCommand TP_A1;
    public static DebugCommand TP_A2;
    public static DebugCommand TP_A3;
    public static DebugCommand TP_A4;
    public static DebugCommand TP_T1;
    public static DebugCommand TP_Cent;
    public static DebugCommand TP_End;
    public static DebugCommand TP_Tut;
    public static DebugCommand TP_TutC;
        // Autocomplete puzzles
    public static DebugCommand Auto_Tut;
       // public static DebugCommand MORE AUTO PUZZLES!!
    public static DebugCommand<int> set_player_speed;
    public static DebugCommand help;

    public List<object> commandList;
    public TransitionManager transitionManager;

    private void Awake()
    {
        Instance = this;
        TP_A1 = new DebugCommand("TP_A1", "Teleport directly to Area 1", "TP_A1", () =>
        {
           transitionManager.CheatTP("Area1");
        });

        TP_A2 = new DebugCommand("TP_A2", "Teleport directly to Area 2", "TP_A2", () =>
        {
           transitionManager.CheatTP("Area2");
        });
        TP_A3 = new DebugCommand("TP_A3", "Teleport directly to Area 3", "TP_A3", () =>
        {
           transitionManager.CheatTP("Area3");
        });

        TP_A4 = new DebugCommand("TP_A4", "Teleport directly to Area 4", "TP_A4", () =>
        {
           transitionManager.CheatTP("Area4");
        });

        TP_T1 = new DebugCommand("TP_T1", "Teleport directly to Treasure 1", "TP_T1", () =>
        {
            transitionManager.CheatTP("Treasure1");
        });

        TP_Cent = new DebugCommand("TP_Cent", "Teleport directly to Central", "TP_Cent", () =>
        {
           transitionManager.CheatTP("Central");
        });

        TP_End = new DebugCommand("TP_End", "Teleport directly to TheEnd", "TP_End", () =>
        {
            transitionManager.CheatTP("TheEnd");
        });

        TP_Tut = new DebugCommand("TP_Tut", "Teleport directly to Tutorial", "TP_Tut", () =>
        {
            transitionManager.CheatTP("Tutorial");
        });
        
        TP_TutC = new DebugCommand("TP_TutC", "Teleport directly to Tutorial Continued", "TP_TutC", () =>
        {
           transitionManager.CheatTP("TutorialContinued");
        });

        help = new DebugCommand("help", "Show list of all commands", "help", ()=>
        {
           showHelp=true; 
        });

        commandList = new List<object>
        {
            TP_A1, 
            TP_A2, 
            TP_A3, 
            TP_A4,
            TP_T1,
            TP_Cent, 
            TP_End,
            TP_Tut,
            TP_TutC, 
            help,
            //Auto_Tut,
            //set_player_speed
        };
    }
    

    // Check for when the magical keyboard combo is pressed and open/close console
    public void ToggleConsole()
    {
        Debug.Log("OnOpenDebugConsole called");
        showConsole = !showConsole;
        print("Debug console is now " + showConsole);
    }

    public void HandleReturn()
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
            
        }
    }

    Vector2 scroll;
    private void OnGUI()
    {
        if(!showConsole) { return; }
        float y = 2f;

        if(showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 250), "");

            GUIStyle helpStyle = new GUIStyle(GUI.skin.label);
            helpStyle.fontSize = 40; // increase this value to make text bigger

            Rect viewport = new Rect(0, 0, Screen.width - 30, 60 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 240), scroll, viewport);
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 40 * i, viewport.width - 100, 50);
                GUI.Label(labelRect, label, helpStyle);
            }
            GUI.EndScrollView();
            y += 250;
    
        }

        GUIStyle consoleStyle = new GUIStyle(GUI.skin.textField);
        consoleStyle.fontSize = 40; 
        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f,50f), input, consoleStyle);
        GUI.FocusControl("console");
        GUI.Box(new Rect(5f,y, Screen.width, 60f), "");
        GUI.backgroundColor = new Color(0,0,0,0);
        
    }

    private void HandleInput()
    {
        print("Handling input: " + input);

        for(int i = 0; i<commandList.Count; i++)
        {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
                if(commandBase == null)
                {
                    Debug.Log($"[DebugController] commandList[{i}] is not a DebugCommandBase or is null.");
                    continue;
                }
                if(input.Contains(commandBase.commandId))
                {
                    Debug.Log($"[DebugController] Matched command: {commandBase.commandId}");
                    // Does the type of object fit the cast?
                    if(commandList[i] as DebugCommand != null)
                    {
                        Debug.Log($"[DebugController] Invoking DebugCommand: {commandBase.commandId}");
                        // If so, roll it back and invoke it.
                        (commandList[i] as DebugCommand).Invoke();
                    }
                    else if(commandList[i] as DebugCommand<int> != null)
                    {
                        Debug.Log($"[DebugController] Invoking DebugCommand<int>: {commandBase.commandId} with param ");
                        string[] properties = input.Split(' ');
                        // If so, roll it back and invoke it with the parameter parsed out of the input string.
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                }
        }
    }
}