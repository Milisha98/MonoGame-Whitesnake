using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Demo
{
    internal class DemoController
    {

        public DemoStep GetNextStep(int currentStep) =>
            Steps.FirstOrDefault(x => x.Sequence > currentStep);


        public List<DemoStep> Steps
        {
            get =>
                new List<DemoStep>
                { // M
                  new DemoStep(10, 10, 0, 1000),
                  new DemoStep(20, 2, -5f, 800, false),
                  new DemoStep(30, 10, 0, 500),
                  new DemoStep(40, 2, -5f, 200),
                  new DemoStep(50, 10, 0, 400),
                  new DemoStep(60, 2, -5f, 800, false),
                  new DemoStep(70, 10, 0, 1000),
                  // e
                  new DemoStep(80, 2, 5f, 800, false),
                  new DemoStep(90, 10, 0, 1000),
                  new DemoStep(100, 10, -4f, 1000),
                  new DemoStep(110, 10, -1, 700),
                  new DemoStep(120, 10, -5f, 300),
                  new DemoStep(130, 10, -1, 1000),
                  // R
                  new DemoStep(140, 2, -5f, 600, false),
                  new DemoStep(150, 10, 0, 1000),
                  new DemoStep(160, 2, 5f, 650),
                  new DemoStep(170, 10, 0, 1000),
                  new DemoStep(180, 10, 4f, 1000),
                  new DemoStep(190, 10, 0f, 400),
                  new DemoStep(200, 1, -5f, 360),
                  new DemoStep(210, 10, 0, 700),
                  new DemoStep(220, 2, -5f, 360),
                  new DemoStep(230, 10, 0, 1100),
                  // R
                  new DemoStep(240, 2, -5f, 700, false),
                  new DemoStep(250, 10, 0, 1000),
                  new DemoStep(260, 2, 5f, 650),
                  new DemoStep(270, 10, 0, 1000),
                  new DemoStep(280, 10, 4f, 1000),
                  new DemoStep(290, 10, 0f, 400),
                  new DemoStep(300, 1, -5f, 360),
                  new DemoStep(310, 10, 0, 700),
                  new DemoStep(320, 2, -5f, 360),
                  new DemoStep(330, 10, 0, 800),
                  // y
                  new DemoStep(340, 10, 0, 400),
                  new DemoStep(350, 2, -5, 800, false),
                  new DemoStep(360, 10, -2, 1000),
                  new DemoStep(370, 1, -5, 700, false),
                  new DemoStep(380, 10, 0, 700),
                  new DemoStep(390, 10, 2, 900),
                  
                  //new DemoStep(100, 2, 5, 950, false), Shortcut

                  // X
                  new DemoStep(400, 10, -5, 250, false),
                  new DemoStep(410, 10, 0, 500, false),
                  new DemoStep(420, 10, 0, 1000, true),
                  new DemoStep(430, 5, 5, 500, false),
                  new DemoStep(440, 10, 0, 800, false),
                  new DemoStep(450, 5, 5, 500, false),
                  new DemoStep(460, 10, 0, 1000, true),
                  new DemoStep(470, 10, -5, 400),
                  new DemoStep(480, 2, -4, 100),

                  // M
                  new DemoStep(490, 10, 0, 1000),
                  new DemoStep(500, 2, -5f, 800, false),
                  new DemoStep(510, 10, 0, 500),
                  new DemoStep(520, 2, -5f, 200),
                  new DemoStep(530, 10, 0, 400),
                  new DemoStep(540, 2, -5f, 800, false),
                  new DemoStep(550, 10, 0, 1000),
                  new DemoStep(560, 10, -5, 550),

                  // A
                  new DemoStep(570, 10, 0, 1000),
                  new DemoStep(580, 2, -5, 800, false),
                  new DemoStep(590, 10, 0, 1400),
                  new DemoStep(600, 10, 3, 1000, false),
                  new DemoStep(610, 10, 0, 800, false),
                  new DemoStep(620, 2, 5, 400, false),
                  new DemoStep(630, 10, 0, 800),

                  // S
                  new DemoStep(640, 10, -1, 1000),
                  new DemoStep(650, 5, -5, 500),
                  new DemoStep(660, 10, -2, 800),
                  new DemoStep(670, 10, 2, 800)

                };
        }


    }
}
