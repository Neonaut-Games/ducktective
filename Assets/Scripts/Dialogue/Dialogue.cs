using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string author;
    [TextArea(3, 10)] public string[] messages;
}
