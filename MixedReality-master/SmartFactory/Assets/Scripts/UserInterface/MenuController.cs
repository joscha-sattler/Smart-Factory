using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuController : SharedDataController
{
    [SerializeField]
    GameObject canvas = null;
    //[SerializeField]
    private GameObject BuildingOptionPanel = null;
    public GameObject LoadButtonPrefab;
    

    private int ProductionAmount;
    private string TaskName;
    private string CurrentFile;

    protected override void Start()
    {
        base.Start();
        SetupButtonEvents();    

    }

    protected override void Update()
    {
        //BuildingOptionPanel.SetActive(true);
        base.Update();
        if(selected != null)
        {
            BuildingOptionPanel.SetActive(true);
            ShowOption();
        } else if(selected == null)
        {
            BuildingOptionPanel.SetActive(false);
            
        }
    }

    //BuildingOptions
    private void HideOptions()
    {
        Button CarrierButton = BuildingOptionPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
        CarrierButton.interactable = false;
        Button ItemButton = BuildingOptionPanel.transform.GetChild(2).gameObject.GetComponent<Button>();
        ItemButton.interactable = false;
    }

    private void ShowOption()
    {
        Button CarrierButton;
        Button ItemButton;
        switch (selected.name)
        {
            case "Storage(Clone)":
                //Carrier Button anzeigen
                CarrierButton = BuildingOptionPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                CarrierButton.interactable = true;
                CarrierButton.onClick.AddListener(delegate { AddCarrier(); });

                //Item Button anzeigen
                ItemButton = BuildingOptionPanel.transform.GetChild(2).gameObject.GetComponent<Button>();
                ItemButton.interactable = true;
                ItemButton.onClick.AddListener(delegate { AddItem(); });
                break;
            case "Convoy(Clone)":
            case "Assembler(Clone)":
            case "Assembler2(Clone)":
            case "Crossing(Clone)":
            case "Parkhouse(Clone)":
                //Carrier Button anzeigen
                CarrierButton = BuildingOptionPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                CarrierButton.interactable = true;
                CarrierButton.onClick.AddListener(delegate { AddCarrier(); });
                ItemButton = BuildingOptionPanel.transform.GetChild(2).gameObject.GetComponent<Button>();
                ItemButton.interactable = false;
                break;
        }
    }

    private void AddCarrier()
    {
        factory.PlaceCarrier(factory.CreateCarrier(0), selected);
    }

    private void AddItem()
    {
        factory.AddItem(factory.CreateItem(0), (Storage)selected);
    }


    private void SetupButtonEvents()
    {
        foreach (Transform child in GetAllChildren(canvas.transform))
        {
            //jedem button loest die methode buttonclick aus -> fuer sound
            if(child.GetComponent<Button>()){
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(); });
            }

            switch (child.gameObject.name)
            {
                case "Conveyor":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(0); });
                    break;
                case "Crossing":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(1); });
                    break;
                case "Storage":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(2); });
                    break;
                case "Assembler":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(3); });
                    break;
                case "Assembler2":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(4); });
                    break;
                case "Parkhouse":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseBuilding(5); });
                    break;
                case "Rotate":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RotateBuilding(); });
                    break;
                case "SaveButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { SaveFactory(); });
                    break;
                case "LoadButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ShowExistingFactorys(); });
                    break;
                case "BuildingOptions":
                    this.BuildingOptionPanel = child.gameObject;
                    break;
            }
        }
    }


    public void ButtonClick(){
        //der eventservice leitet das event weiter
        EventServices.Instance.onButtonClick.Invoke();
    }
    //BuildingMenu
    private void ChooseBuilding(int choice)
    {
        DeleteParented();
        SetParented(SmartFactory.Instance.CreateBuilding(choice));
        
    }

    private void RotateBuilding()
    {
        if (parented != null)
        {
            factory.RotateBuilding(parented);
        }
    }

    //Carrier setzten
    public void PlaceCarrier()
    {
       /* if (selected != null && selected is Storage)
        {
            factory.AddItem(factory.CreateItem(0), (Storage)selected);
            return;
        } else
        {
            Debug.Log("Auf diesem Building kann kein Carrier abgelegt werden.");
        }
        ClearParented();
        parented = factory.CreateBuilding(0);*/
    }

    //TaskMenu
    public void ChooseTask(string name)
    {
        TaskName = name;
    }

    public void SetAmount(Slider Amount)
    {
        ProductionAmount = (int)Amount.value;
    }

    public void ChangeText(Text text)
    {
        text.text = "Amount: " + ProductionAmount;
    }

    public void StartSimulation()
    {
        factory.CreateAssignment(TaskName, ProductionAmount);
    }

    //PersistenceMenu
    public void ShowExistingFactorys()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/SaveFiles");
        FileInfo[] info = dir.GetFiles("*.sav*");
        for (int i = 0; i<info.Length; i++)
        {
            string name = info[i].Name;
            if(!name.Contains(".meta"))
            {
                GameObject button = (GameObject)Instantiate(LoadButtonPrefab);
                button.transform.SetParent(GameObject.Find("PersistenceMenu").transform);
                button.transform.GetChild(0).GetComponent<Text>().text = name; //Bild der Factory vielleicht eindeutiger
                button.GetComponent<Button>().onClick.AddListener(delegate { LoadExsistingGame(name); });
                button.transform.localPosition = new Vector3( 0f, 30f-30f*i, 0f); //Button untereinander anordnen
            }
        }
    }
    
    void LoadExsistingGame(string name)
    {
        factory.LoadConfiguration(name);
        CurrentFile = factory.GetFileName();
    }

    //OptionMenu
    public void SaveFactory()
    {
        string name;
        CurrentFile = factory.GetFileName();
        if (CurrentFile != null)
        {
            name = CurrentFile;
        } else
        {
            name = System.DateTime.Now.ToString("ddMyyyy_HHmmss") + ".sav";
        }
        factory.SaveConfiguration(name);
        Debug.Log("The Factory " + name + " is saved");
    }

    private List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
            foreach (Transform inceptionChild in GetAllChildren(child))
            {
                children.Add(inceptionChild);
            }
        }

        return children;
    }
}