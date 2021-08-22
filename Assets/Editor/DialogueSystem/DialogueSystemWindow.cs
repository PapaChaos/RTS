using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueSystemWindow : EditorWindow
{
    Texture2D background;
    Texture2D topBar;

    List<Node> nodes;
    List<Connection> connections;

    GUIStyle nodeStyle;
    GUIStyle selectedNodeStyle;
    GUIStyle inPointStyle;
    GUIStyle outPointStyle;

    ConnectionPoint selectedInPoint;
    ConnectionPoint selectedOutPoint;

    Vector2 offset;
    Vector2 drag;

    float topBarHeight;

    [MenuItem("Window/Dialogue System")]
    public static void ShowWindow()
    {
        GetWindow<DialogueSystemWindow>("Dialogue System");
    }

    private void OnEnable()
    {
        topBarHeight = 30f;

        background = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        background.SetPixel(0, 0, new Color32(0x30, 0x00, 0x00, 0xFF));
        background.Apply();

        topBar = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        topBar.SetPixel(0, 0, new Color32(0x06, 0x06, 0x15, 0xFF));
        topBar.Apply();


        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    void OnGUI()
    {   //GRID AREA
        GUI.DrawTexture(new Rect(0, topBarHeight, maxSize.x, maxSize.y), background, ScaleMode.StretchToFill);

        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(200, 0.4f, Color.gray);

        GUI.Label(new Rect (30, 50, 400,20) , "Hold 'Alt' and the left mouse button to pan.");

        DrawNodeSystem();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        Process(Event.current);

        //TOP BAR
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, topBarHeight), topBar, ScaleMode.StretchToFill);
        GUI.TextField(new Rect(10, 5, 100, 20), "");
        GUI.Button(new Rect(position.width-125, 5, 100, 20), "Generate");
        GUI.Button(new Rect(position.width-250, 5, 100, 20), "Save");


        if (Keyboard.current.deleteKey.ReadValue() > 0)
        {
            RemoveSelected();
        }

        if (GUI.changed) 
            Repaint();


    }
	void RemoveSelected()
	{
        if(nodes.Count > 0)
        nodes.RemoveAll(Node => Node.isSelected);
        GUI.changed = true;
    }
    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing+50, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j+50, 0) + newOffset, new Vector3(position.width, gridSpacing * j+50, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }


    void DrawNodeSystem()
	{
        if(nodes != null)
		{
            for(int i = 0; i < nodes.Count; i++)
			{
                nodes[i].Draw();
			}
		}
	}
    private void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
    }

    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    void Process(Event e)
	{
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && e.alt)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }
    private void OnClickAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<Node>();
        }

        nodes.Add(new Node(mousePosition, new Vector2(200,50), nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }
    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        connections.Remove(connection);
    }

    private void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }

        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }
    private void OnClickRemoveNode(Node node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                connections.Remove(connectionsToRemove[i]);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }

        GUI.changed = true;
    }



}
