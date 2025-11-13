using UnityEngine;
using Solution;

public class HealerDialogue : DialogueSequen
{
    public Character player;
    protected override void LoadConversations()
    {
        // Dialogue nodes
        DialogueNode greeting = new DialogueNode("Welcome, do you need help?");
        DialogueNode tryToHelp = new DialogueNode("I can help you. But you need to say please.");
        DialogueNode sayAgain = new DialogueNode("Can you say it again?");
        DialogueNode playerNotSay = new DialogueNode("So no medicine for you!");
        DialogueNode finish = new DialogueNode("That's it, take your medicine.");
        DialogueNode goodbye = new DialogueNode("Safe travels!");

        // --- Build tree ---

        // Greeting
        greeting.AddNext(tryToHelp, "I don't feel well, do you have medicine?");
        greeting.AddNext(goodbye, "No, I'm fine.");

        // First "Please" or "No"
        tryToHelp.AddNext(sayAgain, "Please");
        tryToHelp.AddNext(playerNotSay, "No");

        // Second "Please" or "No"
        sayAgain.AddNext(finish, "Please...");
        sayAgain.AddNext(playerNotSay, "No");

        // 'finish', 'playerNotSay', and 'goodbye' have no nexts → will trigger CloseButton
        tree = new DialogueTree(greeting);

    }

    public override void SelectChoice(int index) //no need
    {
        base.SelectChoice(index);

        // If the current dialogue is "finish" node → heal player
        if (currentNode != null && currentNode.text == "That's it, take your medicine.")
        {
            if (player != null)
            {
                player.energy = player.maxEnergy;  // restore full energy
                Debug.Log(player.name + " is fully healed!");
            }
            else
            {
                Debug.LogWarning("No player assigned to HealerDialogue!");
            }
        }
    }

}
