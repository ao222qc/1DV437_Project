using _1DV437_NeilArmstrong.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class GameView : Observer
    {
        ExplosionAnimationHandler explosionHandler;
        List<Unit> myList;
        public Texture2D playerShip;
        public float playerShipScale = 0.25f;
        public Texture2D enemyShip;
        public float enemyShipScale = 0.5f;
        Texture2D gameScreen;
        Texture2D boss;
        public Texture2D basicProjectile;
        Camera camera;
        Texture2D background;
        Texture2D menuBackground;
        Texture2D playButton;
        Vector2 playButtonLocation;
        SoundEffect laserFire;
        SoundEffect playerLaserFire;
        List<SoundEffect> soundEffects = new List<SoundEffect>();
        Texture2D explosion;
        SoundEffect explodingShipSound;
        MovingBackground movingBackground;
        ParticleAnimationHandler particleAnimationHandler;
        bool mouseClick;
        bool mouseClickThisFrame;
        Texture2D particle;

        public GameView()
        {
            myList = new List<Unit>();
        }

        public void Initiate(ContentManager content, Camera camera, GraphicsDevice graphics,
            ExplosionAnimationHandler explosionHandler, ParticleAnimationHandler particleAnimationHandler)
        {
            particle = content.Load<Texture2D>("spark");
            this.particleAnimationHandler = particleAnimationHandler;
            movingBackground = new MovingBackground(content, camera);
            explodingShipSound = content.Load<SoundEffect>("explosionsound");
            this.explosionHandler = explosionHandler;
            explosion = content.Load<Texture2D>("explosion");
            playerLaserFire = content.Load<SoundEffect>("PlayerFireSound");
            laserFire = content.Load<SoundEffect>("LaserFireSound");
            boss = content.Load<Texture2D>("bosship");
            playButtonLocation = new Vector2(0.5f, 0.5f);
            playButton = content.Load<Texture2D>("playbutton");
            menuBackground = content.Load<Texture2D>("spacebackgroundmenu");
            background = content.Load<Texture2D>("spacebackground");
            playerShip = content.Load<Texture2D>("spaceship");
            enemyShip = content.Load<Texture2D>("enemyspaceship");
            basicProjectile = content.Load<Texture2D>("projectile");
            this.camera = camera;
            gameScreen = gameScreen = new Texture2D(graphics, 1, 1);
            gameScreen.SetData<Color>(new Color[]
                {
                    Color.White
                });
            mouseClick = false;
        }

        public void PlayEnemyFireSound()
        {
            soundEffects.Add(laserFire);
            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffects[i].Play(0.5f, 0.5f, 0f);
            }
             soundEffects.Clear();
             //laserFire.Dispose();          
        }

        public void PlayShipExplodingSound()
        {
            soundEffects.Add(explodingShipSound);
            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffects[i].Play(0.5f, 0.5f, 0f);
            }
            soundEffects.Clear();
        }

        public void PlayPlayerFireSound()
        {
            soundEffects.Add(playerLaserFire);
            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffects[i].Play(0.1f, 0.6f, 0f);
            }

            soundEffects.Clear();          
        }

        //Called upon unit being added
        //Updates list of Units to be drawn
        //New units can be added in runtime
        public override void UpdateList(List<Unit> unitList)
        {     
            myList = unitList;
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackground, camera.GetGameWindow(), Color.White);
            spriteBatch.Draw(playButton, camera.scaleVisualPosition(playButtonLocation),
                    null, Color.White, 0f, new Vector2(playButton.Bounds.Width/2, playButton.Bounds.Height/2),
                    1f, SpriteEffects.None, 0f);           
        }


        public bool UserClicksPlay()
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                mouseClickThisFrame = true;
            }
            if (mouseClickThisFrame && !mouseClick)
            {
                Vector2 mousePosition = new Vector2(ms.X, ms.Y);             
                Vector2 visualPos = camera.scaleVisualPosition(playButtonLocation);
               
                if (mousePosition.X >= visualPos.X - playButton.Bounds.Width / 2 && mousePosition.X <= visualPos.X + playButton.Bounds.Width / 2
                    && mousePosition.Y >= visualPos.Y - playButton.Bounds.Height / 2 && mousePosition.Y <= visualPos.Y + playButton.Bounds.Height / 2)
                {                   
                    return true;
                }
            }
            mouseClickThisFrame = false;
            //mouseClick = mouseClickThisFrame;
            return false;           
        }

        public void DrawOnDeathAnimation(Vector2 position)
        {
            particleAnimationHandler.AddParticle(position);
            explosionHandler.AddExplosion(position, explosion, ExplosionType.Death);
        }


        /*
         * Handles basic drawing of the game
         * Projectiles, enemies, player, background
         * checks if units get hit to display
         * an animation (explosion or particles)
         * These animations are drawn in different
         * classes in the view.
         * */
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(gameScreen, camera.GetGameWindow(), Color.White);

            //spriteBatch.Draw(gameScreen, new Vector2(0.5f, 0.5f),Color.White);
            movingBackground.Update();
            movingBackground.Draw(spriteBatch);


            for (int i = 0; i < myList.Count; i++)
            {       
                if (myList[i]  is EnemyShip)
                {
                    if ((myList[i] as EnemyShip).EnemyGotHit())
                    {
                        explosionHandler.AddExplosion((myList[i] as EnemyShip).GetPosition(), explosion, ExplosionType.Hit);
                    }
                    spriteBatch.Draw(enemyShip, camera.scaleVisualPosition(myList[i].GetPosition()),
                    null, Color.White, 0f, new Vector2(enemyShip.Bounds.Width/2, enemyShip.Bounds.Height/2),
                    (myList[i] as EnemyShip).Scale, SpriteEffects.None, 0f);
                }
                else if (myList[i] is PlayerShip)
                {
                    Color color = new Color();
                    color = Color.White;
                    if ((myList[i] as PlayerShip).PlayerGotHit())
                    {
                        explosionHandler.AddExplosion((myList[i] as PlayerShip).GetPosition(), explosion, ExplosionType.Hit);
                    }
                    spriteBatch.Draw(playerShip, camera.scaleVisualPosition(myList[i].GetPosition()),
                    null, color, (myList[i] as PlayerShip).Rotation, new Vector2(playerShip.Bounds.Width / 2, playerShip.Bounds.Height / 2),
                    (myList[i] as PlayerShip).Scale, SpriteEffects.None, 0f);
                }
                else if (myList[i] is Projectile)
                {
                    spriteBatch.Draw(basicProjectile, camera.scaleProjectilePosition(myList[i].GetPosition()),
                    null, Color.White, (myList[i] as Projectile).Angle, new Vector2(playerShip.Bounds.Width / 1.5f, playerShip.Bounds.Height / 2),
                    (myList[i] as Projectile).Scale, SpriteEffects.None, 0f);
                }
                else if (myList[i] is Boss)
                {
                    if ((myList[i] as Boss).BossGotHit())
                    {
                        explosionHandler.AddExplosion((myList[i] as Boss).GetPosition(), explosion, ExplosionType.Hit);
                    }

                    spriteBatch.Draw(boss, camera.scaleVisualPosition(myList[i].GetPosition()),
                    null, Color.White, 3.1f, new Vector2(boss.Bounds.Width / 2, boss.Bounds.Height / 2),
                    (myList[i] as Boss).Scale, SpriteEffects.None, 0f);
                }
            }
                explosionHandler.Draw(spriteBatch, camera);
                particleAnimationHandler.Draw(spriteBatch, camera, particle);
        }
    }
}
