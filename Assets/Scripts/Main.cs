using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public GameObject tile;
    Dictionary<string, int> dict;

    // Use this for initialization
    void Start()
    {
        dict = new Dictionary<string, int>();
        //createPlane();
    }

    void createPlane()
    {
        for (int i = -12; i < 13; i++)
        {
            for (int j = -12; j < 13; j++)
            {
                Instantiate(tile).transform.position = new Vector3(2*i, -1.2f, 2*j);
            }
        }
    }

    public void scan()
    {
        int objCount = 0;
        //iterate through objects
        for (int i = 0; i < transform.childCount; i++)
        {
            //show how much of which lego
            Transform obj = transform.GetChild(i);
            string key = "";
            Color32 color = obj.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color;
            Dictionary<string, Color32> colors = GameObject.Find("Player").GetComponent<Player>().colors;
            foreach (KeyValuePair<string, Color32> entry in colors)
            {
                if (entry.Value.Equals(color))
                {
                    key = entry.Key;
                    break;
                }
            }

            key += " " + obj.name.Substring(0, obj.name.Length - 7);
            if (dict.ContainsKey(key))
            {
                dict[key]++;
            } else
            {
                dict.Add(key, 1);
            }


            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            objCount++;
        }
        toFile();
        StartCoroutine(CreateScreenshots(objCount));
    }

    public void toFile()
    {
        string text = "";
        foreach(KeyValuePair<string, int> entry in dict)
        {
            text += "You need " + entry.Value + " " + entry.Key + "(s)\r\n";
        }
        Debug.Log(text);
        System.IO.File.WriteAllText(Application.dataPath + "/../Saves/resources_needed.txt", text);
    }

    private IEnumerator CreateScreenshots(int objCount)
    {
        int y = 0;
        GameObject hand = GameObject.Find("Hand");
        hand.SetActive(false);
        do
        {
            //List<Transform> objectsOnLayer = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Mathf.RoundToInt(transform.GetChild(i).position.y) == y)
                {
                    //same y
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    //objectsOnLayer.Add(transform.GetChild(i));
                    objCount--;
                }
            }

            //screenshot

            // Wait until we actually rendered
            yield return new WaitForEndOfFrame();

            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            // Read screen contents into the texture
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            // Write to file
            byte[] bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath+"/../Saves/Level" + y + ".png", bytes);

            // Clean up the used texture
            Destroy(texture);

            y++;
        } while (objCount > 0);
        hand.SetActive(true);
    }
}
