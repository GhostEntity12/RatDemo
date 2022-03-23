using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static string[] names = new string[] {
        "Jerry",
        "Cheddar",
        "Gouda",
        "Brie",
        "Cam",
        "Whiskers",
        "Dr. Squeaks",
        "Geronimo",
        "Moz",
        "Roque",
        "Rick",
        "Gru",
        };

    public MouseInfo[] miceInfo;

    [SerializeField] private readonly List<Mouse> mice;
	public List<Mouse> Mice { get; private set; }
	readonly List<Mouse> selectedMice = new List<Mouse>();
    public List<Mouse> SelectedMice => selectedMice;
    public ProgressBar progressBarPrefab;

    public Canvas c;
    // Start is called before the first frame update
    void Start()
    {
        Mice = FindObjectsOfType<Mouse>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
}
