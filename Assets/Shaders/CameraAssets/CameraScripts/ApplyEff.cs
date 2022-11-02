using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
public class ApplyEff : MonoBehaviour {

    private const string LINE_COLOR = "_OutLineColor",
                         LINE_ONLY = "_OutLineOnly";

    RenderTexture rt;
    public Transform Cam_2;

    public Material Post;
    public List<Material> mats;
    private int matID;

    public Transform EffText, 
                     slider, 
                     Fps;

	Ray ray;
    RaycastHit hit;
    public Color color;
	public Texture2D T2d;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.targetFrameRate = 60;
        slider.GetComponent<Slider>().onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        if (Cam_2 != null) {
            rt = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
            Shader.SetGlobalTexture("_Replace", rt);
            Cam_2.GetComponent<Camera>().targetTexture = rt;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        Fps.GetComponent<Text>().text = "FPS: " + (1/ Time.smoothDeltaTime).ToString();

		if (Input.GetMouseButton (0)) {//If on pc it needs to get the mouse position
			
			ChangeColor (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
		
		} else if (Input.touchCount > 0) {//If on mobile it needs to get the touch
		
			ChangeColor (new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		}

        //AnimateEffect.AnimateEff(Post);
    }

	public void ChangeColor(Vector2 ScreenPos){
		if (Input.GetMouseButton (0)) { 
			ray = Camera.main.ScreenPointToRay (ScreenPos);

			if (Physics.Raycast (ray, out hit) && hit.transform.gameObject.name == "ColorPalet") {
				color = T2d.GetPixel ((int)Mathf.Round(hit.textureCoord.x*T2d.width),(int)Mathf.Round(hit.textureCoord.y*T2d.height) );
				Post.SetColor(LINE_COLOR, color);
			}
		}	
	} 
    
    private void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
    }
    
    public void changeMaterial()
    {
        if (matID == (mats.Count)) matID = 0;
        Post = mats[matID];
     	float value = slider.GetComponent<Slider>().value;
        Post.SetFloat(LINE_ONLY, value);
		Post.SetColor(LINE_COLOR, color);

        EffText.GetComponent<Text>().text = mats[matID].name;
        matID++;
    }

    public void ValueChangeCheck() {
        float value = slider.GetComponent<Slider>().value;
        Post.SetFloat(LINE_ONLY, value);
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, Post);
    }

    public void load_2Cameras() {
        SceneManager.LoadScene("2CamerasSolution", LoadSceneMode.Single);
    }

	public void load_SingleCamera() {
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

}
