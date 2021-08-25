using HexRPG.Debug;
using HexRPG.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public static class DebugOverlay
    {
        public enum DebugOverlayItemType
        {
            FPS,
            Coordinates,
            Zoom,
            LastInputDevice,
            LastInputKey,
            GraphicsDevice,
            AudioDevice,
        }

        public static List<DebugOverlayItemType> OverlayContents = new List<DebugOverlayItemType>();

        private static List<IDebugOverlayItem> overlayItems = new List<IDebugOverlayItem>();

        public static bool IsVisible = true;

        public static void Initialize()
        {
            overlayItems = GenerateDebugOverlayItems();
        }

        private static List<IDebugOverlayItem> GenerateDebugOverlayItems()
        {
            List<IDebugOverlayItem> overlayItems = new List<IDebugOverlayItem>();
            int maxOverlayItemsPerSide = (int)Math.Floor((float)ViewPort.WindowHeight / 24);

            int i = 0;
            foreach (DebugOverlayItemType item in OverlayContents)
            {
                Vector2 offset;
                HorizontalAlignment alignment;

                if (i >= maxOverlayItemsPerSide)
                {
                    offset = new Vector2(0, (i - maxOverlayItemsPerSide) * 24);
                    alignment = HorizontalAlignment.Right;
                }
                else
                {
                    offset = new Vector2(0, i * 24);
                    alignment = HorizontalAlignment.Left;
                }

                i += 1;
                IDebugOverlayItem overlayItem = CreateDebugOverlayItem(item, offset, alignment);
                overlayItem.Coordinates = getCoordinate(overlayItem);
                overlayItems.Add(overlayItem);
            }
            return overlayItems;
        }

        private static IDebugOverlayItem CreateDebugOverlayItem(DebugOverlayItemType type, Vector2 offset, HorizontalAlignment alignment)
        {
            switch (type)
            {
                case DebugOverlayItemType.AudioDevice:
                    return null;
                case DebugOverlayItemType.Coordinates:
                    return new Coordinate(offset) { horizontalAlignment = alignment };
                case DebugOverlayItemType.FPS:
                    return new FPS(offset) { horizontalAlignment = alignment };
                case DebugOverlayItemType.GraphicsDevice:
                    return null;
                case DebugOverlayItemType.LastInputDevice:
                    return new LastUsedInput(offset) { horizontalAlignment = alignment };
                case DebugOverlayItemType.LastInputKey:
                    return null;
                case DebugOverlayItemType.Zoom:
                    return new Zoom(offset) { horizontalAlignment = alignment };
            }
            return null;
        }

        public static void Update(float deltaTime)
        {
            foreach (IDebugOverlayItem item in overlayItems)
            {
                item.Update(deltaTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                foreach (IDebugOverlayItem item in overlayItems)
                {
                    item.Draw(spriteBatch, MainGame.FontSet[GameOptions.ForegroundFont], GameOptions.ForegroundColor);

                }
            }
        }

        public static void UpdateForNewScreenSize()
        {
            updateAlignment();
        }

        private static void updateAlignment()
        {
            int maxOverlayItemsPerSide = (int)Math.Floor((float)ViewPort.WindowHeight / 24);
            int i = 0;
            foreach (IDebugOverlayItem item in overlayItems)
            {
                Vector2 offset;
                HorizontalAlignment alignment;
                if (i >= maxOverlayItemsPerSide)
                {
                    offset = new Vector2(0, (i - maxOverlayItemsPerSide) * 24);
                    alignment = HorizontalAlignment.Right;
                }
                else
                {
                    offset = new Vector2(0, i * 24);
                    alignment = HorizontalAlignment.Left;
                }

                i += 1;
                item.horizontalAlignment = alignment;
                item.Coordinates = getCoordinate(item);
            }
        }

        private static Vector2 getCoordinate(IDebugOverlayItem item)
        {
            float x = item.Offset.X;
            float y = item.Offset.Y;
            Vector2 size = MainGame.FontSet[GameOptions.ForegroundFont].MeasureString(item.GetStringValue());

            Rectangle screen = new Rectangle(0, 0, ViewPort.WindowWidth, ViewPort.WindowHeight);

            switch (item.horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x += HorizontalAlignLeft(screen, size);
                    break;
                case HorizontalAlignment.Center:
                    x += HorizontalAlignCenter(screen, size);
                    break;
                case HorizontalAlignment.Right:
                    x += HorizontalAlignRight(screen, size);
                    break;
            }

            switch (item.verticalAlignment)
            {
                case VerticalAlignment.Top:
                    y += VerticalAlignTop(screen, size);
                    break;
                case VerticalAlignment.Center:
                    y += VerticalAlignCenter(screen, size);
                    break;
                case VerticalAlignment.Bottom:
                    y += VerticalAlignBottom(screen, size);
                    break;
            }

            return new Vector2(x, y);
        }
    }
}
