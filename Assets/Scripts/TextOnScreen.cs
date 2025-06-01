using UnityEngine;
using System.Collections;
using TMPro;

public class TextOnScreen : MonoBehaviour{
    public TMP_Text text;
    [SerializeField] private int textIndex;
    [SerializeField] private bool showInitMessages = true;
    private string[] texts = {
        "¡Bienvenido al juego!",
        "Usa W-A-S-D para moverte.",
        "Presiona espacio para saltar.",
        "Y evita a los enemigos peligrosos.",
        "¡Buena suerte y diviértete!",
        "¡HAS DESCUBIERTO UN EASTER EGG!",
        "¡ENHORABUENA! ¡HAS GANADO!",
    };

    void Start(){
        if (showInitMessages) { StartCoroutine(ShowWelcomeMessages()); }
    }

    IEnumerator ShowWelcomeMessages(){
        for (int i = 0; i < texts.Length - 2; i++) {
            Debug.Log(texts[i]);
            UpdateText(texts[i]);
            yield return new WaitForSeconds(2f);
            text.gameObject.SetActive(false);
        }
    }
    
    private void UpdateText(string txt){
        text.text = txt;
        text.gameObject.SetActive(true);
    }
    
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("PlayerDetection")) { StartCoroutine(ShowText(textIndex, 3f)); }
    }

    IEnumerator ShowText(int idx, float duration = 3f) {
        UpdateText(texts[idx]);
        yield return new WaitForSeconds(duration);
        text.gameObject.SetActive(false);
    }
}
