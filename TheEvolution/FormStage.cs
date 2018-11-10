﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using TheEvolution.Core;
using TheEvolution.Stage;
using TheEvolution.Stage.Chapters;
using TheEvolution.Properties;
using TheEvolution.Stage.Cells;
using TheEvolution.Stage.Organella;

namespace TheEvolution {
    public partial class FormStage : Form {

        public static EChapter chapter;
        public static bool isPause, isNextChapter;
        public ChapterTutorial chapterTutorial;
        public ChapterSurvival chapterSurvival;
        public int hpBeatInterval;
        public bool canEat;

        public FormStage() {
            InitializeComponent();
            ImageContainer.PrepareImage();
            GameSystem.formStage = this;
            GameSystem.picBoxStage = picBoxStage;
            picBoxStage.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(picBoxStage, true, null);
            picBoxStage.Size = new Size(3 * GameSystem.screen.Width, 3 * GameSystem.screen.Height);
            picBoxStage.Location = new Point(-GameSystem.screen.Width, -GameSystem.screen.Height);
            canEat = true;
        }

        private void FormStage_Load(object sender, EventArgs e) {
            MCImusic.mciMusic("Musics/5.mp3", "close");

            GameSystem.SetControlSize(panelTip, ClientSize, 0.5, 0.5, 0.6, 0.6);
            GameSystem.SetControlSize(panelHelp, ClientSize, 0.5, 0.5, 0.65, 0.8);

            GameSystem.SetSquareControlSize(labelGoal, panelHelp.Size, 0.06, 0.13, 0.07);
            GameSystem.SetSquareControlSize(helpGoal, panelHelp.Size, 0.06, 0.2, 0.07);

            GameSystem.SetSquareControlSize(labelControl, panelHelp.Size, 0.06, 0.27, 0.07);
            GameSystem.SetSquareControlSize(helpControl, panelHelp.Size, 0.06, 0.34, 0.07);

            GameSystem.SetSquareControlSize(labelFood, panelHelp.Size, 0.06, 0.41, 0.07);
            GameSystem.SetSquareControlSize(helpFood1, panelHelp.Size, 0.06, 0.48, 0.07);
            GameSystem.SetSquareControlSize(helpFood2, panelHelp.Size, 0.14, 0.48, 0.07);

            GameSystem.SetSquareControlSize(labelWall, panelHelp.Size, 0.06, 0.55, 0.07);
            GameSystem.SetSquareControlSize(helpPlantWall, panelHelp.Size, 0.06, 0.62, 0.07);

            GameSystem.SetSquareControlSize(labelEnemy, panelHelp.Size, 0.06, 0.69, 0.07);
            GameSystem.SetSquareControlSize(helpPredator, panelHelp.Size, 0.06, 0.76, 0.07);
            GameSystem.SetSquareControlSize(helpCompetitor, panelHelp.Size, 0.14, 0.76, 0.07);
            GameSystem.SetSquareControlSize(helpTracker, panelHelp.Size, 0.22, 0.76, 0.07);
            GameSystem.SetSquareControlSize(helpShocker, panelHelp.Size, 0.06, 0.87, 0.07);
            GameSystem.SetSquareControlSize(helpVirus, panelHelp.Size, 0.14, 0.87, 0.07);

            GameSystem.SetControlSize(picBoxGif, panelHelp.Size, 0.64, 0.56, 0.66, 0.78);

            GameSystem.SetControlSize(panelStatus, ClientSize, 0.15, 0.025, 0.3, 0.05);
            GameSystem.SetSquareControlSize(picBoxHp, panelStatus.Size, 0.06, 0.5, 0.08);
            GameSystem.SetControlSize(picBoxHpBar, panelStatus.Size, 0.38, 0.5, 0.5, 0.95);
            GameSystem.SetSquareControlSize(picBoxEat, panelStatus.Size, 0.7, 0.5, 0.1);
            GameSystem.SetControlSize(picBoxEatBar, panelStatus.Size, 0.88, 0.5, 0.2, 0.95);

            GameSystem.SetControlSize(labelTime, ClientSize, 0.5, 0.035, 0.1, 0.08);

            GameSystem.SetControlSize(panelSetting, ClientSize, 0.93, 0.025, 0.14, 0.05);
            GameSystem.SetSquareControlSize(picBoxHelp, panelSetting.Size, 0.23, 0.5, 0.27);
            GameSystem.SetSquareControlSize(picBoxPause, panelSetting.Size, 0.5, 0.5, 0.26);
            GameSystem.SetSquareControlSize(picBoxRestart, panelSetting.Size, 0.77, 0.5, 0.27);

            NextChapter(chapter);
        }

