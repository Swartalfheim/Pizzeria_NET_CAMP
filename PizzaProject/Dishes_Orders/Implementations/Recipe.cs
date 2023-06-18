using PizzaProject.Dishes_Orders.Abstractions;

namespace PizzaProject.Dishes_Orders.Implementations
{
    public class Recipe
    {
        public string Name { get; set; }
        public uint Time { get; }
        public Dictionary<Ingredient, uint> Ingredients { get; set; }

        public enum CookingStage
        {
            PrepareBase,
            Cut,
            Filling,
            Bake
        }
        private Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> _cookingStages;

        public Recipe(string name, Dictionary<Ingredient, uint> ingredients, uint time)
        {
            Name = name;
            Time = time * 1000;
            Ingredients = new Dictionary<Ingredient, uint>(ingredients);
        }

        public Recipe(Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> cookingStages)
        {
            _cookingStages = new Dictionary<CookingStage, List<(IStorageable, TimeOnly)>>(cookingStages);
        }
        public TimeOnly TotalTime
        {
            get
            {
                int seconds = _cookingStages.Values.SelectMany(s => s.Select(t => t.Item2.Hour * 3600 + t.Item2.Minute * 60 + t.Item2.Second)).Sum();
                return TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(seconds));
            }
        }
        public void AddCookingStage(KeyValuePair<CookingStage, List<(IStorageable, TimeOnly)>> stage)
        {
            if (_cookingStages.ContainsKey(stage.Key))
            {
                _cookingStages[stage.Key].AddRange(stage.Value);
            }
            else
            {
                _cookingStages.Add(stage.Key, stage.Value);
            }
        }
        public void RemoveCookingStage(KeyValuePair<CookingStage, List<(IStorageable, TimeOnly)>> stage)
        {
            if (_cookingStages.ContainsKey(stage.Key))
            {
                foreach ((IStorageable, TimeOnly) ingredient in stage.Value)
                {
                    (IStorageable, TimeOnly) item = _cookingStages[stage.Key].FirstOrDefault(i => i.Item1.Name == ingredient.Item1.Name);
                    if (item.Item1 != null)
                    {
                        _cookingStages[stage.Key].Remove(item);
                    }
                }
                if (!_cookingStages[stage.Key].Any())
                {
                    _cookingStages.Remove(stage.Key);
                }
            }
        }
        public Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> GetWholeRecipe()
        {
            return _cookingStages;
        }
        public override string ToString()
        {
            string text = string.Empty;
            foreach (KeyValuePair<CookingStage, List<(IStorageable, TimeOnly)>> group in _cookingStages)
            {
                text += $"{group.Key}: ";
                TimeOnly time;
                for (int i = 0; i < group.Value.Count - 1; i++)
                {
                    time = group.Value[i].Item2;
                    text += $"{group.Value[i].Item1.Name} " +
                        $"({(time.Hour > 0 ? time.Hour + "h" : "")}" +
                        $"{(time.Minute > 0 ? time.Minute + "m" : "")}" +
                        $"{(time.Second > 0 ? time.Second + "s" : "")}), ";
                }
                time = group.Value[^1].Item2;
                text += $"{group.Value[^1].Item1.Name} " +
                    $"({(time.Hour > 0 ? time.Hour + "h" : "")}" +
                    $"{(time.Minute > 0 ? time.Minute + "m" : "")}" +
                    $"{(time.Second > 0 ? time.Second + "s" : "")}). ";
            }
            return text;
        }
    }
}
