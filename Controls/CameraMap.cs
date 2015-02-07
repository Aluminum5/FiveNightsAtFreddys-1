using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNAF.Forms;
using FNAF.Common;

namespace FNAF.Controls
{
    public partial class CameraMap : UserControl
    {
        public CameraMap()
        {
            InitializeComponent();
        }

        private void Camera1Button_Click(object sender, EventArgs e)
        {
            Character[] characterArray = new Character[] 
            {
                new Character() 
                { 
                    Name = "Chica", 
                    VisibleImage = new ImageEx(
                        "PartyRoom1FlashlightChica",
                        global::FNAF.Properties.Resources.PartyRoom1FlashlightChica
                    ),
                    ScareImage = new ImageEx(
                        "ToyChicascare",
                        global::FNAF.Properties.Resources.ToyChicaScare
                    ),
                },
                new Character()
                {
                    Name = "Mangle", 
                    VisibleImage = new ImageEx(
                        "PartyRoom1FlashlightMangle",
                        global::FNAF.Properties.Resources.PartyRoom1FlashlightMangle
                    ),
                    ScareImage = new ImageEx(
                        "MangleScare",
                        global::FNAF.Properties.Resources.MangleScare
                    )   
                },
                new Character()
                {
                    Name = "Old Bonnie", 
                    VisibleImage = new ImageEx(
                        "PartyRoom1FlashlightOldBonnie",
                        global::FNAF.Properties.Resources.PartyRoom1FlashlightOldBonnie
                    ),
                    ScareImage = new ImageEx(
                        "OldBonnieScare",
                        global::FNAF.Properties.Resources.OldBonnieScare
                    )   
                }
            };

            CameraForm cameraForm = new CameraForm(new FormData()
            {
                Characters = new CharacterCollection(characterArray.ToList()),
                DefaultImage = new ImageEx(
                    "PartyRoom1NoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom1NoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PartyRoom1FlashlightEmpty",
                    global::FNAF.Properties.Resources.PartyRoom1FlashlightEmpty
                ),
                Name = "Party Room 1",
                ShowCameraMap = true,
                SupportsFlashlight = true,
                SupportsMask = false
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera2Button_Click(object sender, EventArgs e)
        {
            Character[] characterArray = new Character[] 
            {
                new Character() 
                { 
                    Name = "ToyBonnie", 
                    VisibleImage = new ImageEx(
                        "PartyRoom2FlashlightToyBonnie",
                        global::FNAF.Properties.Resources.PartyRoom2FlashlightToyBonnie
                    ),
                    ScareImage = new ImageEx(
                        "ToyBonniescare",
                        global::FNAF.Properties.Resources.ToyBonnieScare
                    )   
                }
            };

            CameraForm cameraForm = new CameraForm(new FormData()
            {
                Characters = new CharacterCollection(characterArray.ToList()),
                DefaultImage = new ImageEx(
                    "PartyRoom2NoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom2NoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PartyRoom2FlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom2FlashlightNoCharacters
                ),
                Name = "Party Room 2",
                ShowCameraMap = true,
                SupportsFlashlight = true,
                SupportsMask = false
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }


        private void Camera3Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "PartyRoom3NoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom3NoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PartyRoom3FlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom3FlashlightNoCharacters
                ),
                Name = "Party Room 3",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera4Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "PartyRoom4NoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom4NoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PartyRoom4FlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PartyRoom4FlashlightNoCharacters
                ),
                Name = "Party Room 4",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera5Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "LeftAirVentNoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.LeftAirVentNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "LeftAirVentFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.LeftAirVentFlashlightNoCharacters
                ),
                Name = "Left Air Vent",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera6Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "RightAirVentNoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.RightAirVentNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "RightAirVentFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.RightAirVentFlashlightNoCharacters
                ),
                Name = "Right Air Vent",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera7Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "MainHallNoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.MainHallNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "MainHallFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.MainHallFlashlightNoCharacters
                ),
                Name = "Main Hall",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera8Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "PartsAndServiceNoFlashlightNoOldCharacters",
                    global::FNAF.Properties.Resources.PartsAndServiceNoFlashlightNoOldCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PartsAndServiceFlashlightAllOldCharacters",
                    global::FNAF.Properties.Resources.PartsAndServiceFlashlightAllOldCharacters
                ),
                Name = "Parts and Services",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera9Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "PrizeCornerNoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PrizeCornerNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "PrizeCornerFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.PrizeCornerFlashlightNoCharacters
                ),
                Name = "Prize Corner",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
            Global.PlaySound(global::FNAF.Properties.Resources.MusicBoxSong, false);
        }

        private void Camera10Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "GameAreaNoFlashlightBB",
                    global::FNAF.Properties.Resources.GameAreaNoFlashlightBB
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "GameAreaFlashlightBB",
                    global::FNAF.Properties.Resources.GameAreaFlashlightBB
                ),
                Name = "Game Area",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera11Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "ShowStageNoFlashlightAllCharacters",
                    global::FNAF.Properties.Resources.ShowStageNoFlashlightAllCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "ShowStageFlashlightAllCharacters",
                    global::FNAF.Properties.Resources.ShowStageFlashlightAllCharacters
                ),
                Name = "Show Stage",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void Camera12Button_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                DefaultImage = new ImageEx(
                    "KidsCoveNoFlashlightMangle",
                    global::FNAF.Properties.Resources.KidsCoveNoFlashlightMangle
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "KidsCoveFlashlightMangle",
                    global::FNAF.Properties.Resources.KidsCoveFlashlightMangle
                ),
                Name = "Show Stage",
                ShowCameraMap = true,
                SupportsFlashlight = true
            }
            );
            WindowControls.ShowForm(cameraForm, (Button)sender, true);
        }

        private void YouButtonPanel_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(new FormData()
            {
                Name = "Office",
                DefaultImage = new ImageEx(
                    "OfficeNoFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.OfficeNoFlashlightNoCharacters
                ),
                DefaultFlashlightOnImage = new ImageEx(
                    "OfficeFlashlightNoCharacters",
                    global::FNAF.Properties.Resources.OfficeFlashlightNoCharacters
                ),
                ShowCameraMap = true,
                SupportsFlashlight = true,
                SupportsMask = true
            });

            WindowControls.ShowForm(cameraForm, (Control)sender, true);
        }
    }
}
