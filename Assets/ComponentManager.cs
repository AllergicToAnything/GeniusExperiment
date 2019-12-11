using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { GetKey, GetKeyDown, GetKeyUp }

public class ComponentManager : MonoBehaviour
{
    public InputFunctionClass[] inputFunction;

    

    void Update()
    {
        for (int i = 0; inputFunction.Length < i; i++)
        {
            InputFunctionClass iF = inputFunction[i];
            InputFunction(iF.inputType, iF.function, iF.keyCode, iF.functionInvokeDelayTime);
        }
    }

    public void InputFunction(InputType inputType, string function,string keyCode, float functionInvokeDelayTime)
    {
        if (keyCode != null && function != null)
        {
            switch (inputType)
            {
                case InputType.GetKey:
                    if (Input.GetKey(keyCode))
                    {
                        Invoke(function, functionInvokeDelayTime);
                    }
                    break;

                case InputType.GetKeyDown:
                    if (Input.GetKeyDown(keyCode))
                    {
                        Invoke(function, functionInvokeDelayTime);
                    }
                    break;

                case InputType.GetKeyUp:
                    if (Input.GetKeyUp(keyCode))
                    {
                        Invoke(function, functionInvokeDelayTime);
                    }
                    break;
            }

        }
    }

    public void Hey()
    {
        print("Hi");
    }
}

[System.Serializable]
public class InputFunctionClass
{
    public InputType inputType = InputType.GetKey;
    public string keyCode;
    public string function;
    public float functionInvokeDelayTime;

}


