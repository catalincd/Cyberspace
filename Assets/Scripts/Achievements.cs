using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{

	public static int getDeaths()
	{
		return PlayerPrefs.GetInt("A_DEATHS", 0);
	}

	public static void incDeaths()
	{
		PlayerPrefs.SetInt("A_DEATHS", getDeaths() + 1);	
	}

	public static int getRespawns()
	{
		return PlayerPrefs.GetInt("A_RESP", 0);
	}

	public static void incRespawns()
	{
		PlayerPrefs.SetInt("A_RESP", getRespawns() + 1);	
	}

	public static int getUps()
	{
		return PlayerPrefs.GetInt("A_UPS", 0);
	}

	public static void incUps()
	{
		PlayerPrefs.SetInt("A_UPS", getUps() + 1);	
	}

	public static int getBytes()
	{
		return PlayerPrefs.GetInt("A_BYTES", 0);
	}

	public static void incBytes(int num)
	{
		PlayerPrefs.SetInt("A_BYTES", getBytes() + num);	
	}
}
