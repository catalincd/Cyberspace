using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTexture : MonoBehaviour
{

    public GameObject objectx;
	public Texture[] maps;
	Material mat;



    // Start is called before the first frame update
    void Start()
    {
        //Texture map = maps[PlayerPrefs.GetInt("selected")];
        if(Data.playerTexture != null)
        {
            Texture map = Data.playerTexture;
            mat = objectx.GetComponent<Renderer>().material;
            mat.SetTexture("_BaseMap", map);
            mat.SetTexture("_EmissionMap", map);
        }
    }

    void setTexture(Texture newTexture)
    {
    	//map = newTexture;
    	mat.SetTexture("_BaseMap", newTexture);
        mat.SetTexture("_EmissionMap", newTexture);
    }

    public static void setMaterial(Material mt, GameObject obj)
    {
        obj.GetComponent<Renderer>().material = mt;
    }

    public static void setTexture(Texture newTexture, GameObject obj)
    {
        Material newMat = obj.GetComponent<Renderer>().material;
        newMat.SetTexture("_BaseMap", newTexture);
        newMat.SetTexture("_EmissionMap", newTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
