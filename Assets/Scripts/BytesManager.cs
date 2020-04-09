using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BytesManager : MonoBehaviour
{


	public static void check()
	{
		uint[] qs = stringToArray(PlayerPrefs.GetString("Available", ""));
		uint[] ns = stringToArray(PlayerPrefs.GetString("New", ""));

		///0 - Always Available

		PlayerPrefs.SetString("Available", arrayToString(qs));
		PlayerPrefs.SetString("New", arrayToString(ns));
	}


	public static string arrayToString(uint[] arr)
    {
        string q = "" + arr[0];
        for(int i=1;i<arr.Length;i++)
        {
            q += "," + arr[i];
        }
        return q;
    }

    public static uint[] stringToArray(string q)
    {
        if(q == "")return null;

        string[] qrr = q.Split(',');
        uint[] arr = new uint[qrr.Length];
        for(int i=0;i<qrr.Length;i++)
        {
            arr[i] = UInt32.Parse(qrr[i]);
        }
        return arr;
    }
}
