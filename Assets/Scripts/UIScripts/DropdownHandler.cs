using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    // public Text text;
    // Start is called before the first frame update
    void Start()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        // dropdown.options.Clear();

        // List<string> items = new List<string>();
        // items.Add("Easy");
        // items.Add("Medium");
        // items.Add("Hard");

        // foreach(string item in items)
        // {
        //     dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        // }

        DropdownItemSelected(dropdown);

        // dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown);});
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        // text.text = dropdown.options[index].text;
    }
}
