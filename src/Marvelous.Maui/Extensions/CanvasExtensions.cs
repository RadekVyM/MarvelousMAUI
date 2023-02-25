using Marvelous.Core.Models;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Extensions
{
    public static class CanvasExtensions
    {
        public static void DrawGlobalTimeline(
            this ICanvas canvas,
            RectF dirtyRect,
            Dictionary<int, List<LayerWonder>> wonderLayers,
            double minWonderWidth,
            double spacing,
            WonderType selectedWonder,
            float strokeSize,
            Color strokeColor,
            Paint selectedPaint,
            Paint defaultPaint = null)
        {
            var wonderHeight = (float)minWonderWidth;
            var padding = 2f;
            var verticalSpacing = spacing;
            var topPadding = (dirtyRect.Height - (wonderHeight * wonderLayers.Count) - ((wonderLayers.Count - 1) * verticalSpacing)) / 2f;
            var maxLayerKey = wonderLayers.Max(wl => wl.Key);

            canvas.StrokeSize = strokeSize;
            canvas.StrokeColor = strokeColor;

            defaultPaint ??= new SolidPaint(Colors.Transparent);

            foreach (var layer in wonderLayers)
            {
                var top = topPadding + ((maxLayerKey - layer.Key) * (wonderHeight + verticalSpacing));

                foreach (var wonder in layer.Value)
                {
                    var rect = new Rect(wonder.Start + padding + dirtyRect.Left, top + padding + dirtyRect.Top, wonder.End - wonder.Start - (2 * padding), wonderHeight - (2 * padding));

                    if ((rect.Top < 0 && rect.Bottom < 0) || (rect.Top > dirtyRect.Bottom && rect.Bottom > dirtyRect.Bottom))
                        continue;

                    canvas.SetFillPaint(selectedWonder == wonder.WonderType ? selectedPaint : defaultPaint, rect);

                    canvas.FillRoundedRectangle(rect, rect.Height / 2d);
                    canvas.DrawRoundedRectangle(rect, rect.Height / 2d);
                }
            }
        }
    }
}
