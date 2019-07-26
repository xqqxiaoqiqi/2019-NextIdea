using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataBase
{
    public class ConvPanel : MonoBehaviour
    {
        private Button convbutton;
        private Text convtext;
        private Text nametext;
        public float charsPerSecond = 1.5f;
        private float timer;
        void Awake()
        {
            convbutton = GetComponent<Button>();
            convtext = GetComponentsInChildren<Text>()[0];
            nametext = GetComponentsInChildren<Text>()[1];
            InstalizePanel();
        }
        private void Update()
        {
            TryShowConv();
        }
        private void InstalizePanel()
        {
            convbutton.onClick.AddListener(PanelOnclick);
        }
        public void PanelOnclick()
        {
            switch (DialogViewer.Instance().paneltype)
            {
                case PanelType.Ready:
                    DialogViewer.Instance().paneltype = PanelType.Showing;
                    break;
                case PanelType.Showing:
                    DialogViewer.Instance().paneltype = PanelType.RequestStop;
                    break;
                case PanelType.Over:
                    DialogViewer.HidePanel(this.gameObject);
                    break;
                default:
                    break;

            }
        }
        private void TryShowConv()
        {
            switch (DialogViewer.Instance().paneltype)
            {
                case PanelType.Showing:
                    nametext.text = DialogViewer.Instance().currname;
                    if (DialogViewer.Instance().paneltype == PanelType.Showing)
                    {
                        DialogViewer.ShowPanel(this.gameObject);
                        timer += Time.deltaTime;
                        if (timer >= charsPerSecond)
                        {
                            DialogViewer.currentPos++;
                            timer = 0;
                            convtext.text = DialogViewer.Instance().currconv.Substring(0, DialogViewer.currentPos);
                            if (DialogViewer.currentPos >= DialogViewer.Instance().currconv.Length)
                            {
                                DialogViewer.Instance().ShowOverProcess();
                            }
                        }
                    }
                    break;
                case PanelType.RequestStop:
                    convtext.text = DialogViewer.Instance().currconv;
                    DialogViewer.Instance().ShowOverProcess();
                    break;
                default:
                    break;

            }

        }

    }

}

