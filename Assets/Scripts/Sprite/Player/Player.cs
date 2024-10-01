using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Player", order = 1)]
public class Player : ScriptableObject
{
    public int money = 8000;
    public int stamina = 100;
    public int nudacoin = 8000;
    public int level = 1;  // New level parameter, default set to 1

    public string lastLogoutTime;  // Storing as string instead of DateTime
    public int inventoryLimit;
    public string playerName;

    // Method to get DateTime from string
    public DateTime GetLastLogoutDateTime()
    {
        if (DateTime.TryParse(lastLogoutTime, out DateTime parsedTime))
        {
            return parsedTime;
        }
        return DateTime.Now;  // Default if parsing fails
    }

    // Method to set DateTime as string
    public void SetLastLogoutDateTime(DateTime dateTime)
    {
        lastLogoutTime = dateTime.ToString();
    }
}
