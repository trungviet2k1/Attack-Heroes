using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{
    public string className;
    public int health;
    public int mana;
    public List<string> abilities;

    void Start()
    {
        if (className == "Warrior")
        {
            health = 200;
            mana = 50;
            abilities.Add("Slash");
            abilities.Add("Block");
        }
        else if (className == "Mage")
        {
            health = 100;
            mana = 200;
            abilities.Add("Fireball");
            abilities.Add("Teleport");
        }
    }

    public void UseAbility(string abilityName)
    {
        if (abilities.Contains(abilityName))
        {
            Debug.Log(className + " uses " + abilityName);
        }
    }
}