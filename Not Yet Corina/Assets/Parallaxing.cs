using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;         //lista dos backgrounds a serem usados no parallax
    private float[] parallaxScales;         //Proporção do moviemtno da câmera para mover os bgs
    public float smoothing = 1f;            //how smooth the parallax is going to be. Set this above zero

    private Transform cam;                  //Referencial da MainCamera
    private Vector3 previousCamPosition;    //Posição da câmera no fram anterior

    //Awake é chamada antes do Start()
    void Awake()
    {
        //Referencial da câmera
        cam = Camera.main.transform;
    }
    

	// Use this for initialization
	void Start () {
        //Armazenar o frame anterior
        previousCamPosition = cam.position;

        //Assigning correspondent parallax scales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    //Para cada background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //Parallax é o oposto do movimento da câmera
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            //Set a target x position which is the current position plus the parallax 
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //Set the previous cam position to the camera's position at the end of the frame
        previousCamPosition = cam.position;
	}
}
