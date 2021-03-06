﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheEvolution.Core;

namespace TheEvolution.Stage.Cells {
    class Virus : Cell {

        public Virus(PictureBox picBoxBg, Point point) : base(picBoxBg, point) {
            GameSystem.otherCells.Add(this);
            images = ImageContainer.imgVirus;
            size = images[0].Size;
            moveSpeed = (int)(0.15 * size.Width);
        }

        public override void Paint(object sender, PaintEventArgs e) {
            lock (rotation) {
                e.Graphics.Transform = rotation;
                e.Graphics.DrawImage(images[imgIndex],
                    position.X, position.Y, size.Width, size.Height);
                rotation.Reset();
                e.Graphics.Transform = rotation;
            }
        }

        public void VirusMove() {
            direction = GetUnitDirectionToTarget(GameSystem.player);
            
            if (DistanceToPlayer <= 4 * size.Width) {
                position.X += direction.X * moveSpeed;
                position.Y += direction.Y * moveSpeed;              
            } else {
                position.X += random.Next(-5, 5);
                position.Y += random.Next(-5, 5);
            }
        }

        public override int GetAngle() {
            if (GameSystem.player.Position.Y >= GetCenter().Y) {
                return AngleToPlayer();
            } else {
                return AngleToPlayer() * (-1) ;
            }
        }

        public int AngleToPlayer() {
            int dirX = GameSystem.player.GetCenter().X - GetCenter().X;
            int dirY = GameSystem.player.GetCenter().Y - GetCenter().Y;
            double R = Math.Sqrt((dirX * dirX) + (dirY * dirY));
            return (int)(Math.Acos(dirX / R) / Math.PI * 180);          
        }

        public override void NextStep() {
            VirusMove();
            Rotate();
        }

        public override void Collide(int myId) {;
            GameSystem.otherCells.RemoveAt(myId);
            Dispose();
        }
    }
}
