using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace OBSHelper
{
    public class SoundManager
    {
        SoundPlayer playerAirHorn;
        SoundPlayer playerApplause;
        SoundPlayer playerBoo;
        SoundPlayer playerCircus;
        SoundPlayer playerSnore;
        SoundPlayer playerTrombone;
        SoundPlayer playerGorilla;
        SoundPlayer playerChimp;

        public SoundManager()
        {
            playerAirHorn = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/airhorn.wav");
            playerApplause = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/applause.wav");
            playerBoo = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/boo.wav");
            playerCircus = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/circus.wav");
            playerSnore = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/snore.wav");
            playerTrombone = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/trombone.wav");
            playerGorilla = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/gorilla.wav");
            playerChimp = new SoundPlayer("C:/Users/Nathan/Documents/WSU Stream/OBS Helper/OBSHelper/OBSHelper/Sounds/chimp.wav");
        }

        public void PlayAirHorn()
        {
            playerAirHorn.Play();
        }

        public void PlayApplause()
        {
            playerApplause.Play();
        }

        public void PlayBoo()
        {
            playerBoo.Play();
        }

        public void PlayCircus()
        {
            playerCircus.Play();
        }

        public void PlaySnore()
        {
            playerSnore.Play();
        }

        public void PlayTrombone()
        {
            playerTrombone.Play();
        }

        public void PlayGorilla()
        {
            playerGorilla.Play();
        }

        public void PlayChimp()
        {
            playerChimp.Play();
        }
    }
}
