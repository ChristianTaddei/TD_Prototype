using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    private TopBar topBar;
    private EditToolBar editToolBar;
    private PlayToolBar playToolBar;

    private InterfaceState defaultInterfaceState;
    private InterfaceState activeInterfaceState;

    void Start()
    {
        Instance = this;

        defaultInterfaceState = new SelectAnything();
        SetState(defaultInterfaceState);

        topBar = new TopBar();

        editToolBar = new EditToolBar();
        editToolBar.show(false);

        playToolBar = new PlayToolBar();
        playToolBar.show(false);
    }


    internal void ResetDefaultState()
    {
        this.SetState(defaultInterfaceState);
    }

    public void OnSelection(IRepresentable selectedObject)
    {
        activeInterfaceState.OnSelection(selectedObject);
    }

    public void OnBoardVertexSelected(Vertex selectedBoardVertex)
    {
        activeInterfaceState.OnBoardVertexSelected(selectedBoardVertex);
    }

    public void OnFaceSelected(Face face)
    {
        // Debug.Log("Face selected " + face.a.Position + " " + face.b.Position + " " + face.c.Position);
    }

    void Update()
    {
        if (activeInterfaceState != null)
        {
            activeInterfaceState.Update();
        }
    }

    // TODO: Property instead?
    public void SetState(InterfaceState state)
    {
        if (activeInterfaceState != state)
        {
            if (activeInterfaceState != null) activeInterfaceState.Unmount();
            activeInterfaceState = state;
            state.Mount();
        }
    }

    // Toggles
    private List<GameObject> debugGameObjects = new List<GameObject>();
    public void OnShowThreatToggled(Toggle toggle)
    {/*
        if (toggle.isOn)
        {
            foreach (BoardPosition boardPosition in simulationManager.Board.Threaths.Keys)
            {
                float threath;
                Cell cell;
                if (simulationManager.Board.Threaths.TryGetValue(boardPosition, out threath)
                    && simulationManager.Board.Cells.TryGetValue(boardPosition, out cell))
                {
                    if (threath == 0) continue;

                    GameObject textContainer = new GameObject();
                    textContainer.transform.SetParent(cell.representation.transform);
                    textContainer.transform.position = cell.representation.transform.position + new Vector3(-0.20f, 0, 0.35f);
                    textContainer.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                    textContainer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    debugGameObjects.Add(textContainer);

                    TextMesh textMesh = textContainer.AddComponent<TextMesh>();
                    textMesh.text = threath.ToString();
                    textMesh.color = Color.red;
                }
            }
        }
        else
        {
            foreach (GameObject gameObject in debugGameObjects)
            {
                GameObject.Destroy(gameObject);
            }
            debugGameObjects.Clear();
        }*/
    }

    private List<GameObject> debugLines = new List<GameObject>();

    public void OnShowDistancesToggled(Toggle toggle)
    {

    }

    // Actions
    public void EnterEditMode()
    {
        ResetDefaultState();
        RepresentationManager.Instance.RepresentationRunning = false;
        RepresentationManager.Instance.RepresentationPaused = true;

        editToolBar.show(true);
        topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        playToolBar.show(false);
        topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }

    public void EnterPlayMode()
    {
        ResetDefaultState();
        RepresentationManager.Instance.RepresentationRunning = true;
        RepresentationManager.Instance.RepresentationPaused = false;
        // SimulationManager.Instance.CurrentStateModified();

        playToolBar.show(true);
        topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        editToolBar.show(false);
        topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }

    internal void ShowSaveAsPopup()
    {
        SaveAsPopup popup = new SaveAsPopup();
    }

    private bool markerLoaded = false;
    private GameObject markerPrefab;

    public GameObject MakeHighlight(SurfacePoint boardPosition, HighlightSize size, Color color)
    {
        return HighlightPosition(boardPosition.GetCartesians(SimulationManager.Instance.CurrentState.BoardState), size, color);
    }

    public GameObject MakeHighlight(Vertex vertex, HighlightSize size, Color color)
    {
        return HighlightPosition(SimulationManager.Instance.CurrentState.BoardState.VertexStates[vertex].Position, size, color);
    }

    public List<GameObject> MakeHighlights(IEnumerable<Vertex> vertices, HighlightSize size, Color color)
    // public GameObject MakeHighlights(IEnumerable<Vertex> vertices, HighlightSize size, Color color)
    {
        // return HighlightGround(vertices, color);

        List<GameObject> highlights = new List<GameObject>();
        foreach (Vertex vertex in vertices)
        {
            highlights.Add(HighlightPosition(SimulationManager.Instance.CurrentState.BoardState.VertexStates[vertex].Position, size, color));
        }
        return highlights;
    }

    public List<GameObject> MakeHighlight(BarycentricLine line)
    {
        List<GameObject> highlights = new List<GameObject>();

        // highlights.Add(HighlightPosition(line.StartPoint.Cartesians, HighlightSize.Small, Color.blue));
        // highlights.Add(HighlightPosition(line.EndPoint.Cartesians, HighlightSize.Small, Color.blue));

        // foreach (IPoint point in line.GetAllIntersectionWithFaces())
        // {
        //     highlights.Add(HighlightPosition(point.Cartesians, HighlightSize.VerySmall, Color.green));
        // }

        return highlights;
    }

    public GameObject HighlightPosition(Vector3 position, HighlightSize size, Color color)
    {
        if (!markerLoaded)
        {
            markerPrefab = (GameObject)Resources.Load("Prefabs/Marker");
            markerLoaded = true;
        }
        GameObject markerInstance = (GameObject)GameObject.Instantiate(markerPrefab);

        markerInstance.transform.position = position;

        if (size == HighlightSize.Large)
        {
            markerInstance.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        else if (size == HighlightSize.VerySmall)
        {
            markerInstance.transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        markerInstance.GetComponent<Renderer>().material.color = color;

        return markerInstance;
    }

    public GameObject HighlightGround(IEnumerable<Vertex> vertices, Color color)
    {
        Surface board = SimulationManager.Instance.Board;

        BoardRepresentation otherRep = BoardRepresentation.MakeFrom(board);

        otherRep.transform.position = new Vector3(
            otherRep.transform.position.x,
            otherRep.transform.position.y + 0.1f,
            otherRep.transform.position.z
        );

        otherRep.SetupMaterial(Resources.Load("Materials/ColorableMaterial", typeof(Material)) as Material);

        Color[] colors = new Color[board.Vertices.Count];

        foreach (Vertex vertex in vertices)
        {
            int i = board.Vertices.IndexOf(vertex);
            colors[i] = color;
        }

        otherRep.Colors = colors;
        otherRep.Sync();

        return otherRep.gameObject;

        // works with sprite/defaul
    }
}