using UnityEngine;
using UnityEngine.UIElements;

public class UIMainMenuState : UIBaseState
{
    public UIMainMenuState(UIController ui) : base(ui) { }

    public override void EnterState()
    {
        controller.mainMenuContainer.style.display = DisplayStyle.Flex;
        controller.buttons = controller.root
            .Query<Button>(null, "menu-button")
            .ToList();
        controller.buttons[0].AddToClassList("active");
        Debug.Log("controller.buttons.Count" + controller.buttons.Count);
    }

    public override void SelectButton()
    {
        Button btn = controller.buttons[controller.currentSelection];
        Debug.Log(btn.name);
        
        if (btn.name == "start-button")
        {
            SceneTransition.Initiate(1);
        }
    }
    
}