        public void GameOver() {
            if (chapterTutorial != null) {
                picBoxStage.Location = new Point(-GameSystem.screen.Width, -GameSystem.screen.Height);
                picBoxHpBar.Image = Resources.Bloodbar5;
                picBoxEatBar.Image = Resources.Progressbar0;
                chapterTutorial.End();
                MCImusic.mciMusic("Musics/easy1.mp3", "close");
                NextChapter(EChapter.Tutorial);
            }

            if (chapterSurvival != null) {
                GameSystem.formEnd = new FormEnd(
                    GameSystem.chapter.survivedTime,
                    GameSystem.player.GetCurrentImages());
                chapterSurvival.End();
                MCImusic.mciMusic("Musics/3.mp3", "close");
                GameSystem.formEnd.Show();
                Close();
            }
        }

        public void NextChapter(EChapter chapter) {
            ClearChapter();
            switch (chapter) {
                case EChapter.Tutorial:
                    chapterTutorial = new ChapterTutorial(picBoxStage);
                    GameSystem.player.HpChanged += OnPlayerHpChanged;
                    GameSystem.player.Eat += OnPlayerEat;
                    chapterTutorial.ShowTip();
                    chapterTutorial.Start();
                    picBoxHelp_Click(picBoxHelp, EventArgs.Empty);
                    MCImusic.mciMusic("Musics/S2.mp3", "play", "repeat");
                    break;
                case EChapter.Survival:
                    picBoxStage.Location = new Point(-GameSystem.screen.Width, -GameSystem.screen.Height);
                    picBoxHpBar.Image = Resources.Bloodbar5;
                    picBoxEatBar.Image = Resources.Progressbar0;
                    labelTime.Visible = true;
                    chapterSurvival = new ChapterSurvival(picBoxStage);
                    GameSystem.player.HpChanged += OnPlayerHpChanged;
                    GameSystem.player.Eat += OnPlayerEat;
                    chapterSurvival.ShowTip();
                    chapterSurvival.Start();
                    MCImusic.mciMusic("Musics/3.mp3", "play", "repeat");
                    break;
            }
        }

        public void ClearChapter() {
            chapterTutorial = null; chapterSurvival = null;
        }

        public void OnPlayerHpChanged(object sender, PlayerEventArgs args) {
            hpBeatInterval = 4;
            if (InvokeRequired) {
                Invoke((Action)delegate () {
                    GameSystem.SetSquareControlSize(picBoxHp, panelStatus.Size, 0.06, 0.5, 0.15);
                });
            } else {
                GameSystem.SetSquareControlSize(picBoxHp, panelStatus.Size, 0.06, 0.5, 0.15);
            }
            switch (args.hp) {
                case 0:
                    picBoxHpBar.Image = Resources.Bloodbar0;
                    break;
                case 1:
                    picBoxHpBar.Image = Resources.Bloodbar1;
                    break;
                case 2:
                    picBoxHpBar.Image = Resources.Bloodbar2;
                    break;
                case 3:
                    picBoxHpBar.Image = Resources.Bloodbar3;
                    break;
                case 4:
                    picBoxHpBar.Image = Resources.Bloodbar4;
                    break;
                case 5:
                    picBoxHpBar.Image = Resources.Bloodbar5;
                    break;
                case 6:
                    picBoxHpBar.Image = Resources.Bloodbar6;
                    break;
                case 7:
                    picBoxHpBar.Image = Resources.Bloodbar7;
                    break;
                case 8:
                    picBoxHpBar.Image = Resources.Bloodbar8;
                    break;
                case 9:
                    picBoxHpBar.Image = Resources.Bloodbar9;
                    break;
                case 10:
                    picBoxHpBar.Image = Resources.Bloodbar10;
                    break;
            }
            if (!canEat) {
                picBoxEatBar.Image = Resources.Progressbar0;
                canEat = true;
            }
        }

