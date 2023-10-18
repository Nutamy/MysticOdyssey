using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private UIDocument uiDocumentCmp;
    public VisualElement root;
    public List<Button> buttons = new List<Button>(); 
    public VisualElement mainMenuContainer;
    public VisualElement playerInfoContainer;
    public Label healthLable;
    public Label potionsLable;
    private VisualElement questItemIcon;
    public UIBaseState currentState;
    public UIMainMenuState mainMenuState;
    public UIDialogueState dialogueState;
    public UIQuestItemState questItemState;
    public int currentSelection = 0;

    private void Awake()
    {        
        uiDocumentCmp = GetComponent<UIDocument>();
        root = uiDocumentCmp.rootVisualElement;
        mainMenuContainer = root.Q<VisualElement>("main-menu-container");
        playerInfoContainer = root.Q<VisualElement>("player-info-container");
        healthLable = playerInfoContainer.Q<Label>("health-label");
        potionsLable = playerInfoContainer.Q<Label>("potions-label");
        questItemIcon = playerInfoContainer.Q<VisualElement>("quest-item-icon");

        mainMenuState = new UIMainMenuState(this);
        dialogueState = new UIDialogueState(this);
        questItemState = new UIQuestItemState(this);
    }

    private void OnEnable()
    {
        EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
        EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
        EventManager.OnInitiateDialogue += HandleInitiateDialogue;
        EventManager.OnTreasureChestUnlocked += HandleTreasureChestUnlocked;
    }

    private void OnDisable()
    {
        EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
        EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
        EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
        EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;
    }

    private void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0)
        {
            currentState = mainMenuState;
            currentState.EnterState();
        }
        else
        {
            playerInfoContainer.style.display = DisplayStyle.Flex;
        }

    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        currentState.SelectButton();
    }

    public void HandleNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || buttons.Count == 0) return;

        buttons[currentSelection].RemoveFromClassList("active");

        Vector2 input = context.ReadValue<Vector2>();
        currentSelection += input.x > 0 ? 1 : -1;
        currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);

        buttons[currentSelection].AddToClassList("active");
    }

    public void HandleChangePlayerHealth(float newHealthPoints)
    {
        healthLable.text = newHealthPoints.ToString();
    }

    public void HandleChangePlayerPotions(int newPotionsPoints)
    {
        potionsLable.text = newPotionsPoints.ToString();
    }

    public void HandleInitiateDialogue(TextAsset inkJSON, GameObject npc)
    {
        currentState = dialogueState;
        currentState.EnterState();
        
        (currentState as UIDialogueState).SetStory(inkJSON, npc);
    }

    public void HandleTreasureChestUnlocked(QuestItemSO item)
    {
        currentState = questItemState;
        currentState.EnterState();

        (currentState as UIQuestItemState).SetQuestItemLabel(item.itemName);
        questItemIcon.style.display = DisplayStyle.Flex;
    }

}
