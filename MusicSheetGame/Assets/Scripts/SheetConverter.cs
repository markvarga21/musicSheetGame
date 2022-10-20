using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetConverter : MonoBehaviour
{
    public static Dictionary<string, int> dict = new Dictionary<string, int>() {
            {"C", 0}, {"D", 1}, {"E", 2}, {"F", 3}, {"G", 4}, {"A", 5}, {"H", 6}, 
            {"^C", 7}, {"^D", 8}, {"^E", 9}, {"^F", 10}, {"^G", 11}, {"^A", 12}
    };

    public int[] getLineIndexArrayForString(string str)
    {
        int strLength = str.Replace("^", "").Length;
        int[] arr = new int[strLength];
        int index = 0;
        while (index < strLength) {
            string note = str[index].ToString();
            int line = -1;
            if (note == "^") {
                string upComing = str[index+1].ToString();
                string concated = "^" + upComing;
                line = SheetConverter.dict[concated];
                arr[index] = line;
                index++;
            } else {
                line = SheetConverter.dict[note];
                arr[index] = line;
                index++;
            }
        }
        return arr;
    }
}
