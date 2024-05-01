using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Records : Sounds
{
    public Text recordsText; // Reference to the UI Text element to display the records

    void Start()
    {
        if (PlayerPrefs.GetInt("Died") == 0)
        {
            PlaySound(sounds[0], 0.2f);
        }
        // Get the list of record counts from PlayerPrefs
        List<int> records = new List<int>();
        if (PlayerPrefs.HasKey("Records"))
        {
            string recordsString = PlayerPrefs.GetString("Records");
            string[] recordArray = recordsString.Split(',');
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
        string recordsDisplay = "\t\tRecords:\n";
        for (int i = 0; i < records.Count; i++)
        {
            if (PlayerPrefs.GetInt("NowRecord") != records[i])
            {
                recordsDisplay += (i + 1) + ". " + records[i] + "\n";
            }
            else
            {
                if (PlayerPrefs.GetInt("Died")==0)
                    recordsDisplay += (i + 1) + ". " + records[i] + "\t\t\t\t <New>" + "\n";
                else
                    recordsDisplay += (i + 1) + ". " + records[i] + "\n";

            }

        }
        recordsText.text = recordsDisplay;
    }
}
