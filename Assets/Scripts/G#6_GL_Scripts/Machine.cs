using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private GameObject cube;
    private Vector3 offset;
    private GameObject text;
    private bool load;
    private float positionText;
    private float positionCube;
    private float positionSphere;
    private int counter;
    private int actualPosition;
    private bool success = false;
    private bool success1 = false;

    private bool error = false;
    private GameObject particle;
    private GameObject child;
    private ParticleSystem sysParticle;
    private RectTransform rt;
    private TuringMachine tm;
    private Dictionary<string, string> rm;
    private bool errorMachine;
    private char dirMachine;
    private int stateMachine;
    private bool endMachine;
    private bool Keys;
    private char symbolMachine;
    private int steps;
    private GameObject initSphere;
    private MachineRules mr;
    public GameObject player;
    public Button btnNext;
    public Button btnReset;
    public Canvas CanvasText;
    public Canvas CanvasButton;
    public GameObject sphere;
    public InputField inputMachine;
    public Dropdown dropMachine;
    public Animator anim;
    public AudioSource somesound;
    public AudioSource accsound;
    public AudioSource rejsound;

    public Sprite[] animateImages;
    public Image animatdImageObj;
    public Sprite[] animateImages1;
    public Image animatdImageObj1;



    //public GameObject accept;
    //public GameObject reject;
    //public GameObject 3dtext;
    // Use this for initialization
    void Start()
    {
     anim = GetComponent<Animator>();

        animatdImageObj.GetComponent<Image>().enabled = false;
        animatdImageObj1.GetComponent<Image>().enabled = false;



        //Initialize button
        btnNext.onClick.AddListener(BuildMachine);
        btnReset.onClick.AddListener(ResetMachine);
        btnReset.interactable = false;
        //accept.SetActive(false);
        //reject.SetActive(false);
        success1= false;
        error = false;

        //distance cube 31
        positionCube = -156f;
        positionText = -301.5f;
        positionSphere = -299f;
        actualPosition = 3;
        counter = 0;

        //Initialize
        InitializeValuesTuring();
        mr = new MachineRules();
        Keys = false;

        //camera view

        offset = transform.position - player.transform.position;
    }

    void InitializeValuesTuring()
    {
        errorMachine = false;
        dirMachine = 'I';
        stateMachine = 0;
        endMachine = false;
        steps = 0;
    }

    void AddBlock(string text)
    {

        //block is added
        CreateCube();

        //sphere is added
        CreateSphere();

        //text is added
        CreateText(text);

        positionCube += 99.0f;
        positionText += 99.0f;
        positionSphere += 99.0f;
        counter++;
    }

    //build the machine
    void BuildMachine()
    {
        string tira = "βββ" + inputMachine.text + "βββ";
        CreateMachine(tira);

        switch (dropMachine.value)
        {
            case 0:
                //Machine Palindrome
                string pattern = @"([0-1]+)$";  //validate String
                Match match = Regex.Match(inputMachine.text, pattern);

                if (match.Success)
                {
                    rm = mr.getPal();
                    tm = new TuringMachine(tira, rm, new int[] { 11 });
                }

                break;

            case 1:
                //Rehana's Language E={0^i1^j|i>j}
                string Rpattern = @"([0-1-X-Y]+)$";  //validate String
                Match Rmatch = Regex.Match(inputMachine.text, Rpattern);

                if (Rmatch.Success)
                {
                    rm = mr.getRehana();
                    tm = new TuringMachine(tira, rm, new int[] { 6 });
                }

                break;

            case 2:
                //Anber's Language WW^R W={ab}*
                string Apattern = @"([a-b]+)$";  //validate String
                Match Amatch = Regex.Match(inputMachine.text, Apattern);

                if (Amatch.Success)
                {
                    rm = mr.getAnber();
                    tm = new TuringMachine(tira, rm, new int[] { 8 });
                   
                }

                break;

            case 3:
                //Anita's Language L={a^m b^n c^m*n|m,n>=1}
                string Anitapattern = @"([a-b-c]+)$";  //validate String
                Match Anitamatch = Regex.Match(inputMachine.text, Anitapattern);

                if (Anitamatch.Success)
                {
                    rm = mr.getAnita();
                    tm = new TuringMachine(tira, rm, new int[] { 6 });
                }

                break;
        }

        //Disble buttons when the machine is working
        btnNext.interactable = false;
        inputMachine.interactable = false;
        dropMachine.interactable = false;
        btnReset.interactable = false;

        //And now Turn on the Machine
        //InvokeRepeating("MotorMachine", 0.5f, 0.5f);
        StartCoroutine(MotorMachine());
    }

    //This is the motor of the TuringMachine
    IEnumerator MotorMachine()
    {
        bool addSymbol = false;
        if (endMachine || errorMachine)
        {
            //recursion is over
        }
        else
        {
            tm.WorkMachine(ref errorMachine, ref dirMachine, ref stateMachine, ref endMachine, ref symbolMachine, ref addSymbol);
            steps++;
            if (errorMachine)
            {
                ErrorMachine();
                btnReset.interactable = true;
            }
            else
            {
                ChangeTextElement("text" + actualPosition, symbolMachine.ToString());

                yield return new WaitForSeconds(0.2f);

                ChangeTextElement("txtState", "Current State: " + stateMachine);
                ChangeTextElement("txtSteps", "Steps: " + steps);

                if (dirMachine == 'R')
                {
                    MoveToTheRight();
                }
                else if (dirMachine == 'L')
                {
                    MoveToTheLeft();
                }

                if (addSymbol)
                {
                    AddBlock("β");
                }

                if (endMachine)
                {
                    SuccessMachine();
                    btnReset.interactable = true;
                }

                yield return new WaitForSeconds(0.2f);

            }

        }
    }

    void CreateMachine(string tira)
    {
        for (int i = 0; i < tira.Length; i++)
        {
            AddBlock(tira[i].ToString());
        }

        particle = GameObject.Find("sphere3").gameObject;
        child = particle.transform.Find("Circle").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(0.078f, 1.0f, 0.65f, 1f);
        child = particle.transform.Find("Beam").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(0.078f, 1.0f, 0.65f, 1f);
        child = particle.transform.Find("Electric").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(0.078f, 1.0f, 0.65f, 1f);
        child = particle.transform.Find("Smog").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(0.078f, 1.0f, 0.65f, 1f);
        initSphere = GameObject.Find("Sphere").gameObject;
        initSphere.SetActive(false);
    }

    void ErrorMachine()
    {
        error = true;
        print("error");
        //the lights turn red
        for (int i = 0; i < counter; i++)
        {
            ChangeColorSphere(i, 0.82f, 0.0f, 0.0f);
        }
        ChangeTextElement("error", "Error Machine");
       // ChangeTextElement("txtSteps", "Error Machine");
        //the head is red
        particle = GameObject.Find("PointPlayer").gameObject;
        particle.GetComponent<Light>().color = new Color(0.82f, 0.0f, 0.0f, 1f);
        Keys = true;
        rejsound.Play();
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        Animator anim = ob.GetComponent<Animator>();
        anim.Play("DAMAGED01", -1, 0f);
    }

    void SuccessMachine()
    {
        success1 = true;
        print("282: successed changed");
        //the lights turn green
        for (int i = 0; i < counter; i++)
        {
            ChangeColorSphere(i, 0.078f, 1.0f, 0.65f);
        }
        ChangeTextElement("Success", "Success Machine");
        //the head is green
        /*
        particle = GameObject.Find("PointPlayer").gameObject;
        particle.GetComponent<Light>().color = new Color(0.0f, 1.0f, 0.0f, 1f);
        particle.GetComponent<Animator>().enabled = true;
        particle.GetComponent<Animator>().Play("JUMP01");
        */
        accsound.Play();
        //anim.Play("JUMP01",-1,0f);
        //print ("accepted")
        GameObject ob = GameObject.FindGameObjectWithTag("Player");
        Animator anim = ob.GetComponent<Animator>();
        anim.Play("WAIT04", -1, 0f);
        //anim.Play("run");
        // Debug.Log("Accept");
        Keys = true;
        // accept.SetActive(true);

    }

    void ResetMachine()
    {
        //remove objects from the scene
        for (int i = 0; i < counter; i++)
        {
            particle = GameObject.Find("CubeMachine" + i).gameObject;
            Destroy(particle);
            particle = GameObject.Find("sphere" + i).gameObject;
            Destroy(particle);
            particle = GameObject.Find("text" + i).gameObject;
            Destroy(particle);
        }

        //reset the initial values
        player.transform.position = new Vector3(-7.4f, -15.9f, 51.9f);
        CanvasButton.transform.position = new Vector3(-62.4f, -8.900002f, 52.90002f);
        positionCube = -156;
        positionText = -301.5f;
        positionSphere = -299f;
        actualPosition = 3;
        counter = 0;
        Keys = false;
        success1 = false;
        error = false;
        
        //the head is yellow
        particle = GameObject.Find("PointPlayer").gameObject;
        particle.GetComponent<Light>().color = new Color(1f, 0.89f, 0.0f, 1f);

        //Initialize Turing Values
        InitializeValuesTuring();

        //the texts are restarted
        ChangeTextElement("txtState", "Current State: 0");
        ChangeTextElement("txtSteps", "Steps: 0");

        //Enable buttons
        btnNext.interactable = true;
        inputMachine.interactable = true;
        dropMachine.interactable = true;
        btnReset.interactable = false;

        initSphere.SetActive(true);
    }

    void CreateCube()
    {
        cube = Instantiate(GameObject.FindGameObjectWithTag("Cube")) as GameObject;
        cube.transform.position = new Vector3(positionCube, 4.0f, 100.0f);
        cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        cube.name = "CubeMachine" + counter;
       
    }

    void CreateText(string character)
    {
        text = new GameObject("text" + counter, typeof(RectTransform));
        rt = (RectTransform)text.transform;
        rt.sizeDelta = new Vector2(20.0f, 20.0f);
        var newTextComp = text.AddComponent<Text>();
        newTextComp.text = character;
        newTextComp.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        newTextComp.fontSize = 16;
        newTextComp.fontStyle = FontStyle.Bold;
        newTextComp.transform.SetParent(CanvasText.transform, true);
        newTextComp.transform.position = new Vector3(positionText, 9f, 54.5f);
    }

    void CreateSphere()
    {
        sphere = Instantiate(GameObject.FindGameObjectWithTag("Particle")) as GameObject;
        sphere.transform.position = new Vector3(positionSphere, 39.6f, 66.8f);
        sphere.transform.localScale = new Vector3(1, 1, 1);
        sphere.name = "sphere" + counter;
    }

    void MoveToTheLeft()
    {
        //Put yellow color to the last sphere position
        ChangeColorSphere(actualPosition, 1f, 0.89f, 0.0f);

        //Put green color to the actual sphere position
        ChangeColorSphere(--actualPosition, 0.078f, 1.0f, 0.65f);

        //move elements
        player.transform.Translate(-99.0f, 0.0f, 0.0f);
        CanvasButton.transform.Translate(-99.0f, 0.0f, 0.0f);
    }

    void MoveToTheRight()
    {
        //Put yellow color to the last sphere position
        ChangeColorSphere(actualPosition, 1f, 0.89f, 0.0f);

        //Put green color to the actual sphere position
        ChangeColorSphere(++actualPosition, 0.078f, 1.0f, 0.65f);

        //move elements
        player.transform.Translate(99.0f, 0.0f, 0.0f);
        CanvasButton.transform.Translate(99.0f, 0.0f, 0.0f);
    }

    //Change the color of a sphere in some position
    void ChangeColorSphere(int number, float RColor, float GColor, float BColor)
    {
        /*particle = GameObject.Find("sphere" + number).gameObject;
        child = particle.transform.Find("Circle").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(RColor, GColor, BColor, 1f);
        child = particle.transform.Find("Beam").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(RColor, GColor, BColor, 1f);
        child = particle.transform.Find("Electric").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(RColor, GColor, BColor, 1f);
        child = particle.transform.Find("Smog").gameObject;
        child.GetComponent<ParticleSystem>().startColor = new Color(RColor, GColor, BColor, 1f);*/
    }

    void ChangeTextElement(string element, string text)
    {
        particle = GameObject.Find(element).gameObject;
        particle.GetComponent<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (success1)
        {
            print("success: " + success1);
            animatdImageObj.GetComponent<Image>().enabled = true;
            animatdImageObj.sprite = animateImages[(int)(Time.time * 10) % animateImages.Length];
        }
       if(error)
        {
            print("error:" + error);
            animatdImageObj1.GetComponent<Image>().enabled = true;
            animatdImageObj1.sprite = animateImages1[(int)(Time.time * 10) % animateImages.Length];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            somesound.Play();
            
            StartCoroutine(MotorMachine());
        }
        if (!Keys)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            if (Input.GetKey("up"))
                transform.Translate(0.0f, 0.0f, 0.20f);

            if (Input.GetKey("down"))
                transform.Translate(0.0f, 0.0f, -0.20f);

        }
    }
}
