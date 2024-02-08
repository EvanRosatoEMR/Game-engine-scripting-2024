using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{

    public TextMeshProUGUI label;


    public float prevInput;

 
    public bool clearPrevInput = true;


    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {

        if (clearPrevInput)
        {
            label.text = string.Empty;
            clearPrevInput = false;
        }


        label.text += input;
    }

    public void SetEquationAsAdd()
    {
        
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    public void SetEquationAsSubtract()
    {
        
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }
    
    public void SetEquationAsMultiply()
    {
        
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }
    
    public void SetEquationAsDivide()
    {
       
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }
    public void Add()
    {
        var sum = prevInput + float.Parse(label.text);
        label.text = sum.ToString();

    }

    
    public void Subtract()
    {
        
        var dif = prevInput - float.Parse(label.text);
        label.text = dif.ToString();
    }

    
    public void Multiply()
    {
        
        var quo = prevInput * float.Parse(label.text);
        label.text = quo.ToString();
    }

    
    public void Divide()
    {
        
        var div = prevInput / float.Parse(label.text);
        label.text = div.ToString();
    }

    public void Clear()
    {

        label.text = "0";
        prevInput = 0;
        clearPrevInput = false;
        equationType = EquationType.None;        
    }

    public void Calculate()
    {
        if (equationType == EquationType.ADD) Add();
        else if (equationType == EquationType.SUBTRACT) Subtract();
        else if (equationType == EquationType.MULTIPLY) Multiply();
        else Divide();
    }

    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}
