using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour
{
    public Text recordsText; // Reference to the UI Text element to display the records

    void Start()
    {
        // Get the list of record counts from PlayerPrefs
        List<int> records = new List<int>();
        if (PlayerPrefs.HasKey("Records"))
        {
            string recordsString = PlayerPrefs.GetString("Records");
            string[] recordArray = recordsString.Split(',');
            Debug.Log("ÐÅÊÎÐÄÛ");

            foreach (string rec in recordArray)
            {
                Debug.Log(rec);
            }
            foreach (string record in recordArray)
            {
                records.Add(int.Parse(record));
            }
        }

        // Sort the records in descending order
        records = records.OrderByDescending(r => r).ToList();

        // Keep only the top 10 records
        if (records.Count > 10)
        {
            records = records.GetRange(0, 10);
        }

        // Display the record counts in the UI Text element as a table
        string recordsDisplay = "Records:\n";
        for (int i = 0; i < records.Count; i++)
        {
            recordsDisplay += (i + 1) + ". " + records[i] + "\n";
        }
        recordsText.text = recordsDisplay;
    }
}
