using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pvbs
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _sirkaOkna = 800;
        private int _vyskaOkna = 600;

        private Texture2D _kruhovaTextura;
        private int x, y, w, h, rychlost, prumer, polomer;
        private Color barva;

        public MouseState mys;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.Title = "Put title here";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _sirkaOkna;
            _graphics.PreferredBackBufferHeight = _vyskaOkna;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            barva = Color.White;

            prumer = 100;
            polomer = prumer / 2;

            w = 100;
            h = 100;
            x = _sirkaOkna / 2 - w / 2;
            y = _vyskaOkna / 2 - h / 2;

            Color[] pixely = new Color[prumer * prumer];

            for (int i = 0; i < prumer; i++)
            {
                for (int j = 0; j < prumer; j++)
                {
                    if (Math.Sqrt(Math.Pow(j - polomer, 2) + Math.Pow(i - polomer, 2)) < polomer)
                    {
                        pixely[100 * i + j] = barva;
                    }
                    else
                    {
                        pixely[100 * i + j] = Color.Transparent;
                    }
                }
            }

            _kruhovaTextura = new Texture2D(GraphicsDevice, prumer, prumer);
            _kruhovaTextura.SetData(pixely);
        }
        protected override void Update(GameTime gameTime)
        {
            rychlost = 0;
            mys = Mouse.GetState();


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // kontrola proti utikani z okna
            if (x >= _sirkaOkna - prumer)
            {
                x = _sirkaOkna - prumer;
            }
            if (x <= 0)
            {
                x = 0;
            }
            if (y >= _vyskaOkna - prumer)
            {
                y = _vyskaOkna - prumer;
            }
            if (y <= 0)
            {
                y = 0;
            }

            // pozice kurzoru
            if (mys.X - 200 < x + prumer && mys.X + 200 > x && mys.Y - 200 < y + prumer && mys.Y + 200 > y)
            {
                rychlost = 1;

                barva = Color.Green;
            }
            else
            {
                rychlost = 0;

                barva = Color.Red;
            }
            if (mys.X > x && mys.X < x + w && mys.Y > y && mys.Y < y + h)
            {
                rychlost = 0;

                barva = Color.Blue;
            }

            // utikani od kurzoru
            if (rychlost != 0)
            {
                if (mys.X < x && mys.X > x - 200 && x < _sirkaOkna - prumer)
                {
                    x += rychlost;

                    if (mys.X < x && mys.X > x - 100)
                    {
                        rychlost = 5;

                        x += rychlost;
                        if (mys.X < x && mys.X > x - 50)
                        {
                            rychlost = 10;

                            x += rychlost;
                        }
                    }
                }
                if (mys.X < x + prumer + 200 && mys.X > x + prumer && x > 0)
                {
                    x -= rychlost;
                    if (mys.X < x + prumer + 100 && mys.X > x + prumer)
                    {
                        rychlost = 5;

                        x -= rychlost;
                        if (mys.X < x + prumer + 50 && mys.X > x + prumer)
                        {
                            rychlost = 10;

                            x -= rychlost;
                        }
                    }
                }
                if (mys.Y < y && mys.Y > y - 200 && y < _vyskaOkna - prumer)
                {
                    y += rychlost;

                    if (mys.Y < y && mys.Y > y - 100)
                    {
                        rychlost = 5;

                        y += rychlost;
                        if (mys.Y < y && mys.Y > y - 50)
                        {
                            rychlost = 10;

                            y += rychlost;
                        }
                    }
                }
                if (mys.Y < y + prumer + 200 && mys.Y > y + prumer && y > 0)
                {
                    y -= rychlost;
                    if (mys.Y < y + prumer + 100 && mys.Y > y + prumer)
                    {
                        rychlost = 5;

                        y -= rychlost;
                        if (mys.Y < y + prumer + 50 && mys.Y > y + prumer)
                        {
                            rychlost = 10;

                            y -= rychlost;
                        }
                    }
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_kruhovaTextura, new Rectangle(x, y, w, h), barva);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
