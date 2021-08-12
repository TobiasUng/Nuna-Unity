using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public GameObject wall;
    public Dictionary<string, Transform> screwPlacementsMap = new Dictionary<string, Transform>();
    public GameObject publisher;
    private KeywordRecognizer keywordRecognizer;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform row in wall.transform)
        {
            foreach (Transform placement in row.transform)
            {
                screwPlacementsMap.Add(row.tag + placement.GetSiblingIndex(), placement);
            }
        }

        Debug.Log(Microphone.devices[0]);
        
        keywordRecognizer = new KeywordRecognizer(screwPlacementsMap.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
        //keywordRecognizer.Stop();
        
        
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        if (!publisher.GetComponent<TrajectoryPlanner>().isExecuting)
        {
            Transform placement = screwPlacementsMap[args.text];
            placement.GetChild(0).GetComponent<ScrewPlacement>().moveScrewToPlacement();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
