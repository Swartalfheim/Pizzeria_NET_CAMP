namespace PizzaProject
{
    public class Recipe
    {
        public enum CookingStage
        {
            PrepareBase,
            Cut,
            Filling,
            Bake
        }
        private Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> _cookingStages;
        public Recipe(Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> cookingStages)
        {
            _cookingStages = new Dictionary<CookingStage, List<(IStorageable, TimeOnly)>>(cookingStages);
        }
        public TimeOnly TotalTime
        {
            get
            {
                int seconds = _cookingStages.Values.SelectMany(s => s.Select(t => t.Item2.Second)).Sum();
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
            //TODO
            /*if (_cookingStages.ContainsKey(stage.Key))
            {
                foreach ((IStorageable, TimeOnly) ingredient in stage.Value)
                {
                    _cookingStages[stage.Key].
                }
            }*/
            throw new NotImplementedException();
        }
        public Dictionary<CookingStage, List<(IStorageable, TimeOnly)>> GetWholeRecipe()
        {
            return _cookingStages;
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
