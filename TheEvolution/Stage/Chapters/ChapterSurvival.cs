﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TheEvolution.Core;
using TheEvolution.Stage.Cells;
using TheEvolution.Stage.Organella;
using TheEvolution.Stage.Foods;
using System.Windows.Forms;

namespace TheEvolution.Stage.Chapters {
    class ChapterSurvival : Chapter {

        public ChapterSurvival(PictureBox picBoxBg) : base(picBoxBg) {
            GetReady();
            for (int i = 0; i < 8; i++) {
                food.Add(new Algae(picBoxBg));
                food.Add(new Charophyta(picBoxBg));
            }
            for (int i = 0; i < 2; i++) {
                otherCells.Add(new PlantWall(picBoxBg, pPlantWall[i]));
                otherCells.Add(new Tracker(picBoxBg, pTracker[i]));
                otherCells.Add(new Shocker(picBoxBg, pShocker[i]));
                otherCells.Add(new Predator(picBoxBg, pPredator[i]));
                otherCells.Add(new Virus(picBoxBg, pVirus[i]));
            }
        }

        protected override void GetReady() {
            SetBorderPosition();
            SetPlantWallPosition();
        }

        private void SetPlantWallPosition() {
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.05));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.15));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.75));
            pPlantWall.Add(GameSystem.SetPosition(0.125, 0.85));
            pPlantWall.Add(GameSystem.SetPosition(0.275, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.275, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.275, 0.55));
            pPlantWall.Add(GameSystem.SetPosition(0.275, 0.75));
            pPlantWall.Add(GameSystem.SetPosition(0.275, 0.85));
            pPlantWall.Add(GameSystem.SetPosition(0.325, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.325, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.375, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.375, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.425, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.425, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.475, 0.65));
            pPlantWall.Add(GameSystem.SetPosition(0.525, 0.65));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.05));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.15));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.55));
            pPlantWall.Add(GameSystem.SetPosition(0.575, 0.65));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.55));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.65));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.75));
            pPlantWall.Add(GameSystem.SetPosition(0.725, 0.85));
            pPlantWall.Add(GameSystem.SetPosition(0.775, 0.25));
            pPlantWall.Add(GameSystem.SetPosition(0.775, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.775, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.775, 0.55));
            pPlantWall.Add(GameSystem.SetPosition(0.775, 0.65));
            pPlantWall.Add(GameSystem.SetPosition(0.825, 0.35));
            pPlantWall.Add(GameSystem.SetPosition(0.825, 0.45));
            pPlantWall.Add(GameSystem.SetPosition(0.825, 0.55));
            pPlantWall.Add(GameSystem.SetPosition(0.825, 0.65));
        }
    }
}
