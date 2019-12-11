using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { GetKey, GetKeyDown, GetKeyUp }

public class ComponentManager : MonoBehaviour
{
    public InputFunctionClass[] inputFunction;
    public bool debug;

    void Update()
    {
        RunInputClassFunction();
    }

    public void RunInputClassFunction()
    {
        foreach (InputFunctionClass r in inputFunction)
        {
            if (r.monobehaviourScript != null)
            {
                r.InputFunction(r.monobehaviourScript, r.inputType, r.function, r.keyCode, r.functionInvokeDelayTime);
            }
            else
            {
                r.InputFunction(this, r.inputType, r.function, r.keyCode, r.functionInvokeDelayTime);
            }
            if (debug)
            {
                if (Input.anyKeyDown)
                {
                    Debug.Log("GET!! : " + r.monobehaviourScript.name);
                }
            }
        }
    }

    public void Test01()
    {
        if (debug)
        {
            Debug.Log(name+ " : Congratz!! You are so brilliant!!");
        }
    }

}

[System.Serializable]
public class InputFunctionClass
{
    public MonoBehaviour monobehaviourScript;
    public InputType inputType = InputType.GetKeyDown;
    public string keyCode;
    public string function;
    public float functionInvokeDelayTime;

    public void InputFunction(MonoBehaviour mono, InputType inputType, string function, string keyCode, float functionInvokeDelayTime)
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

}


