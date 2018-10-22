﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using TheEvolution.Stage.Cells;

namespace TheEvolution.Core {
    static class GameSystem {

        public static Form currentForm;
        public static PlayerCell currentPlayer;

        public static void Act() {
            while (true) {
                currentPlayer.NextStep();
                Thread.Sleep(100);
            }
        }

        public static void SetControlSize(Control control, Size ClientSize, double ratioL, double ratioT, double ratioW, double ratioH) {
            control.Width = Convert.ToInt32(ClientSize.Width * ratioW);
            control.Height = Convert.ToInt32(ClientSize.Height * ratioH);
            control.Left = Convert.ToInt32(ClientSize.Width * ratioL) - (control.Width / 2);
            control.Top = Convert.ToInt32(ClientSize.Height * ratioT) - (control.Height / 2);
        }

        public static void SetFrame(IPainting painter, Size ClientSize, double ratioX, double ratioY, double ratioW, double ratioH) {
            int W = Convert.ToInt32(ClientSize.Width * ratioW);
            int H = Convert.ToInt32(ClientSize.Height * ratioH);
            int X = Convert.ToInt32(ClientSize.Width * ratioX) - (W / 2);
            int Y = Convert.ToInt32(ClientSize.Height * ratioY) - (H / 2);
            painter.Frame = new Rectangle(X, Y ,W, H);
        }
    }
}
