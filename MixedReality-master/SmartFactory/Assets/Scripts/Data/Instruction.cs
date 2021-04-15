using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction
{
    List<Coordinates> path;
    string stationType;
    List<string> input;
    List<string> output;

    public List<Coordinates> Path { get { return path; } }
    public string StationType { get { return stationType; } }
    public List<string> Input { get { return input; } set { input = value; } }
    public List<string> Output { get { return output; } set { output = value; } }

    public Instruction(List<Coordinates> path_, string stationType_)
    {
        path = path_;
        stationType = stationType_;
        input = new List<string>();
        output = new List<string>();
    }

    public void AddInput(string item)
    {
        input.Add(item);
    }

    public void AddOutput(string item)
    {
        output.Add(item);
    }

    public string PopInput()
    {
        if (input.Count == 0)
        {
            return null;
        }

        string item = input[0];
        input.RemoveAt(0);

        return item;
    }

    public string PopOutput()
    {
        string item = output[0];
        output.RemoveAt(0);

        return item;
    }
}
