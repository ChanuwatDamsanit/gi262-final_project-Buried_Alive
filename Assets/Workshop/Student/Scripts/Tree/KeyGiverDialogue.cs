using UnityEngine;
using Solution;

public class KeyGiverDialogue : DialogueSequen
{
    [Header("References")]
    public Character player; // Assign your player (must have Inventory component)
    private Inventory playerInventory; // cache inventory

    [Header("Key Info")]
    public string keyName = "Key";
    public int keyAmount = 2;

    protected override void LoadConversations()
    {
        // Dialogue nodes
        DialogueNode greeting = new DialogueNode("Hey there, traveler. What do you want?");
        DialogueNode firstAsk = new DialogueNode("You want a key, huh? Say that again, I didn’t hear you.");
        DialogueNode deny = new DialogueNode("Then you don’t need a key, I guess.");
        DialogueNode secondAsk = new DialogueNode("Fine, here’s your key. Don’t lose it!");
        DialogueNode alreadyHave = new DialogueNode("You already have a key. Don’t be greedy!");
        DialogueNode goodbye = new DialogueNode("Alright, see you around.");

        // --- Build tree ---
        greeting.AddNext(firstAsk, "I want a key");
        greeting.AddNext(deny, "Nothing, just looking around.");

        firstAsk.AddNext(secondAsk, "I want a key");
        firstAsk.AddNext(deny, "Nevermind.");

        tree = new DialogueTree(greeting);
    }

    public override void SelectChoice(int index) // no need
    {
        base.SelectChoice(index);

        // Cache inventory if not already done
        if (player != null && playerInventory == null)
        {
            playerInventory = player.GetComponent<Inventory>();
        }

        // Check if this is the key-giving node
        if (currentNode != null && currentNode.text == "Fine, here’s your key. Don’t lose it!")
        {
            if (playerInventory != null)
            {
                // Only give if player doesn’t already have it
                if (!playerInventory.HasItem(keyName, keyAmount))
                {
                    playerInventory.AddItem(keyName, keyAmount);
                    Debug.Log($"{player.name} received {keyAmount}x {keyName}!");
                }
                else
                {
                    // Replace dialogue text to show "already have" message
                    currentNode = new DialogueNode("You already have a key. Don’t be greedy!");
                    dialogueUI.ShowDialogue(currentNode); 
                }
            }
            else
            {
                Debug.LogWarning("Player has no Inventory component!");
            }
        }
    }
}
