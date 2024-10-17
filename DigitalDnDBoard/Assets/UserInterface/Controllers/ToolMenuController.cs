using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
/*
    Make sure the button name in UI Toolkit and the name in ChangeToolState is correct.
*/
public class ToolMenuController : MonoBehaviour
{
    private GroupBox toolbar;
    private Button selectedButton;
    [SerializeField]
    private ToolState currentToolState;
    private List<ToolState> toolStates;

    private void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        toolbar = root.Q<GroupBox>("ToolBarMenu");
        var buttons = toolbar.Query<Button>().ToList();
        
        //Store all the tool states available.
        toolStates = new List<ToolState>{
            new DefaultToolState(),
            new PolygonToolState(),
        };

        //Add button click event listeners to the buttons
        foreach(Button button in buttons){
            button.clicked += () => OnButtonClicked(button);
        }
        
        //Set initial selected button and its tool state.
        if(buttons.Count > 0){
            SelectButton(buttons[0]);
            ChangeToolState(buttons[0]);
        }
    }
    private void OnButtonClicked(Button clickedButton){
        SelectButton(clickedButton);
        ChangeToolState(clickedButton);
    }
    private void Update() {
        currentToolState.UpdateState();
    }

    private void SelectButton(Button newSelectedButton){
        if(selectedButton != null) {
            selectedButton.RemoveFromClassList("selected");
        }

        newSelectedButton.AddToClassList("selected");
        selectedButton = newSelectedButton;
    }



    public void ChangeToolState(ToolState newToolState){
        if(currentToolState != null){
            currentToolState.ExitState();
        }

        currentToolState = newToolState;
        currentToolState.EnterState();
    }

    private void ChangeToolState(Button button){
        var toolState = button.name switch
        {
            "Default"   => toolStates[0],
            "Polygon"   => toolStates[1],
            _           => toolStates[0],
        };
        ChangeToolState(toolState);
    }
}





public static class ToolbarStateMachine{
    private static ToolState currentToolState;
    private static List<ToolState> toolStates;


}
