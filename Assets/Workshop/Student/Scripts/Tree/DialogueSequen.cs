using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DialogueSequen : MonoBehaviour
{
    //public Character player;
    public DialogueTree tree;
        public DialogueNode currentNode;
        public DialogueUI dialogueUI; // ลาก DialogueUI Component มาใส่

        public void Start()
        {

            LoadConversations();

        }

        protected virtual void LoadConversations()
        {
        // NPC: Ah, traveler! What brings you to this old place?
        //     |
        //     +-- [1] Can you give me a quest?
        //     |       |
        //     |       +-- NPC: I have a task for you. There’s a beast in the woods. Can you take care of it?
        //     |               |
        //     |               +-- [1] I’m ready for anything!
        //     |               |       |
        //     |               |       +-- NPC: You're not ready for this yet. Come back when you're stronger.
        //     |               |
        //     |               +-- [2] Maybe later.
        //     |                       |
        //     |                       +-- NPC: Safe travels, adventurer.
        //     |
        //     +-- [2] Where is the village?
        //     |       |
        //     |       +-- NPC: Follow the road south, and you’ll reach the village.
        //     |
        //     +-- [3] How do I get to the forest?
        //     |       |
        //     |       +-- NPC: Head west, into the forest. But beware, it's dangerous.
        //     |
        //     +-- [4] Goodbye.
        //             |
        //             +-- NPC: Safe travels, adventurer.

        // Create the dialogue nodes
        DialogueNode greeting = new DialogueNode("You’re finally awake... You’re underground, traveler.");
        DialogueNode askHowToEscape = new DialogueNode("To get out, you must find two keys and bring them to the exit gate.");
        DialogueNode askWhereAmI = new DialogueNode("This place is called the Forgotten Depths. Few ever see the sunlight again.");
        DialogueNode goodbye = new HealerDialogueNode("Stay safe down here, and don’t lose hope.");

        // --- Dialogue flow ---
        greeting.AddNext(askWhereAmI, "Where am I?");
        greeting.AddNext(askHowToEscape, "How do I get out?");
        greeting.AddNext(goodbye, "Goodbye.");

        askWhereAmI.AddNext(askHowToEscape, "How do I get out?");
        askWhereAmI.AddNext(goodbye, "Goodbye.");

        // Set up the root of the dialogue tree
        tree = new DialogueTree(greeting);
        }

    // **เมธอดใหม่สำหรับรับการเลือกจากปุ่ม UI**
    public virtual void SelectChoice(int index) // fix this
    {
        var choiceTextKeys = new List<string>(currentNode.nexts.Keys);

        if (index >= 0 && index < choiceTextKeys.Count)
        {
            string choiceKey = choiceTextKeys[index];

            // 1. เลื่อนไปยัง Dialogue Node ถัดไป
            currentNode = currentNode.nexts[choiceKey];

            // 2. ตรวจสอบว่ามีตัวเลือกถัดไปหรือไม่ (จบการสนทนา)
            if (currentNode.nexts.Count > 0)
            {
                dialogueUI.ShowDialogue(currentNode); // แสดง Node ถัดไป
                //if (currentNode.giveHp > 0) {
                //    if (player != null)
                //    {
                //        player.energy = player.maxEnergy;  // restore full energy
                //        Debug.Log(player.name + " is fully healed!");
                //    }
                //    else
                //    {
                //        Debug.LogWarning("No player assigned to HealerDialogue!");
                //    }
                }

                if (currentNode is HealerDialogueNode) { 
                
                }
            }
            else
            {
                // ถ้าไม่มีตัวเลือกถัดไป ถือว่าจบบทสนทนา
                dialogueUI.ShowDialogue(currentNode);   // แสดงข้อความสุดท้าย
                dialogueUI.ShowCloseButtonDialog();    // อาจเพิ่ม Delay และเรียก dialogueUI.HideDialogue() ที่นี่
                                                      // หรือทำให้ปุ่ม "ปิด" แสดงขึ้นมา
            }
        }
    }
}

