using _1DV437_NeilArmstrong.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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


        public GameView()
        {
            myList = new List<Unit>();
        }

        public void Initiate(ContentManager content, Camera camera, GraphicsDevice graphics)
        {
            explosionHandler = new ExplosionAnimationHandler();
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
        }

        public void PlayEnemyFireSound()
        {
            soundEffects.Add(laserFire);
            foreach(SoundEffect se in soundEffects)
            {
                se.Play(0.5f, 0.5f, 0f);
            }
             soundEffects.Clear();        
        }

        public void PlayPlayerFireSound()
        {
            soundEffects.Add(playerLaserFire);
            foreach (SoundEffect se in soundEffects)
            {
                se.Play(0.1f, 0.6f, 0f);
            }
            soundEffects.Clear();
        }

        public void DrawExplosion()
        {
 
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
                Vector2 mousePosition = new Vector2(ms.X, ms.Y);             
                Vector2 visualPos = camera.scaleVisualPosition(playButtonLocation);
               
                if (mousePosition.X >= visualPos.X - playButton.Bounds.Width / 2 && mousePosition.X <= visualPos.X + playButton.Bounds.Width / 2
                    && mousePosition.Y >= visualPos.Y - playButton.Bounds.Height / 2 && mousePosition.Y <= visualPos.Y + playButton.Bounds.Height / 2)
                {                   
                    return true;
                }
            }
            return false;           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, camera.GetGameWindow(), Color.White);
    
            foreach(Unit unit in myList)
            {
                if (unit is EnemyShip)
                {
                    spriteBatch.Draw(enemyShip, camera.scaleVisualPosition(unit.GetPosition()),
                    null, Color.White, 0f, new Vector2(enemyShip.Bounds.Width/2, enemyShip.Bounds.Height/2),
                    (unit as EnemyShip).Scale, SpriteEffects.None, 0f);
                }
                else if (unit is PlayerShip)
                {
                    Color color = new Color();
                    color = Color.White;
                    if ((unit as PlayerShip).PlayerGotHit())
                    {
                        explosionHandler.AddExplosion((unit as PlayerShip).GetPosition());


                        //color = Color.Red;
                        //draw explosion animation
                        //play hit sound
                        //particle effect etc
                    }
                    spriteBatch.Draw(playerShip, camera.scaleVisualPosition(unit.GetPosition()),
                    null, color, (unit as PlayerShip).Rotation, new Vector2(playerShip.Bounds.Width / 2, playerShip.Bounds.Height / 2),
                    (unit as PlayerShip).Scale, SpriteEffects.None, 0f);
                }
                else if (unit is Projectile)
                {
                    spriteBatch.Draw(basicProjectile, camera.scaleProjectilePosition(unit.GetPosition()),
                    null, Color.White, (unit as Projectile).Angle, new Vector2(playerShip.Bounds.Width/1.5f, playerShip.Bounds.Height/2),
                    (unit as Projectile).Scale, SpriteEffects.None, 0f);
                }
                else if (unit is Boss)
                {
                    spriteBatch.Draw(boss, camera.scaleVisualPosition(unit.GetPosition()),
                    null, Color.White, 3.1f, new Vector2(boss.Bounds.Width / 2, boss.Bounds.Height / 2),
                    (unit as Boss).Scale, SpriteEffects.None, 0f);
                }
            }           
        }
    }
}
