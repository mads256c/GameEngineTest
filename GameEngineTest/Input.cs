using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;

namespace GameEngineTest
{
    class Input
    {
        private static List<Key> keysDown = new List<Key>();
        private static List<Key> keysDownLast = new List<Key>();
        private static List<MouseButton> buttonsDown = new List<MouseButton>();
        private static List<MouseButton> buttonsDownLast = new List<MouseButton>();

        /// <summary>
        /// Initialize the Input class, by setting up input events.
        /// </summary>
        /// <param name="game">The GameWindow the Input class should listen to events from.</param>
        public static void Initialize(GameWindow game)
        {
            game.KeyDown += Game_KeyDown;
            game.KeyUp += Game_KeyUp;

            game.MouseDown += Game_MouseDown;
            game.MouseUp += Game_MouseUp;
        }

        private static void Game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (buttonsDown.Contains(e.Button))
            {
                buttonsDown.Remove(e.Button);
            }
        }

        private static void Game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }

        private static void Game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }

        private static void Game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }

        /// <summary>
        /// Updates the keys last frame to current.
        /// </summary>
        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);
        }

        /// <summary>
        /// Returns true when the key is first pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public static bool KeyPress(Key key)
        {
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }
        /// <summary>
        /// Returns true when the key is first released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public static bool KeyRelease(Key key)
        {
            return (!keysDown.Contains(key) && keysDownLast.Contains(key));
        }
        /// <summary>
        /// Returns true when the key is down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public static bool KeyDown(Key key)
        {
            return keysDown.Contains(key);
        }

        /// <summary>
        /// Returns true when the button is first pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns></returns>
        public static bool MousePress(MouseButton button)
        {
            return (buttonsDown.Contains(button) && !buttonsDownLast.Contains(button));
        }
        /// <summary>
        /// Returns true when the button is first released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns></returns>
        public static bool MouseRelease(MouseButton button)
        {
            return (!buttonsDown.Contains(button) && buttonsDownLast.Contains(button));
        }
        /// <summary>
        /// Returns true when the button is down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns></returns>
        public static bool MouseDown(MouseButton button)
        {
            return buttonsDown.Contains(button);
        }
    }
}