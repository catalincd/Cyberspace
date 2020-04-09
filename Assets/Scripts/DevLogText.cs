using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevLogText : MonoBehaviour
{
	public TextAsset textAsset;
	public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = textAsset.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
