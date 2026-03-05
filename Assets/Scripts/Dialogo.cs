using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public GameObject panelDialogo;
    public GameObject textoInteractuar;
    public TextMeshProUGUI TextoDialogo;

    public string[] lineas;
    int indice;

    void Start()
    {
        panelDialogo.SetActive(false);
    }

    public void IniciarDialogo()
    {
        panelDialogo.SetActive(true);
        indice = 0;
        TextoDialogo.text = lineas[indice];
    }

    public void SiguienteLinea()
    {
        indice++;

        if (indice < lineas.Length)
        {
            TextoDialogo.text = lineas[indice];
        }
        else
        {
            panelDialogo.SetActive(false);
            textoInteractuar.SetActive(true);
        }
    }
    public bool EstaActivo()
    {
        return panelDialogo.activeSelf;
    }

    public void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
    }
}