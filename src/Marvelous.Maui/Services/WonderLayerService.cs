using Marvelous.Core.Models;
using Marvelous.Maui.Models;

namespace Marvelous.Maui.Services
{
    public class WonderLayerService
    {
        public void UpdateWonders(IList<Wonder> wonders, Dictionary<int, List<LayerWonder>> wonderLayers)
        {
            wonderLayers.Clear();

            var layerEnds = new Dictionary<int, int>();

            var sortedWonders = wonders
                .OrderBy(w => w.StartYr)
                .ToList();

            foreach (var wonder in sortedWonders)
            {
                int layer = 0;

                while (layerEnds.ContainsKey(layer) && layerEnds[layer] > wonder.StartYr)
                    layer++;

                layerEnds[layer] = wonder.EndYr;
                AddWonderToLayer(layer, wonder, wonderLayers);
            }
        }

        public void UpdateWondersPosition(Dictionary<int, List<LayerWonder>> wonderLayers, int minYear, int maxYear, double timelineSize, double spacing, double minWonderSize)
        {
            var totalYears = Math.Abs(minYear) + Math.Abs(maxYear);
            var yearOffset = timelineSize / totalYears;

            foreach (var layer in wonderLayers)
            {
                for (int i = 0; i < layer.Value.Count; i++)
                {
                    var wonder = layer.Value[i];
                    var start = spacing + ((wonder.StartYear + Math.Abs(minYear)) * yearOffset);
                    var end = spacing + ((wonder.EndYear + Math.Abs(minYear)) * yearOffset);
                    var wonderSize = end - start;

                    if (wonderSize < minWonderSize)
                    {
                        var difference = minWonderSize - wonderSize;
                        start -= difference / 2;
                        end += difference / 2;
                    }

                    wonder.Start = start;
                    wonder.End = end;

                    if (i < 1)
                        continue;

                    var previousWonder = layer.Value[i - 1];
                    if (previousWonder.End > wonder.Start)
                        GetRidOfOverlaps(previousWonder, wonder, minWonderSize);
                }
            }
        }

        private void AddWonderToLayer(int layer, Wonder wonder, Dictionary<int, List<LayerWonder>> wonderLayers)
        {
            if (!wonderLayers.ContainsKey(layer))
                wonderLayers[layer] = new List<LayerWonder>();

            var config = WonderViewConfig.GetWonderViewConfig(wonder.Type);

            wonderLayers[layer].Add(new LayerWonder(wonder.Type, wonder.StartYr, wonder.EndYr, config.Flattened, config.SecondaryColor));
        }

        private void GetRidOfOverlaps(LayerWonder previous, LayerWonder next, double minWonderSize)
        {
            var overlap = previous.End - next.Start;
            var toRemove = overlap / 2;

            var previousRest = TryShrink(previous, toRemove, false);
            var nextRest = TryShrink(next, toRemove, true);

            if (previousRest > 0 && nextRest == 0)
                previousRest = TryShrink(next, previousRest, true);
            else if (nextRest > 0 && previousRest == 0)
                nextRest = TryShrink(previous, nextRest, false);

            var rest = (previousRest + nextRest) / 2;

            if (rest == 0)
                return;

            Shrink(previous, rest, false);
            Shrink(next, rest, true);

            double TryShrink(LayerWonder wonder, double amount, bool takeFromTop)
            {
                var height = wonder.End - wonder.Start;

                if (height - amount >= minWonderSize)
                {
                    Shrink(wonder, amount, takeFromTop);
                    return 0;
                }
                else
                {
                    var canTakeAmount = height - minWonderSize;
                    Shrink(wonder, canTakeAmount, takeFromTop);
                    return amount - canTakeAmount;
                }
            }

            void Shrink(LayerWonder wonder, double amount, bool takeFromTop)
            {
                if (takeFromTop)
                    wonder.Start += amount;
                else
                    wonder.End -= amount;
            }
        }
    }
}
