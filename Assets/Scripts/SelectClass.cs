using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectClass : MonoBehaviour {

    public int player_1_selection;
    public int player_2_selection;

    public void p1SelectedClass(int i)
    {
        player_1_selection = i;
    }

    public void p2SelectedClass(int i)
    {
        player_2_selection = i;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


}
