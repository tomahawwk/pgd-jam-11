using System.Collections;
using Dialogue;
using Prototype.Plugins.FadeOutSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private DialogueSystem _dialogue => DialogueSystem.Instance;
    private FadeOutSystem _fadeOut => FadeOutSystem.Instance;
    [SerializeField] private string _sceneToLoad;
    [SerializeField] [TextArea] private string _firstReplica = "";
    [SerializeField] [TextArea] private string _secondReplica;
    

    private void Start()
    {
        _dialogue.Dialogue(_firstReplica);
        _dialogue.Dialogue(_secondReplica);
        StartCoroutine(WaitTillDialog());
    }

    private IEnumerator WaitTillNextDialog()
    {
        while (_dialogue.IsOpen())
        {
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        
    }
    private IEnumerator WaitTillDialog()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        while (_dialogue.IsOpen())
        {
            yield return null;
        }
        _fadeOut.FadeOut(() => SceneManager.LoadScene(_sceneToLoad));
    }
}
