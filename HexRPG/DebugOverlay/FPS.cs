using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public class FPS : IDebugOverlayItem
    {

        public Vector2 Offset { get; set; }

        public Vector2 Coordinates { get; set; }

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Manual;

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Manual;

        private long totalFrames;

        private long GetTotalFrames()
        {
            return totalFrames;
        }

        private void SetTotalFrames(long value)
        {
            totalFrames = value;
        }

        private float totalSeconds;

        private float GetTotalSeconds()
        {
            return totalSeconds;
        }

        private void SetTotalSeconds(float value)
        {
            totalSeconds = value;
        }

        private float AverageFramesPerSecond { get; set; }
        private float CurrentFramesPerSecond { get; set; }

        private const int MAXIMUM_SAMPLES = 20;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public FPS(Vector2 Coordinates)
        {
            this.Offset = Coordinates;
        }

        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = (float)Math.Round(_sampleBuffer.Average(i => i));
            }
            else
            {
                AverageFramesPerSecond = (float)Math.Round(CurrentFramesPerSecond);
            }

            SetTotalFrames(GetTotalFrames() + 1);
            SetTotalSeconds(GetTotalSeconds() + deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, $"FPS     {AverageFramesPerSecond}", Coordinates, color);
        }
    }
}
