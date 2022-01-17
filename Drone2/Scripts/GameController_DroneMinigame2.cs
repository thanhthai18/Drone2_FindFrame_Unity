using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_DroneMinigame2 : MonoBehaviour
{
    public static GameController_DroneMinigame2 instance;
    public Camera mainCamera;
    public List<GameObject> listFlyCam = new List<GameObject>();
    public bool isHoldBG;
    public bool isWin;
    public Vector2 mousePos;
    public Vector2 tmpPos_BG;
    public Vector2 tmpPos_Frame;
    public GameObject backGround;
    public RaycastHit2D[] hit;
    public bool isHoldFrame;
    public FrameObj_DroneMinigame2 tmpFrame;
    public float f2;
    public float sizeCamera;
    public Vector2 startPosTmpFrame;
    public List<Vector2> listPosHideFrame = new List<Vector2>();
    public Hide_DroneMinigame2 hideFramePrefab;
    public List<Hide_DroneMinigame2> listHideObj = new List<Hide_DroneMinigame2>();
    public bool isCompleted;
    public int countWin;
    public DroneObj_DroneMinigame2 droneObj;
    public GameObject tutorial, tutorial2;
    public Canvas canvas;
    public bool isBegin;
    public int indexFrame;



    public int CountWin
    {
        get => countWin;
        set
        {
            if (countWin <= 6)
            {
                countWin = value;
                if (countWin == 6)
                {
                    Invoke(nameof(Win), 1);
                }
            }
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isHoldBG = false;
        isHoldFrame = false;
        isCompleted = false;
        isBegin = false;
        indexFrame = -1;
    }


    private void Start()
    {
        SetSizeCamera();
        tutorial2.SetActive(false);
        canvas.gameObject.SetActive(false);
        mainCamera.orthographicSize *= 2.0f / 5;
        SetUpFrameHide();
        Invoke(nameof(DisableFlyCam), 0.1f);
        ZoomOutCamera();
        
    }

    void SetSizeCamera()
    {
        float f1;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }
    void ZoomOutCamera()
    {
        mainCamera.DOOrthoSize(mainCamera.orthographicSize * 5.0f / 2, 3).SetEase(Ease.Linear).OnComplete(() =>
          {
              sizeCamera = mainCamera.orthographicSize;
              canvas.gameObject.SetActive(true);
              isBegin = true;
              tutorial.SetActive(true);
              Tutorial1();
          });
    }

    void Tutorial1()
    {
        tutorial.transform.DOMoveX(-6.56f, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            tutorial.transform.DOMoveX(6.68f, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (tutorial.activeSelf)
                {
                    Tutorial1();
                }
            });
        });
    }
    void Tutorial2()
    {
        tutorial2.transform.position = new Vector2(1.55f, -3.86f);
        tutorial2.SetActive(true);
        tutorial2.transform.DOMove(new Vector2(2.24f, 2.19f), 1).SetEase(Ease.Linear).SetLoops(-1);
    }

    void SetUpFrameHide()
    {
        for (int i = 0; i < listFlyCam.Count; i++)
        {
            listFlyCam[i].transform.position = new Vector3(Random.Range(-36, 37), Random.Range(1.2f, 3.5f), -1.5f);
            listPosHideFrame.Add(listFlyCam[i].transform.position);
        }
    }
    void DisableFlyCam()
    {
        for (int i = 0; i < listFlyCam.Count; i++)
        {
            listFlyCam[i].GetComponent<Camera>().enabled = false;
        }
        SpawnHide();
    }

    void SpawnHide()
    {
        for (int i = 0; i < listPosHideFrame.Count; i++)
        {
            listHideObj.Add(Instantiate(hideFramePrefab, listPosHideFrame[i], Quaternion.identity));
            listHideObj[i].transform.parent = backGround.transform;
            listHideObj[i].indexCheck = i;
        }
    }

    void Win()
    {
        Debug.Log("Win");
        isWin = true;
        droneObj.transform.DOMoveX(droneObj.transform.position.x + 30, 2);
        droneObj.pathCreator = null;
    }

    void Handle_Event_OnGetIndex(int index)
    {
        indexFrame = index;
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isWin && isBegin)
        {
            if (tutorial.activeSelf)
            {
                tutorial.SetActive(false);
                tutorial.transform.DOKill();
                Tutorial2();
            }

            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.gameObject.CompareTag("MainCamera"))
                    {
                        isHoldBG = true;
                        tmpPos_BG = mousePos - (Vector2)backGround.transform.position;
                    }

                    else if (hit[i].collider.gameObject.CompareTag("Tree") || hit[i].collider.gameObject.CompareTag("People") || hit[i].collider.gameObject.CompareTag("Balloon") || hit[i].collider.gameObject.CompareTag("ColorHive") || hit[i].collider.gameObject.CompareTag("BoundScreen") || hit[i].collider.gameObject.CompareTag("TrashRecycleTruck"))
                    {
                        if (tutorial2.activeSelf)
                        {
                            tutorial2.SetActive(false);
                            tutorial2.transform.DOKill();
                        }
                        isHoldFrame = true;
                        tmpFrame = hit[i].collider.gameObject.GetComponent<FrameObj_DroneMinigame2>();
                        startPosTmpFrame = tmpFrame.transform.position;
                        tmpPos_Frame = mousePos - (Vector2)tmpFrame.transform.position;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && !isWin && isBegin)
        {
            isHoldBG = false;
            isHoldFrame = false;
            if (tmpFrame != null)
            {
                if (!isCompleted)
                {
                    droneObj.Sad();
                    tmpFrame.transform.position = startPosTmpFrame;
                    tmpFrame = null;
                    startPosTmpFrame = Vector2.zero;
                    indexFrame = -1;
                }
                else
                {
                    droneObj.Happy();
                    tmpFrame.transform.position = tmpFrame.hideObjExact.transform.position;
                    tmpFrame.hideObjExact.gameObject.SetActive(false);
                    tmpFrame.GetComponent<BoxCollider2D>().enabled = false;
                    tmpFrame.GetComponent<Image>().DOFade(0, 0.5f);
                    CountWin++;
                    tmpFrame.transform.GetChild(0).GetComponent<RawImage>().DOFade(0, 0.5f).OnComplete(() =>
                    {
                        tmpFrame = null;
                    });
                    var tmpFrameCompleted = canvas.transform.GetChild(0).GetChild(indexFrame);
                    tmpFrameCompleted.GetChild(0).GetComponent<RawImage>().DOFade(0.5f, 0.1f).OnComplete(() => 
                    {
                        tmpFrameCompleted.GetChild(1).gameObject.SetActive(true);
                    });

                }
            }
            isCompleted = false;
        }

        if (isHoldBG && !isWin)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -29f + tmpPos_BG.x, 28.4f + tmpPos_BG.x), backGround.transform.position.y);
            backGround.transform.DOMoveX(mousePos.x - tmpPos_BG.x, 0.5f);
        }

        if (isHoldFrame && !isWin)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -sizeCamera * f2 + 1.3f + tmpPos_Frame.x, sizeCamera * f2 - 1.3f + tmpPos_Frame.x), Mathf.Clamp(mousePos.y, -sizeCamera + 0.5f + tmpPos_Frame.y, sizeCamera - 0.5f + tmpPos_Frame.y));
            tmpFrame.transform.position = new Vector2(mousePos.x - tmpPos_Frame.x, mousePos.y - tmpPos_Frame.y);
        }
    }

    private void OnEnable()
    {
        Hide_DroneMinigame2.Event_OnGetIndex += Handle_Event_OnGetIndex;
    }
    private void OnDisable()
    {
        Hide_DroneMinigame2.Event_OnGetIndex -= Handle_Event_OnGetIndex;
    }
}

