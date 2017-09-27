using UnityEngine;

public class Controles : MonoBehaviour
{
    [SerializeField]
    private bool _ControlesMouse = true;
    [SerializeField]
    private bool _ControlesTeclado = true;
    [SerializeField]
    private bool _ControlesCelular = true;

    public float DirecaoZoomCamera()
    {
        #region "Computador"
        float lDirecaoZoomScroll = -Input.GetAxis("Mouse ScrollWheel");
        float lDirecaoZoomTeclado = 0;
        if (Input.GetKey(KeyCode.KeypadPlus))
            lDirecaoZoomTeclado = -1 * Time.deltaTime;
        else if (Input.GetKey(KeyCode.KeypadMinus))
            lDirecaoZoomTeclado = +1 * Time.deltaTime;
        #endregion

        #region "Celular"
        float lDirecaoZoomCelular = 0;
        if (Input.touchCount == 2)
        {
            Vector2 lPosIniPriToque = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
            Vector2 lPosIniSegToque = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
            float lMagToqueIni = (lPosIniPriToque - lPosIniSegToque).magnitude;
            float lMagMedToqueIni = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;
            lDirecaoZoomCelular = (lMagToqueIni - lMagMedToqueIni) * Time.deltaTime;
        }
        #endregion

        float lDirecaoZoomCamera = 0;
        if (lDirecaoZoomScroll != 0 && _ControlesMouse)
        {
            lDirecaoZoomCamera = lDirecaoZoomScroll;
        }
        else if (lDirecaoZoomTeclado != 0 && _ControlesTeclado)
        {
            lDirecaoZoomCamera = lDirecaoZoomTeclado;
        }
        else if (lDirecaoZoomCelular != 0 && _ControlesCelular)
        {
            lDirecaoZoomCamera = lDirecaoZoomCelular;
        }
        return lDirecaoZoomCamera;
    }

    public Vector2 MovimentoCamera()
    {
        #region "Computador"        
        Vector2 lMovimentoMouse = new Vector2(Input.GetAxis("Mouse X") * Time.deltaTime, Input.GetAxis("Mouse Y") * Time.deltaTime);
        Vector2 lMovimentoTeclado = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime);
        #endregion

        #region "Celular"     
        Vector2 lMovimentoToque = Vector2.zero;
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            lMovimentoToque = Input.GetTouch(0).deltaPosition * Time.deltaTime;
        }
        #endregion

        Vector2 lMovimentoCamera = Vector2.zero;
        if (lMovimentoMouse != Vector2.zero && _ControlesMouse)
        {
            lMovimentoCamera = lMovimentoMouse;
        }
        else if (lMovimentoTeclado != Vector2.zero && _ControlesTeclado)
        {
            lMovimentoCamera = lMovimentoTeclado;
        }
        else if (lMovimentoToque != Vector2.zero && _ControlesCelular)
        {
            lMovimentoCamera = lMovimentoToque;
        }
        return lMovimentoCamera;
    }
}
