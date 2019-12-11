using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { GetKey, GetKeyDown, GetKeyUp }

public class MonoScript : MonoBehaviour
{
    public NonMonoScript[] nonMonoScripts;

    void Update()
    {
        for (int i = 0; nonMonoScripts.Length < i; i++)
        {
            //Pass MonoBehaviour to non MonoBehaviour class
            nonMonoScripts[i].InputFunction(this, nonMonoScripts[i].keyCode);

        }
    }

    public void PrintHi()
    {
        print("Hi");
    }

}

[System.Serializable]
public class NonMonoScript
{
    public InputType inputType = InputType.GetKey;
    public string keyCode;
    public string function;
    public float functionInvokeDelayTime;

    public void InputFunction(MonoBehaviour mono, string keyCode)
    {
        if (keyCode != null && function != null)
        {
            switch (inputType)
            {
                case InputType.GetKey:
                    if (Input.GetKey(keyCode))
                    {
                        mono.Invoke(function, functionInvokeDelayTime);
                    }
                    break;

                case InputType.GetKeyDown:
                    if (Input.GetKeyDown(keyCode))
                    {
                        mono.Invoke(function, functionInvokeDelayTime);
                    }
                    break;

                case InputType.GetKeyUp:
                    if (Input.GetKeyUp(keyCode))
                    {
                        mono.Invoke(function, functionInvokeDelayTime);
                    }
                    break;
            }
            
        }
    }

    public void Hey(MonoScript mono)
    {
        mono.PrintHi();
        Debug.Log("Hey");
    }


}


