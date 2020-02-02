using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerColor : MonoBehaviour
{
    private static List<bool> usedList;
    private static List<ColorTableTuple> colorTable;
    public List<Color> colorList;
    [SerializeField] private SkinnedMeshRenderer rend;

    void Start()
    {
        if(colorTable == null)
        {
            colorTable = new List<ColorTableTuple>();
            for(int i = 0; i < colorList.Count; i++)
            {    
                colorTable.Add(new ColorTableTuple(){Color = colorList[i],Used = false});
            }
            
        }
        var availColors = colorTable.Where(x => x.Used == false).ToList();
        var index = Random.Range(0, availColors.Count);
        var newColor = availColors.ElementAt(index).Color;
        rend.materials[0].SetColor("_BaseColor",newColor);
        colorTable[index] = new ColorTableTuple(){Color = newColor,Used = true};


    }

    public struct ColorTableTuple
    {
        public Color Color;
        public bool Used;
    }
}