        public void OnPlayerEat(object sender, PlayerEventArgs args) {
            if (args.hp != 10) {
                switch (args.foodCount) {
                    case 0:
                        picBoxEatBar.Image = Resources.Progressbar0;
                        break;
                    case 1:
                        picBoxEatBar.Image = Resources.Progressbar1;
                        break;
                    case 2:
                        picBoxEatBar.Image = Resources.Progressbar2;
                        break;
                    case 3:
                        picBoxEatBar.Image = Resources.Progressbar3;
                        break;
                }
            } else {
                picBoxEatBar.Image = Resources.Progressbar4;
                canEat = false;
            }
        }

        private void picBoxRestart_Click(object sender, EventArgs e) {
            if (!isPause) {
                if (chapterTutorial != null) {
                    chapterTutorial.End();
                }
                if (chapterSurvival != null) {
                    chapterSurvival.End();
                }
                Application.Restart();
            }
        }

        public void Pause_Click(object sender, EventArgs e) {
            if (panelTip.Visible) {
                ExitTip();
            } else {
                if (chapterTutorial != null) {
                    chapterTutorial.Pause();
                } else if (chapterSurvival != null) {
                    chapterSurvival.Pause();
                }

                if (sender != picBoxPause) {
                    if (sender is Mitochondria) {
                        panelTip.BackgroundImage = Resources.MitoIntro;
                    } else if (sender is Lysosome) {
                        panelTip.BackgroundImage = Resources.LysoIntro;
                    } else if (sender is ER) {
                        panelTip.BackgroundImage = Resources.ERIntro;
                    } else if (sender is Centromere) {
                        panelTip.BackgroundImage = Resources.CentroIntro;
                        isNextChapter = true;
                    }
                } else {
                    panelTip.BackgroundImage = Resources.PauseScreen;
                }
                panelTip.Visible = true;
            }
        }

        private void picBoxHelp_Click(object sender, EventArgs e) {
            if (panelHelp.Visible) {
                panelHelp.Hide();
                if (chapterTutorial != null) {
                    chapterTutorial.Resume();
                } else if (chapterSurvival != null) {
                    chapterSurvival.Resume();
                }
            } else {
                panelHelp.Show();
                if (chapterTutorial != null) {
                    chapterTutorial.Pause();
                } else if (chapterSurvival != null) {
                    chapterSurvival.Pause();
                }
            }
        }

        private void panelTip_Click(object sender, EventArgs e) {
            ExitTip();
        }

        private void FormStage_KeyDown(object sender, KeyEventArgs e) {
            if (panelTip.Visible) {
                if (e.KeyCode == Keys.Enter) {
                    ExitTip();
                }
            }
        }

        private void helpGoal_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Evolution;
        }

        private void helpControl_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Movegif;
        }

        private void helpPlantWall_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.PlantWallgif;
        }

        private void helpFood1_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.EatFoodgif;
        }

        private void helpFood2_Click(object sender, EventArgs e) {
            helpFood1_Click(sender, e);
        }

        private void helpPredator_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Predatorgif;
        }

        private void helpCompetitor_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Competitorgif;
        }

        private void helpTracker_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Trackergif;
        }

        private void helpShocker_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Shockergif;
        }

        private void helpVirus_Click(object sender, EventArgs e) {
            picBoxGif.Image = Resources.Virusgif;
        }

        private void panelHelp_Click(object sender, EventArgs e) {
            if (panelHelp.Visible) {
                panelHelp.Hide();
                if (chapterTutorial != null) {
                    chapterTutorial.Resume();
                } else if (chapterSurvival != null) {
                    chapterSurvival.Resume();
                }
            }
        }

        private void ExitTip() {
            if (chapterTutorial != null) {
                chapterTutorial.Resume();
            } else if (chapterSurvival != null) {
                chapterSurvival.Resume();
            }

            panelTip.Visible = false;

            if (isNextChapter) {
                isNextChapter = false;
                picBoxStage.Location = new Point(-GameSystem.screen.Width, -GameSystem.screen.Height);
                picBoxHpBar.Image = Resources.Bloodbar5;
                picBoxEatBar.Image = Resources.Progressbar0;
                if (chapter == EChapter.Tutorial) {
                    chapterTutorial.End();
                    chapter = EChapter.Survival;
                    NextChapter(EChapter.Survival);
                }
            }
        }
    }

    public enum EChapter {
        Tutorial, Survival
    }
}
