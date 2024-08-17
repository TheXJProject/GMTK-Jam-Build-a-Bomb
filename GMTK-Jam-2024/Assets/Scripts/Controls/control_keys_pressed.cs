using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class control_keys_pressed : MonoBehaviour
{
    public int[] keysPressed = new int[26]; // Defaultly, each keysPressed[i] is set to 0, where 0 means the key is not held and 1 means it is being held
    public int[] keysProbsNeed = new int[26]; // labels the keys that are currently in use by a problem, 0 means it isn't and 1 means it is
    public string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    
    Player_controller inputActions;
    InputAction[] keys = new InputAction[26];

    int alphabetLength = 26;

    private void Awake()
    {
        inputActions = new Player_controller();
    }

    private void OnEnable() // Enables every button press
    {
        keys[0] = inputActions.keyboard.a;
        keys[1] = inputActions.keyboard.b;
        keys[2] = inputActions.keyboard.c;
        keys[3] = inputActions.keyboard.d;
        keys[4] = inputActions.keyboard.e;
        keys[5] = inputActions.keyboard.f;
        keys[6] = inputActions.keyboard.g;
        keys[7] = inputActions.keyboard.h;
        keys[8] = inputActions.keyboard.i;
        keys[9] = inputActions.keyboard.j;
        keys[10] = inputActions.keyboard.k;
        keys[11] = inputActions.keyboard.l;
        keys[12] = inputActions.keyboard.m;
        keys[13] = inputActions.keyboard.n;
        keys[14] = inputActions.keyboard.o;
        keys[15] = inputActions.keyboard.p;
        keys[16] = inputActions.keyboard.q;
        keys[17] = inputActions.keyboard.r;
        keys[18] = inputActions.keyboard.s;
        keys[19] = inputActions.keyboard.t;
        keys[20] = inputActions.keyboard.u;
        keys[21] = inputActions.keyboard.v;
        keys[22] = inputActions.keyboard.w;
        keys[23] = inputActions.keyboard.x;
        keys[24] = inputActions.keyboard.y;
        keys[25] = inputActions.keyboard.z;
        
        for (int i = 0; i < alphabetLength; i++)
        {
            keys[i].Enable();
            keys[i].performed += AssignButtonDown;
            keys[i].canceled += AssignButtonUp;
        }
    }

    private void OnDisable() // Disables every button press
    {
        for (int i = 0; i < alphabetLength; i++)
        {
            keys[i].Disable();
        }
    }

    void AssignButtonDown(InputAction.CallbackContext context) // Goes through every key to test which one has been pressed and sets the correct value
    {
        for (int i = 0; i < alphabetLength; i++)
        {
            if (string.Equals(context.action.name, alphabet[i])) 
            {
                keysPressed[i] = 1;
                break;
            }
        }
    }

    void AssignButtonUp(InputAction.CallbackContext context) // Goes through every key to test which one has been released and sets the correct value
    {
        for (int i = 0; i < alphabetLength; i++)
        {
            if (string.Equals(context.action.name, alphabet[i]))
            {
                keysPressed[i] = 0;
                break;
            }
        }
    }
}
