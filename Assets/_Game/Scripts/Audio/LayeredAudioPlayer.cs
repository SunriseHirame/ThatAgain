using UnityEngine;

public class LayeredAudioPlayer : MonoBehaviour
{
    [SerializeField] private MultiSource[] sources;
    private int currentCombo = -1;

    private void Awake ()
    {
        Reset ();
    }

    public void Reset ()
    {
        currentCombo = -1;
        foreach (var source in sources)
        {
            foreach (var s in source.sources)
            {
                s.mute = true;
            }
        }

        ActivateNext ();
    }

    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.O))
            ActivateNext ();
        if (Input.GetKeyDown (KeyCode.P))
            Reset ();
    }

    public void ActivateNext ()
    {
        print ("Activate");
        currentCombo++;
        if (currentCombo >= sources.Length)
            return;

        foreach (var s in sources[currentCombo].sources)
        {
            print (s.name);
            s.mute = false;
        }    
    }

    [System.Serializable]
    private struct MultiSource
    {
        public AudioSource[] sources;
    }
}