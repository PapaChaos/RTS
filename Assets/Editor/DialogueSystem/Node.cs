using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Node
{
    public Rect rect;
    public string title;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;

    public bool isDragged;
    public bool isSelected;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;

    public Action<Node> OnRemoveNode;

    public Node(Vector2 position, Vector2 size, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        rect = new Rect(position.x, position.y, size.x, size.y);
        style = nodeStyle;

        inPoint = new ConnectionPoint(this, ConnectionPointType.input, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.output, outPointStyle, OnClickOutPoint);
        
        

        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;

        OnRemoveNode = OnClickRemoveNode;
    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();

        GUI.Box(rect, title, style);
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;

                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if ((e.button == 1 && isSelected && rect.Contains(e.mousePosition)))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.AddItem(new GUIContent("Add Dialogue Branch"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    public void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}
