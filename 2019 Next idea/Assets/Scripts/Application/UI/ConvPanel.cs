using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

namespace GameGUI
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
            switch (LevelManager.presentviewer.GetComponent<DialogViewer>().paneltype)
            {
                case PanelType.Ready:
                    LevelManager.presentviewer.GetComponent<DialogViewer>().paneltype = PanelType.Showing;
                    break;
                case PanelType.Showing:
                    LevelManager.presentviewer.GetComponent<DialogViewer>().paneltype = PanelType.RequestStop;
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
            if(LevelManager.presentviewer!=null)
            {
                switch (LevelManager.presentviewer.GetComponent<DialogViewer>().paneltype)
                {
                    case PanelType.Showing:
                        nametext.text = LevelManager.presentviewer.GetComponent<DialogViewer>().currname;
                        if (LevelManager.presentviewer.GetComponent<DialogViewer>().paneltype == PanelType.Showing)
                        {
                            DialogViewer.ShowPanel(this.gameObject);
                            timer += Time.deltaTime;
                            if (timer >= charsPerSecond)
                            {
                                DialogViewer.currentPos++;
                                timer = 0;
                                convtext.text = LevelManager.presentviewer.GetComponent<DialogViewer>().currconv.Substring(0, DialogViewer.currentPos);
                                if (DialogViewer.currentPos >= LevelManager.presentviewer.GetComponent<DialogViewer>().currconv.Length)
                                {
                                    LevelManager.presentviewer.GetComponent<DialogViewer>().ShowOverProcess();
                                }
                            }
                        }
                        break;
                    case PanelType.RequestStop:
                        convtext.text = LevelManager.presentviewer.GetComponent<DialogViewer>().currconv;
                        LevelManager.presentviewer.GetComponent<DialogViewer>().ShowOverProcess();
                        break;
                    default:
                        break;

                }

            }

        }

    }

}

